using System;
using System.Web;
using log4net;

using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {
	/// <summary>
	/// Handles Payment Providers that require the user to go to a payment page
	/// </summary>
	public abstract class AbstractWebFormPaymentProvider : AbstractPaymentProvider, IWebFormPaymentProvider {

		public const string CONFIGURATION_KEY_PAYMENT_PAGE_URL = "PaymentPageUrl";
		public const string CONFIGURATION_KEY_DEFAULT_CURRENCY_CULTURE = "DefaultCurrencyCulture";
		public const string CONFIGURATION_KEY_ALLOWED_IP_ADDRESS = "AllowedIpAddress";
		public const string CONFIGURATION_KEY_CALLBACK_PASSWORD_EXPECTED = "CallbackPasswordExpected";

		private Currency _currency = null;
		private string _allowedIpAddress = "";
		private string _paymentPageUrl = "";
		private string _callbackPasswordExpected = "";

		public AbstractWebFormPaymentProvider() {
		}

		public override void LoadConfiguration(System.Collections.Hashtable configuration) {

			base.LoadConfiguration (configuration);

			try {
				PaymentPageUrl = (string) GetConfigurationValue(CONFIGURATION_KEY_PAYMENT_PAGE_URL);
			} catch {
				Logger.Info("Payment Page URL not defined");
			}

			try {
				DefaultCurrency = new Currency((string) GetConfigurationValue(CONFIGURATION_KEY_DEFAULT_CURRENCY_CULTURE));
			} catch {
				Logger.Info("Default Currency not defined");
			}
			
			try {
				AllowedIpAddress = (string) GetConfigurationValue(CONFIGURATION_KEY_ALLOWED_IP_ADDRESS);
			} catch {
				Logger.Info("Allowed IP Address not defined");
			}

			try {
				CallbackPasswordExpected = (string) GetConfigurationValue(CONFIGURATION_KEY_CALLBACK_PASSWORD_EXPECTED);
			} catch {
				Logger.Info("Callback Password not defined");
			}
		}

		#region Payment Requests
		/// <summary>
		/// Sends the payment request to the gateway by transferring the users web client to
		/// the merchant web site. No changes are made to the supplied payment object
		/// </summary>
		/// <param name="payment">payment to be processed</param>
		public override void RequestAuthPayment(IElectronicPayment payment) {
			RequestAuthPayment(payment, false);
		}

		public override void RequestPreAuthPayment(IElectronicPayment payment) {
			RequestPreAuthPayment(payment, false);
		}

		/// <summary>
		/// Sends the payment request to the gateway by transferring the users web client to
		/// the merchant web site. No changes are made to the supplied payment object
		/// </summary>
		/// <param name="payment">payment to be processed</param>
		/// <param name="isDebug">if <c>true</c>, inserts a submit button to the redirection form allowing
		/// the user to examine the data to be posted</param>
		public void RequestAuthPayment(IElectronicPayment payment, bool isDebug) {
			CheckRequestParameters(payment, PaymentRequestTypes.ImmediatePayment);
			TransferClientToPaymentPage(payment, PaymentRequestTypes.ImmediatePayment, isDebug);
		}

		public void RequestPreAuthPayment(IElectronicPayment payment, bool isDebug) {
			CheckRequestParameters(payment, PaymentRequestTypes.ReservePayment);
			TransferClientToPaymentPage(payment, PaymentRequestTypes.ReservePayment, isDebug);
		}

		/// <summary>
		/// Transfers automatically
		/// </summary>
		public virtual void TransferClientToPaymentPage(IElectronicPayment payment, PaymentRequestTypes paymentType) {
			TransferClientToPaymentPage(payment, paymentType, false);
		}

		/// <summary>
		/// Transfers the client to the payment page
		/// </summary>
		/// <param name="isDebug">If true, doesn submit automatically</param>
		public virtual void TransferClientToPaymentPage(IElectronicPayment payment, PaymentRequestTypes paymentType, bool isDebug) {

			LogRequest(payment);

			HttpResponse response = HttpContext.Current.Response;

			response.Write("<html>\n");

			if (isDebug == false) {
				//Auto submit this form
				response.Write("<body onLoad=\"document.forms[0].submit();\">\n");
			} else {
				response.Write("<body>\n");
			}

			response.Write("<form method=\"POST\" action=\"" + PaymentPageUrl + "\">\n");
			response.Write(RenderFormHiddenValues(payment, paymentType));

			if (isDebug) {
				//Show a button to submit this form when in debug mode
				response.Write("Press the button to submit this payment request<br><br>");
				response.Write("<input type=\"submit\" value=\"send\">");
			}

			response.Write("</form>\n");
			response.Write("</body>\n</html>");

			try {
				response.End();
			} catch {
				//Why doesn't this stop a further exception being thrown?
			}
		}

		protected abstract string RenderFormHiddenValues(IElectronicPayment payment, PaymentRequestTypes paymentType);
		#endregion

		#region Payment Responses
		public virtual IElectronicPayment ProcessAuthPaymentResponse(HttpRequest postBackData) {
			return ProcessHttpResponse(postBackData, PaymentRequestTypes.ImmediatePayment);
		}

		public virtual IElectronicPayment ProcessPreAuthPaymentResponse(HttpRequest postBackData) {
			return ProcessHttpResponse(postBackData, PaymentRequestTypes.ReservePayment);
		}

		public virtual IElectronicPayment ProcessPostAuthPaymentResponse(HttpRequest postBackData) {
			return ProcessHttpResponse(postBackData, PaymentRequestTypes.CollectPayment);
		}
		
		public virtual IElectronicPayment ProcessRefundResponse(HttpRequest postBackData) {
			return ProcessHttpResponse(postBackData, PaymentRequestTypes.RefundPayment);
		}
		
		public virtual IElectronicPayment ProcessSettlePaymentResponse(HttpRequest postBackData) {
			return ProcessHttpResponse(postBackData, PaymentRequestTypes.SettlePayment);
		}

		public virtual IElectronicPayment ProcessVoidPaymentResponse(HttpRequest postBackData) {
			return ProcessHttpResponse(postBackData, PaymentRequestTypes.VoidPayment);
		}

		/// <summary>
		/// Handles the post back from the payment gateway
		/// </summary>
		/// <param name="postBackData"></param>
		/// <param name="paymentType"></param>
		/// <returns></returns>
		public abstract IElectronicPayment ProcessHttpResponse(HttpRequest postBackData, PaymentRequestTypes paymentType);
		#endregion

		#region Properties
		public string PaymentPageUrl {
			get {
				return _paymentPageUrl;
			}
			set {
				_paymentPageUrl = value;
			}
		}
	
		/// <summary>
		/// Currency used when none is specified
		/// </summary>
		public Currency DefaultCurrency {
			get {
				return _currency;
			}
			set {
				_currency = value;
			}
		}

		public string AllowedIpAddress {
			get {
				return _allowedIpAddress;
			}
			set {
				_allowedIpAddress = value;
			}
		}

		public string CallbackPasswordExpected {
			get {
				return _callbackPasswordExpected;
			}
			set {
				_callbackPasswordExpected = value;
			}
		}
		#endregion

		protected virtual bool IsAllowedRemoteAddress(HttpRequest response) {
			if (AllowedIpAddress != null 
				&& AllowedIpAddress.Length > 0 
				&& AllowedIpAddress != response.UserHostAddress) {
				return false;
			} else {
				return true;
			}
		}

        protected virtual bool IsCallbackAuthenticated(HttpRequest postBackData) {
            //Default to allow all requests
            return true;
        }

		protected string GetResponseHttpLogInfo(HttpRequest response) {
			return "IP [" + response.UserHostAddress + "], "
				+ "IsSecure [" + ((response.IsSecureConnection) ? "YES" : "NO") + "]";
		}
	}
}