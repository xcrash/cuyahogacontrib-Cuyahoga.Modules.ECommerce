using System;
using Cuyahoga.Core.Domain;

namespace Cuyahoga.Modules.ECommerce.Core {

    public class ModuleConsumerControl : System.Web.UI.UserControl, IModuleConsumer {

        private ModuleBase _module = null;

        #region IModuleConsumer Members

        public Cuyahoga.Core.Domain.ModuleBase Module {
            get {
                return _module;
            }
            set {
                _module = value;
            }
        }

        #endregion
    }
}
