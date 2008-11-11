using System;
using System.Collections;
using Cuyahoga.Modules.ECommerce.Util;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	/// <summary>
	/// Takes a payment, submits it to the gateway and updates the payments details.
	/// May be used for batch payment processing because the payment is not stored within the object
	/// </summary>
	public abstract class AbstractPaymentProvider : ConfiguredObject, IPaymentProvider {

		public const string CONFIGURATION_KEY_TRANSACTION_MODE = "TransactionMode";
		public const string CONFIGURATION_KEY_MERCHANT_PASSWORD = "MerchantPassword";
		public const string CONFIGURATION_KEY_MERCHANT_ID = "MerchantID";

		private string _merchantPassword;
		private string _merchantID = "";
		private TransactionMode _transactionMode = TransactionMode.Test;
		private ILog _logger;

		public AbstractPaymentProvider() {
			_logger = LogManager.GetLogger(GetType());
		}

		public override void LoadConfiguration(Hashtable configuration) {

			base.LoadConfiguration(configuration);

			try {
				Mode = (TransactionMode) ((int) GetConfigurationValue(CONFIGURATION_KEY_TRANSACTION_MODE));
			} catch {
				//MUST do something here. We don't want to make an assumption about the mode
				throw new ArgumentException("Transaction Mode not defined");
			}
			try {
				MerchantPassword = (string) GetConfigurationValue(CONFIGURATION_KEY_MERCHANT_PASSWORD);
			} catch {
				_logger.Info("Merchant Password not defined");
			}
			try {
				MerchantID = (string) GetConfigurationValue(CONFIGURATION_KEY_MERCHANT_ID);
			} catch {
				_logger.Info("Merchant ID not defined");
			}
		}

		public virtual TransactionMode Mode {
			get {
				return _transactionMode;
			}
			set {
				_transactionMode = value;
			}
		}

		public string MerchantPassword {
			get {
				return _merchantPassword;
			}
			set {
				_merchantPassword = value;
			}
		}

		public string MerchantID {
			get {
				return _merchantID;
			}
			set {
				_merchantID = value;
			}
		}

		public virtual ILog Logger {
			get {
				return _logger;
			}
		}

		/// <summary>
		/// In standard operation, takes the payment, submits it to the merchant and modifies the
		/// payment to reflect the merchant response
		/// </summary>
		/// <param name="payment"></param>
		public virtual void RequestPreAuthPayment(IElectronicPayment payment) {
			throw new NotImplementedException("Not yet implemented");
		}

		public virtual void RequestAuthPayment(IElectronicPayment payment) {
			throw new NotImplementedException("Not yet implemented");
		}

		public virtual void RequestPostAuthPayment(IElectronicPayment payment) {
			throw new NotImplementedException("Not yet implemented");
		}

		public virtual void RequestRefund(IElectronicPayment payment) {
			throw new NotImplementedException("Not yet implemented");
		}

		public virtual void RequestRefund(IElectronicPayment payment, Money refundAmount) {
			throw new NotImplementedException("Not yet implemented");
		}

		public virtual void RequestSettlePayment(IElectronicPayment payment) {
			throw new NotImplementedException("Not yet implemented");
		}

		public virtual void RequestVoidPayment(IElectronicPayment payment) {
			throw new NotImplementedException("Not yet implemented");
		}

		protected virtual void CheckRequestParameters(IElectronicPayment payment, PaymentRequestTypes paymentType) {
		}

		protected virtual void LogRequest(IElectronicPayment payment) {
			LogRequest(payment, null);
		}

		protected virtual void LogRequest(IElectronicPayment payment, string extraInfo) {

			string logMessage = "";

			try {
				logMessage = "Requested payment"
					+ " [" + Mode + "]"
					+ ", Date [" + DateTime.Now
					+ "], OrderNum [" + payment.LocalRequestReference
					+ "], Total[" + payment.PaymentAmount.Amount + " " + payment.PaymentAmount.CurrencyCode
					+ "]";

			} catch {
				logMessage = "Received request" 
					+ " [" + Mode + "]"
					+ ", Date [" + DateTime.Now 
					+ "]";
			}

			if (extraInfo != null && extraInfo.Length > 0) {
				logMessage += ", " + extraInfo;
			}

			Logger.Info(logMessage);

		}

		protected virtual void LogResponse(IElectronicPayment payment) {
			LogResponse(payment, null);
		}

		protected virtual void LogResponse(IElectronicPayment payment, string extraInfo) {

			string logMessage = "";

			try {
				logMessage = "Received payment" 
					+ " [" + Mode + "]"
					+ ", Date [" + payment.PaymentDate 
					+ "], OrderNum [" + payment.LocalRequestReference
					+ "], TransactionRef [" + payment.TransactionReference
					+ "], Total[" + payment.PaymentAmount.Amount + " " + payment.PaymentAmount.CurrencyCode
					+ "], StatusCode [" + payment.TransactionStatus + "]";

			} catch {
				logMessage = "Received response" 
					+ " [" + Mode + "]"
					+ ", Date [" + DateTime.Now 
					+ "]";
			}

			if (extraInfo != null && extraInfo.Length > 0) {
				logMessage += ", " + extraInfo;
			}

			Logger.Info(logMessage);
		}
	}
}