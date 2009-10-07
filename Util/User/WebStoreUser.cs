using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util.Location;

namespace Cuyahoga.Modules.ECommerce.Util {
    public class WebStoreUser : IWebStoreUser {

        private IUserDetails userDetails;
        private IAddress addressDetails;
        private string _accountID = "";

        #region IWebStoreUser Members

        public IUserDetails UserDetails {
            get {
                return userDetails;
            }
            set {
                userDetails = value;
            }
        }

        public IAddress UserAddress {
            get {
                return addressDetails;
            }
            set {
                addressDetails = value;
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

        #endregion
    }
}
