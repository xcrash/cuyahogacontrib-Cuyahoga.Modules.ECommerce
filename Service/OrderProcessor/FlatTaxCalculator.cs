using System.Collections.Generic;
using System.Collections;
using Igentics.Common.ECommerce;
using Igentics.Common.ECommerce.DataTransferObjects;
using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;

namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

    public class FlatTaxCalculator : IOrderProcessor {

        private decimal _taxRate;

        public FlatTaxCalculator(decimal taxRate) {
            _taxRate = taxRate;
        }

        #region IOrderProcessor Members

        public ProcessStatusMessage Process(Basket order) {

            order.TaxPrice = new Money(order.CurrencyCode, 0M);

            foreach (BasketItem line in order.BasketItemList) {
                if (line.LinePrice != null) {
                    Money lineTax = new Money(line.LinePrice);
                    lineTax.Multiply(_taxRate);
                    order.TaxPrice.Add(lineTax);
                }
            }

            if (order.DeliveryCharge != null && order.DeliveryCharge.Amount > 0) {
                Money delTax = new Money(order.DeliveryCharge);
                delTax.Multiply(_taxRate);
                order.TaxPrice.Add(delTax);
            }

            return new ProcessStatusMessage(ProcessStatus.Success);
        }

        #endregion
    }
}
