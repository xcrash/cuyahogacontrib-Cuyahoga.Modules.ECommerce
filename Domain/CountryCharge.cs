using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Domain {
    public class CountryCharge {
        private int _chargeID;
        private decimal _price;
        private string _countryCode;
        private Charge _charge;

        public CountryCharge() {
            _chargeID = 0;
        }

        public virtual string CountryCode {
            get {
                return _countryCode;
            }

            set {
                _countryCode = value;
            }
        }

        public virtual int ChargeID {
            get {
                return _chargeID;
            }
            set {
                _chargeID = value;
            }
        }

        public virtual decimal Price {
            get {
                return _price;
            }

            set {
                _price = value;
            }
        }

        public Charge ChargeDetails {
            get {
                return _charge;
            }

            set {
                _charge = value;

            }
        }

        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj) {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            CountryCharge castObj = (CountryCharge)obj;
            return (castObj != null) &&
                (this._chargeID == castObj.ChargeID) &&
                (this._countryCode == castObj.CountryCode);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _chargeID.GetHashCode();
            hash = 27 * hash * _countryCode.GetHashCode();
            return hash;
        }
        #endregion

    
    }
}
