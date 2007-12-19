using System;
using System.Web;
using System.Text;
using log4net;

using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	/// <summary>
	/// Summary description for EpdqPaymentProvider.
	/// </summary>
	public class EpdqCpiPaymentProvider : AbstractWebFormPaymentProvider {

		public const string BANK_URL_POST = "https://secure2.epdq.co.uk/cgi-bin/CcxBarclaysEpdq.e";
		public const string BANK_URL_ENCRYPT = "https://secure2.epdq.co.uk/cgi-bin/CcxBarclaysEpdqEncTool.e";

		//Different methods of getting money
		public const string BANK_CHARGE_TYPE_IMMEDIATE_SALE = "Auth";
		public const string BANK_CHARGE_TYPE_RESERVE_PAYMENT = "PreAuth";
		public const string BANK_CHARGE_TYPE_COLLECT_PAYMENT = "PostAuth";

		//Supported card types
		public const int SUPPORTED_CARDS_ALL_CARDS = 127;
		public const int SUPPORTED_CARDS_ALL_CARDS_EXCEPT_AMEX = 125;
		public const int SUPPORTED_CARDS_VISA_AND_ELECTRON = 65;

		public const string BANK_ERR_TEXT = "Form Processing Error";

		public const string DEFAULT_CULTURE = "en-GB";

		//These must all be lower case for comparison
		public const string STATUS_SUCCESS = "success";
		public const string STATUS_DECLINED = "declined";
		public const string STATUS_FRAUD = "fraud";

		private string _encryptURL = BANK_URL_ENCRYPT;
		private string _returnURL;
		private string _merchantName;
		private string _clientID;
		private int _supportedCardTypes = SUPPORTED_CARDS_ALL_CARDS_EXCEPT_AMEX;

		//Customisation information
		private string _cpiTextColour;
		private string _cpiBackgroundColour;
		private string _cpiLogoUrl;

		public EpdqCpiPaymentProvider() {
		}

		public string EncryptionUrl {
			get {
				return _encryptURL;
			}
			set {
				_encryptURL = value;
			}
		}
        
		public string ReturnUrl {
			get {
				return _returnURL;
			}
			set {
				_returnURL = value;
			}
		}

		public string MerchantDisplayName {
			get {
				return _merchantName;
			}
			set {
				_merchantName = value;
			}
		}

		public string ClientID {
			get {
				return _clientID;
			}
			set {
				_clientID = value;
			}
		}

		public int SupportedCardTypes {
			get {
				return _supportedCardTypes;
			}
			set {
				_supportedCardTypes = value;
			}
		}

		public string CpiTextColour {
			get {
				return _cpiTextColour;
			}
			set {
				_cpiTextColour = value;
			}
		}

		public string CpiBackgroundColour {
			get {
				return _cpiBackgroundColour;
			}
			set {
				_cpiBackgroundColour = value;
			}
		}

		public string CpiLogoUrl {
			get {
				return _cpiLogoUrl;
			}
			set {
				_cpiLogoUrl = value;
			}
		}

		private string GetEpdqEncryptedData(IElectronicPayment payment, PaymentRequestTypes paymentType) {

			if (paymentType != PaymentRequestTypes.ImmediatePayment && paymentType != PaymentRequestTypes.ReservePayment) {
				throw new ArgumentException("Invalid payment type");
			}

			string encryptionRequestData = "";
			string encryptedResponseData;
            
			//POST some initial data to see if (the service is available...
			//Record this request
			Logger.Info("Getting EPDQ Encrypted Data, OrderID:" + payment.LocalRequestReference + ", Value:" + payment.PaymentAmount.Amount + " " + payment.PaymentAmount.CurrencyCode);
        
			//Build the data to post
			encryptionRequestData = "clientid=" + ClientID
				+ "&password=" + MerchantPassword 
				+ "&chargetype=" + ((paymentType == PaymentRequestTypes.ImmediatePayment) ? BANK_CHARGE_TYPE_IMMEDIATE_SALE : BANK_CHARGE_TYPE_RESERVE_PAYMENT)
				+ "&currencycode=" + payment.PaymentAmount.Currency.CurrencyCodeNumeric
				+ "&total=" + payment.PaymentAmount.Amount.ToString("0.00")
				+ "&oid=" + payment.LocalRequestReference;
        
			//Get encrypted data from EPDQ server
			try {

				encryptedResponseData = HttpUtils.ReadHtmlPage(EncryptionUrl, "POST", encryptionRequestData);
                
				if (encryptedResponseData.ToLower().IndexOf(BANK_ERR_TEXT.ToLower()) > -1) {
            
					//Record this in the log
					throw new Exception("EPDQ connection refused by server");
				}
 
				//Record this was successful
				Logger.Info("EPDQ Encrypted Data OK, OrderID:" + payment.LocalRequestReference);
            
			} catch (Exception e) {
				Logger.Info("EPDQ failure, OrderID:" + payment.LocalRequestReference, e);
				encryptedResponseData = null;
			}

			return encryptedResponseData;

		}

		protected override void CheckRequestParameters(IElectronicPayment payment, PaymentRequestTypes paymentType) {

			if (ClientID == null || ClientID.Length == 0) {
				throw new ArgumentException("Invalid ClientID");
			}

			if (MerchantPassword == null || MerchantPassword.Length == 0) {
				throw new ArgumentException("Invalid MerchantPassword");
			}

			//CurrencyCode is three digits and numeric
			if (payment.PaymentAmount.CurrencyCode == null || payment.PaymentAmount.CurrencyCode.Length != 3) {
				throw new ArgumentException("Invalid CurrencyCode");
			}
		}

		protected override string RenderFormHiddenValues(IElectronicPayment payment, PaymentRequestTypes paymentType) {

			string encryptedData = GetEpdqEncryptedData(payment, paymentType);

			if (encryptedData != null && encryptedData.Length > 0) {

				StringBuilder html = new StringBuilder();

				//Required data
				html.Append("<!-- Begin EPDQ Data -->\n");
				html.Append(encryptedData); //This includes its own input tag
				html.Append("<!-- End EPDQ Data -->\n");
				html.Append("<input type=\"hidden\" name=\"returnurl\" value=\"" + ReturnUrl + "\">\n");
				html.Append("<input type=\"hidden\" name=\"merchantdisplayname\" value=\"" + MerchantDisplayName + "\">\n");
				html.Append("<input type=\"hidden\" name=\"supportedcardtypes\" value=\"" + SupportedCardTypes + "\">\n");
        
				//Customisation information
				if (CpiTextColour != null && CpiTextColour.Length > 0) {
					html.Append("<input type=\"hidden\" name=\"cpi_textcolor\" value=\"" + CpiTextColour + "\">\n");
				}

				if (CpiBackgroundColour != null && CpiBackgroundColour.Length > 0) {
					html.Append("<input type=\"hidden\" name=\"cpi_bgcolor\" value=\"" + CpiBackgroundColour + "\">\n");
				}

				//This logo should be accessed via SSL to prevent browsers complaining
				if (CpiLogoUrl != null && CpiLogoUrl.Length > 0) {
					html.Append("<input type=\"hidden\" name=\"cpi_logo\" value=\"" + CpiLogoUrl + "\">\n");
				}

				if (payment.UserInfo != null) {
					//Optional data to make EPDQ form semi-complete
					html.Append("<input type=\"hidden\" name=\"email\" value=\"" + StringUtils.Left(payment.UserInfo.EmailAddress, 64) + "\">\n");                
					html.Append("<input type=\"hidden\" name=\"baddr1\" value=\"" + StringUtils.Left(payment.UserInfo.Address1, 60) + "\">\n");
					html.Append("<input type=\"hidden\" name=\"baddr2\" value=\"" + StringUtils.Left(payment.UserInfo.Address2, 60) + "\">\n");
					html.Append("<input type=\"hidden\" name=\"bcity\" value=\"" + StringUtils.Left(payment.UserInfo.City, 25) + "\">\n");
					html.Append("<input type=\"hidden\" name=\"bpostalcode\" value=\"" + StringUtils.Left(payment.UserInfo.PostCode, 9) + "\">\n");
					html.Append("<input type=\"hidden\" name=\"bcountry\" value=\"" + StringUtils.Left(payment.UserInfo.CountryName, 3) + "\">\n");
        
					if (payment.UserInfo.State == null || payment.UserInfo.State.Length > 2) {
						//This is for non-US customers
						html.Append("<input type=\"hidden\" name=\"bcountyprovince\" value=\"" + StringUtils.Left(payment.UserInfo.State, 25) + "\">\n");
					} else {
						//This is for US customers
						html.Append("<input type=\"hidden\" name=\"bstate value=\"" + StringUtils.Left(payment.UserInfo.State, 2) + "\">\n");
					}
				}
            
				return html.ToString();

			} else {
				throw new ArgumentOutOfRangeException();
			}
		}

		public override IElectronicPayment ProcessHttpResponse(HttpRequest postBackData, PaymentRequestTypes paymentType) {

			IElectronicPayment payment = new ElectronicPayment();

			if (DefaultCurrency == null) {
				DefaultCurrency = new Currency(DEFAULT_CULTURE);
			}

			try {
				payment.TransactionReference = postBackData["oid"];
				Money amount = new Money(DefaultCurrency, Decimal.Parse(postBackData["total"]));
				payment.PaymentAmount = amount;
				payment.PaymentDate = DateTime.Parse(postBackData["datetime"]);

				//EPDQ don't seem to have a reference. They will create a new one
				//if you don't supply a local reference
				payment.LocalRequestReference = payment.TransactionReference;

				//Make sure this request came from the right address
				if (IsAllowedRemoteAddress(postBackData)) {        
   
					string status = postBackData["transactionstatus"];

					switch (status.ToLower()) {
						case STATUS_SUCCESS:
							payment.TransactionStatus = PaymentStatus.Approved;
							break;
						case STATUS_DECLINED:
							payment.TransactionStatus = PaymentStatus.Declined;
							break;
						case STATUS_FRAUD:
							payment.TransactionStatus = PaymentStatus.Fraud;
							break;
						default:
							payment.TransactionStatus = PaymentStatus.Other;
							break;
					}
				} else {
					payment.TransactionStatus = PaymentStatus.Fraud;
				}

				LogResponse(payment, "ePDQ Status [" + postBackData["transactionstatus"] + "], " + GetResponseHttpLogInfo(postBackData));

			} catch (Exception e) {
				Logger.Error(e);
			}

			return payment;
		}
	}
}
