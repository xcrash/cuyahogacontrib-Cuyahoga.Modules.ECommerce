using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Service.Email {

	/// <summary>
	/// Summary description for XslTemplateEngine.
	/// </summary>
	public class XslTemplateEngine : TemplateEngineBase {

		public const string STYLESHEET_EXTENSION = "xsl";
		public const string ELEMENT_NAME_ROOT = "TemplateData";
		public const string ATTRIBUTE_NAME_ID = "id";

		public XslTemplateEngine() : base() {
		}

		public override string GetTemplateText(string templateName, Hashtable data) {

			try {

				StringWriter sw = new StringWriter();
				XslTransform xsl = new XslTransform();

				string templateFileName = TemplatePath + "\\" + templateName + "." + STYLESHEET_EXTENSION;
				xsl.Load(templateFileName);

				//A null XmlResolver is passed for this basic transformation
				XmlDocument xmlDoc = GetValueDocument(data);
				xsl.Transform(xmlDoc, null, sw, null);

				return sw.ToString();

			} catch (Exception e) {
				LogManager.GetLogger(GetType()).Error(e);
				return "";
			}
		}

		private XmlDocument GetValueDocument(Hashtable values) {

			XmlDocument document = new XmlDocument();
			XmlElement rootElement = document.CreateElement(ELEMENT_NAME_ROOT);
			document.AppendChild(rootElement);

			foreach (object keyObject in values.Keys) {

				string key = keyObject.ToString();
				object val = values[keyObject];

				//Ignore non-display items
				if (val == null || key.StartsWith(TemplateEngineBase.DATA_KEY_PREFIX_NON_DISPLAY)) {
					continue;
				}

				XmlDocument serialisedObject = SerializeObject(val);

				if (serialisedObject != null) {

					XmlElement field = document.ImportNode(serialisedObject.ChildNodes[serialisedObject.ChildNodes.Count - 1], true) as XmlElement;

					if (field != null) {
						XmlAttribute nameAttr = document.CreateAttribute(ATTRIBUTE_NAME_ID);
						field.SetAttribute(nameAttr.Name, key);
						rootElement.AppendChild(field);
					}
				}
			}

			return document;
		}

		private XmlDocument SerializeObject(object o) {

			XmlDocument xmlDoc = new XmlDocument();
			XmlSerializer xmls = new XmlSerializer(o.GetType());
			MemoryStream ms = new MemoryStream();

			xmls.Serialize(ms, o);
			ms.Position = 0;
			xmlDoc.Load(ms);

			ms.Close();
			
			return xmlDoc;
		}
	}
}
