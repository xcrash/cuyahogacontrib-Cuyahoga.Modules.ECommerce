using System;

namespace Cuyahoga.Modules.ECommerce.Service.Translation {

	/// <summary>
	/// Marks an object as being translatable and provides translators to perform this operation
	/// </summary>
	public interface ITranslatable : ITextTranslator {
		ITextTranslator Translator {get; set;}
	}
}