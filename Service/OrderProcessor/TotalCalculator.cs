using System.Collections.Generic;
using System.Collections;
using Igentics.Common.ECommerce;
using Igentics.Common.ECommerce.DataTransferObjects;
using Igentics.Soa.Commerce.Core.Util.Enums;

namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

    /// <summary>
    /// Determines the pre-tax price for this basket
    /// </summary>
    public class TotalCalculator : IOrderProcessor {

        public TotalCalculator() {
        }

        public ProcessStatusMessage Process(Basket basket) {

            basket.GrandTotal = new Money(basket.SubTotal);
            basket.GrandTotal.Add(basket.TaxPrice);

            return new ProcessStatusMessage(ProcessStatus.Success);
        }
    }
}
