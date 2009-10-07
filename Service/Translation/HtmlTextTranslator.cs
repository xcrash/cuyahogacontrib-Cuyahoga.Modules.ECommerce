using System.Web;

namespace Cuyahoga.Modules.ECommerce.Service.Translation {

    /// <summary>
    /// Summary description for HtmlTextTranslator.
    /// </summary>
    public class HtmlTextTranslator : ITextTranslator {

        private ITextTranslator _translator;
        private HttpServerUtility _util = null;

        public HtmlTextTranslator(ITextTranslator translator) {
            
            _translator = translator;

            if (HttpContext.Current != null) {
                _util = HttpContext.Current.Server;
            }
        }

        public string GetText(string tagName) {
            return GetEncodedText(GetRawText(tagName));
        }

        public string CultureCode {
            get {
                return _translator.CultureCode;
            }
        }

        public string[] GetTextArray(string tagName, string seperators) {
            return GetText(tagName).Split(seperators.ToCharArray());
        }

        public string GetRawText(string tagName) {
            return _translator.GetText(tagName);
        }

        //Makes life easier for NVelocity
        public string[] GetRawTextArray(string tagName, string seperators) {
            return GetRawText(tagName).Split(seperators.ToCharArray());
        }

        private string GetEncodedText(string text) {

            if (text == null) return "";

            string html = text.Replace("\\n", "").Replace("\\r", "\r").Replace("\\t", "\t").Trim().Replace("\r", "<br/>");
            return _util.HtmlEncode(html);
        }
    }
}