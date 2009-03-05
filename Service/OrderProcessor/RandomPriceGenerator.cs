using System.Collections.Generic;
using System.Collections;
using Igentics.Common.ECommerce;
using Igentics.Common.ECommerce.DataTransferObjects;
using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

    public class RandomPriceGenerator : IOrderProcessor {

        private decimal _minPrice;
        private decimal _maxPrice;

        public RandomPriceGenerator(decimal minPrice, decimal maxPrice) {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
        }

        #region IOrderProcessor Members

        public ProcessStatusMessage Process(IBasket order) {

            foreach (BasketItem line in order.BasketItemList) {

                line.UnitPrice = new Money();
                line.UnitPrice.Amount = Igentics.Common.Util.RandomSingleton.Instance.Next((int) (100 * _minPrice), (int) (100 * _maxPrice));
                line.UnitPrice.Divide(100);

                line.LinePrice = new Money(line.UnitPrice);
                line.LinePrice.Multiply(line.Quantity);
                line.PricingStatus = PricingStatus.OK;
            }

            return new ProcessStatusMessage(ProcessStatus.Success);
        }

        #endregion
    }
}
