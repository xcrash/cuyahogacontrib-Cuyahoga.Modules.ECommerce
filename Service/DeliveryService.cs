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
    public class DeliveryService : IDeliveryService, INHibernateModule, IWeightBandingService {

        private ISessionManager _sessionManager;
        private IExtCommonDao _dao;

        public DeliveryService(ISessionManager sessionManager, IExtCommonDao dao) {
            _sessionManager = sessionManager;
            _dao = dao;
        }
    
        #region IDeliveryService Members

        public System.Collections.IList GetAllDeliveryMethods() {
            return _dao.GetAll(typeof(DeliveryType));
        }

        public DeliveryType GetDeliveryType(string name) {
          return (DeliveryType)_dao.GetObjectByDescription(typeof(DeliveryType), "_Name", name);
        }

        public void Save(DeliveryType deliveryType) {
            _dao.SaveOrUpdateObject(deliveryType);
        }

        #endregion

        #region IWeightBandingService Members

        public IList GetCountryWeightBandings(string country) {

            IQuery query = this._sessionManager.OpenSession().CreateQuery("from CountryDeliveryWeight where CountryCode = :code");
            query.SetString("code", country);

            return query.List();

        }

        public IList GetStateWeightBandings(string country) {
            IQuery query = this._sessionManager.OpenSession().CreateQuery("from CountryDeliveryStateWeight where CountryCode = :code");
            query.SetString("code", country);

            return query.List();
        }

        public CountryDeliveryWeight GetCountryDeliveryWeight(String country, decimal weight) {
            IQuery query = this._sessionManager.OpenSession().CreateQuery("from CountryDeliveryWeight where CountryCode = :code and WeightLevel = :weight");
            query.SetString("code", country);
            query.SetDecimal("weight", weight);

            IList results = query.List();
            foreach (CountryDeliveryWeight cdw in results) {
                return cdw;
            }

            return null;
        }

        public void Save(CountryDeliveryWeight cdw) {
            _dao.SaveOrUpdateObject(cdw);
        }

        public void Delete(CountryDeliveryWeight cdw) {
            _dao.DeleteObject(cdw);
        }


        #endregion
    }
}
