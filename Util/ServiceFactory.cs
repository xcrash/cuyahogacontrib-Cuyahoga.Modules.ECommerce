using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Util {

    public class ServiceFactory {

        public static object GetService(string key) {

            Cuyahoga.Web.Components.CuyahogaContainer container = Cuyahoga.Web.Util.ContainerAccessorUtil.GetContainer();

            if (container != null) {
                return container[key];
            }

            return null;
        }

        public static object GetService(Type service) {

            Cuyahoga.Web.Components.CuyahogaContainer container = Cuyahoga.Web.Util.ContainerAccessorUtil.GetContainer();

            if (container != null) {
                return container[service];
            }

            return null;
        }
    }
}