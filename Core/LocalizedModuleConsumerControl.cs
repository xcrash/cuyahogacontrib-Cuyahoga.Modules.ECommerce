using System;

using Cuyahoga.Core.Domain;
using Cuyahoga.Modules.ECommerce.Service.Translation;
using Cuyahoga.Web.UI;

namespace Cuyahoga.Modules.ECommerce.Core {

    public class LocalizedModuleConsumerControl : LocalizedUserControl, IModuleConsumer, ITextTranslator {

        private ModuleBase _module = null;

        #region IModuleConsumer Members

        public virtual Cuyahoga.Core.Domain.ModuleBase Module {
            get {
                return _module;
            }
            set {
                _module = value;
            }
        }

        #endregion

        #region ITextTranslator Members
        public virtual string CultureCode {
            get {
                return Module.Section.Node.Culture;
            }
            set {
            }
        }

        public new string GetText(string key) {
            return base.GetText(key);
        }
        #endregion
    }
}
