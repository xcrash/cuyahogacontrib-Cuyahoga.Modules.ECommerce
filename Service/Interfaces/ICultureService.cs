using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Cuyahoga.Modules.ECommerce.Service {
    public interface ICultureService {
        IList GetAllCountries();
        IList GetStateByCountry(string countryCode);

        IList GetAllCurrenciesByMaketCode(string marketCode);
        IList GetEnabledCurrenciesByMaketCode(string marketCode);

        IList GetAllMarkets();
        IList GetMarketsByStatus();
        IList GetMarketByID(int marketID);
    }
}
