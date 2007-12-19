using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

using log4net;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider.Streamline {

	/// <summary>
	/// Gets payment using Streamlines Redirect service
	/// </summary>
	public class StreamlineRedirectPaymentProvider : AbstractWebFormPaymentProvider {

		public const string CARD_TYPE_DELIMITER = ";";

		private const string REQUEST_FIELD_ORDER_KEY = "orderKey";
		private const string REQUEST_FIELD_PAYMENT_STATUS = "paymentStatus";
		private const string REQUEST_FIELD_PAYMENT_AMOUNT = "paymentAmount";
		private const string REQUEST_FIELD_PAYMENT_CURRENCY = "paymentCurrency";
		private const string REQUEST_FIELD_MAC = "mac";
		private const string ORDER_KEY_DELIMITER = "^";
		
		private const int ORDER_KEY_LENGTH = 3;

		private string _versionNumber = "1.4";
		private string _cardTypeInclude = null;
		private string _cardTypeExclude = null;

		private string _countryCode = null;
		private string _languageCode = null;
		private string _bodyAttributes = null;
		private string _fontAttributes = null;
		private string _successUrl = null;
		private string _failureUrl = null;
		private string _preferredPaymentMethod = null;
		private IPaymentChangedHandler _paymentHandler = null;

		public StreamlineRedirectPaymentProvider() {
		}

		public StreamlineRedirectPaymentProvider(IPaymentChangedHandler handler) {
			_paymentHandler = handler;
		}

		public string VersionNumber {
			get {
				return _versionNumber;
			}
			set {
				_versionNumber = value;
			}
		}

		public string CardTypeInclude {
			get {
				return _cardTypeInclude;
			}
			set {
				_cardTypeInclude = value;
			}
		}

		public string CardTypeExclude {
			get {
				return _cardTypeExclude;
			}
			set {
				_cardTypeExclude = value;
			}
		}

		public string CountryCode {
			get {
				return _countryCode;
			}
			set {
				_countryCode = value;
			}
		}

		public string LanguageCode {
			get {
				return _languageCode;
			}
			set {
				_languageCode = value;
			}
		}

		/// <summary>
		/// HTML attributes for old style body formatting
		/// </summary>
		public string BodyAttributes {
			get {
				return _bodyAttributes;
			}
			set {
				_bodyAttributes = value;
			}
		}

		/// <summary>
		/// HTML attributes for defining page fonts
		/// </summary>
		public string FontAttributes {
			get {
				return _fontAttributes;
			}
			set {
				_fontAttributes = value;
			}
		}

		public string SuccessUrl {
			get {
				return _successUrl;
			}
			set {
				_successUrl = value;
			}
		}

		public string FailureUrl {
			get {
				return _failureUrl;
			}
			set {
				_failureUrl = value;
			}
		}

		public string PreferredPaymentMethod {
			get {
				return _preferredPaymentMethod;
			}
			set {
				_preferredPaymentMethod = value;
			}
		}

		public override IElectronicPayment ProcessHttpResponse(HttpRequest postBackData, PaymentRequestTypes paymentType) {

			/*
				https://www.mymerchant.com/Success.jsp
				?orderKey=MYADMINCODE^MYMERCHANT^T0211010
				&paymentStatus=AUTHORISED
				&paymentAmount=1400
				&paymentCurrency=GBP
				&mac=25eefe952a6bbd09fe1c2c09bca4fa09
			 */

			if (!IsCallbackAuthenticated(postBackData)) {
				throw new InvalidOperationException("Invalid details supplied - MAC check failed");
			}

			IElectronicPayment payment = new ElectronicPayment();

			payment.PaymentDate = DateTime.Now;
			payment.TransactionStatus = (postBackData[REQUEST_FIELD_PAYMENT_STATUS] == "AUTHORISED") ? PaymentStatus.Approved : PaymentStatus.Declined;

			string orderKey = postBackData[REQUEST_FIELD_ORDER_KEY];
			string[] orderInfo = new string[] {};

			if (orderKey != null && orderKey.Length > 0) {
				orderInfo = orderKey.Split(ORDER_KEY_DELIMITER.ToCharArray());
			}

			if (orderInfo.Length == ORDER_KEY_LENGTH) {

				string mac = postBackData[REQUEST_FIELD_MAC];
				string orderRef = orderInfo[2];
			
				//Maybe check other parameters as well
				/*
				 * Not documented, but it appears that when you use a mac secret, the order reference becomes the
				 * original local reference number, NOT the transaction reference
				 */
				if (mac != null && mac.Length > 0) {
					payment.LocalRequestReference = orderRef;
				} else {
					payment.TransactionReference = orderRef;
				}

				payment.PaymentAmount = GetPaymentAmount(postBackData);
			
			} else {
				//Something wrong with the values
				payment.TransactionStatus = PaymentStatus.Other;
			}

			//Record the details
			LogResponse(payment);

			return payment;
		}

		private Money GetPaymentAmount(HttpRequest postBackData) {

			Currency currency = new Currency();
			currency.CurrencyCode = postBackData[REQUEST_FIELD_PAYMENT_CURRENCY];

			//The fraction digits *could* be wrong
            Money money = new Money(currency, 0);
            money.LongValue = Int64.Parse(postBackData[REQUEST_FIELD_PAYMENT_AMOUNT]);
			return money;
		}

		public string GetMacHash(string orderKey, string paymentAmount, string paymentCurrency, string paymentStatus, string macSecret) {
			string preHash = orderKey + paymentAmount + paymentCurrency + paymentStatus + macSecret;
			return ComputeHexHash(preHash);
		}

		protected override string RenderFormHiddenValues(IElectronicPayment payment, PaymentRequestTypes paymentType) {
			return null;
		}

		protected override bool IsCallbackAuthenticated(HttpRequest postBackData) {

			string suppliedMacHash = postBackData[REQUEST_FIELD_MAC];

			if (suppliedMacHash != null && suppliedMacHash.Length > 0) {

				string macHash = GetMacHash(
					postBackData[REQUEST_FIELD_ORDER_KEY],
					postBackData[REQUEST_FIELD_PAYMENT_AMOUNT],
					postBackData[REQUEST_FIELD_PAYMENT_CURRENCY],
					postBackData[REQUEST_FIELD_PAYMENT_STATUS],
					CallbackPasswordExpected);

				return (macHash == suppliedMacHash);
			} else {
				return true;
			}
		}

		/// <summary>
		/// Transfers the client to the payment page
		/// </summary>
		/// <param name="isDebug">If true, doesn submit automatically</param>
		public override void TransferClientToPaymentPage(IElectronicPayment payment, PaymentRequestTypes paymentType, bool isDebug) {

			LogRequest(payment);

			//This may throw an exception if the request is invalid
			string pageUrl = GetPaymentPageUrl(payment, paymentType);

			//payment now contains the transaction reference
			if (_paymentHandler != null) {
				//Tell the handler to store the transaction reference so we can find it again later
				_paymentHandler.HandleChange(this, payment);
			}

			HttpResponse response = HttpContext.Current.Response;

			response.Write("<html>\n");

			if (isDebug == false) {
				//Auto submit this form
				response.Write("<body onLoad=\"document.forms[0].submit();\">\n");
			} else {
				response.Write("<body>\n");
			}

			response.Write("<form method=\"POST\" action=\"" + pageUrl + "\">\n");

			if (isDebug) {
				//Show a button to submit this form when in debug mode
				response.Write("Press the button to submit this payment request<br/><br/>");
				response.Write("<input type=\"submit\" value=\"send\">");
			}

			response.Write("</form>\n");
			response.Write("</body>\n</html>");

			response.End();
		}

		public virtual string GetPaymentPageUrl(IElectronicPayment payment, PaymentRequestTypes paymentType) {

			/*
			<?xml version="1.0"?>
			<!DOCTYPE paymentService PUBLIC "-//streamline-esolutions//DTD streamline-esolutions PaymentService v1//EN"
			"http://dtd.streamline-esolutions.com/paymentService_v1.dtd">
			<paymentService merchantCode="MYMERCHANT" version="1.4">
				<reply>
					<orderStatus orderCode="T0211010">
						<reference id="1234567">
						https://secure.streamline-esolutions.com/jsp/shopper/SelectPaymentMethod.jsp?orderKey=MYMERCHANT^T0211010
						</reference>
					</orderStatus>
				</reply>
			</paymentService>
			*/

			Logger.Info("Getting Streamline URL, OrderID [" + payment.LocalRequestReference + "], Value [" + payment.PaymentAmount.Amount + " " + payment.PaymentAmount.CurrencyCode + "]");

			string outXml = CreateXML(payment);
			string inXml = PostXml(PaymentPageUrl, outXml, 0);

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(inXml);

			XmlNode pageUrl = doc.SelectSingleNode("//paymentService/reply/orderStatus/reference");
			XmlNode orderCode = doc.SelectSingleNode("//paymentService/reply/orderStatus/@orderCode");
			XmlNode referenceID = doc.SelectSingleNode("//paymentService/reply/orderStatus/reference/@id");

			if (IsValidOrder(referenceID, orderCode, pageUrl)) {

				Logger.Info("Streamline created reference [" + referenceID.Value + "] for OrderID [" + payment.LocalRequestReference + "]");

				//Put the transaction reference into the payment so we can refer to it later
				payment.TransactionReference = referenceID.Value;

				string url = pageUrl.InnerText;

				//Optional stuff
				if (CountryCode != null && CountryCode.Length == 2) {
					url += GetNameValue("country", CountryCode);
				} else {
					if (payment.UserInfo.CountryName != null && payment.UserInfo.CountryName.Length == 2) {
						url += GetNameValue("country", payment.UserInfo.CountryName);
					}
				}

				url += GetNameValue("language", LanguageCode);
				url += GetNameValue("bodyAttr", BodyAttributes);
				url += GetNameValue("fontAttr", FontAttributes);
				url += GetNameValue("successURL", SuccessUrl);
				url += GetNameValue("failureURL", FailureUrl);
				url += GetNameValue("preferredPaymentMethod", PreferredPaymentMethod);

				return url;

			} else {
				Logger.Info("Streamline failure, OrderID [" + payment.LocalRequestReference + "]");
				if (outXml != null && outXml.Length > 0) Logger.Info("Sent: [" + outXml + "]");
				if (inXml != null && inXml.Length > 0) Logger.Info("Received: [" + inXml + "]");
			}

			throw new InvalidOperationException("Payment rejected by provider");
		}

		private string GetNameValue(string fieldName, string fieldValue) {
			return (fieldValue != null && fieldValue.Length > 0) ? ("&" + fieldName + "=" + System.Web.HttpUtility.UrlEncode(fieldValue)) : "";
		}

		private bool IsValidOrder(XmlNode referenceID, XmlNode orderCode, XmlNode pageUrl) {
			return referenceID != null && referenceID.Value != null && referenceID.Value.Length > 0 
				&& orderCode != null && orderCode.Value != null && orderCode.Value.Length > 0 
				&& pageUrl != null && pageUrl.InnerText != null && pageUrl.InnerText.Length > 0;
		}

		private string PostXml(string url, string contents, int timeoutMilliseconds) {
         
			string result = "";

			HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
			request.Method = "POST";

			if (timeoutMilliseconds > 0) {
				request.Timeout = timeoutMilliseconds;
			}

			StreamWriter writer = null;

			try {
				request.ContentType = "text/xml";
				request.KeepAlive = false;

				byte[] credentialBuffer = new UTF8Encoding().GetBytes(MerchantID + ":" + MerchantPassword);
				request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialBuffer);

				writer = new StreamWriter(request.GetRequestStream());
				writer.Write(contents);

			} catch (Exception e) {
				LogManager.GetLogger(GetType()).Error(e);
				return e.Message;
			} finally {
				writer.Close();
				writer = null;
			}

			try {
				HttpWebResponse response = (HttpWebResponse) request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				result = reader.ReadToEnd();
				reader.Close();
				reader = null;

				//Should always get something of interest...
				if (result == null || result.Length == 0) {
					LogManager.GetLogger(GetType()).Error("Returned empty string reading URL [" + url + "]");
				}

			} catch (Exception e) {
				LogManager.GetLogger(GetType()).Error(
					"Error requesting URL [" + url + "], "
					+ "contents [" + contents + "]"
					);
				LogManager.GetLogger(GetType()).Error(e);
			}

			return result;
		}

		public string CreateXML(IElectronicPayment payment) {

			string header = "<?xml version=\"1.0\"?><!DOCTYPE paymentService PUBLIC \"-//streamline-esolutions//DTD streamline-esolutions PaymentService v1//EN\" \"http://dtd.streamline-esolutions.com/paymentService_v1.dtd\">";

			XmlDocument xml = new XmlDocument();

			//Must be a better way to do this
			xml.LoadXml(header + "<paymentService/>");

			XmlElement root = xml.SelectSingleNode("/paymentService") as XmlElement;
			XMLUtils.AppendAttribute(root, "version", VersionNumber);
			XMLUtils.AppendAttribute(root, "merchantCode", MerchantID);

			XmlElement submit = XMLUtils.AppendElement(root, "submit");
			
			XmlElement order = XMLUtils.AppendElement(submit, "order");
			XMLUtils.AppendAttribute(order, "orderCode", payment.LocalRequestReference);

			XMLUtils.AppendElement(order, "description", StringUtils.Left(payment.Description, 50));

			XmlElement amount = XMLUtils.AppendElement(order, "amount");
			XMLUtils.AppendAttribute(amount, "currencyCode", payment.PaymentAmount.CurrencyCode);
			XMLUtils.AppendAttribute(amount, "value", payment.PaymentAmount.LongValue);
			XMLUtils.AppendAttribute(amount, "exponent", payment.PaymentAmount.Currency.FractionDigits);

			AddPaymentMask(order);

			if (payment.UserInfo != null && payment.UserInfo.EmailAddress != null && payment.UserInfo.EmailAddress.Length > 0) {
				XmlElement shopper = XMLUtils.AppendElement(order, "shopper");
				XMLUtils.AppendElement(shopper, "shopperEmailAddress", payment.UserInfo.EmailAddress);
			}

			AddAddressInfo(payment, order);
	
			return xml.OuterXml;
		}

		private void AddAddressInfo(IElectronicPayment payment, XmlElement order) {

			if (payment.UserInfo != null && payment.UserInfo.Address1 != null && payment.UserInfo.Address1.Length > 0) {

				XmlElement shippingAddress = XMLUtils.AppendElement(order, "shippingAddress");
				XmlElement address = XMLUtils.AppendElement(shippingAddress, "address");

				string sp = " ";
				string[] names = payment.UserInfo.UserName.Split(sp.ToCharArray());

				if (names.Length == 2) {
					XMLUtils.AppendElement(address, "firstName", names[0]);
					XMLUtils.AppendElement(address, "lastName", names[1]);
				}

				Regex streetParser = new Regex(@"(^\d+)\s*(\w)?,?\s*(.*)$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
				Match match = streetParser.Match(payment.UserInfo.Address1);

				if (match.Groups.Count == 4 && match.Groups[3].Value != null&& match.Groups[3].Value.Length > 0) {

					XMLUtils.AppendElement(address, "street", match.Groups[3].Value);

					Group houseNumber = match.Groups[1];
					if (houseNumber != null && houseNumber.Value != null) {
						XMLUtils.AppendElement(address, "houseNumber", houseNumber.Value);
					}

					Group houseNumberExtension = match.Groups[2];
					if (houseNumberExtension != null && houseNumberExtension.Value != null) {
						XMLUtils.AppendElement(address, "houseNumberExtension", houseNumberExtension.Value);
					}
				} else {
					XMLUtils.AppendElement(address, "street", payment.UserInfo.Address1);
				}

				XMLUtils.AppendElement(address, "postalCode", payment.UserInfo.PostCode);
				XMLUtils.AppendElement(address, "city", payment.UserInfo.City);
				XMLUtils.AppendElement(address, "state", payment.UserInfo.State);
				XMLUtils.AppendElement(address, "countryCode", payment.UserInfo.CountryName);

				XMLUtils.AppendElement(address, "telephoneNumber", payment.UserInfo.TelephoneNumber);
			}
		}

		private string ComputeHexHash(string plainText) {
			
			byte[] hashedBytes = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(plainText));
			
			StringBuilder hexValue = new StringBuilder();
			
			for (int i = 0; i < hashedBytes.Length; i++) {
				hexValue.AppendFormat("{0:x2}", hashedBytes[i]);
			}
			
			return hexValue.ToString();
		}

		private void AddPaymentMask(XmlElement order) {

			XmlElement paymentMethodMask = XMLUtils.AppendElement(order, "paymentMethodMask");

			string[] includeList = (CardTypeInclude != null) ? CardTypeInclude.Split(CARD_TYPE_DELIMITER.ToCharArray()) : new string[] {};
			string[] excludeList = (CardTypeExclude != null) ? CardTypeExclude.Split(CARD_TYPE_DELIMITER.ToCharArray()) : new string[] {};

			if (includeList.Length == 0 && excludeList.Length == 0) {
				XmlElement includeAll = XMLUtils.AppendElement(paymentMethodMask, "include");
				XMLUtils.AppendAttribute(includeAll, "code", "ALL");
			} else {
				for (int i = 0; i < includeList.Length; i++) {
					XMLUtils.AppendAttribute(XMLUtils.AppendElement(paymentMethodMask, "include"), "code", includeList[i]);
				}
				for (int i = 0; i < excludeList.Length; i++) {
					XMLUtils.AppendAttribute(XMLUtils.AppendElement(paymentMethodMask, "exclude"), "code", excludeList[i]);
				}
			}
		}

		//See where these calls are coming from
		protected override void LogResponse(IElectronicPayment payment, string extraInfo) {
			if (payment == null || ((payment.LocalRequestReference == null || payment.LocalRequestReference.Length == 0) && 
				(payment.TransactionReference == null || payment.TransactionReference.Length == 0))) {
				Logger.Info("Invalid response received from [" + HttpContext.Current.Request.UserHostAddress + "]");
			} else {
				base.LogResponse (payment, extraInfo);
			}
		}
	}
}