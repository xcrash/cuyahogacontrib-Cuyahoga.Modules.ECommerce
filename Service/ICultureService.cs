using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Cuyahoga.Modules.ECommerce.Service {
    public interface ICultureService {
        IList GetAllCountries();
        IList GetStateByCountry(string countryCode);
    }
}
