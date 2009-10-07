using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Service.Interfaces;
using Cuyahoga.Web.HttpModules;

using Castle.Facilities.NHibernateIntegration;

using Cuyahoga.Modules.ECommerce.DataAccess;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Castle.Services.Transaction;
using NHibernate;
                     

using log4net;
namespace Cuyahoga.Modules.ECommerce.Service {

    public class AuthenticationService : IAuthenticationService {

        private ISessionManager _sessionManager;
        private IExtCommonDao _dao;

        public AuthenticationService(ISessionManager sessionManager, IExtCommonDao dao) {
            _sessionManager = sessionManager;
            _dao = dao;
        }

        public bool AuthenticateUser(AuthenticationModule authModule, string username, string password) {
            if (authModule.AuthenticateUser(username, password, false)) {
                try {
                    //WebstoreLogin();
                    return true;
                } catch (Exception ex) {
                    LogManager.GetLogger(GetType()).Debug(ex);
                }
            }

            return false;
        }


    }
}
