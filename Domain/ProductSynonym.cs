/*

insert license info here

*/

using System;

namespace Cuyahoga.Modules.ECommerce.Domain {
    /// <summary>
    ///	Generated by MyGeneration using the Serdar's NHibernate Object Mapping 1.1 template (based on Gustavo's) - serdar@argelab.net
    /// </summary>
    [Serializable]
    public class ProductSynonym {
        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private string _alternativephrase;
        private long _productid;
        private DateTime _inserttimestamp;
        private DateTime _updatetimestamp;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public ProductSynonym() {
            _alternativephrase = null;
            _productid = 0;
            _inserttimestamp = DateTime.Now;
            _updatetimestamp = DateTime.Now;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Internal Accessors for NHibernate

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual string _AlternativePhrase {
            get { return _alternativephrase; }
            set { _alternativephrase = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual long _Productid {
            get { return _productid; }
            set { _productid = value; }
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
        public virtual string AlternativePhrase {
            get { return _alternativephrase; }
            set {
                if (value != null)
                    if (value.Length > 128)
                        throw new ArgumentOutOfRangeException("Invalid value for alternative phrase", value, value.ToString());

                _isChanged |= (_alternativephrase != value); _alternativephrase = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual long ProductID {
            get { return _productid; }
            set { _isChanged |= (_productid != value); _productid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime Inserttimestamp {
            get { return _inserttimestamp; }
            set { _isChanged |= (_inserttimestamp != value); _inserttimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime Updatetimestamp {
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
            ProductSynonym castObj = (ProductSynonym)obj;
            return (castObj != null) &&
                (this._alternativephrase == castObj._alternativephrase) &&
                (this._productid == castObj.ProductID);

        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _alternativephrase.GetHashCode();
            return hash;
        }
        #endregion

    }
}
