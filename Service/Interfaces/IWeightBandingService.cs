using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Cuyahoga.Modules.ECommerce.Domain;

namespace Cuyahoga.Modules.ECommerce.Service {
    public interface IWeightBandingService {
        IList GetCountryWeightBandings(string country);
        IList GetStateWeightBandings(string country);
        CountryDeliveryWeight GetCountryDeliveryWeight(String country, decimal weight);
        void Save(CountryDeliveryWeight cdw);
        void Delete(CountryDeliveryWeight cdw);
    }
}
