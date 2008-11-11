using System;
using Cuyahoga.Modules.ECommerce.Util;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    [Serializable]
    public class PriceBreak {

        private Money _listPrice;
        private int _quantity;

        public int Quantity {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public Money UnitListPrice {
            get { return _listPrice; }
            set { _listPrice = value; }
        }
    }
}
