using System;
using Cuyahoga.Modules.ECommerce.Service.Translation;

namespace Cuyahoga.Modules.ECommerce.Web.Controls {

	/// <summary>
	/// Summary description for TranslatedControl.
	/// </summary>
	public class TranslatedControl : Cuyahoga.Modules.ECommerce.Core.LocalizedModuleConsumerControl {

		private ITextTranslator _translator = null;

		#region ITranslatable Members
        public Cuyahoga.Web.UI.BaseModuleControl ParentComponent {
			get {
                return Parent as Cuyahoga.Web.UI.BaseModuleControl;
			}
		}

		public ITextTranslator Translator {
			get {
				//Why can't we get this from the parent?
				if (_translator == null) {

					_translator = Parent as ITextTranslator;
					
					if (_translator == null) {
						_translator = TranslatorUtils.GetTextTranslator(GetType(), CultureCode);
					}
				}
				return _translator;
			}
			set {
				_translator = value;
			}
		}

		#endregion

		#region ITextTranslator Members
		public new string GetText(string key) {
            return Translator.GetText(key);
		}
		#endregion
	}
}