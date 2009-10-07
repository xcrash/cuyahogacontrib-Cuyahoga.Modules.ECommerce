using System;

namespace Cuyahoga.Modules.ECommerce.Service.Translation {

	/// <summary>
	/// Represents an object which has text for a given locale
	/// </summary>
	public interface ITextTranslator {
		string GetText(string key);
		string CultureCode { get; }
	}
}