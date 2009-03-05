using System;
using System.Collections.Generic;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {

    public class SimpleUkVatProcessor : IOrderProcessor {

        public const decimal VAT_RATE = 0.175M;

        #region IOrderProcessor Members

        public void Process(IBasket order) {

            order.TaxPrice = new Money(order.CurrencyCode, 0);

            foreach (IBasketLine line in order.BasketItemList) {

                line.TaxPrice.Amount = line.UnitLinePrice.Amount * VAT_RATE * line.Quantity;

                switch (line.ItemType) {
                    case BasketItemType.CreditNote:
                    case BasketItemType.Discount:
                    case BasketItemType.Voucher:
                    case BasketItemType.OtherCredit:
                        order.TaxPrice.Subtract(line.TaxPrice);
                        break;
                    default:
                        order.TaxPrice.Add(line.TaxPrice);
                        break;
                }
            }
        }

        #endregion
    }
}
