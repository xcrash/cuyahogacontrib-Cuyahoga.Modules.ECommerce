using System;
using System.Collections;
using System.Collections.Generic;

using Castle.Facilities.NHibernateIntegration;
using System.Data.SqlClient;
using Cuyahoga.Core;
using Cuyahoga.Modules.ECommerce.DataAccess;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Castle.Services.Transaction;
using NHibernate;
using Cuyahoga.Modules.ECommerce.Service;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service {
   public class CultureService : ICultureService, INHibernateModule {
       
        private ISessionManager _sessionManager;
        private IExtCommonDao _dao;

        public CultureService(ISessionManager sessionManager, IExtCommonDao dao) {
            _sessionManager = sessionManager;
            _dao = dao;
        }

        #region ICultureService Members

        public System.Collections.IList GetAllCountries() {
           return _dao.GetAll(typeof(Country));
        }

        public System.Collections.IList GetStateByCountry(string countryCode) {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList GetAllCurrenciesByMaketCode(string marketCode) {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList GetEnabledCurrenciesByMaketCode(string marketCode) {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList GetAllMarkets() {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList GetMarketsByStatus() {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList GetMarketByID(int marketID) {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
