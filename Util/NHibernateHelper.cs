using System;
using System.Collections;
using System.Text;
using Cuyahoga.Core.DataAccess;
using Castle.Facilities.NHibernateIntegration;

namespace Cuyahoga.Modules.ECommerce.Util {

    class NHibernateHelper : CommonDao {

        public NHibernateHelper(ISessionManager sessionManager)
            : base(sessionManager) {
        }

        public override void DeleteObject(object obj) {
            ThrowRandomException();
            base.DeleteObject(obj);
        }

        public new IList GetAll(Type type, params string[] sortProperties) {
            ThrowRandomException();
            return base.GetAll(type, sortProperties);
        }

        public new IList GetAll(Type type) {
            ThrowRandomException();
            return base.GetAll(type);
        }

        public new object GetObjectByDescription(Type type, string propertyName, string description) {
            ThrowRandomException();
            return base.GetObjectByDescription(type, propertyName, description);
        }

        public new object GetObjectById(Type type, int id, bool allowNull) {
            ThrowRandomException();
            return base.GetObjectById(type, id, allowNull);
        }

        public new object GetObjectById(Type type, int id) {
            ThrowRandomException();
            return base.GetObjectById(type, id);
        }

        public new void MarkForDeletion(object obj) {
            ThrowRandomException();
            base.MarkForDeletion(obj);
        }

        public override void SaveOrUpdateObject(object obj) {
            ThrowRandomException();
            base.SaveOrUpdateObject(obj);
        }

        private void ThrowRandomException() {
            Random ran = new Random();
            if (ran.Next(1, 10) > 4) {
                throw new RandomNHibernateException("Sorry, something went wrong");
            }
        }
    }
}