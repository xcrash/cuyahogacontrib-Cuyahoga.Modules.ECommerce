using System;
using System.Collections;

using Cuyahoga.Core.Domain;

using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util.Enums;

namespace Cuyahoga.Modules.ECommerce.Domain {

    #region Basket

    /// <summary>
    /// Basket object for NHibernate mapped table 'Ecommerce_Basket'.
    /// </summary>
    public class Basket : IBasket {

        #region Member Variables

        private long _basketID;
        private DateTime _inserttimestamp;
        private DateTime _updatetimestamp;
        private string _currencyCode;
        private string _cultureCode = "en-GB";
        private User _user;
        private IUserDetails _altUser;
        private OrderHeader _orderHeader;
        private IList _ecommercePayments;
        private IList _ecommerceBasketItems;

        //Store these data to prevent expensive calculations each time the results are required
        private Money _basketTaxPriceMoney = null;
        private decimal _basketTaxPriceDecimal = 0;
        private Money _basketPreTaxPriceMoney = null;
        private decimal _basketPreTaxPriceDecimal = 0;
        private decimal _deliveryCost = 0;
        #endregion

        private decimal _SubtotalDecimal {
            get { return _basketPreTaxPriceDecimal; }
            set { _basketPreTaxPriceDecimal = value; }
        }

        private decimal _TaxDecimal {
            get { return _basketTaxPriceDecimal; }
            set { _basketTaxPriceDecimal = value; }
        }

        #region Constructors

        public Basket() {
            _inserttimestamp = DateTime.Now;
            _updatetimestamp = DateTime.Now;
        }

        public Basket(IStoreContext context) : this() {
            _currencyCode = context.CurrencyCode;
            _cultureCode = "en-GB";
            //User = context.CurrentUser;
        }
        #endregion

        #region Public Properties

        public virtual Decimal DeliveryCost {
            get { return _deliveryCost; }
            set { _deliveryCost = value; }
        }

        public virtual long BasketID {
            get { return _basketID; }
            set { _basketID = value; }
        }

        public DateTime CreatedDate {
            get { return _inserttimestamp; }
            set { _inserttimestamp = value; }
        }

        public virtual DateTime ModifiedDate {
            get { return _updatetimestamp; }
            set { _updatetimestamp = value; }
        }

        public virtual string CurrencyCode {
            get { return _currencyCode; }
            set { _currencyCode = value; }
        }

        public virtual string CultureCode {
            get { return _cultureCode; }
            set { _cultureCode = value; }
        }

        public virtual User UserDetails {
            get { return _user; }
            set { _user = value; }
        }

        public virtual IUserDetails AltUserDetails {
            get { return _altUser; }
            set { _altUser = value; }
        }

        public virtual IList Payments {
            get {
                if (_ecommercePayments == null) {
                    _ecommercePayments = new ArrayList();
                }
                return _ecommercePayments;
            }
            set { _ecommercePayments = value; }
        }

        public virtual IList BasketItemList {
            get {
                if (_ecommerceBasketItems == null) {
                    _ecommerceBasketItems = new ArrayList();
                }
                return _ecommerceBasketItems;
            }
            set { _ecommerceBasketItems = value; }
        }
        #endregion

        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj) {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            Basket castObj = (Basket)obj;
            return (castObj != null) && (this._basketID == castObj.BasketID);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {
            int hash = 57;
            hash = 27 * hash * _basketID.GetHashCode();
            return hash;
        }
        #endregion

    #endregion

    #region IBasket Members

        public virtual IOrderHeader OrderHeader {
            get {
                return _orderHeader;
            }
            set {
                _orderHeader = value as OrderHeader;
            }
        }

        public virtual Money SubTotal {
            get {
                if (_basketPreTaxPriceMoney == null) {
                    _basketPreTaxPriceMoney = new Money(CurrencyCode, _basketPreTaxPriceDecimal);
                    _basketPreTaxPriceMoney.OnChange += new Money.OnChangeHandler(SubTotal_OnChange);
                }
                return _basketPreTaxPriceMoney;
            }
            set {
                _basketPreTaxPriceMoney = value;
                _basketPreTaxPriceMoney.OnChange += new Money.OnChangeHandler(SubTotal_OnChange);
            }
        }

        protected virtual void SubTotal_OnChange(Money money) {
            if (money != null) {
                _basketPreTaxPriceDecimal = money.Amount;
            } else {
                _basketPreTaxPriceDecimal = 0;
            }
        }

        public virtual Money TaxPrice {
            get {
                if (_basketTaxPriceMoney == null) {
                    _basketTaxPriceMoney = new Money(CurrencyCode, _basketTaxPriceDecimal);
                    _basketTaxPriceMoney.OnChange += new Money.OnChangeHandler(TaxPrice_OnChange);
                }
                return _basketTaxPriceMoney;
            }
            set {
                _basketTaxPriceMoney = value;
                _basketTaxPriceMoney.OnChange += new Money.OnChangeHandler(TaxPrice_OnChange);
            }
        }

        protected virtual void TaxPrice_OnChange(Money money) {
            if (money != null) {
                _basketTaxPriceDecimal = money.Amount;
            } else {
                _basketTaxPriceDecimal = 0;
            }
        }

        //This should be the preferred method
        public virtual IBasketLine AddItem(string itemCode, int quantity) {
            throw new Exception("The method or operation is not implemented.");
        }

        //I don't like this. The item code version is much better.
        public virtual IBasketLine AddItem(Product product, System.Collections.Generic.IList<IAttributeSelection> optionList, int quantity) {

            BasketItem item = new BasketItem();
            item.Basket = this;
            item.Quantity = quantity;
            item.Product = product;

            if (optionList != null) {

                for (int i = 0; i < optionList.Count; i++) {

                    BasketItemAttribute attr = new BasketItemAttribute();
                    
                    attr.AttributeID = optionList[i].AttributeID;
                    attr.OptionValue = optionList[i].OptionValue;

                    item.OptionList.Add(attr);
                }
            }

            BasketItemList.Add(item);

            return item;
        }

        public virtual IBasketLine AddItem(Product product, int quantity) {
            return AddItem(product, quantity, BasketItemType.StandardItem);
        }

        public virtual IBasketLine AddItem(Product product, int quantity, BasketItemType itemType) {
            
            BasketItem item = new BasketItem();

            item.Basket = this;
            item.Product = product;
            item.Quantity = quantity;
            item.ItemType = itemType;

            BasketItemList.Add(item);

            return item;
        }

        public virtual IBasketLine AddItem(BasketItemType itemType, string description, decimal price) {

            BasketItem item = new BasketItem();

            item.Basket = this;
            item.Quantity = 1;
            item.ItemType = itemType;
            item.Status = PricingStatus.OK;
            Money m = new Money(price);
            item.UnitLinePrice = m;
            
            BasketItemList.Add(item);

            return item;
        }

        public virtual IBasketLine UpdateItem(BasketItem item, string description, decimal price) {

            BasketItemList.Remove(item);
            item.Description = description;
            Money m = new Money(price);
            item.UnitLinePrice = item.LinePrice = item.TaxPrice = m;
            BasketItemList.Add(item);

            return item;
        }

        //This should be a preferred method
        public virtual IBasketLine AddItem(string itemCode, int quantity, BasketItemType itemType, string description, decimal unitPrice) {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void RemoveItem(string itemCode) {

            BasketItem[] clone = new BasketItem[BasketItemList.Count];
            BasketItemList.CopyTo(clone, 0);

            for (int i = 0; i < clone.Length; i++) {
                if (clone[i].ItemCode == itemCode) {
                    try {
                        BasketItemList.Remove(clone[i]);
                    } catch {}
                }
            }
        }

        public virtual void RemoveItem(IBasketLine item) {

            BasketItem bi = item as BasketItem;

            if (bi != null) {
                BasketItemList.Remove(bi);
            } else if (item != null) {
                RemoveItem(item.BasketItemID);
            }
        }

        public virtual void RemoveItem(long basketLineID) {
            BasketItem item = GetItem(basketLineID);
            if (item != null) {
                BasketItemList.Remove(item);
            }
        }

        protected virtual BasketItem GetItem(long basketLineID) {
            for (int i = 0; i < BasketItemList.Count; i++) {
                BasketItem bi = BasketItemList[i] as BasketItem;
                if (bi != null && bi.BasketItemID == basketLineID) {
                    return bi;
                }
            }
            return null;
        }

        //This should be the preferred access method
        public virtual void AmendItemQuantity(string itemCode, int newQuantity) {
            throw new Exception("The method or operation is not implemented.");
        }
       #endregion
    }
}