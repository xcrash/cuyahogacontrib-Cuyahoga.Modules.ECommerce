using System;
using Cuyahoga.Modules.ECommerce.Service.Translation;
using Cuyahoga.Web.UI;

namespace Cuyahoga.Modules.ECommerce.Core {

    public class GenericModuleControl : BaseModuleControl, ITextTranslator {

        private ITextTranslator _translator;

        #region ITextTranslator Members
        public string CultureCode {
            get {
                return Module.Section.Node.Culture;
            }
            set {
            }
        }

        public new string GetText(string key) {
            if (_translator == null) {
                _translator = TranslatorUtils.GetTextTranslator(GetType(), CultureCode);
            }
            return _translator.GetText(key.ToLower());
        }
        #endregion

        protected string AppPath {
            get { return Cuyahoga.Web.Util.UrlHelper.GetApplicationPath(); }
        }
    }
}
