using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Cuyahoga.Modules.ECommerce.Service.Email;
using Cuyahoga.Modules.ECommerce.Service.Translation;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {

    public class BaseEmailProcessor : IOrderProcessor {

        public const string TRANSLATOR_KEY = "__translator";

        private Cuyahoga.Core.Service.Email.IEmailSender _transport;
		private ITemplateEngine _templateEngine;

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

        public BaseEmailProcessor(ITemplateEngine engine, Cuyahoga.Core.Service.Email.IEmailSender sender) {
			_transport = sender;
			_templateEngine = engine;
		}

		protected virtual void SendEmail(Hashtable data, ITextTranslator translator) {
			SendEmail(data, translator, TemplateName, SenderEmail, ToEmail, SubjectTag, IsHtml);
		}

		protected virtual void SendEmail(Hashtable data, ITextTranslator translator, string templateName, string senderEmail, string toEmail, string subjectTag, bool isHtml) {

			//Apply configured defaults, possibly
			senderEmail = GetOptimalValue(senderEmail, SenderEmail);
			toEmail = GetOptimalValue(toEmail, ToEmail);

			string[] ccEmailArray = (!string.IsNullOrEmpty(CcEmail)) ? new string[] {CcEmail} : new string[] {};
            string[] bccEmailArray = (!string.IsNullOrEmpty(BccEmail)) ? new string[] { BccEmail } : new string[] { };

			templateName = GetOptimalValue(templateName, TemplateName);
			subjectTag = GetOptimalValue(subjectTag, SubjectTag);
			if (_isHtmlSet) isHtml = IsHtml;

			AddTranslatorData(translator, data, isHtml);
			
			string text = _templateEngine.GetTemplateText(templateName, data);

            _transport.Send(senderEmail, toEmail, translator.GetText(subjectTag), text, ccEmailArray, bccEmailArray);
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

		protected virtual void AddTranslatorData(ITextTranslator translator, Hashtable data, bool isHtml) {

			//Make a translator available
			if (translator != null && !data.ContainsKey(TRANSLATOR_KEY)) {

				if (!isHtml) {
					data.Add(TRANSLATOR_KEY, translator);
				} else {
				
					HtmlTextTranslator htm = translator as HtmlTextTranslator;

					if (!isHtml || htm == null) {
						data.Add(TRANSLATOR_KEY, new HtmlTextTranslator(translator));
					} else {
						data.Add(TRANSLATOR_KEY, translator);
					}
				}
			}
		}

        #region IOrderProcessor Members

        public virtual void Process(IBasket order) {
            ITextTranslator translator = Cuyahoga.Modules.ECommerce.Service.Translation.TranslatorUtils.GetTextTranslator(order.GetType(), order.CultureCode);
            Hashtable data = new Hashtable();
            data.Add("header", order.OrderHeader);
            data.Add("order", new BasketDecorator(order));

            this.SendEmail(data, translator);
        }

        #endregion
    }
}