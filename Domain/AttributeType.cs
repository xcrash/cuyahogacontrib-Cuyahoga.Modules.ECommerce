/*

insert license info here

*/

using System;

namespace Cuyahoga.Modules.ECommerce.Domain {
    /// <summary>
    ///	Generated by MyGeneration using the Serdar's NHibernate Object Mapping 1.1 template (based on Gustavo's) - serdar@argelab.net
    /// </summary>
    [Serializable]
    public class AttributeType {
        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private int _typeid;
        private string _name;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public AttributeType() {
            _typeid = 0;
            _name = null;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Internal Accessors for NHibernate

        /// <summary>
        /// 
        /// </summary>
        internal virtual int _TypeID {
            get { return _typeid; }
            set { _typeid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual string _Name {
            get { return _name; }
            set { _name = value; }
        }

        #endregion // Internal Accessors for NHibernate

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>		
        public virtual int TypeID {
            get { return _typeid; }
            set { _isChanged |= (_typeid != value); _typeid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string Name {
            get { return _name; }
            set {
                if (value != null)
                    if (value.Length > 50)
                        throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());

                _isChanged |= (_name != value); _name = value;
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
            AttributeType castObj = (AttributeType)obj;
            return (castObj != null) &&
                (this._TypeID == castObj.TypeID);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _typeid.GetHashCode();
            return hash;
        }
        #endregion

    }
}
