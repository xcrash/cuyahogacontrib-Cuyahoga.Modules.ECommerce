using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using Cuyahoga.Modules.ECommerce.Util;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    [Serializable]
    public class Basket {

        private string _basketID;
        private string _storeID;
        private string _cultureCode;
        private string _currencyCode;
        private OrderHeader _orderHeader;

        private List<BasketItem> _basketItemList = new List<BasketItem>();
        private List<CustomField> _customFields;
        private Money _subtotal;
        private Money _taxAmount;
        private Money _grandTotal;
        private Money _deliveryCharge;

        [XmlAttribute("id")]
        public string BasketID {
            get { return _basketID; }
            set { _basketID = value; }
        }

        public string StoreID {
            get { return _storeID; }
            set { _storeID = value; }
        }

        public string CultureCode {
            get { return _cultureCode; }
            set { _cultureCode = value; }
        }

        public string CurrencyCode {
            get { return _currencyCode; }
            set { _currencyCode = value; }
        }

        public Money SubTotal {
            get { return _subtotal; }
            set { _subtotal = value; }
        }

        public Money DeliveryCharge {
            get { return _deliveryCharge; }
            set { _deliveryCharge = value; }
        }

        public Money TaxPrice {
            get { return _taxAmount; }
            set { _taxAmount = value; }
        }

        public Money GrandTotal {
            get { return _grandTotal; }
            set { _grandTotal = value; }
        }

        public OrderHeader Header {
            get { return _orderHeader; }
            set { _orderHeader = value; }
        }

        [XmlArrayItem(typeof(BasketItem))]
        public List<BasketItem> BasketItemList {
            get { return _basketItemList; }
            set { _basketItemList = value; }
        }

        [XmlArrayItem(typeof(CustomField))]
        public List<CustomField> CustomFieldList {
            get { return _customFields; }
            set { _customFields = value; }
        }
    }
}
