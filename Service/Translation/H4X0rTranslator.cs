using System;

namespace Cuyahoga.Modules.ECommerce.Service.Translation {
	/// <summary>
	/// Summary description for H4X0rTranslator.
	/// </summary>
	public class H4X0rTranslator : ITextTranslator {

		public H4X0rTranslator() {
		}

		public string GetText(string text) {
			if (text != null) {
				return text.ToLower().Trim().Replace("er ", "0r ").Replace("s ", "z ").Replace("a", "4").Replace("e", "3").Replace("o", "0").Replace("t", "7").Replace("s", "5").Replace("i", "1").Replace("f", "ph").Replace(",", " ").Replace(".", " ").Replace("  ", " ").Replace("cks", "x").Replace("ck", "x");
			} else {
				return text;
			}
		}

		public string CultureCode {
			get {
				return System.Threading.Thread.CurrentThread.CurrentCulture.Name;
			}
		}
	}
}