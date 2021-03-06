using System;

using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;
using PaymentMethodTypeEnum = Cuyahoga.Modules.ECommerce.Util.Enums.PaymentMethodType;

namespace Cuyahoga.Modules.ECommerce.Domain {

    /// <summary>
    ///	Generated by MyGeneration using the Serdar's NHibernate Object Mapping 1.1 template (based on Gustavo's) - serdar@argelab.net
    /// </summary>
    [Serializable]
    public class Payment : IPaymentRecord {

        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private long _paymentid;
        private Basket _basketid;
        private short _paymenttypeid;
        private short _paymentstatusid;
        private string _currencycode;

        private string _transactionReference;
        private short _paymentProviderID;
        
        private decimal _amountDecimal;
        private Money _amountMoney = null;

        private DateTime _inserttimestamp;
        private DateTime _updatetimestamp;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public Payment() {
            _paymentid = 0;
            _basketid = null;
            _paymenttypeid = (short) PaymentMethodTypeEnum.NotSet;
            _paymentstatusid = 0;
            _currencycode = null;
            _amountDecimal = 0;
            _inserttimestamp = DateTime.Now;
            _updatetimestamp = DateTime.Now;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Internal Accessors for NHibernate

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual long _Paymentid {
            get { return _paymentid; }
            set { _paymentid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual Basket _Basketid {
            get { return _basketid; }
            set { _basketid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual short _PaymentTypeid {
            get { return _paymenttypeid; }
            set { _paymenttypeid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual short _PaymentStatusid {
            get { return _paymentstatusid; }
            set { _paymentstatusid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual decimal _Amount {
            get { return _amountDecimal; }
            set { _amountDecimal = value; _amountMoney = null; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual DateTime _Inserttimestamp {
            get { return _inserttimestamp; }
            set { _inserttimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual DateTime _Updatetimestamp {
            get { return _updatetimestamp; }
            set { _updatetimestamp = value; }
        }

        #endregion // Internal Accessors for NHibernate

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>		
        public virtual long PaymentID {
            get { return _paymentid; }
            set { _isChanged |= (_paymentid != value); _paymentid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual IBasket Basket {
            get { return _basketid; }
            set { _isChanged |= (_basketid != value); _basketid = value as Basket; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual PaymentMethodTypeEnum PaymentType {
            get { return (PaymentMethodTypeEnum) _paymenttypeid; }
            set { _isChanged |= (_paymenttypeid != (short) value); _paymenttypeid = (short) value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CurrencyCode {
            get { return _currencycode; }
            set { _isChanged |= (_currencycode != value); _currencycode = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime InsertTimestamp {
            get { return _inserttimestamp; }
            set { _isChanged |= (_inserttimestamp != value); _inserttimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime UpdateTimestamp {
            get { return _updatetimestamp; }
            set { _isChanged |= (_updatetimestamp != value); _updatetimestamp = value; }
        }

        /// <summary>
        /// Returns whether or not the object has changed it's values.
        /// </summary>
        public virtual bool IsChanged {
            get { return _isChanged; }
        }

        /// <summary>
        /// Returns whether or not the object has changed it's values.
        /// </summary>
        public virtual bool IsDeleted {
            get { return _isDeleted; }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// mark the item as deleted
        /// </summary>
        public virtual void MarkAsDeleted() {
            _isDeleted = true;
            _isChanged = true;
        }


        #endregion

        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj) {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            Payment castObj = (Payment)obj;
            return (castObj != null) &&
                (this._paymentid == castObj.PaymentID);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _paymentid.GetHashCode();
            return hash;
        }
        #endregion

        #region IPaymentRecord Members

        public virtual PaymentMethodTypeEnum PaymentMethod {
            get {
                return (PaymentMethodTypeEnum)_paymenttypeid;
            }
            set {
                _paymenttypeid = (short)value;
            }
        }

        public virtual Money PaymentAmount {
            get {
                if (_amountMoney == null) {
                    _amountMoney = new Money(CurrencyCode, _amountDecimal);
                    _amountMoney.OnChange += new Money.OnChangeHandler(Amount_OnChange);
                }
                return _amountMoney;
            }
            set {
                _amountMoney = value;
                _currencycode = value.CurrencyCode;
                _amountDecimal = value.Amount;
                _amountMoney.OnChange += new Money.OnChangeHandler(Amount_OnChange);
            }
        }

        protected virtual void Amount_OnChange(Money money) {
            if (money != null) {
                _amountDecimal = money.Amount;
                _currencycode = money.CurrencyCode;
            } else {
                _amountDecimal = 0;
                _currencycode = "GBP"; //anything!
            }
        }

        public virtual string TransactionReference {
            get {
                return _transactionReference;
            }
            set {
                _transactionReference = value;
            }
        }

        public virtual PaymentStatus TransactionStatus {
            get {
                return (PaymentStatus)_PaymentStatusid;
            }
            set {
                _PaymentStatusid = (short)value;
            }
        }

        public virtual DateTime PaymentDate {
            get {
                return _inserttimestamp;
            }
            set {
                _inserttimestamp = value;
            }
        }

        public virtual short PaymentProviderID {
            get {
                return _paymentProviderID;
            }
            set {
                _paymentProviderID = value;
            }
        }

        #endregion
    }
}