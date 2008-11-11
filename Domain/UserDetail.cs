using System;
using System.Collections;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util.Enums;

namespace Cuyahoga.Modules.ECommerce.Domain {

    [Serializable]
    public class UserDetail : IUserDetails {

        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private long _userid;

        private string _firstName;
        private string _lastName;
        private string _emailAddress;
        private string _telephoneNumber;
        private string _faxNumber;

        private string _companyName;
        private string _accountNumber;

        private Int16 _accounttypeid;
        private Address _addressid;
        private DateTime _inserttimestamp;
        private DateTime _updatetimestamp;
        private IList _address;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public UserDetail() {
            _userid = 0;
            _accounttypeid = (Int16) AccountType.CreditCard;
            _addressid = null;
            _inserttimestamp = DateTime.Now;
            _updatetimestamp = DateTime.Now;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Internal Accessors for NHibernate

        /// <summary>
        /// 
        /// </summary>
        internal virtual long _Userid {
            get { return _userid; }
            set { _userid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual Int16 _AccountTypeID {
            get { return _accounttypeid; }
            set { _accounttypeid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual Address _Addressid {
            get { return _addressid; }
            set { _addressid = value; }
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

        public virtual IList AddressList {
            get { return _address; }
            set { _address = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual long UserID {
            get { return _userid; }
            set { _isChanged |= (_userid != value); _userid = value; }
        }

        public virtual string FirstName {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public virtual string LastName {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public virtual string EmailAddress {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        public virtual string TelephoneNumber {
            get { return _telephoneNumber; }
            set { _telephoneNumber = value; }
        }

        public virtual string FaxNumber {
            get { return _faxNumber; }
            set { _faxNumber = value; }
        }

        public virtual string CompanyName {
            get { return _companyName; }
            set { _companyName = value; }
        }
        
        public virtual string AccountNumber {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual AccountType AccountType {
            get { return (AccountType) _accounttypeid; }
            set { _isChanged |= (_accounttypeid != (Int16) value); _accounttypeid = (Int16) value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual Address Address {
            get { return _addressid; }
            set { _isChanged |= (_addressid != value); _addressid = value; }
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
            UserDetail castObj = (UserDetail)obj;
            return (castObj != null) &&
                (this._userid == castObj.UserID);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _userid.GetHashCode();
            return hash;
        }
        #endregion
    }
}