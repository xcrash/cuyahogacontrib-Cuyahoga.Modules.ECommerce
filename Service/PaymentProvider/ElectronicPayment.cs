using System;
using System.Web;
using System.Collections;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	//Hmmm. Not too sure whether this should be an enum or not
	public enum PaymentStatus : int {
		/// <summary>Approved by provider</summary>
		Approved = 1,
		/// <summary>Declined</summary>
		Declined = 2,
		/// <summary>Provider considers it fraudulent</summary>
		Fraud = 3,
		/// <summary>Any other status</summary>
		Other = 4,
		/// <summary>Transaction was not submitted</summary>
		NotSubmitted = 5,
		/// <summary>Payment amount does not match requested amount</summary>
		InvalidAmount = 6,
		/// <summary>Need to contact bank before payment can be released</summary>
		Referred = 7,
		/// <summary>Card type not supported</summary>
		InvalidTender = 8,
		/// <summary>Invalid card details</summary>
		InvalidCardDetails = 9,
		/// <summary>Insufficent funds</summary>
		InsufficientFunds = 10
	}

	/// <summary>
	/// Summary description for AbstractPaymentBase.
	/// </summary>
	public class ElectronicPayment : IElectronicPayment {

		private string _localReference = "";
		private IDictionary _customValues = new Hashtable();
        private WebStoreUser _userDetails = null;
		private Money _paymentAmount = null;
		private DateTime _dateTime;
		private PaymentStatus _status = PaymentStatus.Other;
		private string _transactionReference = "";
		private string _authCode = "";
		private IBasket _order = null;
		private string _description = "";

		public ElectronicPayment() {
		}

		/// <summary>
		/// The reference used by our application
		/// </summary>
		public string LocalRequestReference {
			get {
				return _localReference;
			}
			set {
				_localReference = value;
			}
		}

		public Money PaymentAmount {
			get {
				return _paymentAmount;
			}
			set {
				_paymentAmount = value;
			}
		}

		/// <summary>
		/// Allows support for arbitary properties that may not be supported or persisted
		/// by all providers
		/// </summary>
		public IDictionary CustomValues {
			get {
				return _customValues;
			}
			set {
				_customValues = value;
			}
		}

		/// <summary>
		/// Stores basic user information and delivery addresses
		/// where relevant
		/// </summary>
        public WebStoreUser UserInfo {
			get {
				if (_userDetails == null) {
                    _userDetails = new WebStoreUser();
				}
				return _userDetails;
			}
		
			set {
				_userDetails = value;
			}
		}

		/// <summary>
		/// The reference given by the provider
		/// </summary>
		public string TransactionReference {
			get {
				return _transactionReference;
			}
			set {
				_transactionReference = value;
			}
		}

		public PaymentStatus TransactionStatus {
			get {
				return _status;
			}
			set {
				_status = value;
			}
		}

		/// <summary>
		/// The date the payment was made
		/// </summary>
		public DateTime PaymentDate {
			get {
				return _dateTime;
			}
			set {
				_dateTime = value;
			}
		}

		public string AuthorisationCode { 
			get {
				return _authCode;
			}
			set {
				_authCode = value;
			}
		}

		public IBasket Basket {
			get {
				return _order;
			}
			set {
				_order = value;
			}
		}

		public string Description {
			get {
				return _description;
			}
			set {
				_description = value;
			}
		}
	}
}