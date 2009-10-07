using System;

namespace Cuyahoga.Modules.ECommerce.Service.Translation {

	/// <summary>
	/// Creates a new text translator, based upon the type and culture code
	/// </summary>
	public interface ITextTranslatorFactory {
		ITextTranslator GetTranslator(Type type, string cultureCode);
	}
}