using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Enums;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    [Serializable]
    [XmlInclude(typeof(AttributeOptionChoice))]
    public class BasketItem : IItemLine {

        private string _itemCode;
        private int _quantity;
        private string _description;
        private Money _unitPrice;
        private Money _linePrice;
        private string _basketLineID;
        private PricingStatus _pricingStatus = PricingStatus.NotChecked;

        private List<AttributeOptionChoice> _optionList;

        [XmlArrayItem(typeof(AttributeOptionChoice))]
        public List<AttributeOptionChoice> OptionList {
            get { return _optionList; }
            set { _optionList = value; }
        }

        [XmlAttribute("id")]
        public string BasketLineID {
            get { return _basketLineID; }
            set { _basketLineID = value; }
        }

        public PricingStatus PricingStatus {
            get { return _pricingStatus; }
            set { _pricingStatus = value; }
        }

        public Money UnitPrice {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        public Money LinePrice {
            get { return _linePrice; }
            set { _linePrice = value; }
        }

        public int Quantity {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public string Description {
            get { return _description; }
            set { _description = value; }
        }

        public string ItemCode {
            get { return _itemCode; }
            set { _itemCode = value; }
        }
    }
}
