using System;
using System.Resources;

namespace Cuyahoga.Modules.ECommerce.Service.Translation {

	/// <summary>
	/// A means to bootstrap the translator
	/// </summary>
	public class NonCompiledResourceFileTextSource : ITextTranslatorFactory {

		public NonCompiledResourceFileTextSource() {
		}

		public ITextTranslator GetTranslator(Type type, string cultureCode) {
			return new NonCompiledResourceFileTextTranslator(type, cultureCode);
		}
	}

	/// <summary>
	/// Summary description for NonCompiledResourceFileTextTranslator.
	/// </summary>
	public class NonCompiledResourceFileTextTranslator : ResourceFileTextTranslator {

		public NonCompiledResourceFileTextTranslator(Type type, string cultureCode) : base(type, cultureCode) {
		}

		protected override ResourceManager CreateResourceManager(Type type) {
			return FileResourceManager.CreateResourceManager(type.Assembly.GetName().Name, type.Assembly);
		}
	}
}