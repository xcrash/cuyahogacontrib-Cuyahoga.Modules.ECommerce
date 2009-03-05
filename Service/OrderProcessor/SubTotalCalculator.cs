using System.Collections.Generic;
using System.Collections;
using Igentics.Common.ECommerce;
using Igentics.Common.ECommerce.DataTransferObjects;
using Igentics.Soa.Commerce.Core.Util.Enums;

namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

    /// <summary>
    /// Determines the pre-tax price for this basket
    /// </summary>
    public class SubTotalCalculator : IOrderProcessor {

        public SubTotalCalculator() {
        }

        public ProcessStatusMessage Process(Basket basket) {

            basket.SubTotal = new Money(basket.CurrencyCode, 0M);

            foreach (BasketItem line in basket.BasketItemList) {
                if (line.LinePrice != null) {
                    basket.SubTotal.Add(line.LinePrice);
                }
            }

            return new ProcessStatusMessage(ProcessStatus.Success);
        }
    }
}
