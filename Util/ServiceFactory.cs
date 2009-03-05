using System;
using System.Collections.Generic;
using System.Text;
using Castle.Windsor;
using Cuyahoga.Core.Service;
using Cuyahoga.Web.Components;

namespace Cuyahoga.Modules.ECommerce.Util {

    public class ServiceFactory {

        private static IWindsorContainer _container = null;

        public static object GetService(string key) {

            if (Container != null) {
                return Container.Kernel[key];
            }

            return null;
        }

        public static object GetService(Type service) {

            if (Container != null) {
                return Container.Kernel[service];
            }

            return null;
        }

        private static IWindsorContainer Container {
            get {
                if (_container != null) return _container;

                try {
                    _container = Cuyahoga.Web.Util.ContainerAccessorUtil.GetContainer();
                } catch { }

                if (_container == null) {
                    _container = new CuyahogaContainer();
                    SessionFactoryHelper helper = new SessionFactoryHelper(_container.Kernel);
                    helper.AddAssembly(typeof(Cuyahoga.Modules.ECommerce.Domain.Basket).Assembly);
                }

                return _container;
            }
        }
    }
}