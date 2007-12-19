using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Domain;
namespace Cuyahoga.Modules.ECommerce.Service {
    public interface IAccountService {

        //Are you mad??
        List<Domain.UserDetail> GetAllAccounts(int storeID, string cultureCode);

        UserDetail GetAccount(long accountID);
        bool SaveAccountDetails(UserDetail ud);

        //What is this doing here??
        List<Country> GetCountries();
    }
}
