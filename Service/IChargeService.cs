using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections;

using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce.Domain;
namespace Cuyahoga.Modules.ECommerce.Service {

    public interface IChargeService {
        decimal GetMinimumDeliveryCharge(string countryCode);
    }
}
