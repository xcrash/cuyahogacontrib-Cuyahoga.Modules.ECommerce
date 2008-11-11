using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

using Igentics.Common.Service.EmailTransport;
using Igentics.Common.Service.Templates;
using Igentics.Common.Service.Translation;

using Igentics.Common.ECommerce;
using Igentics.Common.ECommerce.DataTransferObjects;
using Igentics.Common.Logging;

namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

    public class BaseEmailSender : IOrderProcessor {

        public const string TRANSLATOR_KEY = "__translator";

        private IEmailTransport _transport;
		private ITemplateEngine _templateEngine;
        private ITextTranslatorFactory _translatorFactory;

		private string _subjectTag = null;
		private string _senderEmail = null;
		private string _toEmail = null;
		private bool _isHtml = false;
		private bool _isHtmlSet = false;
		private string _templateName = null;
		private string _bccEmail = null;
		private string _ccEmail = null;

		#region Configuration for all emails
		public string SubjectTag {
			get {
				return _subjectTag;
			}
			set {
				_subjectTag = value;
			}
		}

		public string SenderEmail {
			get {
				return _senderEmail;
			}
			set {
				_senderEmail = value;
			}
		}

		public string ToEmail {
			get {
				return _toEmail;
			}
			set {
				_toEmail = value;
			}
		}

		public string CcEmail {
			get {
				return _ccEmail;
			}
			set {
				_ccEmail = value;
			}
		}

		public string BccEmail {
			get {
				return _bccEmail;
			}
			set {
				_bccEmail = value;
			}
		}

		public bool IsHtml {
			get {
				return _isHtml;
			}
			set {
				_isHtml = value;
				_isHtmlSet = true;
			}
		}

		public string TemplateName {
			get {
				return _templateName;
			}
			set {
				_templateName = value;
			}
		}
		#endregion

        public BaseEmailSender(ITemplateEngine engine, IEmailTransport sender, ITextTranslatorFactory translatorFactory) {
			_transport = sender;
			_templateEngine = engine;
            _translatorFactory = translatorFactory;
		}

		protected virtual bool SendEmail(IDictionary context, ITextTranslator translator) {
			return SendEmail(context, translator, TemplateName, SenderEmail, ToEmail, SubjectTag, IsHtml);
		}

        protected virtual bool SendEmail(IDictionary context, ITextTranslator translator, string templateName, string senderEmail, string toEmail, string subjectTag, bool isHtml) {

            //Apply configured defaults, possibly
            senderEmail = GetOptimalValue(senderEmail, SenderEmail);
            toEmail = GetOptimalValue(toEmail, ToEmail);

            templateName = GetOptimalValue(templateName, TemplateName);
            subjectTag = GetOptimalValue(subjectTag, SubjectTag);
            if (_isHtmlSet) isHtml = IsHtml;

            AddTranslatorData(translator, context, isHtml);

            StringWriter sw = new StringWriter();

            try {
                _templateEngine.Process(context, templateName, sw);

                MailMessage message = MailHelper.CreateMessage(senderEmail, toEmail, CcEmail, BccEmail, translator.GetText(subjectTag), sw.ToString(), isHtml);
                _transport.Send(message);
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
                return false;
            }
            return true;
        }

		/// <summary>
		/// Configured values override default values
		/// </summary>
		/// <param name="suppliedValue">Value supplied by the class at runtime</param>
		/// <param name="configuredValue">Externally configured value</param>
		private string GetOptimalValue(string suppliedValue, string configuredValue) {
			return ((configuredValue != null && configuredValue.Length > 0) || suppliedValue == null || suppliedValue.Length == 0)
				? configuredValue : suppliedValue;
		}

		protected virtual void AddTranslatorData(ITextTranslator translator, IDictionary context, bool isHtml) {

			//Make a translator available
			if (translator != null && !context.Contains(TRANSLATOR_KEY)) {

				if (!isHtml) {
					context.Add(TRANSLATOR_KEY, translator);
				} else {
				
					HtmlTextTranslator htm = translator as HtmlTextTranslator;

					if (!isHtml || htm == null) {
						context.Add(TRANSLATOR_KEY, new HtmlTextTranslator(translator));
					} else {
						context.Add(TRANSLATOR_KEY, translator);
					}
				}
			}
		}

        #region IOrderProcessor Members

        public virtual ProcessStatusMessage Process(Basket order) {
            
            ITextTranslator translator = _translatorFactory.GetTranslator(order.GetType(), order.CultureCode);
            Hashtable data = new Hashtable();
            data.Add("header", order.Header);
            data.Add("order", order);

            if (!SendEmail(data, translator)) {
                return new ProcessStatusMessage(ProcessStatus.Error, translator.GetText("Email_send_failed"));
            }

            return new ProcessStatusMessage(ProcessStatus.Success);
        }

        #endregion
    }
}