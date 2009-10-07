using System;
using System.IO;
using System.Xml;

using Cuyahoga.Modules.ECommerce.Util;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Service.Email {

    /// <summary>
    /// Logs the email, but does not send it.
    /// Optionally dumps the HTML part to a separate file for easy viewing
    /// </summary>
    public class XmlEmailSender : HtmlEmailSender {

        private string _smtpServer = null;
        private string _outputPath = null;
        private bool _createHtmFile = true;

        public XmlEmailSender(string host) : base(host) {
            OutputPath = "outbox";
        }

        public string OutputPath {
            get {
                return _outputPath;
            }
            set {
                if (value != null) {
                    if (value.EndsWith("\\")) {
                        _outputPath = value;
                    } else {
                        _outputPath = value + "\\";
                    }
                }
            }
        }

        /// <summary>
        /// If <c>true</c>, additionally dumps the HTML body text to a file
        /// </summary>
        public bool CreateHtmlFile {
            get {
                return _createHtmFile;
            }
            set {
                _createHtmFile = true;
            }
        }

        #region IEmailSender Members

        public override void Send(string from, string to, string subject, string body, string[] cc, string[] bcc) {

            XmlDocument xmlDoc = GenerateEmailXml(from, to, cc, bcc, subject, IsBodyHtml, body);

            DateTime now = DateTime.Now;
            string filenameBase = now.ToString("yyyy-MM-dd-hh-mm-ss-") + GenerateRandomText(4);

            WriteTextFile(filenameBase + ".xml", xmlDoc.OuterXml);

            if (IsBodyHtml && CreateHtmlFile) {
                WriteTextFile(filenameBase + ".htm", body);
            }
        }

        public override void Send(string from, string to, string subject, string body) {
            Send(from, to, subject, body, null, null);
        }

        #endregion

        #region IEmailTransport Members

        public string SmtpServer {
            get {
                return _smtpServer;
            }
            set {
                _smtpServer = value;
            }
        }

        protected virtual XmlDocument GenerateEmailXml(string senderEmail, string toEmail, string[] ccEmail, string[] bccEmail, string subject, bool isHtml, string bodyText) {

            XmlDocument xmlDoc = new XmlDocument();

            XmlElement root = XmlBuilder.AppendElement(xmlDoc, "email");

            XmlBuilder.AppendElement(root, "senderEmail", senderEmail);
            XmlBuilder.AppendElement(root, "toEmail", toEmail);

            if (ccEmail != null) {
                XmlElement cc = XmlBuilder.AppendElement(root, "ccEmail");
                for (int i = 0; i < ccEmail.Length; i++) {
                    XmlBuilder.AppendElement(cc, "recipient", ccEmail[i]);
                }
            }

            if (bccEmail != null) {
                XmlElement bcc = XmlBuilder.AppendElement(root, "bccEmail");
                for (int i = 0; i < bccEmail.Length; i++) {
                    XmlBuilder.AppendElement(bcc, "recipient", bccEmail[i]);
                }
            }

            XmlBuilder.AppendElement(root, "subject", subject);
            XmlBuilder.AppendElement(root, "isHtml", isHtml);
            XmlBuilder.AppendElement(root, "bodyText", bodyText);

            return xmlDoc;
        }

        public string GenerateRandomText(int length) {

            string possibleValues = "01234567890ABCDEF";

            Random random = new Random(DateTime.Now.Millisecond);

            //this ensures that the error code that is returned looks more important than it is
            string strString = "";

            for (int i = 0; i < length; i++) {
                int index = random.Next(possibleValues.Length);
                strString += possibleValues[index];
            }

            return strString;
        }


        private void WriteTextFile(string fileName, string contents) {

            StreamWriter sr = null;

            try {

                sr = File.CreateText(OutputPath + fileName);
                sr.WriteLine(contents);

            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
            } finally {
                try {
                    if (sr != null) sr.Close();
                } catch { }
            }
        }
        #endregion
    }
}
