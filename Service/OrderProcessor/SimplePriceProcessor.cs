using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {
    
    public class SimplePriceProcessor : IOrderProcessor {

        #region IOrderProcessor Members

        public void Process(IBasket order) {

            foreach (IBasketLine line in order.BasketItemList) {

                BasketItem item = line as BasketItem;

                if (item != null && item.Status != PricingStatus.OK) {

                    decimal unitPrice = item.Product.BasePrice;

                    if (item.OptionList != null) {
                        foreach (BasketItemAttribute attribute in item.OptionList) {
                            unitPrice += attribute.OptionPrice;
                        }
                    }

                    item.UnitLinePrice = new Money(order.CurrencyCode, unitPrice);
                    item.Status = PricingStatus.OK;
                }
            }
        }

        #endregion
    }
}