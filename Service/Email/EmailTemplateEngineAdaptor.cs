using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Cuyahoga.Core.Service.Email;

namespace Cuyahoga.Modules.ECommerce.Service.Email {

    /// <summary>
    /// Provides an adaptor between IEmailTemplateEngine and ITemplateEngine
    /// </summary>
    public class EmailTemplateEngineAdaptor : IEmailTemplateEngine {

        private ITemplateEngine _engine;

        public EmailTemplateEngineAdaptor(ITemplateEngine engine) {
            _engine = engine;
        }

        #region IEmailTemplateEngine Members

        public string[] ProcessTemplate(string templatePath, Dictionary<string, string> subjectParams, Dictionary<string, string> bodyParams) {

            string templateName = Path.GetFileNameWithoutExtension(templatePath);

            string[] text = new string[2];
            text[0] = _engine.GetTemplateText(templateName, CreateHashtable(subjectParams));
            text[1] = _engine.GetTemplateText(templateName, CreateHashtable(bodyParams));

            return text;
        }

        #endregion

        private Hashtable CreateHashtable(Dictionary<string, string> parameters) {
            Hashtable hash = new Hashtable();
            foreach (string key in parameters.Keys) {
                hash.Add(key, parameters[key]);
            }
            return hash;
        }
    }
}
