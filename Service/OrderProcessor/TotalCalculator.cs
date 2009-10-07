using System.Collections.Generic;
using System.Collections;
using Igentics.Common.ECommerce;
using Igentics.Common.ECommerce.DataTransferObjects;
using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

    /// <summary>
    /// Determines the pre-tax price for this basket
    /// </summary>
    public class TotalCalculator : IOrderProcessor {

        public TotalCalculator() {
        }

        public ProcessStatusMessage Process(IBasket basket) {

            basket.GrandTotal = new Money(basket.SubTotal);
            basket.GrandTotal.Add(basket.TaxPrice);

            return new ProcessStatusMessage(ProcessStatus.Success);
        }
    }
}
