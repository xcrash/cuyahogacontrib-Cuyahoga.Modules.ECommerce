using System;
using System.Collections.Specialized;
using System.Xml;
using System.Text;
using Cuyahoga.Modules.ECommerce.Util;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	/// <summary>
	/// Summary description for CcxPaymentRequest.
	/// </summary>
	public class VeriSignPayflowProPaymentProvider : AbstractPaymentProvider {

		public class PayflowProComWrapper : IDisposable {

			private ComFacade _facade = null;
			private ILog _logger;

			public PayflowProComWrapper(ILog logger) : this() {
				_logger = logger;
			}

			public PayflowProComWrapper() {
				_facade = new ComFacade("PFProCOMControl.PFProCOMControl.1");
				_logger = LogManager.GetLogger(GetType());
			}

			public int CreateContext(string hostAddress, short portNumber, int timeout) {
				return (int) _facade.InvokeMethod("CreateContext", new object[] {hostAddress, portNumber, timeout, "", 0, "", ""});
			}

			public NameValueCollection SubmitTransaction(int context, string parameters) {
				
				string response = (string) _facade.InvokeMethod("SubmitTransaction", new object[] {context, parameters, parameters.Length});

				if (response != null) {
					_logger.Info("Received response [" + response + "]");
				} else {
					_logger.Info("Received null response");
				}

				return StringUtils.GetNameValueCollection(response, "&", "=");
			}

			public void DestroyContext(int context) {
				_facade.InvokeMethod("DestroyContext", new object[] {context});
			}

			#region IDisposable Members
			public void Dispose() {
				if (_facade != null) {
					_facade.Dispose();
				}			
			}    
			#endregion
		}

		public enum CcErrCodes : int {
			Authorised = 0,
			InvalidTender = 2,
			InvalidAmount = 4,
			Declined = 12,
			Referred = 13,
			InvalidAccountNumber = 23,
			InvalidExpiryDate = 24,
			InsufficientFunds = 50
		}
		
		private string _comments = "";

		//Different methods of getting money
		public const string BANK_CHARGE_TYPE_IMMEDIATE_SALE = "S";
		public const string BANK_CHARGE_TYPE_RESERVE_PAYMENT = "A";
		public const string BANK_CHARGE_TYPE_COLLECT_PAYMENT = "D";

		private string _ipAddress;
		private string _partner;
		private string _vendor;
		private string _user;
		private string _password;
		private short _portNumber = 443;

		public const string CONFIGURATION_KEY_IP_ADDRESS = "IpAddress";
		public const string CONFIGURATION_KEY_PORTNUMBER = "PortNumber";
		public const string CONFIGURATION_KEY_PARTNER_ID = "PartnerID";
		public const string CONFIGURATION_KEY_VENDOR_ID = "MerchantID";
		public const string CONFIGURATION_KEY_USER = "Username";
		public const string CONFIGURATION_KEY_PASSWORD = "Password";

		public const string PARM_TENDER_VALUE = "C";

		public VeriSignPayflowProPaymentProvider() {
		}

		public override void RequestPreAuthPayment(IElectronicPayment payment) {
			RequestPayment(payment, VeriSignPayflowProPaymentProvider.BANK_CHARGE_TYPE_RESERVE_PAYMENT);
		}

		public override void RequestAuthPayment(IElectronicPayment payment) {
			RequestPayment(payment, VeriSignPayflowProPaymentProvider.BANK_CHARGE_TYPE_IMMEDIATE_SALE);
		}

		public string IpAddress {
			get {
				return _ipAddress;
			}
			set {
				_ipAddress = value;
			}
		}

		public string Partner {
			get {
				return _partner;
			}
			set {
				_partner = value;
			}
		}

		public string Vendor {
			get {
				return _vendor;
			}
			set {
				_vendor = value;
			}
		}

		public string User {
			get {
				return _user;
			}
			set {
				_user = value;
			}
		}

		public string Password {
			get {
				return _password;
			}
			set {
				_password = value;
			}
		}

		public short PortNumber {
			get {
				return _portNumber;
			}
			set {
				_portNumber = value;
			}
		}

		public override void LoadConfiguration(System.Collections.Hashtable configuration) {

			base.LoadConfiguration (configuration);

			try {
				IpAddress = (string) GetConfigurationValue(CONFIGURATION_KEY_IP_ADDRESS);
			} catch {
				Logger.Info("IP Address not defined");
			} 
			
			try {
				Partner = (string) GetConfigurationValue(CONFIGURATION_KEY_PARTNER_ID);
			} catch {
				Logger.Info("Partner ID not defined");
			}

			try {
				Vendor = (string) GetConfigurationValue(CONFIGURATION_KEY_VENDOR_ID);
			} catch {
				Logger.Info("Vendor ID not defined");
			}

			try {
				User = (string) GetConfigurationValue(CONFIGURATION_KEY_USER);
			} catch {
				Logger.Info("Username not defined");
			}

			try {
				Password = (string) GetConfigurationValue(CONFIGURATION_KEY_PASSWORD);
			} catch {
				Logger.Info("Password not defined");
			}
		}

		private void RequestPayment(IElectronicPayment payment, string paymentType) {

			//Record this request
			LogRequest(payment);

			VeriSignPayflowProPaymentProvider.PayflowProComWrapper prov = new VeriSignPayflowProPaymentProvider.PayflowProComWrapper(Logger);
			int context = prov.CreateContext(IpAddress, PortNumber, 30);
			
			string requestString = CreatePaymentString(payment, paymentType);
			
			NameValueCollection response = prov.SubmitTransaction(context, requestString);
			
			prov.DestroyContext(context);
			prov.Dispose();

			int responseCode = Int32.Parse(response["RESULT"]);

			if (responseCode >= 0) {

				payment.TransactionStatus = TranslatePaymentStatusCode(responseCode);
				payment.TransactionReference = response["PNREF"];

				//Might not get a transaction reference, especially if it fails
				if (payment.TransactionReference == null) {
					payment.TransactionReference = "";
				}
			} else {
				string errorMessage = response["RESPMSG"];
				Logger.Error("Error " + responseCode + " " + errorMessage);
			}
		}

		private PaymentStatus TranslatePaymentStatusCode(int ccErrCode) {
			switch (ccErrCode) {
				case (int) CcErrCodes.Authorised:
					return PaymentStatus.Approved;
				case (int) CcErrCodes.Referred:
					return PaymentStatus.Referred;
				case (int) CcErrCodes.Declined:
					return PaymentStatus.Declined;
				case (int) CcErrCodes.InvalidAmount:
					return PaymentStatus.InvalidAmount;
				case (int) CcErrCodes.InvalidTender:
					return PaymentStatus.InvalidTender;
				case (int) CcErrCodes.InvalidAccountNumber:
				case (int) CcErrCodes.InvalidExpiryDate:
					return PaymentStatus.InvalidCardDetails;
				case (int) CcErrCodes.InsufficientFunds:
					return PaymentStatus.InsufficientFunds;
				default:
					return PaymentStatus.Other;
			}
		}

		protected void CheckRequestParameters(IElectronicPayment payment) {
			CreditCardPayment ccPayment = (CreditCardPayment) payment;
			ccPayment.CheckCardDetails();
		}

		private string CreatePaymentString(IElectronicPayment payment, string transactionType) {

			CreditCardPayment ccPayment = (CreditCardPayment) payment;
			StringBuilder postData = new StringBuilder();
			string expDate = FormatDate(ccPayment.CardExpiresEndMonth, ccPayment.CardExpiresEndYear);
    
			postData.Append("TENDER=" + PARM_TENDER_VALUE);
			postData.Append("&TRXTYPE=" + transactionType);
			postData.Append("&PARTNER=" + Partner);
			postData.Append("&USER=" + User);
			postData.Append("&PWD=" + Password);
			postData.Append("&VENDOR=" + Vendor);
			postData.Append("&ACCT=" + ccPayment.CardNumber);
			postData.Append("&AMT=" + payment.PaymentAmount.Amount);
			postData.Append("&EXPDATE=" + expDate);
			postData.Append("&COMMENT1=" + Comments);
            postData.Append("&STREET=" + ccPayment.UserInfo.UserAddress.AddressLine1);
            postData.Append("&ZIPCODE=" + ccPayment.UserInfo.UserAddress.Postcode);

			return postData.ToString();
		}

		private string FormatDate(short month, short year) {

			short shortYear = (short) (year - 2000);
			if (shortYear < 0) {
				shortYear += 100;
			}

			return StringUtils.Right("00" + month, 2) + StringUtils.Right("00" + shortYear, 2);
		}

		public string Comments {
			get {
				return _comments;
			}
			set {
				_comments = value;
			}
		}
	}
}