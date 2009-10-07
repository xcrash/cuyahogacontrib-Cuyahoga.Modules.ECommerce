using System;
using System.Web;
using System.Text;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	public enum WorldPayTestMode : int {
		AlwaysPass = 100,
		AlwaysFail = 101,
		Production = 0
	}

	/// <summary>
	/// Summary description for WorldPaySelectJuniorPaymentProvider.
	/// </summary>
	public class WorldPaySelectJuniorPaymentProvider : AbstractWebFormPaymentProvider {

		public const string BANK_URL_POST = "https://select.worldpay.com/wcc/purchase";
		public const string STATUS_SUCCESS = "Y";
		public const string STATUS_DECLINED = "C";

		public const string AUTH_MODE_AUTH = "A";
		public const string AUTH_MODE_PREAUTH = "E";

		public const string DEFAULT_CULTURE = "en-GB";

		private string _installationID = "";

		public WorldPaySelectJuniorPaymentProvider() {
			PaymentPageUrl = BANK_URL_POST;
			Mode = TransactionMode.Production;
		}

		public string InstallationID {
			get {
				return _installationID;
			}
			set {
				_installationID = value;
			}
		}

		private WorldPayTestMode WorldPayTestMode {
			get {
				switch (Mode) {
					case TransactionMode.AlwaysFail:
						return WorldPayTestMode.AlwaysFail;
					case TransactionMode.AlwaysPass:
						return WorldPayTestMode.AlwaysPass;
					case TransactionMode.Production:
						return WorldPayTestMode.Production;
					default:
						throw new InvalidOperationException("Test mode not supported [" + Mode + "]");
				}

			}
		}

		public override IElectronicPayment ProcessHttpResponse(HttpRequest postBackData, PaymentRequestTypes paymentType) {
			
			IElectronicPayment payment = new ElectronicPayment();

			switch (paymentType) {
				case PaymentRequestTypes.ImmediatePayment:
				case PaymentRequestTypes.ReservePayment:

					try {

						Currency currency = GetPaymentCurrency(postBackData);
						payment.LocalRequestReference = postBackData["cartId"];
						Money amount = new Money(currency, Decimal.Parse(postBackData["authAmount"]));
						payment.PaymentAmount = amount;

						SetPaymentDate(postBackData, payment);

						//Make sure this request came from the right address
						if (IsAllowedRemoteAddress(postBackData) && IsCallbackAuthenticated(postBackData)) {        
   
							string status = postBackData["transStatus"];

							switch (status.ToUpper()) {
								case STATUS_SUCCESS:
									payment.TransactionStatus = PaymentStatus.Approved;
									payment.TransactionReference = postBackData["transId"];
									break;
								case STATUS_DECLINED:
									payment.TransactionStatus = PaymentStatus.Declined;
									break;
								default:
									payment.TransactionStatus = PaymentStatus.Other;
									break;
							}
						} else {
							payment.TransactionStatus = PaymentStatus.Fraud;
						}

						LogResponse(payment, "WorldPay Status [" + postBackData["transStatus"]
							+ "], Raw Auth Code [" + postBackData["rawAuthCode"] 
							+ "], Raw Auth Message [" + postBackData["rawAuthMessage"] 
							+ "], " + GetResponseHttpLogInfo(postBackData));

					} catch (Exception e) {
						Logger.Error(e);
					}

					try {
						//payment.UserInfo.UserName = postBackData["name"];
						payment.UserInfo.UserAddress.Postcode = postBackData["postcode"];
						payment.UserInfo.UserDetails.EmailAddress = postBackData["email"];
						payment.UserInfo.UserAddress.AddressLine1 = postBackData["address"];
						payment.UserInfo.UserAddress.CountryCode = postBackData["countryString"];
						payment.UserInfo.UserDetails.TelephoneNumber = postBackData["tel"];
						payment.UserInfo.UserDetails.FaxNumber = postBackData["fax"];
					} catch (Exception f) {
						Logger.Error(f);
					}

					return payment;

				default:
					throw new InvalidOperationException("Payment request type not supported [" + paymentType + "]");			
			}
		}

        protected override bool IsCallbackAuthenticated(HttpRequest postBackData) {
            return (CallbackPasswordExpected == null || CallbackPasswordExpected.Length == 0 
                || CallbackPasswordExpected == postBackData["callbackPW"]);
        }

		private void SetPaymentDate(System.Web.HttpRequest postBackData, IElectronicPayment payment) {

			//transTime is milliseconds since 1970
			try {
				double transTime = Double.Parse(postBackData["transTime"]);
				DateTime transDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
				payment.PaymentDate = transDate.AddMilliseconds(transTime);
			} catch {
				payment.PaymentDate = DateTime.Now;
			}
		}

		private Currency GetPaymentCurrency(System.Web.HttpRequest postBackData) {

			Currency currency;
			
			try {
				currency = new Currency(postBackData["country"]);
			} catch {
				currency = new Currency(DEFAULT_CULTURE);
			}

			if (currency.CurrencyCode != postBackData["authCurrency"]) {
				throw new InvalidOperationException("Invalid currency code");
			}

			return currency;
		}

		protected override void CheckRequestParameters(IElectronicPayment payment, PaymentRequestTypes paymentType) {

			if (InstallationID == null || InstallationID.Length == 0) {
				throw new InvalidOperationException("Invalid Installation ID");
			}

			if (payment.LocalRequestReference == null || payment.LocalRequestReference.Length == 0) {
				throw new InvalidOperationException("Invalid Local Reference");
			}

			if (payment.PaymentAmount == null || payment.PaymentAmount.Amount < 0) {
				throw new InvalidOperationException("Invalid Payment Amount");
			}
		}


		protected override string RenderFormHiddenValues(IElectronicPayment payment, PaymentRequestTypes paymentType) {

			StringBuilder html = new StringBuilder();

			switch (paymentType) {
				case PaymentRequestTypes.ImmediatePayment:
				case PaymentRequestTypes.ReservePayment:

					string authMode = AUTH_MODE_AUTH;
					if (paymentType == PaymentRequestTypes.ReservePayment) {
						authMode = AUTH_MODE_PREAUTH;
					}

					//obligatory fields
					html.Append("<input type=\"hidden\" name=\"instId\" value=\"" + InstallationID + "\">\n");
					html.Append("<input type=\"hidden\" name=\"cartId\" value=\"" + payment.LocalRequestReference + "\">\n");
					html.Append("<input type=\"hidden\" name=\"amount\" value=\"" + payment.PaymentAmount.Amount + "\">\n");
					html.Append("<input type=\"hidden\" name=\"currency\" value=\"" + payment.PaymentAmount.CurrencyCode + "\">\n");
					html.Append("<input type=\"hidden\" name=\"desc\" value=\"" + payment.Description + "\">\n");

					//Not strictly required
					html.Append("<input type=\"hidden\" name=\"authMode\" value=\"" + authMode + "\">\n");

					//Optional stuff
					if (Mode != TransactionMode.Production) {
						html.Append("<input type=\"hidden\" name=\"testMode\" value=\"" + ((int) WorldPayTestMode) + "\">\n");
					}

					//User stuff
					if (payment.UserInfo != null) {
                        html.Append("<input type=\"hidden\" name=\"name\" value=\"" + payment.UserInfo.UserDetails.FirstName + " " + payment.UserInfo.UserDetails.LastName + "\">\n");
						html.Append("<input type=\"hidden\" name=\"address\" value=\"" + payment.UserInfo.UserAddress.AddressLine1 + "\">\n");
						html.Append("<input type=\"hidden\" name=\"postcode\" value=\"" + payment.UserInfo.UserAddress.Postcode + "\">\n");
						html.Append("<input type=\"hidden\" name=\"country\" value=\"" + payment.UserInfo.UserAddress.CountryCode + "\">\n");
						html.Append("<input type=\"hidden\" name=\"tel\" value=\"" + payment.UserInfo.UserDetails.TelephoneNumber + "\">\n");
						html.Append("<input type=\"hidden\" name=\"email\" value=\"" + payment.UserInfo.UserDetails.EmailAddress + "\">\n");
					}

					//Custom values
					if (payment.CustomValues.Count > 0) {
						foreach (object key in payment.CustomValues.Keys) {
							html.Append("<input type=\"hidden\" name=\"M_" + key.ToString().Replace(" ", "-") + "\" value=\"" + payment.CustomValues[key].ToString() + "\">");
						}
					}

					break;
				default:
					throw new InvalidOperationException("Payment request type not supported [" + paymentType + "]");
			}

			return html.ToString();
		}
	}
}