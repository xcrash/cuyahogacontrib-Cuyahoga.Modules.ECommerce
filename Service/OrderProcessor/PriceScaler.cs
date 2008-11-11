using System.Collections.Generic;
using System.Collections;
using Igentics.Common.ECommerce;
using Igentics.Common.ECommerce.DataTransferObjects;
using Igentics.Soa.Commerce.Core.Util.Enums;

namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

	/// <summary>
	/// Takes lines with the same item codes and reduces them to single lines
	/// </summary>
	public class PriceScaler : IOrderProcessor {

        private decimal _scalingFactor;

        public PriceScaler(decimal scalingFactor) {
            _scalingFactor = scalingFactor;
		}

		public ProcessStatusMessage Process(Basket basket) {

            foreach (BasketItem line in basket.BasketItemList) {
                if (line.PricingStatus == PricingStatus.OK) {
                    line.UnitPrice.Multiply(_scalingFactor);
                    line.LinePrice = new Money(line.UnitPrice);
                    line.LinePrice.Multiply(line.Quantity);
                }
            }

            return new ProcessStatusMessage(ProcessStatus.Success);
		}
	}
}