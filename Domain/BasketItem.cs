/*

insert license info here

*/

using System;
using System.Collections;

using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Enums;

namespace Cuyahoga.Modules.ECommerce.Domain {

    /// <summary>
    ///	Generated by MyGeneration using the Serdar's NHibernate Object Mapping 1.1 template (based on Gustavo's) - serdar@argelab.net
    /// </summary>
    [Serializable]
    public class BasketItem : IBasketLine {

        #region Private Members
        private long _basketitemid;
        private Basket _basket;
        private Product _product;
        private int _quantity;
        private short _itemtypeid;
        private short _pricingstatusid;
        private DateTime _inserttimestamp;
        private DateTime _updatetimestamp;
        private IList _optionList;
        private long _basketID;

        private decimal _linePreTaxDecimal;
        private Money _linePreTaxMoney = null;

        private decimal _lineTaxDecimal;
        private Money _lineTaxMoney = null;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public BasketItem() {

            _basketitemid = 0;
            _basket = null;
            _product = null;
            _lineTaxDecimal = 0;
            _linePreTaxDecimal = 0;
            _quantity = 0;
            _itemtypeid = 0;
            _pricingstatusid = 0;
            _inserttimestamp = DateTime.Now;
            _updatetimestamp = DateTime.Now;

            _optionList = new ArrayList();
            Status = PricingStatus.NotChecked;
            ItemType = BasketItemType.StandardItem;

            _linePreTaxMoney = null;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        /// <summary>
        /// 
        /// </summary>
        public virtual Basket Basket {
            get { return _basket; }
            set { _basket = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Product Product {
            get { return _product; }
            set { _product = value; }
        }

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>		
        public virtual long BasketItemID {
            get { return _basketitemid; }
            set { _basketitemid = value; }
        }

        public virtual long BasketID {
            get { return _basketID; }
            set { _basketID = value; }
        }

        public virtual IList OptionList {
            get { return _optionList; }
            set { _optionList = value; }
        }

        /*
        private int _basketID;
        public virtual int BasketID {
            get { return _basketid; }
            set { _basketid = value; }
        }
         */

        /// <summary>
        /// 
        /// </summary>		
        private decimal ItemTaxDecimal {
            get { return _lineTaxDecimal; }
            set { _lineTaxDecimal = value; }
        }

        public virtual Money TaxPrice {
            get {
                if (_lineTaxMoney == null) {
                    _lineTaxMoney = new Money(Basket.CurrencyCode, _lineTaxDecimal);
                    _lineTaxMoney.OnChange += new Money.OnChangeHandler(LineTax_OnChange);
                }
                return _lineTaxMoney;
            }
            set {
                _lineTaxMoney = value;
                _lineTaxMoney.OnChange += new Money.OnChangeHandler(LineTax_OnChange);
                _lineTaxDecimal = value.Amount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private decimal LinePriceDecimal {
            get { return _linePreTaxDecimal; }
            set { _linePreTaxDecimal = value; }
        }

        public virtual Money LinePrice {
            get {
                if (_linePreTaxMoney == null) {
                    _linePreTaxMoney = new Money(Basket.CurrencyCode, _linePreTaxDecimal);
                    _linePreTaxMoney.OnChange += new Money.OnChangeHandler(LinePreTax_OnChange);
                }
                return _linePreTaxMoney;
            }
            set {
                _linePreTaxMoney = value;
                _linePreTaxMoney.OnChange += new Money.OnChangeHandler(LinePreTax_OnChange);
                _linePreTaxDecimal = value.Amount;
            }
        }

        public virtual  Money UnitLinePrice {
            get {
                if (LinePrice != null && Quantity > 0) {
                    return new Money(LinePrice, LinePrice.Amount / Quantity);
                }
                return null;
            }
            set {
                LinePrice = new Money(value, value.Amount * Quantity);
            }
        }

        private void LinePreTax_OnChange(Money money) {
            _linePreTaxDecimal = money.Amount;
        }

        private void LineTax_OnChange(Money money) {
            _lineTaxDecimal = money.Amount;
        }

        public virtual BasketItemType ItemType {
            get {
                return (BasketItemType)ItemTypeID;
            }
            set {
                ItemTypeID = (Int16)value;
            }
        }

        public virtual bool IsChargeItem {
            get {
                return (ItemType != BasketItemType.StandardItem
                    && ItemType != BasketItemType.Discount
                    && ItemType != BasketItemType.OtherCharge
                    );
            }
        }

        public virtual bool RequiresPricing {
            get {
                return ItemType == BasketItemType.StandardItem
                    && (Status == PricingStatus.BackOfficeDown || Status == PricingStatus.NotChecked);
            }
        }

        public virtual PricingStatus Status {
            get {
                return ((PricingStatus)PricingStatusID);
            }
            set {
                PricingStatusID = (short)value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual int Quantity {
            get { return _quantity; }
            set { _quantity = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual short ItemTypeID {
            get { return _itemtypeid; }
            set { _itemtypeid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual short PricingStatusID {
            get { return _pricingstatusid; }
            set { _pricingstatusid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime CreatedDate {
            get { return _inserttimestamp; }
            set { _inserttimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime ModifiedDate {
            get { return _updatetimestamp; }
            set { _updatetimestamp = value; }
        }
        #endregion

        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj) {
            if (this == obj) return true;
            if (obj == null) return false;
            BasketItem castObj = (BasketItem)obj;
            return (castObj != null) &&
                (this._basketitemid == castObj.BasketItemID);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _basketitemid.GetHashCode();
            return hash;
        }
        #endregion

        #region IItemDetails Members

        public virtual string ItemCode {
            get {
                if (this._product != null) {
                    return _product.ItemCode;
                }
                return "";
            }
            set {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public virtual string Description {
            get {
                if (this._product != null) {
                    return _product.ShortProductDescription;
                }
                return "";
            }
            set {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion
    }
}