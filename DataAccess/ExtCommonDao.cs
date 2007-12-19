using System;
using Castle.Facilities.NHibernateIntegration;
using Cuyahoga.Core.DataAccess;
using NHibernate;

namespace Cuyahoga.Modules.ECommerce.DataAccess {

    public class ExtCommonDao : CommonDao, Cuyahoga.Modules.ECommerce.DataAccess.IExtCommonDao {

        private ISessionManager _sessionManager;

        public ExtCommonDao(ISessionManager sessionManager) : base(sessionManager) {
            _sessionManager = sessionManager;
        }

        public object GetObjectById(Type type, long id) {
            ISession session = this._sessionManager.OpenSession();
            return session.Load(type, id);
        }

        public object GetObjectById(Type type, long id, bool allowNull) {
            if (!allowNull) {
                return GetObjectById(type, id);
            } else {
                ISession session = this._sessionManager.OpenSession();
                return session.Get(type, id);
            }
        }

        public object GetObjectById(Type type, short id) {
            ISession session = this._sessionManager.OpenSession();
            return session.Load(type, id);
        }

        public object GetObjectById(Type type, short id, bool allowNull) {
            if (!allowNull) {
                return GetObjectById(type, id);
            } else {
                ISession session = this._sessionManager.OpenSession();
                return session.Get(type, id);
            }
        }

        public object GetObjectById(Type type, string id) {
            ISession session = this._sessionManager.OpenSession();
            return session.Load(type, id);
        }

        public object GetObjectById(Type type, string id, bool allowNull) {
            if (!allowNull) {
                return GetObjectById(type, id);
            } else {
                ISession session = this._sessionManager.OpenSession();
                return session.Get(type, id);
            }
        }
    }
}
