using System;
using System.Net;
using System.Net.Mail;
using System.Text;

using Cuyahoga.Core.Service.Email;

namespace Cuyahoga.Modules.ECommerce.Service.Email {

    /// <summary>
    /// Implements IEmailSender using the System.Net.Email classes from the .NET 2.0 framework with optional HTML body.
    /// </summary>
    public class HtmlEmailSender : IEmailSender {

        private string _host;
        private int _port;
        private string _smtpUsername;
        private string _smtpPassword;
        private Encoding _encoding;
        private bool _isHtml;

        /// <summary>
        /// SMTP port (default 25).
        /// </summary>
        public int Port {
            set { _port = value; }
        }

        /// <summary>
        /// SMTP Username
        /// </summary>
        public string SmtpUsername {
            set { _smtpUsername = value; }
        }

        /// <summary>
        /// SMTP Password
        /// </summary>
        public string SmtpPassword {
            set { _smtpPassword = value; }
        }

        /// <summary>
        /// Email body encoding
        /// </summary>
        public string EmailEncoding {
            set {
                if (!String.IsNullOrEmpty(value)) {
                    _encoding = Encoding.GetEncoding(value);
                }
            }
        }

        /// <summary>
        /// Determines whether emails are sent as an HTML email
        /// </summary>
        public bool IsBodyHtml {
            get {
                return _isHtml;
            }
            set {
                _isHtml = value;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="host">SMTP hostname is required for this service to work.</param>
        public HtmlEmailSender(string host) {
            _host = host;
            _port = 25;
            _encoding = Encoding.Default;
        }

        #region IEmailSender Members

        public virtual void Send(string from, string to, string subject, string body) {
            Send(from, to, subject, body, null, null);
        }

        public virtual void Send(string from, string to, string subject, string body, string[] cc, string[] bcc) {
            
            // Create mail message
            MailMessage message = new MailMessage(from, to, subject, body);
            message.BodyEncoding = _encoding;
            message.IsBodyHtml = _isHtml;

            if (cc != null && cc.Length > 0) {
                foreach (string ccAddress in cc) {
                    message.CC.Add(new MailAddress(ccAddress));
                }
            }

            if (bcc != null && bcc.Length > 0) {
                foreach (string bccAddress in bcc) {
                    message.Bcc.Add(new MailAddress(bccAddress));
                }
            }

            // Send email
            SmtpClient client = new SmtpClient(_host, _port);
            if (!String.IsNullOrEmpty(_smtpUsername) && !String.IsNullOrEmpty(_smtpPassword)) {
                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            }

            client.Send(message);
        }

        #endregion
    }
}
