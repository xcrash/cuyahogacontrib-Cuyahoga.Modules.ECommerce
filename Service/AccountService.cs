using System;
using System.Collections;
using System.Collections.Generic;

using Castle.Facilities.NHibernateIntegration;

using Cuyahoga.Core;
using Cuyahoga.Modules.ECommerce.DataAccess;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

using NHibernate;
using DbProduct = Cuyahoga.Modules.ECommerce.Domain.Product;


namespace Cuyahoga.Modules.ECommerce.Service {

    public class AccountService : IAccountService, INHibernateModule {

        private ISessionManager _sessionManager;
        private IExtCommonDao _dao;

        public AccountService(ISessionManager sessionManager, IExtCommonDao dao) {
            _sessionManager = sessionManager;
            _dao = dao;
        }

        #region IAccountService Members

        public List<UserDetail> GetAllAccounts(int storeID, string cultureCode) {

            List<UserDetail> accountList = new List<UserDetail>();


            IQuery query = this._sessionManager.OpenSession().CreateQuery("from UserDetail ");

            IList aList = query.List();

            foreach (UserDetail ud in aList) {
                accountList.Add(ud);
            }

            return accountList;
        }

        public bool SaveAccountDetails(UserDetail ud) {
            try {
                this._sessionManager.OpenSession().SaveOrUpdate(ud);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                return false;
            }
            return true;
        }

        public UserDetail GetAccount(long accountID) {

            UserDetail account = new UserDetail();

            IQuery query = this._sessionManager.OpenSession().CreateQuery("from UserDetail where userID = :id");
            query.SetInt64("id", accountID);
            IList aList = query.List();

            foreach (UserDetail ud in aList) {
                account = ud;
            }

            return account;
        }

        public List<Country> GetCountries() {

            List<Country> cList = new List<Country>();


            IQuery query = this._sessionManager.OpenSession().CreateQuery("from Country ");

            IList countryList = query.List();

            foreach (Country c in countryList) {
                cList.Add(c);
            }

            return cList;
        }

        #endregion
    }
}