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
namespace Cuyahoga.Modules.ECommerce.Service
{
    public class ChargeService : IChargeService, INHibernateModule {

        public const string MINIMUM_DELIVERY_CHARGE = "MinimumDeliveryCharge";
        private ISessionManager _sessionManager;
        private IExtCommonDao _dao;

        public ChargeService(ISessionManager sessionManager, IExtCommonDao dao) {
            _sessionManager = sessionManager;
            _dao = dao;
        }
    
        #region IChargeService Members

        public decimal GetMinimumDeliveryCharge(string countryCode) {

            using (SpHandler sph = new SpHandler("getMinimumDeliveryCharge", new SqlParameter("@chargeName", MINIMUM_DELIVERY_CHARGE), new SqlParameter("@countryCode", countryCode))) {

                sph.ExecuteReader();

                if (sph.DataReader.Read()) {
                    string val = sph.DataReader["Price"] as string;
                    if (!string.IsNullOrEmpty(val)) {
                        return Convert.ToDecimal(val);
                    }
                }
            }

            return 0;
        }

        #endregion
    }
}
