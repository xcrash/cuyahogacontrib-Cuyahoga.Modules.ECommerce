using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Domain {
    public class CountryDeliveryWeight {

        private decimal _price;
        private string _countryCode;
        private Decimal _weightLevel;

        public String CountryCode {
            get {
                return _countryCode;
            }

            set {
                _countryCode = value;
            }
        }

        public Decimal WeightLevel {
            get {
                return _weightLevel;
            }

            set {
                _weightLevel = value;
            }
        }

        public decimal Price {
            get {
                return _price;
            }

            set {
                _price = value;
            }
        }

        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj) {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            CountryDeliveryWeight castObj = (CountryDeliveryWeight)obj;
            return (castObj != null) &&
                (this._weightLevel == castObj.WeightLevel) &&
                (this._countryCode == castObj.CountryCode);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _weightLevel.GetHashCode();
            hash = 27 * hash * _countryCode.GetHashCode();
            return hash;
        }
        #endregion
    }
}
