using System;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	/// <summary>
	/// Adds a bypass for testing purposes: in test mode, payment requests are faked
	/// </summary>
	public class PaymentProviderTestDecorator : AbstractPaymentProvider {

		private IPaymentProvider _provider;

		public PaymentProviderTestDecorator(IPaymentProvider provider) {

			_provider = provider;

			//Copy known properties
			this.MerchantID = _provider.MerchantID;
			this.MerchantPassword = _provider.MerchantPassword;
			this.Mode = _provider.Mode;
		}

		public override void RequestPreAuthPayment(IElectronicPayment payment) {
			if (_provider.Mode == TransactionMode.Production) {
				_provider.RequestPreAuthPayment(payment);
			} else {
				ProcessTestPayment(payment);
			}
		}

		public override void RequestAuthPayment(IElectronicPayment payment) {
			if (_provider.Mode == TransactionMode.Production) {
				_provider.RequestAuthPayment(payment);
			} else {
				ProcessTestPayment(payment);
			}
		}

		public override void RequestPostAuthPayment(IElectronicPayment payment) {
			if (_provider.Mode == TransactionMode.Production) {
				_provider.RequestPostAuthPayment(payment);
			} else {
				ProcessTestPayment(payment);
			}
		}

		public override void RequestRefund(IElectronicPayment payment) {
			_provider.RequestRefund(payment);
		}

		public override void RequestRefund(IElectronicPayment payment, Cuyahoga.Modules.ECommerce.Util.Money refundAmount) {
			_provider.RequestRefund(payment, refundAmount);
		}

		public override void RequestSettlePayment(IElectronicPayment payment) {
			_provider.RequestSettlePayment(payment);
		}

		public override void RequestVoidPayment(IElectronicPayment payment) {
			_provider.RequestVoidPayment(payment);
		}

		public override log4net.ILog Logger {
			get {
				return _provider.Logger;
			}
		}

		/// <summary>
		/// Adds statuses and other stuff to look like a payment has been processed correctly
		/// </summary>
		/// <param name="payment">Electronic payment to be spoofed</param>
		protected void ProcessTestPayment(IElectronicPayment payment) {

			switch (_provider.Mode) {

				default:
					return; //Nothing to do here

				case TransactionMode.AlwaysPass:
				case TransactionMode.Test:
					payment.TransactionStatus = PaymentStatus.Approved;
					break;

				case TransactionMode.AlwaysFail:
					payment.TransactionStatus = PaymentStatus.Declined;
					break;

				case TransactionMode.Random:
					Random r = new Random(DateTime.Now.Millisecond);
					payment.TransactionStatus = (r.Next(1) > 0.5) ? PaymentStatus.Approved : PaymentStatus.Declined;
					break;
			}

			//Add a fake transaction status if it was a success and this isn't live
			if (payment.TransactionStatus == PaymentStatus.Approved || payment.TransactionStatus == PaymentStatus.Referred) {
				payment.TransactionReference = "TEST-" + StringUtils.GenerateRandomText(16, "ABCDEFGHJKLMNPRSTVWXY0123456789");
			}

			//Standard response logging
			LogResponse(payment);
		}
	}
}