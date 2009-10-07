using System;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	/*
	 * This list could become a maintenance problem.
	 * It might be better having card type as proper class,
	 * together with validation routines
	 */
	public enum CreditCardType : int {
		Visa = 1,
		MasterCard = 2,
		Switch = 3,
		Solo = 4,
		AmericanExpress = 5,
		JCB = 6,
		Maestro = 7
	}

	/// <summary>
	/// Summary description for CreditCardPayment.
	/// </summary>
	public class CreditCardPayment : ElectronicPayment {

		private string _cvmNumber = "";
		private string _cardNumber = "";
		private CreditCardType _cardType = CreditCardType.Visa;

		private short _cardExpiryMonth = 0;
		private short _cardExpiryYear = 0;

		private short _cardValidFromMonth = 0;
		private short _cardValidFromYear = 0;

		private string _cardIssueNumber = "";

		public CreditCardPayment() {
		}

		/// <summary>
		/// Determines whether the card type usually requires
		/// an issue number. This may differ from bank to bank, even with the same
		/// card type.
		/// </summary>
		/// <param name="cardType">Type of card</param>
		/// <returns><c>true</c>, if you at least need to provide an option</returns>
		public static bool RequiresIssueNumber(CreditCardType cardType) {
			return (cardType == CreditCardType.Switch || cardType == CreditCardType.Solo || cardType == CreditCardType.Maestro);
		}

		public string CardCvmNumber {
			get {
				return _cvmNumber;
			}
			set {
				_cvmNumber = value;
			}
		}

		public string CardNumber {
			get {
				return _cardNumber;
			}
			set {
				_cardNumber = value;
			}
		}

		public CreditCardType CardType {
			get {
				return _cardType;
			}
			set {
				_cardType = value;
			}
		}

		public short CardExpiresEndMonth {
			get {
				return _cardExpiryMonth;
			}
			set {
				_cardExpiryMonth = value;
			}
		}

		public short CardExpiresEndYear {
			get {
				return _cardExpiryYear;
			}
			set {
				_cardExpiryYear = value;
			}
		}

		public short CardValidFromMonth {
			get {
				return _cardValidFromMonth;
			}
			set {
				_cardValidFromMonth = value;
			}
		}

		public short CardValidFromYear {
			get {
				return _cardValidFromYear;
			}
			set {
				_cardValidFromYear = value;
			}
		}

		public string CardIssueNumber {
			get {
				return _cardIssueNumber;
			}
			set {
				_cardIssueNumber = value;
			}
		}

		public void CheckCardDetails() {
			if (CardNumber == null || CardNumber.Length == 0) {
				throw new ArgumentException("Invalid card number");
			}
		}
	}
}