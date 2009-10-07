using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Util;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    [Serializable]
    public class PriceAvailability {

        /*
         * Add promotion text?
         * Saving?
         * RRP?
         */
    
        private Money _listPrice;
        private bool _canPurchase;
        private string _availabilityText;
        private List<PriceBreak> _priceBreakList;

        //Is this really needed?
        [XmlArrayItem(typeof(PriceBreak))]
        public List<PriceBreak> PriceBreakList {
            get { return _priceBreakList; }
            set { _priceBreakList = value; }
        }

        public Money UnitListPrice {
            get { return _listPrice; }
            set { _listPrice = value; }
        }

        /// <summary>
        /// Used to indicate if the item can be purchased
        /// </summary>
        public bool CanPurchase {
            get { return _canPurchase; }
            set { _canPurchase = value; }
        }

        public string AvailabilityText {
            get { return _availabilityText; }
            set { _availabilityText = value; }
        }
    }
}
