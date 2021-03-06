using System;

namespace Cuyahoga.Modules.ECommerce.Domain {

    /// <summary>
    ///	Generated by MyGeneration using the Serdar's NHibernate Object Mapping 1.1 template (based on Gustavo's) - serdar@argelab.net
    /// </summary>
    [Serializable]
    public class AttributeOptionValue {

        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private Attribute _attributeid;
        private long _optionid;
        private string _optionname;
        private string _optionData;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public AttributeOptionValue() {
            _attributeid = null;
            _optionid = 0;
            _optionname = null;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Internal Accessors for NHibernate

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual Attribute _Attributeid {
            get { return _attributeid; }
            set { _attributeid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual long _Optionid {
            get { return _optionid; }
            set { _optionid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual string _OptionName {
            get { return _optionname; }
            set { _optionname = value; }
        }

        protected internal virtual string _OptionData {
            get { return _optionData; }
            set { _optionData = value; }
        }

        #endregion // Internal Accessors for NHibernate

        #region Public Properties

        public virtual string OptionData {
            get { return _optionData; }
            set { _optionData = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual Attribute Attribute {
            get { return _attributeid; }
            set { _isChanged |= (_attributeid != value); _attributeid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual long OptionID {
            get { return _optionid; }
            set { _isChanged |= (_optionid != value); _optionid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string OptionName {
            get { return _optionname; }
            set {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for OptionName", value, value.ToString());

                _isChanged |= (_optionname != value); _optionname = value;
            }
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
            AttributeOptionValue castObj = (AttributeOptionValue)obj;
            return (castObj != null) &&
                (this._optionid == castObj.OptionID);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _optionid.GetHashCode();
            return hash;
        }
        #endregion
    }
}