using System.Collections;

namespace Cuyahoga.Modules.ECommerce.Service.Email {

	/// <summary>
	/// Provides stuff
	/// </summary>
	public interface ITemplateEngine {
		string GetTemplateText(string templateName, Hashtable data);
		//string TemplatePath {get; set;}
	}
}