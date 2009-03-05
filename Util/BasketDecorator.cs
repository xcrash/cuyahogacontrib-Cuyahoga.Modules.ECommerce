using System;
using System.Collections;
using System.Xml.Serialization;

using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Location;
using Cuyahoga.Modules.ECommerce.DataAccess;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Core.Domain;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Allows us to add functionality to any basket that implements IBasket
	/// </summary>
	public class BasketDecorator : IBasket {

		public const string BASKET_REF_PREFIX = "B";
		public const int BASKET_REF_LENGTH = 14;

		public static readonly OrderStatus[] ORDER_STATUSES_PURCHASED = new OrderStatus[] {
																							  OrderStatus.OrderedPaid,
																							  OrderStatus.OrderedPaidAcknowledged,
																							  OrderStatus.OrderedReferred,
																							  OrderStatus.OrderedReferredAcknowledged,
																							  OrderStatus.Processing,
																							  OrderStatus.Shipped
																						  };


		private IBasket _basket;

		public BasketDecorator(IBasket basket) {
            if (basket != null) {
                _basket = basket;
            } else {
                throw new ArgumentException("Null child basket supplied to BasketDecorator");
            }
		}

		public static string CreateBasketReference() {
			
			DateTime now = DateTime.Now;

			string reference = BASKET_REF_PREFIX
				+ StringUtils.Right("0" + now.Year, 2) + StringUtils.Right("0" + now.Month, 2) + StringUtils.Right("0" + now.Day, 2) + "-"
				+ StringUtils.GenerateRandomText(BASKET_REF_LENGTH, StringUtils.CAR_PLATE_VALUES);

			return StringUtils.Left(reference, BASKET_REF_LENGTH);
		}

        public IBasket Basket {
            get {
                return _basket;
            }
        }

		/// <summary>
		/// Determines whether the customer has paid for this order
		/// </summary>
		public bool IsPurchased {
			get {
				if (OrderHeader == null) {
					return false;
				}

				OrderStatus status = OrderHeader.Status;

				for (int i = 0; i < ORDER_STATUSES_PURCHASED.Length; i++) {
					if (status == ORDER_STATUSES_PURCHASED[i]) return true;
				}

				return false;
			}
		}

        /// <summary>
        /// Determines whether this order is effectively closed and should be dealt with offline
        /// </summary>
        public bool IsAwaitingAccountPaymentApproval {
            get {
                return (OrderHeader != null && OrderHeader.Status == OrderStatus.OrderedSubmittedForAccountPayment);
            }
        }
		#region IBasket Members

		public IOrderHeader OrderHeader {
			get {
				return _basket.OrderHeader;
			}
			set {
				_basket.OrderHeader = value;
			}
		}

		public User UserDetails {
			get {
				return _basket.UserDetails;
			}
			set {
				_basket.UserDetails = value;
			}
		}

        public IUserDetails AltUserDetails {
            get {
                return _basket.AltUserDetails;
            }
            set {
                _basket.AltUserDetails = value;
            }
        }
        
        public long BasketID {
			get {
				return _basket.BasketID;
			}
			set {
				_basket.BasketID = value;
			}
		}

		public DateTime CreatedDate {
			get {
				return _basket.CreatedDate;
			}
			set {
				_basket.CreatedDate = value;
			}
		}

		public string CultureCode {
			get {
				return _basket.CultureCode;
			}
			set {
				_basket.CultureCode = value;
			}
		}

		public string CurrencyCode {
			get {
				return _basket.CurrencyCode;
			}
			set {
				_basket.CurrencyCode = value;
			}
		}

        public IList BasketItemList {
			get {
                return _basket.BasketItemList;
			}
            set {
                _basket.BasketItemList = value;
            }
		}


        public IBasketLine AddItem(BasketItemType itemType, string description, decimal price) {

            throw new Exception("The method or operation is not implemented.");
        }

        public IBasketLine UpdateItem(BasketItem item, string description, decimal price) {

            throw new Exception("The method or operation is not implemented.");
        }

		public IBasketLine AddItem(string itemCode, int quantity) {
			return _basket.AddItem(itemCode, quantity);
		}

		public IBasketLine AddItem(string itemCode, int quantity, BasketItemType itemType, string description, decimal unitPrice) {
			return _basket.AddItem(itemCode, quantity, itemType, description, unitPrice);
		}

		public void RemoveItem(string itemCode) {
			_basket.RemoveItem(itemCode);
		}

		/// <summary>
		/// Reduces the quantity of the supplied items by quantity
		/// </summary>
		/// <param name="itemCode">The unique item code identifying the item to add</param>
		/// <param name="quantity">The quantity to be removed</param>
		public void RemoveItem(string itemCode, int quantity) {

			IBasketLine bi = GetBasketItem(itemCode);

			if (bi != null) {
				if (bi.Quantity <= quantity) {
					RemoveItem(bi);
				} else {
					bi.Quantity -= quantity;
				}
			}
		}

		/// <summary>
		/// Reduces the quantity of the supplied item by quantity
		/// </summary>
		/// <param name="item">item to be removed</param>
		/// <param name="quantity">The quantity to be removed</param>
		public void RemoveItem(IItemDetails details, int quantity) {
			RemoveItem(details.ItemCode, quantity);
		}

		public void AmendItemQuantity(string itemCode, int newQuantity) {
			_basket.AmendItemQuantity(itemCode, newQuantity);
		}

		/// <summary>
		/// Removes all items from the basket
		/// </summary>
		public void Empty() {

            ArrayList listCopy = new ArrayList();
            foreach (IBasketLine item in BasketItemList) {
                listCopy.Add(item);
            }

            foreach (IBasketLine item in listCopy) {
				RemoveItem(item);
			}
		}

		#endregion

		public bool IsEmpty {
			get {
				if (BasketItemList == null || BasketItemList.Count == 0) {
					return true;
				}

				//Shouldn't be allowed to have a non-empty basket with no standard items
				if (GetStandardItems().Count == 0) {
					Empty();
					return true;
				}

				return false;
			}
		}

		[XmlIgnore]
		public bool IsPricedOK {
			get {
                if (BasketItemList == null) return true;

				foreach (IBasketLine line in BasketItemList) {
					if (line.Status != PricingStatus.OK) {
						return false;
					}
				}
				return true;
			}
		}

		#region Adding Items
		public void AddBasket(IBasket basket) {
			AddItemList(basket.BasketItemList);
		}

		public void AddItemList(IList itemList) {
			foreach (IItemLine line in itemList) {
				AddItem(line);
			}
		}
		
		public IBasketLine AddItem(IItemLine line) {
			return AddItem(line.ItemCode, line.Quantity);
		}
		#endregion

		#region Removing Items
		public void RemoveItem(IBasketLine item) {
			_basket.RemoveItem(item);
		}

		public int RemoveItems(BasketItemType itemType) {
			return RemoveArrayItems(GetBasketItemList(itemType));
		}

		public int RemoveItems(PricingStatus status) {
			return RemoveArrayItems(GetBasketItemList(status));
		}

		private int RemoveArrayItems(ArrayList itemList) {

			int itemCount = itemList.Count;
			
			foreach (IBasketLine basketItem in itemList) {
				RemoveItem(basketItem);
			}

			return itemCount;

		}

		public void RemoveNonPurchaseItems() {
		
			ArrayList items = GetNonPurchaseItems();

			foreach (IBasketLine item in items) {
				RemoveItem(item);
			}
		}
		#endregion

		public IBasketLine GetBasketItem(string itemCode) {

			IList basketLines = BasketItemList;

			foreach (IBasketLine bi in basketLines) {
				if (string.Compare(bi.ItemCode, itemCode, true) == 0) {
					return bi;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets a list of items that cannot be purchased
		/// </summary>
		/// <returns></returns>
		public ArrayList GetNonPurchaseItems() {
			
			ArrayList items = new ArrayList();

			items.AddRange(GetBasketItemList(PricingStatus.NotFound));
			items.AddRange(GetBasketItemList(PricingStatus.Obsolete));
			items.AddRange(GetBasketItemList(PricingStatus.Replaced));
			items.AddRange(GetBasketItemList(PricingStatus.ZeroPrice));

			return items;
		}

		public ArrayList GetBasketItemList(BasketItemType itemType) {

			IList itemList = BasketItemList;
			ArrayList foundItems = new ArrayList();

			foreach (IBasketLine item in itemList) {
				if (item.ItemType == itemType) {
					foundItems.Add(item);
				}
			}

			return foundItems;

		}

		public ArrayList GetBasketItemList(PricingStatus status) {

			IList itemList = BasketItemList;
			ArrayList foundItems = new ArrayList();

			foreach (IBasketLine item in itemList) {
				if (item.Status == status) {
					foundItems.Add(item);
				}
			}

			return foundItems;

		}

		/// <summary>
		/// Convenience method to access standard items
		/// </summary>
		/// <returns></returns>
		public ArrayList GetStandardItems() {
			return GetBasketItemList(BasketItemType.StandardItem);
		}

		protected Money GetBasketItemTotal(BasketItemType itemType) {

			Money total = new Money(CurrencyCode, 0);
			ArrayList itemList = GetBasketItemList(itemType);

			foreach (IBasketLine item in itemList) {
				total.Amount += item.LinePrice.Amount;
			}

			return total;
		}

		/// <summary>
		/// Gets the total before tax
		/// </summary>
		public virtual Money SubTotal {
			get {
				return _basket.SubTotal;
			}
			set {
				_basket.SubTotal = value;
			}
		}

		public virtual Money StandardItemPrice {
			get {
				return GetBasketItemTotal(BasketItemType.StandardItem);
			}
		}

		/// <summary>
		/// Lumps together all charges
		/// </summary>
		public Money ChargePrice {
			get {
				return new Money(SubTotal, SubTotal.Amount - StandardItemPrice.Amount);
			}
		}

		/// <summary>
		/// Includes small order charge and minimum order charge
		/// </summary>
		public virtual Money OrderChargePrice {
			get {
				Money smallOrderCharge = GetBasketItemTotal(BasketItemType.SmallOrderCharge);
				Money minimumOrderCharge = GetBasketItemTotal(BasketItemType.MinimumOrderCharge);

				return new Money(CurrencyCode, smallOrderCharge.Amount + minimumOrderCharge.Amount);
			}
		}

		public virtual Money AdministrationCharge {
			get {
				return GetBasketItemTotal(BasketItemType.AdminSurcharge);
			}
		}

		public virtual Money DeliveryPrice {
			get {
				return GetBasketItemTotal(BasketItemType.DeliveryCharge);
			}
		}

		public virtual Money TaxPrice {
			get {
				return _basket.TaxPrice;
			}
			set {
				_basket.TaxPrice = value;
			}
		}

		/// <summary>
		/// Total amount payable including all discounts, charges and taxes
		/// </summary>
		public virtual Money GrandTotal {
			get {
				return new Money(CurrencyCode, SubTotal.Amount + TaxPrice.Amount);
			}
		}

		public void ConvertToOrder(ICommerceDao dao, Cuyahoga.Core.DataAccess.ICommonDao commonDao) {

			//Creates header and addresses
			IOrderHeader header = dao.CreateOrderHeader(_basket);

			header.Comment = "";
			header.OrderedDate = DateTime.Now;
			header.PurchaseOrderNumber = "";
			header.Status = OrderStatus.OrderedNotPaid;

            commonDao.SaveOrUpdateObject(header);
            
			_basket.OrderHeader = header;
		}

		/// <summary>
		/// Derives the country code to use for this basket, if possible
		/// </summary>
		/// <returns></returns>
		public string GetDeliveryCountryCode() {

			string deliveryCountry = null;

			if (OrderHeader != null) {

				deliveryCountry = GetDeliveryCountry(OrderHeader.DeliveryAddress);

				if (deliveryCountry == null) {
					deliveryCountry = GetDeliveryCountry(OrderHeader.InvoiceAddress);
				}
			}

			if (deliveryCountry == null) {
				deliveryCountry = GetDeliveryCountry(UserDetails as IAddress);
			}

			return deliveryCountry;
		}

		private string GetDeliveryCountry(IAddress address) {

			if (address != null && address.CountryCode != null && address.CountryCode.Length == CountryCode.COUNTRY_CODE_LENGTH) {
				return address.CountryCode;
			}

			return null;
		}
	}
}