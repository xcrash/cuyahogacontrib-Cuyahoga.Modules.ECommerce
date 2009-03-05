using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Service.Translation {

	/// <summary>
	/// A means to bootstrap the translator
	/// </summary>
	public class ResourceFileTextSource : ITextTranslatorFactory {

		public ResourceFileTextSource() {
		}

		public ITextTranslator GetTranslator(Type type, string cultureCode) {
			return new ResourceFileTextTranslator(type, cultureCode);
		}
	}

	/// <summary>
	/// Summary description for ResourceFileTextTranslator.
	/// </summary>
	public class ResourceFileTextTranslator : ITextTranslator {

		private ResourceManager _resMan;
		private CultureInfo _currentUICulture;

		public ResourceFileTextTranslator(Type type, string cultureCode) {
			_resMan = CreateResourceManager(type);
			_currentUICulture = new CultureInfo(cultureCode);
		}

		public string CultureCode {
			get {
				return _currentUICulture.Name;
			}
		}

		protected virtual string GetBaseName(Type type) {
			return type.Namespace + ".Resources.Strings";
		}

		protected virtual ResourceManager CreateResourceManager(Type type) {
					
			LogManager.GetLogger(GetType()).Info("Getting base name [" + GetBaseName(type) + "] for type [" + type.FullName + "]");

			try {
				return new ResourceManager(GetBaseName(type), type.Assembly);
			} catch (Exception e) {
				LogManager.GetLogger(GetType()).Error(e);
				return null;
			}
		}

		/// <summary>
		/// Get a localized text string for a given key.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public virtual string GetText(string key) {

			if (key == null) {
				throw new ArgumentException("Null key supplied to translator");
			}

			if (key.Length == 0) {
				throw new ArgumentException("Empty key supplied to translator");
			}

			//Avoid confusion by always using lower case keys
			string text = _resMan.GetString(key.ToLower(), this._currentUICulture);

			if (text != null) {
				return text;
			}

			LogManager.GetLogger("ResourceFileEntry").Info(_resMan.BaseName + " requires ["
				+ string.Format("<data name=\"{0}\"><value>{1}</value></data>", key.ToLower(), key)
				+ "]");

			//Always return a non-null string
			return "";
		}
	}
}