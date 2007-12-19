using System;
using System.Collections;
using System.Xml;

using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	public enum CcErrCodes : int {
		Authorised = 1,
		Referred = 3,
		Declined = 50,
		None = 1054
	}

	/// <summary>
	/// Summary description for CcxPaymentRequest.
	/// </summary>
	public class EpdqMpiPaymentProvider : AbstractPaymentProvider {
		
		#region XML Tags
		public const string BANK_URL_POST = "https://secure2.epdq.co.uk:11500";

		public const string PAYMENT_MODE_PRODUCTION = "P";
		public const string PAYMENT_MODE_TEST = "T";
		public const string PAYMENT_MODE_ALWAYS_PASS = "Y";
		public const string PAYMENT_MODE_ALWAYS_FAIL = "N";
		public const string PAYMENT_MODE_RANDOM_PASS_FAIL = "R";

		public const string PAYMENT_POSTED_FIELD_NAME = "CLRCMRC_XML";

		public const short CVM_VALUE_SUBMITTED_BY_STORE = 1;

		public const string XML_TAG_ENGINE_DOC_LIST = "EngineDocList";
		public const string XML_TAG_ENGINE_DOC = "EngineDoc";
		public const string XML_TAG_DOC_VERSION = "DocVersion";
		public const string XML_TAG_CONTENT_TYPE = "ContentType";

		public const string XML_TAG_USER = "User";		
		public const string XML_TAG_USER_NAME = "Name";
		public const string XML_TAG_PASSWORD = "Password";
		public const string XML_TAG_CLIENT_ID = "ClientId";

		public const string XML_TAG_INSTRUCTIONS = "Instructions";
		public const string XML_TAG_PIPELINE = "Pipeline";
		public const string XML_TAG_ORDER_FORM_DOC = "OrderFormDoc";

		public const string XML_TAG_PAYMENT_MODE = "Mode";
		public const string XML_TAG_COMMENTS = "Comments";
		public const string XML_TAG_CONSUMER = "Consumer";
		public const string XML_TAG_EMAIL = "Email";
		public const string XML_TAG_PAYMENT_MECHANISM = "PaymentMech";

		public const string XML_TAG_CREDIT_CARD = "CreditCard";
		public const string XML_TAG_CREDIT_CARD_NUMBER = "Number";
		public const string XML_TAG_CREDIT_CARD_START_DATE = "StartDate";
		public const string XML_TAG_CREDIT_CARD_EXPIRES = "Expires";
		public const string XML_TAG_CREDIT_CARD_ISSUE_NUMBER = "IssueNum";

		public const string XML_TAG_TRANSACTION = "Transaction";
		public const string XML_TAG_TRANSACTION_TYPE = "Type";
		public const string XML_TAG_CURRENT_TOTALS = "CurrentTotals";
		public const string XML_TAG_TOTALS = "Totals";
		public const string XML_TAG_TOTAL = "Total";
		public const string XML_TAG_ID = "Id";

		public const string XML_TAG_BILL_TO = "BillTo";
		public const string XML_TAG_LOCATION = "Location";
		public const string XML_TAG_ADDRESS = "Address";
		public const string XML_TAG_STREET1 = "Street1";
		public const string XML_TAG_POST_CODE = "PostalCode";

		public const string XML_TAG_CARD_PROCESS_RESPONSE = "CardProcResp";
		public const string XML_TAG_AUTH_CODE = "AuthCode";
		public const string XML_TAG_CC_ERROR_CODE = "CcErrCode";

		public const string XML_TAG_CVM_VALUE = "Cvv2Val";
		public const string XML_TAG_CVM_INDICATOR = "Cvv2Indicator";

		public const string XML_TAG_SHIPPING_CHARGE = "Ship";
		public const string XML_TAG_VAT = "VatTax";

		public const string XML_TAG_ORDER_ITEM_LIST = "OrderItemList";
		public const string XML_TAG_ORDER_ITEM = "OrderItem";

		public const string XML_TAG_ORDER_ITEM_NUMBER = "ItemNumber";
		public const string XML_TAG_PRODUCT_CODE = "ProductCode";
		public const string XML_TAG_DESCRIPTION = "Desc";
		public const string XML_TAG_QUANTITY = "Qty";
		public const string XML_TAG_TAX_EXEMPT = "TaxExempt";
		public const string XML_TAG_UNIT_PRICE = "Price";

		public const string XML_ATTRIBUTE_DATA_TYPE = "DataType";
		public const string XML_ATTRIBUTE_LOCALE = "Locale";
		public const string XML_ATTRIBUTE_CURRENCY = "Currency";

		public const string DATA_TYPE_INTEGER = "S32";
		public const string DATA_TYPE_NUMERIC = "Numeric";
		public const string DATA_TYPE_MONEY = "Money";
		public const string DATA_TYPE_DATE_TIME = "DateTime";
		public const string DATA_TYPE_START_DATE = "StartDate";
		public const string DATA_TYPE_EXPIRATION_DATE = "ExpirationDate";
		
		public const string CONTENT_TYPE_ORDER_FORM_DOC = "OrderFormDoc";

		public const string PIPELINE_PAYMENT_NO_FRAUD = "PaymentNoFraud";
		#endregion

		private string _paymentPageUrl = BANK_URL_POST;
		private string _documentVersion = "1.0";
		private string _merchantUserName = "";
		private string _clientID = "";
		private string _paymentMode = PAYMENT_MODE_TEST;
		private string _comments = "";

		public EpdqMpiPaymentProvider() {
		}

		public override void RequestPreAuthPayment(IElectronicPayment payment) {
			RequestPayment(payment, EpdqCpiPaymentProvider.BANK_CHARGE_TYPE_RESERVE_PAYMENT);
		}

		public override void RequestAuthPayment(IElectronicPayment payment) {
			RequestPayment(payment, EpdqCpiPaymentProvider.BANK_CHARGE_TYPE_IMMEDIATE_SALE);
		}

		private void RequestPayment(IElectronicPayment payment, string paymentType) {

			XmlDocument requestDoc = CreatePaymentXml(payment, paymentType);

			//Record this request
			LogRequest(payment);

			LogXml("Sent request", requestDoc.DocumentElement.OuterXml);
			string response = HttpUtils.ReadHtmlPage(PaymentPageUrl, "POST", PAYMENT_POSTED_FIELD_NAME + "=" + requestDoc.DocumentElement.OuterXml);
			LogXml("Received response", response);

			//Doesn't seem to like header. Hope this isn't a problem
			XMLObject responseXml = new XMLObject(XMLUtils.StripXmlHeader(response));

			string authCode = responseXml["//" + XML_TAG_TRANSACTION + "/" + XML_TAG_AUTH_CODE];
			int ccErrCode = responseXml.GetInt32Value("//" + XML_TAG_TRANSACTION + "/" 
				+ XML_TAG_CARD_PROCESS_RESPONSE + "/" + XML_TAG_CC_ERROR_CODE);

			string transactionReference = responseXml["//" + XML_TAG_TRANSACTION + "/" + XML_TAG_ID];

			payment.PaymentDate = DateTime.Now;
			payment.TransactionStatus = TranslatePaymentStatusCode(ccErrCode);
			payment.TransactionReference = transactionReference;
			payment.AuthorisationCode = authCode;
		}

		private PaymentStatus TranslatePaymentStatusCode(int ccErrCode) {
			switch (ccErrCode) {
				case (int) CcErrCodes.Authorised:
					return PaymentStatus.Approved;
				case (int) CcErrCodes.Referred:
					return PaymentStatus.Referred;
				case (int) CcErrCodes.Declined:
					return PaymentStatus.Declined;
				default:
					return PaymentStatus.Other;
			}
		}

		protected void CheckRequestParameters(IElectronicPayment payment) {

			CreditCardPayment ccPayment = (CreditCardPayment) payment;
			ccPayment.CheckCardDetails();

		}

		private XmlNode CreateEngineDoc() {

			XmlDocument doc = new XmlDocument();
			XmlNode root = (XmlNode) doc;

			XmlNode engineDocList = AppendCcApiRecord(ref root, XML_TAG_ENGINE_DOC_LIST);
			AppendCcApiStringField(ref engineDocList, XML_TAG_DOC_VERSION, DocumentVersion);

			//Engine Doc
			XmlNode engineDoc = AppendCcApiRecord(ref engineDocList, XML_TAG_ENGINE_DOC);

			return engineDoc;
		}

		private XmlNode CreateOrderFormDoc() {

			XmlNode engineDoc = CreateEngineDoc();
			AppendCcApiStringField(ref engineDoc, XML_TAG_CONTENT_TYPE, CONTENT_TYPE_ORDER_FORM_DOC);

			//Merchant Info
			XmlNode user = AppendCcApiRecord(ref engineDoc, XML_TAG_USER);
			AppendCcApiStringField(ref user, XML_TAG_USER_NAME, MerchantUserName);
			AppendCcApiStringField(ref user, XML_TAG_PASSWORD, MerchantPassword);
			AppendCcApiIntegerField(ref user, XML_TAG_CLIENT_ID, Int32.Parse(ClientID));

			//Instructions
			XmlNode instructions = AppendCcApiRecord(ref engineDoc, XML_TAG_INSTRUCTIONS);
			AppendCcApiStringField(ref instructions, XML_TAG_PIPELINE, PIPELINE_PAYMENT_NO_FRAUD);

			//Order Form Doc
			XmlNode orderFormDoc = AppendCcApiRecord(ref engineDoc, XML_TAG_ORDER_FORM_DOC);

			return orderFormDoc;

		}

		private XmlDocument CreatePaymentXml(IElectronicPayment payment, string transactionType) {

			CreditCardPayment ccPayment = (CreditCardPayment) payment;
			XmlNode orderFormDoc = CreateOrderFormDoc();

			AppendCcApiStringField(ref orderFormDoc, XML_TAG_ID, payment.LocalRequestReference);
			AppendCcApiStringField(ref orderFormDoc, XML_TAG_PAYMENT_MODE, PaymentMode);
			AppendCcApiStringField(ref orderFormDoc, XML_TAG_COMMENTS, Comments);

			//Consumer
			XmlNode consumer = AppendCcApiRecord(ref orderFormDoc, XML_TAG_CONSUMER);
			AppendCcApiStringField(ref consumer, XML_TAG_EMAIL, ccPayment.UserInfo.EmailAddress);
			XmlNode paymentMech = AppendCcApiRecord(ref consumer, XML_TAG_PAYMENT_MECHANISM);

			//Address verification information
			if (payment.UserInfo.Address1.Length > 0 && payment.UserInfo.PostCode.Length > 0) {
				AppendAvsElements(ref consumer, payment);
			}

			//Credit card
			XmlNode creditCard = AppendCcApiRecord(ref paymentMech, XML_TAG_CREDIT_CARD);
			AppendCcApiStringField(ref creditCard, XML_TAG_CREDIT_CARD_NUMBER, ccPayment.CardNumber);
			
			AppendCcApiStartDateField(ref creditCard, 
				XML_TAG_CREDIT_CARD_START_DATE, 
				ccPayment.CardValidFromMonth, 
				ccPayment.CardValidFromYear, 
				ccPayment.PaymentAmount.Currency.CurrencyCodeNumeric);

			AppendCcApiExpirationDateField(ref creditCard, 
				XML_TAG_CREDIT_CARD_EXPIRES, 
				ccPayment.CardExpiresEndMonth, 
				ccPayment.CardExpiresEndYear, 
				ccPayment.PaymentAmount.Currency.CurrencyCodeNumeric);

			if (ccPayment.CardIssueNumber.Length > 0) {
				AppendCcApiStringField(ref creditCard, XML_TAG_CREDIT_CARD_ISSUE_NUMBER, ccPayment.CardIssueNumber);
			}

			//CVM Stuff
			if (ccPayment.CardCvmNumber.Length > 0) {
				AppendCcApiStringField(ref creditCard, XML_TAG_CVM_VALUE, ccPayment.CardCvmNumber);
				AppendCcApiStringField(ref creditCard, XML_TAG_CVM_INDICATOR, "" + CVM_VALUE_SUBMITTED_BY_STORE);
			}
			
			//Transaction
			XmlNode transaction = AppendCcApiRecord(ref orderFormDoc, XML_TAG_TRANSACTION);
			AppendCcApiStringField(ref transaction, XML_TAG_TRANSACTION_TYPE, transactionType);

			//Totals
			XmlNode currentTotals = AppendCcApiRecord(ref transaction, XML_TAG_CURRENT_TOTALS);
			XmlNode totals = AppendCcApiRecord(ref currentTotals, XML_TAG_TOTALS);

			AppendCcApiMoneyField(ref totals, XML_TAG_TOTAL, ccPayment.PaymentAmount);

			if (ccPayment.Basket != null) {

                BasketDecorator basket = new BasketDecorator(ccPayment.Basket);
				
				AppendCcApiMoneyField(ref totals, XML_TAG_SHIPPING_CHARGE, basket.DeliveryPrice);
				AppendCcApiMoneyField(ref totals, XML_TAG_VAT, basket.TaxPrice);

				AppendOrderItems(ref orderFormDoc, ccPayment.Basket);
			}

			return orderFormDoc.OwnerDocument;
		}

		private void AppendOrderItems(ref XmlNode orderFormDoc, IBasket order) {

			XmlNode items = AppendCcApiRecord(ref orderFormDoc, XML_TAG_ORDER_ITEM_LIST);

			for (int i = 1; i <= order.BasketItemList.Count; i++) {

                IBasketLine line = (IBasketLine) order.BasketItemList[i - 1];

				XmlNode item = AppendCcApiRecord(ref items, XML_TAG_ORDER_ITEM);

				//Get the unit price before tax, shipping, but after discount
				Money preTaxPrice = new Money(line.LinePrice);

				Money totalPrice = new Money(line.LinePrice);
				totalPrice.Add(line.TaxPrice);

				AppendCcApiIntegerField(ref item, XML_TAG_ORDER_ITEM_NUMBER, i);
				AppendCcApiStringField(ref item, XML_TAG_PRODUCT_CODE, line.ItemCode);
				AppendCcApiStringField(ref item, XML_TAG_ID, line.ItemCode);
				AppendCcApiStringField(ref item, XML_TAG_DESCRIPTION, StringUtils.Left(line.Description, 64));
				AppendCcApiIntegerField(ref item, XML_TAG_QUANTITY, line.Quantity);
				AppendCcApiIntegerField(ref item, XML_TAG_TAX_EXEMPT, 0);

				AppendCcApiMoneyField(ref item, XML_TAG_UNIT_PRICE, preTaxPrice);

				//Ignore tax and total for now...
				//AppendCcApiMoneyField(ref item, XML_TAG_VAT, line.TaxPrice);
				//AppendCcApiMoneyField(ref item, XML_TAG_TOTAL, totalPrice);

			}

			/*
			 			<OrderItemList>
			  <OrderItem>
			    <ItemNumber DataType="S32">1</ItemNumber>
			    <ProductCode>100200400</ProductCode>
			    <Id>100200400</Id>
			    <Desc>Some Stuff</Desc>
			    <Price DataType="Money" Currency="826">1125</Price>
			    <StateTax DataType="Money" Currency="826">395</StateTax>
			    <Total DataType="Money" Currency="826">2645</Total>
			    <Qty DataType="S32">2</Qty>
			    <TaxExempt DataType="S32">0</TaxExempt>
			  </OrderItem>
			</OrderItemList>
			*/

		}

		#region Append ClearCommerce Fields
		/*
		 DateTime		Specifies a timestamp, in milliseconds since
						January 1, 1970. Times should be expressed in
						Greenwich Mean Time (GMT).
						
		ExpirationDate	Specifies the expiration date, such as that of a
						credit card. Valid formats are “MM/YY” and
						“MM/DD/YY”
						
		Money			Specifies a monetary amount.
						The Money data type requires knowledge of the
						ISO 4217 currency codes and decimal values. For
						U.S. dollars, these values are 840 and 2,
						respectively. See Appendix K, “Currency Codes”
						on page 483 for a list of supported currencies.
						A Store can support only one currency. Therefore,
						all orders sent to the ClearCommerce Engine for
						processing must be in the same currency as
						configured for the Store. Otherwise, the transaction
						will fail.
						
		Numeric			Represents a decimal value by specifying an
						amount and the precision of the amount.
		
		S32				Specifies a 32-bit integer.
						
		StartDate		Specifies the start date of a credit card or a
						customer’s birth date. (Most credit cards do not
						have a start date.) Valid formats for credit cards are
						“MM/YY” and “MM/DD/YY”. For birth date, the
						format is “MM/DD/YYYY”.
						
		String			Specifies a text string.
*/

		protected XmlNode AppendCcApiRecord(ref XmlNode node, string recordName) {
			return XMLUtils.AppendNewElement(ref node, recordName);
		}

		protected void AppendCcApiDateTimeField() {
		}

		protected void AppendCcApiExpirationDateField(ref XmlNode node, string fieldName, short expiresEndMonth, short expiresEndYear, int locale) {

			XmlElement expires = XMLUtils.AppendNewElement(ref node, fieldName, FormatDate(expiresEndMonth, expiresEndYear));

			XMLUtils.AppendNewAttribute(ref expires, XML_ATTRIBUTE_DATA_TYPE, DATA_TYPE_EXPIRATION_DATE);
			XMLUtils.AppendNewAttribute(ref expires, XML_ATTRIBUTE_LOCALE, "" + locale);
		
		}

		protected void AppendCcApiMoneyField(ref XmlNode node, string fieldName, Money amount) {

			XmlElement total = XMLUtils.AppendNewElement(ref node, fieldName, "" + amount.LongValue);
			XMLUtils.AppendNewAttribute(ref total, XML_ATTRIBUTE_DATA_TYPE, DATA_TYPE_MONEY);
			XMLUtils.AppendNewAttribute(ref total, XML_ATTRIBUTE_CURRENCY, "" + amount.Currency.CurrencyCodeNumeric);

		}

		protected void AppendCcApiNumericField(ref XmlNode node, string fieldName, double amount) {
			XmlElement integer = XMLUtils.AppendNewElement(ref node, fieldName, "" + amount);
			XMLUtils.AppendNewAttribute(ref integer, XML_ATTRIBUTE_DATA_TYPE, DATA_TYPE_NUMERIC);
		}

		protected void AppendCcApiIntegerField(ref XmlNode node, string fieldName, int amount) {
			XmlElement integer = XMLUtils.AppendNewElement(ref node, fieldName, "" + amount);
			XMLUtils.AppendNewAttribute(ref integer, XML_ATTRIBUTE_DATA_TYPE, DATA_TYPE_INTEGER);
		}

		protected void AppendCcApiStartDateField(ref XmlNode node, string fieldName, short validFromMonth, short validFromYear, int locale) {
			XmlElement starts = XMLUtils.AppendNewElement(ref node, fieldName, FormatDate(validFromMonth, validFromYear));

			XMLUtils.AppendNewAttribute(ref starts, XML_ATTRIBUTE_DATA_TYPE, DATA_TYPE_START_DATE);
			XMLUtils.AppendNewAttribute(ref starts, XML_ATTRIBUTE_LOCALE, "" + locale);
		}

		protected void AppendCcApiStringField(ref XmlNode node, string fieldName, string text) {
			XMLUtils.AppendNewElement(ref node, fieldName, text);
		}
		#endregion

		private void AppendAvsElements(ref XmlNode consumerNode, IElectronicPayment payment) {

			XmlNode billTo = AppendCcApiRecord(ref consumerNode, XML_TAG_BILL_TO);
			XmlNode location = AppendCcApiRecord(ref billTo, XML_TAG_LOCATION);
			XmlNode address = AppendCcApiRecord(ref location, XML_TAG_ADDRESS);

			AppendCcApiStringField(ref address, XML_TAG_STREET1, payment.UserInfo.Address1);
			AppendCcApiStringField(ref address, XML_TAG_POST_CODE, payment.UserInfo.PostCode);
		}

		private void LogXml(string message, string xml) {

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			HideContents(doc, XML_TAG_CREDIT_CARD_NUMBER);
			HideContents(doc, XML_TAG_USER_NAME);
			HideContents(doc, XML_TAG_PASSWORD);

			Logger.Info(message + ": " + doc.DocumentElement.OuterXml);
		}

		private void HideContents(XmlDocument doc, string fieldName) {
			XmlNode node = doc.SelectSingleNode("//" + fieldName);
			if (node != null && node.InnerText != null && node.InnerText.Length > 0) {
				node.InnerText = "".PadRight(node.InnerText.Length, '*');
			}
		}

		private string FormatDate(short month, short year) {

			short shortYear = (short) (year - 2000);
			if (shortYear < 0) {
				shortYear += 100;
			}

			return StringUtils.Right("00" + month, 2) + "/" + StringUtils.Right("00" + shortYear, 2);
		}

		virtual public string DocumentVersion {
			get {
				return _documentVersion;
			}
			set {
				_documentVersion = value;
			}
		}

		public string MerchantUserName {
			get {
				return _merchantUserName;
			}
			set {
				_merchantUserName = value;
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

		public string PaymentPageUrl {
			get {
				return _paymentPageUrl;
			}
			set {
				_paymentPageUrl = value;
			}
		}

		public string PaymentMode {
			get {
				return _paymentMode;
			}
			set {
				_paymentMode = value;
				base.Mode = ConvertPaymentModeToTransactionMode(value);
			}
		}

		public override TransactionMode Mode {
			get {
				return base.Mode;
			}
			set {
				base.Mode = value;
				_paymentMode = ConvertTransactionModeToPaymentMode(value);
			}
		}


		public static TransactionMode ConvertPaymentModeToTransactionMode(string paymentMode) {

			switch (paymentMode) {
				case PAYMENT_MODE_ALWAYS_FAIL:
					return TransactionMode.AlwaysFail;
				case PAYMENT_MODE_ALWAYS_PASS:
					return TransactionMode.AlwaysPass;
				case PAYMENT_MODE_PRODUCTION:
					return TransactionMode.Production;
				case PAYMENT_MODE_RANDOM_PASS_FAIL:
					return TransactionMode.Random;
				case PAYMENT_MODE_TEST:
					return TransactionMode.Test;
				default:
					return TransactionMode.Test;
			}
		}

		public static string ConvertTransactionModeToPaymentMode(TransactionMode mode) {

			switch (mode) {
				case TransactionMode.AlwaysFail:
					return PAYMENT_MODE_ALWAYS_FAIL;
				case TransactionMode.AlwaysPass:
					return PAYMENT_MODE_ALWAYS_PASS;
				case TransactionMode.Production:
					return PAYMENT_MODE_PRODUCTION;
				case TransactionMode.Random:
					return PAYMENT_MODE_RANDOM_PASS_FAIL;
				case TransactionMode.Test:
					return PAYMENT_MODE_TEST;
				default:
					return PAYMENT_MODE_TEST;
			}
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