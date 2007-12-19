using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {
    class MinimumDeliveryChargeOrderProcessor : IOrderProcessor {

        private decimal _minimumDeliveryCharge;
        private bool _hasDeliveryAttached = false;
        public MinimumDeliveryChargeOrderProcessor(decimal charge) {
            _minimumDeliveryCharge = charge;
        }

        #region IOrderProcessor Members

        void IOrderProcessor.Process(IBasket order) {
            foreach (IBasketLine line in order.BasketItemList) {

                BasketItem item = line as BasketItem;

                if (item != null && item.ItemType == Cuyahoga.Modules.ECommerce.Util.Enums.BasketItemType.DeliveryCharge) {
                    if (item.LinePrice.Amount < _minimumDeliveryCharge) {
                        order.UpdateItem(item, "Minimum Delivery Charge", _minimumDeliveryCharge);
                    }

                    _hasDeliveryAttached = true;
                }
            }

            if (!_hasDeliveryAttached) {
                order.AddItem(Cuyahoga.Modules.ECommerce.Util.Enums.BasketItemType.DeliveryCharge, "Minimum Delivery Charge", _minimumDeliveryCharge);
            }
        }

        #endregion
    }
}
