/*

insert license info here

*/

using System;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Domain {

    /// <summary>
    ///	Generated by MyGeneration using the Serdar's NHibernate Object Mapping 1.1 template (based on Gustavo's) - serdar@argelab.net
    /// </summary>
    [Serializable]
    public class BasketItemAttribute : IAttributeSelection {

        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private long _basketitemid;
        private int _attributeid;
        private string _optionvalue;
        private long _optionprice;
        private DateTime _inserttimestamp;
        private DateTime _updatetimestamp;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public BasketItemAttribute() {
            _basketitemid = 0;
            _attributeid = 0;
            _optionvalue = null;
            _optionprice = 0;
            _inserttimestamp = DateTime.Now;
            _updatetimestamp = DateTime.Now;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Internal Accessors for NHibernate

        /// <summary>
        /// 
        /// </summary>
        internal virtual long _Basketitemid {
            get { return _basketitemid; }
            set { _basketitemid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual int _Attributeid {
            get { return _attributeid; }
            set { _attributeid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual string _OptionValue {
            get { return _optionvalue; }
            set { _optionvalue = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual long _OptionPrice {
            get { return _optionprice; }
            set { _optionprice = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual DateTime _Inserttimestamp {
            get { return _inserttimestamp; }
            set { _inserttimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual DateTime _Updatetimestamp {
            get { return _updatetimestamp; }
            set { _updatetimestamp = value; }
        }

        #endregion // Internal Accessors for NHibernate

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>		
        public virtual long BasketItemID {
            get { return _basketitemid; }
            set { _isChanged |= (_basketitemid != value); _basketitemid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual int AttributeID {
            get { return _attributeid; }
            set { _isChanged |= (_attributeid != value); _attributeid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string OptionValue {
            get { return _optionvalue; }
            set {
                if (value != null)
                    if (value.Length > 10)
                        throw new ArgumentOutOfRangeException("Invalid value for OptionValue", value, value.ToString());

                _isChanged |= (_optionvalue != value); _optionvalue = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual long OptionPrice {
            get { return _optionprice; }
            set { _isChanged |= (_optionprice != value); _optionprice = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime CreatedDate {
            get { return _inserttimestamp; }
            set { _isChanged |= (_inserttimestamp != value); _inserttimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime ModifiedDate {
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
            BasketItemAttribute castObj = (BasketItemAttribute)obj;
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
    }
}