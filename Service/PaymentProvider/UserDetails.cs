using System;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

    /// <summary>
    /// Wraps the data relevant to a user and not necessarily to do with a payment
    /// </summary>
    [Obsolete("Needs to be replaced by IUserDetails and IAddress, or at least implement them")]
    public class UserDetails {

        private string _userName = "";
        private string _accountID = "";
        
        private string _address1 = "";
        private string _address2 = "";
        private string _city = "";
        private string _state = "";
        private string _countryName = "";
        private string _postCode = "";

        private string _telephoneNo = "";
        private string _faxNo = "";
        private string _userEmail = "";

        public UserDetails() {
        }

        /// <summary>
        /// The user's full name
        /// </summary>
        public string UserName {
            get {
                return _userName;
            }
            set {
                _userName = value;
            }
        }

        public string AccountID {
            get {
                return _accountID;
            }
            set {
                _accountID = value;
            }
        }

        public string EmailAddress {
            get {
                return _userEmail;
            }
            set {
                _userEmail = value;
            }
        }

        public string Address1 {
            get {
                return _address1;
            }
            set {
                _address1 = value;
            }
        }

        public string Address2 {
            get {
                return _address2;
            }
            set {
                _address2 = value;
            }
        }

        public string City {
            get {
                return _city;
            }
            set {
                _city = value;
            }
        }

        public string State {
            get {
                return _state;
            }
            set {
                _state = value;
            }
        }

        public string CountryName {
            get {
                return _countryName;
            }
            set {
                _countryName = value;
            }
        }

        public string PostCode {
            get {
                return _postCode;
            }
            set {
                _postCode = value;
            }
        }
 
        public string TelephoneNumber {
            get {
                return _telephoneNo;
            }
            set {
                _telephoneNo = value;
            }
        }

        public string FaxNumber {
            get {
                return _faxNo;
            }
            set {
                _faxNo = value;
            }
        }
    }
}