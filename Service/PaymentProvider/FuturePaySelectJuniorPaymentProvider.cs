using System;
using System.Text;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	/// <summary>
	/// Payments for Regular FuturePay Agreements are at regular intervals.
	/// The agreement behaviour is dependent on the option chosen
	/// </summary>
	public enum FuturePayAgreementOption : int {
		/// <summary>
		/// The individual payment amounts are fixed on creation of the agreement. 
		/// You can set a different initial payment amount for the first payment if required.
		/// </summary>
		FixedAmount = 0,
		/// <summary>
		/// You must set the normal payment amount when the agreement is created. 
		/// You can set a different initial payment amount for the first payment if required. 
		/// If you wish you can then adjust the individual payment amount after the agreement has started, 
		/// using either the Customer Management System (CMS) or a script. 
		/// The amount cannot be adjusted when less then a week from the next payment date. 
		/// The shopper will be sent an email whenever you adjust the amount. 
		/// </summary>
		VariableDefineAmountAtStart = 1,
		/// <summary>
		/// You cannot set the payment amount when the agreement is created. 
		/// Before each payment you must set the payment amount using CMS. 
		/// If you do not set the amount then no payment is taken. 
		/// The amount cannot be set when less than two weeks from the next payment date. 
		/// The shopper will be sent an email whenever you set the amount. 
		/// </summary>
		VariableDefineAmountInCMS = 2
	}

	public enum FuturePayPaymentInterval : int {
		Day = 1,
		Week = 2,
		Month = 3,
		Year = 4
	}

	/// <summary>
	/// Adds FuturePay facilities to the WorldPay provider
	/// </summary>
	public class FuturePaySelectJuniorPaymentProvider : WorldPaySelectJuniorPaymentProvider {

		public const int NUMBER_OF_PAYMENTS_UNLIMITED = 0;

		public FuturePaySelectJuniorPaymentProvider() : base() {
		}

		protected override string RenderFormHiddenValues(IElectronicPayment payment, PaymentRequestTypes paymentType) {

			FuturePayElectronicPayment fPayment = payment as FuturePayElectronicPayment;

			StringBuilder html = new StringBuilder();

			html.Append(base.RenderFormHiddenValues(payment, paymentType));

			switch (paymentType) {
				case PaymentRequestTypes.ImmediatePayment:
				case PaymentRequestTypes.ReservePayment:

					html.Append("<!-- Start FuturePay Fields -->\r\n");
						
					html.Append("<input type=\"hidden\" name=\"futurePayType\" value=\"regular\">\r\n");

					if (fPayment.StartDate > DateTime.Now) {
						html.Append("<input type=\"hidden\" name=\"startDate\" value=\"" + fPayment.StartDate.ToString("yyyy-MM-dd") + "\">\r\n");
					} else {
						html.Append("<input type=\"hidden\" name=\"startDelayUnit\" value=\"" + fPayment.StartDelayUnit + "\">\r\n");
						html.Append("<input type=\"hidden\" name=\"startDelayMult\" value=\"" + fPayment.StartDelayMult + "\">\r\n");
					}

					html.Append("<input type=\"hidden\" name=\"noOfPayments\" value=\"" + fPayment.NumberOfPayments + "\">\r\n");

					if (fPayment.NumberOfPayments != 1) {
						html.Append("<input type=\"hidden\" name=\"intervalUnit\" value=\"" + fPayment.IntervalUnit + "\">\r\n");
						html.Append("<input type=\"hidden\" name=\"intervalMult\" value=\"" + fPayment.IntervalMult + "\">\r\n");
					}

					if (fPayment.InitialAmount != null && fPayment.InitialAmount.Amount > 0) {
						html.Append("<input type=\"hidden\" name=\"initialAmount\" value=\"" + fPayment.InitialAmount.Amount + "\">\r\n");
					}

					if (fPayment.NormalAmount != null && fPayment.NormalAmount.Amount > 0) {
						html.Append("<input type=\"hidden\" name=\"normalAmount\" value=\"" + fPayment.NormalAmount.Amount + "\">\r\n");
					}

					html.Append("<input type=\"hidden\" name=\"option\" value=\"" + fPayment.AgreementOption + "\">\r\n");

					html.Append("<!-- End FuturePay Fields -->\r\n");

					break;
				default:
					throw new InvalidOperationException("Payment request type not supported [" + paymentType + "]");
			}

			return html.ToString();

		}

		protected override void CheckRequestParameters(IElectronicPayment payment, PaymentRequestTypes paymentType) {

			base.CheckRequestParameters (payment, paymentType);

			FuturePayElectronicPayment fPayment = payment as FuturePayElectronicPayment;

			if (fPayment == null) {
				throw new InvalidOperationException("Invalid payment type");
			}

			if (fPayment.AgreementOption != (int) FuturePayAgreementOption.FixedAmount
				&& fPayment.AgreementOption != (int) FuturePayAgreementOption.VariableDefineAmountAtStart
				&& fPayment.AgreementOption != (int) FuturePayAgreementOption.VariableDefineAmountInCMS) {
				throw new InvalidOperationException("Invalid Agreement Option");
			}

			if (fPayment.StartDate != SqlUtils.MinSqlDateTime) {
					
				if (fPayment.StartDate < DateTime.Now) {
					throw new InvalidOperationException("Invalid start date");
				}

				if (fPayment.AgreementOption == (int) FuturePayAgreementOption.VariableDefineAmountInCMS 
					&& fPayment.StartDate < DateTime.Now.AddDays(14)) {
					throw new InvalidOperationException("Invalid start date (at least two weeks)");
				}

			} else {

				if (fPayment.StartDelayMult < 1) {
					throw new InvalidOperationException("Invalid start delay (delay >= 1)");
				}

				if (fPayment.AgreementOption == (int) FuturePayAgreementOption.VariableDefineAmountInCMS 
					&& fPayment.StartDelayDays < 14) {
					throw new InvalidOperationException("Invalid start delay (at least two weeks)");
				}
			}

			if (fPayment.NumberOfPayments < 0) {
				throw new InvalidOperationException("Invalid number of payments");
			}

			if (fPayment.NumberOfPayments != FuturePaySelectJuniorPaymentProvider.NUMBER_OF_PAYMENTS_UNLIMITED 
				&& fPayment.NumberOfPayments > 1 && fPayment.IntervalDays < 14) {
				throw new InvalidOperationException("Invalid interval (at least two weeks)");
			}

			if (fPayment.InitialAmount != null && fPayment.InitialAmount.Amount < 0) {
				throw new InvalidOperationException("Invalid initial amount");
			}

			if (fPayment.AgreementOption == (int) FuturePayAgreementOption.VariableDefineAmountInCMS
				&& fPayment.InitialAmount.Amount > 0) {
				throw new InvalidOperationException("Cannot set Initial amount for this agreement option [" + fPayment.AgreementOption + "]");
			}

			if (fPayment.AgreementOption == (int) FuturePayAgreementOption.VariableDefineAmountInCMS) {
				if (fPayment.NormalAmount != null && fPayment.NormalAmount.Amount != 0) {
					throw new InvalidOperationException("Cannot set normal amount for this agreement option [" + fPayment.AgreementOption + "]");
				}
			} else {
				if (fPayment.NormalAmount == null || fPayment.NormalAmount.Amount == 0) {
					throw new InvalidOperationException("Must set normal amount for this agreement option [" + fPayment.AgreementOption + "]");
				}
			}
		}
	}

	public class FuturePayElectronicPayment : ElectronicPayment {

		private DateTime _startDate = SqlUtils.MinSqlDateTime;
		private int _option = (int) FuturePayAgreementOption.VariableDefineAmountAtStart;
		private int _startDelayUnit = (int) FuturePayPaymentInterval.Day;
		private int _startDelayMult = 1;
		private int _noOfPayments = FuturePaySelectJuniorPaymentProvider.NUMBER_OF_PAYMENTS_UNLIMITED;
		private int _intervalUnit = (int) FuturePayPaymentInterval.Day;
		private int _intervalMult = 1;
		private Money _initialAmount = null;
		private Money _normalAmount = null;

		public FuturePayElectronicPayment() : base() {
		}

		/// <summary>
		/// Date on which the first payment will be made.
		/// 
		/// If set, the start date must be in the future and not today. 
		/// If not set, the agreement is set up and marked as awaiting-start-date. 
		/// The start date can be set later via the Customer Management System (CMS).
		/// VariableDefineAmountInCMS: must be two or more weeks in the future.
		/// </summary>
		public DateTime StartDate {
			get {
				return _startDate;
			}
			set {
				_startDate = value;
			}
		}

		/// <summary>
		/// Unit of the delay between when the agreement is created and when the first payment will be made.
		/// 
		/// Can only be set if start date is not specified. Only the listed values are valid.
		/// VariableDefineAmountInCMS: Start delay must be at least 2 weeks.
		/// </summary>
		public int StartDelayUnit {
			get {
				return _startDelayUnit;
			}
			set {
				_startDelayUnit = value;
			}
		}

		/// <summary>
		/// Delay unit multiplier.
		/// 
		/// The actual delay is obtained by multiplying the startDelayUnit by startDelayMult.
		/// If set must be >= 1.
		/// </summary>
		public int StartDelayMult {
			get {
				return _startDelayMult;
			}
			set {
				_startDelayMult = value;
			}
		}

		/// <summary>
		/// Number of payments which will be made under the agreement
		/// 
		/// Positive integer. Set to 0 or leave unset for unlimited.
		/// </summary>
		public int NumberOfPayments {
			get {
				return _noOfPayments;
			}
			set {
				_noOfPayments = value;
			}
		}

		/// <summary>
		/// The unit of the interval between payments.
		/// 
		/// Must be set except when number of payments is 1, in which case it cannot be set.
		/// Only the listed values are valid. 
		/// VariableDefineAmountAtStart, VariableDefineAmountInCMS: minimum interval is 2 weeks.
		/// </summary>
		public int IntervalUnit {
			get {
				return _intervalUnit;
			}
			set {
				_intervalUnit = value;
			}
		}

		/// <summary>
		/// The interval unit multiplier
		/// 
		/// The actual interval between payments is intervalUnit multiplied by intervalMult.
		/// If set must be >=1
		/// </summary>
		public int IntervalMult {
			get {
				return _intervalMult;
			}
			set {
				_intervalMult = value;
			}
		}

		/// <summary>
		/// The amount of the initial payment
		/// 
		/// If not set, first payment will be for the normal amount. 
		/// FixedAmount: can be set or not, as you want 
		/// VariableDefineAmountAtStart: can be set or not, as you want 
		/// Option 3: cannot be set
		/// </summary>
		public Money InitialAmount {
			get {
				return _initialAmount;
			}
			set {
				_initialAmount = value;
			}
		}

		/// <summary>
		/// Amount of normal payments
		/// 
		/// FixedAmount: must be set, cannot be zero. 
		/// VariableDefineAmountAtStart: must be set, cannot be zero. You can adjust it via CMS at any time. 
		/// VariableDefineAmountInCMS: cannot be automatically set. You must set it via CMS before every payment.
		/// </summary>
		public Money NormalAmount {
			get {
				return _normalAmount;
			}
			set {
				_normalAmount = value;
			}
		}

		/// <summary>
		/// Agreement option
		/// 
		/// Must be one of the listed values.
		/// </summary>
		public int AgreementOption {
			get {
				return _option;
			}
			set {
				if (value == (int) FuturePayAgreementOption.FixedAmount
					|| value == (int) FuturePayAgreementOption.VariableDefineAmountAtStart
					|| value == (int) FuturePayAgreementOption.VariableDefineAmountInCMS) {
					_option = value;
				}
			}
		}

		public int StartDelayDays {
			get {
				return CalculateTimeInDays(StartDelayUnit, StartDelayMult);
			}
		}

		public int IntervalDays {
			get {
				return CalculateTimeInDays(IntervalUnit, IntervalMult);
			}
		}

		private int CalculateTimeInDays(int unit, int mult) {
			switch (unit) {
				case (int) FuturePayPaymentInterval.Day:
					return mult;
				case (int) FuturePayPaymentInterval.Week:
					return mult * 7;
				case (int) FuturePayPaymentInterval.Month:
					return mult * 30;
				case (int) FuturePayPaymentInterval.Year:
					return mult * 365;
				default:
					throw new ArgumentException("Invalid time unit");
			}
		}
	}
}