using System;
using System.Collections.Generic;
using Igentics.Common.ECommerce.DataTransferObjects;
using Igentics.Common.ECommerce.Interfaces;

namespace Igentics.Common.ECommerce.Messages {

    public enum BasketAction {
        Add,
        Alter,
        Remove
    }

    [Serializable]
    public class BasketRequest {

        string _currencyCode;
        List<BasketItemChange> _actionList = new List<BasketItemChange>();

        public string CurrencyCode {
            get { return _currencyCode; }
            set { _currencyCode = value; }
        }

        public List<BasketItemChange> ActionList {
            get { return _actionList; }
            set { _actionList = value; }
        }
    }

    public class BasketItemChange : IProductSpecifier {

        private BasketAction _action;
        private string _basketItemID;
        private string _itemCode;
        private int _quantity = 1;

        private List<AttributeOptionChoice> _optionList;

        public BasketItemChange() {
            _action = BasketAction.Add;
        }

        public BasketItemChange(BasketAction action, string basketItemID) {
            Action = action;
            BasketItemID = basketItemID;
        }

        public BasketItemChange(BasketAction action, string basketItemID, string itemCode) : this(action, basketItemID) {
            ItemCode = itemCode;
        }

        public BasketItemChange(BasketAction action, string basketItemID, string itemCode, int quantity)
            : this(action, basketItemID, itemCode) {
            Quantity = quantity;
        }

        public BasketItemChange(BasketAction action, string basketItemID, string itemCode, int quantity, List<AttributeOptionChoice> optionList)
            : this(action, basketItemID, itemCode, quantity) {
            OptionList = optionList;
        }

        public BasketAction Action {
            get { return _action; }
            set { _action = value; }
        }

        public string BasketItemID {
            get { return _basketItemID; }
            set { _basketItemID = value; }
        }

        #region IProductSpecifier Members

        public string ItemCode {
            get {
                return _itemCode;
            }
            set {
                _itemCode = value;
            }
        }

        public int Quantity {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public List<AttributeOptionChoice> OptionList {
            get {
                return _optionList;
            }
            set {
                _optionList = value;
            }
        }

        #endregion
    }
}
