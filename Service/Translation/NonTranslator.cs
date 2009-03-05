using System;

namespace Cuyahoga.Modules.ECommerce.Service.Translation {

	/// <summary>
	/// Summary description for NonTranslator.
	/// </summary>
	public class NonTranslator : ITextTranslator {

		public NonTranslator() {
		}

		public string GetText(string tagName) {
			if (tagName != null) {
				return "Translator[\"" + tagName + "\"]";
			} else {
				return "";
			}
		}

		public string CultureCode {
			get {
				return System.Threading.Thread.CurrentThread.CurrentCulture.Name;
			}
		}
	}
}