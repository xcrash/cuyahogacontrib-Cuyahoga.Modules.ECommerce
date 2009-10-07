using System;
using System.Collections.Specialized;
using System.Text;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	/// <summary>
	/// Summary description for NetbanxPaymentProvider.
	/// </summary>
	public class NetBanxPaymentProvider : AbstractPaymentProvider {

		public const string STATUS_SUCCESS = "Y";
		public const string STATUS_DECLINED = "N";

		public const string CONFIGURATION_KEY_PAYMENT_URL = "PaymentUrl";
		public const string CONFIGURATION_KEY_CLIENT_IP_ADDRESS = "ClientIpAddress";

		private const string RESPONSE_SEPERATOR_PARAMS = ","; 
		private const string RESPONSE_SEPERATOR_NAME_VALUE = "|"; 

		private const string RESPONSE_KEY_STATUS = "Status";
		private const string RESPONSE_KEY_STATUS_MESSAGE = "StatusMessage";
		private const string RESPONSE_KEY_TRANSACTION_ID = "StatusMessage";

		private string _paymentUrl;
		private string _clientIp;

		public NetBanxPaymentProvider() {
		}

		public string PaymentUrl {
			get {
				return _paymentUrl;
			}
			set {
				_paymentUrl = value;
			}
		}

		public string ClientIpAddress {
			get {
				return _clientIp;
			}
			set {
				_clientIp = value;
			}
		}

		public override void LoadConfiguration(System.Collections.Hashtable configuration) {

			base.LoadConfiguration (configuration);

			try {
				PaymentUrl = (string) GetConfigurationValue(CONFIGURATION_KEY_PAYMENT_URL);
			} catch {
				Logger.Info("Payment URL not defined");
			}

			try {
				ClientIpAddress = (string) GetConfigurationValue(CONFIGURATION_KEY_CLIENT_IP_ADDRESS);
			} catch {
				Logger.Info("Client IP not defined");
			}
		}

		public override void RequestAuthPayment(IElectronicPayment payment) {

			CreditCardPayment ccPayment = payment as CreditCardPayment;

			if (ccPayment == null) {
				
				if (payment != null) {
					Logger.Error("Payment is NOT a credit card payment");
					payment.TransactionStatus = PaymentStatus.NotSubmitted;
				} else {
					Logger.Error("Null payment supplied");
				}

				//Cannot continue
				return;
			}

			//Record this request
			LogRequest(payment);

			//This is standard
			string data = GetAuthPostData(ccPayment);
			string response = HttpUtils.ReadHtmlPage(PaymentUrl, "POST", data);

			if (response != null && response.Length > 0) {

				Logger.Info("Received response: " + response);
				NameValueCollection responseValues = GetResponseValues(response);

				if (responseValues[RESPONSE_KEY_STATUS] == STATUS_SUCCESS) {
					payment.TransactionStatus = PaymentStatus.Approved;
					payment.TransactionReference = responseValues[RESPONSE_KEY_TRANSACTION_ID];
				} else {
					payment.TransactionStatus = PaymentStatus.Declined;
				}

				//It isn't provider explicitly
				payment.PaymentDate = DateTime.Now;
			}

			//Standard response logging
			LogResponse(payment);
		}

		private NameValueCollection GetResponseValues(string response) {

			NameValueCollection values = new NameValueCollection();

			string[] parameters = response.Split(RESPONSE_SEPERATOR_PARAMS.ToCharArray());

			for (int i = 0; i < parameters.Length; i++) {
				
				string nameValue = parameters[i];

				if (nameValue.IndexOf(RESPONSE_SEPERATOR_NAME_VALUE) > 0) {
					string[] nameValuePair = nameValue.Split(RESPONSE_SEPERATOR_NAME_VALUE.ToCharArray());

					if (i > 0) {
						values.Add(nameValuePair[0].Trim(), nameValuePair[1].Trim());
					} else {
						/*
						 * First item gives status info
						 * If it suceeds, the value is the transaction reference
						 * If it fails, the value is the error message
						 */
						values.Add(RESPONSE_KEY_STATUS, nameValuePair[0].Trim());
						values.Add(RESPONSE_KEY_STATUS_MESSAGE, nameValuePair[1].Trim());
					}
				}
			}

			return values;
		}

		protected string GetAuthPostData(CreditCardPayment ccPayment) {

			StringBuilder postData = new StringBuilder();
    
			postData.Append("card_number=" + ccPayment.CardNumber);
			postData.Append("&issue_number=" + ccPayment.CardIssueNumber);
			postData.Append("&expiry1=" + GetMonthString(ccPayment.CardExpiresEndMonth));
			postData.Append("&expiry2=" + GetYearString(ccPayment.CardExpiresEndYear));
			postData.Append("&start1=" + GetMonthString(ccPayment.CardValidFromMonth));
			postData.Append("&start2=" + GetYearString(ccPayment.CardValidFromYear));
			postData.Append("&payment_amount=" + (ccPayment.PaymentAmount.Amount * 100));
			postData.Append("&postcode=" + ccPayment.UserInfo.UserAddress.Postcode);
			postData.Append("&clientip=" + ClientIpAddress);
			postData.Append("&merchant=" + MerchantID);
			postData.Append("&webordernumber=" + ccPayment.LocalRequestReference);

			return postData.ToString();
		}

		private string GetYearString(short year) {

			string yearString = year.ToString();
			
			if (yearString.Length == 4) {
				return StringUtils.Right(yearString, 2);
			}

			return yearString;
		}

		private string GetMonthString(short month) {
			return StringUtils.Right("00" + month, 2);
		}
	}
}