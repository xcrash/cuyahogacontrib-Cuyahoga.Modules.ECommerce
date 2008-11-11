using System;

using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Service.Translation;
using Cuyahoga.Modules.ECommerce.Util.Location;
using Cuyahoga.Modules.ECommerce.DataAccess;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Summary description for PaymentHelper.
	/// </summary>
	public class PaymentHelper {

		private ICommerceDao _dao;

		public PaymentHelper(ICommerceDao dao) {
			_dao = dao;
		}

		public IElectronicPayment CreatePayment(IBasket order, ITextTranslator translator) {
			
			IElectronicPayment payment = new ElectronicPayment();
			PopulatePayment(payment, order, translator);

			return payment;
		}

		public void PopulatePayment(IElectronicPayment payment, IBasket order, ITextTranslator translator) {

			BasketDecorator basket = new BasketDecorator(order);

			payment.LocalRequestReference = basket.OrderHeader.OrderHeaderID.ToString();
			payment.Description = string.Format(translator.GetText("web_store_purchase{0}"), payment.LocalRequestReference);
			payment.PaymentAmount = basket.GrandTotal;
			payment.PaymentDate = DateTime.Now;

            IUserDetails userDetails = (order.UserDetails != null) ? new UserDecorator(order.UserDetails) : order.AltUserDetails;

            //payment.UserInfo.UserDetails. = userDetails.FirstName + " " + userDetails.LastName; -- Is this needed?
            payment.UserInfo.UserDetails.EmailAddress = userDetails.EmailAddress;
            payment.UserInfo.UserDetails.TelephoneNumber = userDetails.TelephoneNumber;

			IAddress invoiceAddress = basket.OrderHeader.InvoiceAddress; 

			payment.UserInfo.UserAddress.AddressLine1 = invoiceAddress.AddressLine1;

			if (invoiceAddress.AddressLine3 != null && invoiceAddress.AddressLine3.Length > 0) {
				payment.UserInfo.UserAddress.AddressLine2 = invoiceAddress.AddressLine2 + ", " + invoiceAddress.AddressLine3;
			} else {
				payment.UserInfo.UserAddress.AddressLine2 = invoiceAddress.AddressLine2;
			}

			payment.UserInfo.UserAddress.City = invoiceAddress.City;
			payment.UserInfo.UserAddress.Region = invoiceAddress.Region;
			payment.UserInfo.UserAddress.Postcode = invoiceAddress.Postcode;
			
			ITextTranslator ccTrans = TranslatorUtils.GetTextTranslator(typeof(CountryCode), translator.CultureCode);
			payment.UserInfo.UserAddress.CountryCode = invoiceAddress.CountryCode;
		}

        public BasketDecorator ProcessReceivedPayment(IElectronicPayment ccPayment, ITextTranslator translator) {

			try {

				long orderID = Int64.Parse(ccPayment.LocalRequestReference);
				IBasket basket = _dao.FindOrder(orderID);
				BasketDecorator order;

				if (basket != null) {
					order = new BasketDecorator(basket);
				} else {
					throw new InvalidOperationException("Could not find order for local ccPayment ref [" + ccPayment.LocalRequestReference + "]");
				}

				//Make a record in the database about this ccPayment
				RecordPayment(basket, ccPayment);

				//Make sure this isn't already purchased
				if (order.IsPurchased) {
					throw new InvalidOperationException("Paid for order [" + order.OrderHeader.OrderHeaderID + "] more than once");
				}

				//Make sure the amount is correct (assume the same currency)
				if (order.GrandTotal.Amount != (decimal) ccPayment.PaymentAmount.Amount) {
					throw new InvalidOperationException("Invalid ccPayment amount - expected " + order.GrandTotal.Amount
						+ ", recieved " + ccPayment.PaymentAmount.Amount + " " 
						+ ccPayment.PaymentAmount.CurrencyCode);
				}

				//Either PO or successful credit card
				if (order.OrderHeader.PaymentMethod == PaymentMethodType.PurchaseOrderInvoice 
					|| ccPayment != null && (ccPayment.TransactionStatus == PaymentStatus.Approved || ccPayment.TransactionStatus == PaymentStatus.Referred)) {

					//This is a purchase order, or a direct credit card order
                    return order;
				}

			} catch (Exception f) {
				LogManager.GetLogger(GetType()).Error(f);
			}

            return null;
		}

		//Make a note of whether this payment worked
		private void RecordPayment(IBasket order, IElectronicPayment ccPayment) {

			IPaymentRecord payment = _dao.CreatePaymentRecord(order);

			BasketDecorator basketOrder = new BasketDecorator(order);

			payment.Basket = order;

			payment.PaymentMethod = order.OrderHeader.PaymentMethod;
			payment.PaymentAmount = basketOrder.GrandTotal;
			payment.PaymentDate = System.DateTime.Now;
			payment.TransactionReference = ccPayment.TransactionReference;
			payment.TransactionStatus = ccPayment.TransactionStatus;
			payment.PaymentProviderID = 1; // provider.PaymentProviderID;

			_dao.Save(payment);
		}
	}
}
