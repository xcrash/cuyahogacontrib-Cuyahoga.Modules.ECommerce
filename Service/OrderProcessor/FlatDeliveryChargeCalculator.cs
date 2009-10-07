using System.Collections.Generic;
using System.Collections;
using Igentics.Common.ECommerce;
using Igentics.Common.ECommerce.DataTransferObjects;
using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
using Cuyahoga.Modules.ECommerce.Util;
namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

    public class FlatDeliveryChargeCalculator : IOrderProcessor {

        private Money _charge;

        public FlatDeliveryChargeCalculator(string currencyCode, decimal chargeAmount) {
            _charge = new Money(currencyCode, chargeAmount);
        }

        #region IOrderProcessor Members

        public ProcessStatusMessage Process(Basket order) {
            order.DeliveryCharge = new Money(_charge);
            return new ProcessStatusMessage(ProcessStatus.Success);
        }

        #endregion
    }
}
