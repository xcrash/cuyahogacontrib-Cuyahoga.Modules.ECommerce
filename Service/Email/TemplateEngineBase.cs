using System.Collections;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.Email {

	/// <summary>
	/// Summary description for GenericTemplateEngine.
	/// </summary>
	public abstract class TemplateEngineBase : ITemplateEngine {

		public const string DATA_KEY_PREFIX_NON_DISPLAY = "__";

		public const string DEFAULT_TEMPLATE_PATH = "templates";

		private string _templatePath;

		public TemplateEngineBase() {
			TemplatePath = WebHelper.GetPhysicalApplicationPath() + DEFAULT_TEMPLATE_PATH;
		}

		public TemplateEngineBase(string templatePath) {
			TemplatePath = templatePath;
		}

		public abstract string GetTemplateText(string templateName, Hashtable data);

		public string TemplatePath {
			get {
				return _templatePath;
			}
			set {
				if (value != null) {
					
					_templatePath = value.Replace("/", "\\");

					if (!_templatePath.EndsWith("\\")) {
						_templatePath += "\\";
					}
				} else {
					_templatePath = value;
				}
			}
		}
	}
}
