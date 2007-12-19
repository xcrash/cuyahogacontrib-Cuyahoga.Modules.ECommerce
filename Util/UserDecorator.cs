using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Core.Domain;

namespace Cuyahoga.Modules.ECommerce.Util {

    public class UserDecorator : IUserDetails {

        private User _user;

        public UserDecorator(User user) {
            _user = user;
        }

        #region IUserDetails Members

        public string FirstName {
            get {
                return _user.FirstName;
            }
            set {
                _user.FirstName = value;
            }
        }

        public string LastName {
            get {
                return _user.LastName;
            }
            set {
                _user.LastName = value;
            }
        }

        public string EmailAddress {
            get {
                return _user.Email;
            }
            set {
                _user.Email = value;
            }
        }

        public string TelephoneNumber {
            get {
                return "";
            }
            set {
            }
        }

        public long UserID {
            get {
                return _user.Id;
            }
            set {
                _user.Id = (int) value;
            }
        }

        #endregion
    }
}
