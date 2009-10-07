using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Cuyahoga.Modules.ECommerce.Domain;
namespace Cuyahoga.Modules.ECommerce.Service {
    public interface IDeliveryService : IWeightBandingService {
        IList GetAllDeliveryMethods();
        DeliveryType GetDeliveryType(string name);
        void Save(DeliveryType deliveryType);
    }
}
