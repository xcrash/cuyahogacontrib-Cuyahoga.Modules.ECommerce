using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Cuyahoga.Modules.ECommerce.Util {
    
	public class XMLUtils {

		public static XmlElement AppendNewElement(ref XmlNode objNode, string name) {
			return AppendNewElement(ref objNode, name, null);
		}
    
		public static XmlElement AppendNewElement(ref XmlNode objNode, string name, string value) {

			XmlDocument parent;
			XmlElement newElement = null;

			parent = objNode.OwnerDocument;

			//Something went wrong - assume this is a document
			if (parent == null) {
				parent = objNode as XmlDocument;
			}

			if (parent != null) {
				newElement = parent.CreateElement(name);
				objNode.AppendChild(newElement);

				//Append text if it is supplied
				if (value != null && value.Length > 0) {
					newElement.AppendChild(parent.CreateTextNode(value));
				}

			}

			return newElement;

		}

		public static void AppendNewAttribute(ref XmlElement objNode, string name, string value) {

			XmlDocument parent;
			XmlAttribute newAttribute;

			parent = objNode.OwnerDocument;

			newAttribute = parent.CreateAttribute(name);
			objNode.SetAttribute(newAttribute.Name, value);

		}

        public static XmlElement AppendElement(XmlNode node, string name) {
            return AppendElement(node, name, null);
        }

        public static XmlElement AppendElement(XmlNode node, string name, object value) {

            XmlDocument parent = node.OwnerDocument;

            //Something went wrong - assume this is a document
            if (parent == null) {
                parent = node as XmlDocument;
            }

            XmlElement newElement = null;

            if (parent != null) {

                newElement = parent.CreateElement(name);
                node.AppendChild(newElement);

                //Append value if it is supplied
                if (value != null) {

                    string stringValue = value.ToString();

                    if (stringValue.Length > 0) {
                        newElement.AppendChild(parent.CreateTextNode(stringValue));
                    }
                }
            }

            return newElement;
        }

        public static XmlAttribute AppendAttribute(XmlElement node, string name, object value) {

            XmlAttribute newAttribute = node.OwnerDocument.CreateAttribute(name);
            node.SetAttribute(newAttribute.Name, value.ToString());

            return newAttribute;
        }

		public static string ObjectToXml(object objSource) {

			XMLObject obj = new XMLObject(objSource);
			return obj.XML;

		}

		public static object XmlToObject(string xml, object dest) {

			XMLObject obj = new XMLObject(xml);
			obj.PopulateObject(dest);
			return dest;
		}

		public static string StripXmlHeader(string xml) {
			Regex re = new Regex("<\\?.*?\\?>");
			return re.Replace(xml, "").Replace("\r\n", "");
		}

		public static DataSet CreateDataSet(string xml) {
			System.IO.StringReader sr = new System.IO.StringReader(xml);            
			DataSet ds = new DataSet();
			ds.ReadXml(sr);
			return ds;
		}

		public static DataSet CreateDataSet(string xml, string rootElement) {

			XmlDocument responseXml = new XmlDocument();
			responseXml.LoadXml(xml);

			return CreateDataSet(responseXml, rootElement);
		}


		public static DataSet CreateDataSet(XmlDocument responseXml, string rootElement) {

			XmlNodeList list = responseXml.SelectNodes("//" + rootElement);

			StringBuilder xml = new StringBuilder();

			xml.Append("<Root>");
			foreach (XmlNode node in list) {
				xml.Append(node.OuterXml);
			}
			xml.Append("</Root>");

			System.IO.StringReader sr = new System.IO.StringReader(xml.ToString());            
			DataSet ds = new DataSet();
			ds.ReadXml(sr);
			return ds;
		}

		public static DataSet CreateDataSet(XmlDocument xml) {
			return CreateDataSet(xml.OuterXml);
		}

		//Crude way of renaming elements
		public static string RenameElement(string xml, string oldName, string newName) {
			xml = xml.Replace("<" + oldName + ">", "<" + newName + ">");
			xml = xml.Replace("</" + oldName + ">", "</" + newName + ">");
			xml = xml.Replace("<" + oldName + "/>", "<" + newName + "/>");
			xml = xml.Replace("<" + oldName + " />", "<" + newName + " />");
			return xml;
		}

    
		public static String DecodeWhiteSpace(String text) {
    	
			//Replace characters lost in XML transmission
			string clean = text;
			clean = text.Replace("\\r", "\r");
			clean = clean.Replace("\\n", "\n");
			clean = clean.Replace("\\t", "\t");
		
			clean = text.Replace("\\\\r", "\\r");
			clean = clean.Replace("\\\\n", "\\n");
			clean = clean.Replace("\\\\t", "\\t");
			return clean;

		}

		public static String EncodeWhiteSpace(String text) {
    	
			string clean = text;
			clean = text.Replace("\r", "\\r");
			clean = clean.Replace("\n", "\\n");
			clean = clean.Replace("\t", "\\t");
		
			return clean;

		}

		/// <summary>
		/// Builds up the path in the XML document, if it does not exist, plus any required attributes
		/// </summary>
		/// <param name="path">A simple XPath expression</param>
		/// <param name="doc">Parent document</param>
		/// <remarks>Supports XPath expressions of the form
		/// <c>/UserProfile/DomainList[@Count="4"]/Domain[DomainName="Wibble"]/RoleList[Role="Igentics Support"]</c>
		/// </remarks>
		public static XmlNode CreateXmlNode(string path, XmlDocument doc) {

			const string elementSearch = "([\\w-_]+)(\\[(@?[\\w-_]+)=['\"](.+?)['\"]\\])";

			Regex elementParser = new Regex(@"\/(" + elementSearch + "?)");

			if (path.StartsWith("//") == false && elementParser.IsMatch(path)) {

				ArrayList elements = new ArrayList();

				MatchCollection elementCollection = elementParser.Matches(path);
				foreach (Match match in elementCollection) {
					elements.Add(match.Value);
				}

				string currentPath = "";
				XmlNode lastNode = doc;

				foreach (string element in elements) {

					//See if this is a reasonably complex XPath query
					Regex re = new Regex("^/" + elementSearch + "$");

					string elementName = "";
					string qualifierName = "";
					string qualifierValue = "";

					if (re.IsMatch(element)) {
						MatchCollection col = re.Matches(element);
						if (col.Count > 0 && col[0].Groups.Count > 4) {
							elementName = col[0].Groups[1].Value;
							qualifierName = col[0].Groups[3].Value;
							qualifierValue = col[0].Groups[4].Value;
						}
					} else {
						//Take element without the leading '/'
						elementName = element.Substring(1);
					}

					currentPath += element;
					XmlNode currentNode = doc.SelectSingleNode(currentPath);
					
					if (currentNode == null) {
						currentNode = AppendNewElement(ref lastNode, elementName);

						//Add referenced elements or attributes, depending upon qualifier name
						if (qualifierName.Length > 1) {
							if (qualifierName.StartsWith("@")) {
								XmlElement elem = (XmlElement) currentNode;
								AppendNewAttribute(ref elem, qualifierName.Substring(1), qualifierValue);
							} else {
								AppendNewElement(ref currentNode, qualifierName, qualifierValue);
							}
						}
					}

					lastNode = currentNode;
				}

				return lastNode;

			}

			throw new NotSupportedException("Unsupported XPath expression [" + path + "]");

		}

		public static XmlDocument SerializeObject(object o) {

			XmlDocument xmlDoc = new XmlDocument();
			XmlSerializer xmls = new XmlSerializer(o.GetType());
			System.IO.MemoryStream ms = new System.IO.MemoryStream();

			xmls.Serialize(ms,o);
			ms.Position = 0;
			xmlDoc.Load(ms);

			ms.Close();
			
			return xmlDoc; 

		}

		public static object DeserializeObject(Type type, string xml) {

			XmlSerializer xmls = new XmlSerializer(type);
			return xmls.Deserialize(new System.IO.StringReader(xml));

		}

		public static object DeserializeObject(Type type, XmlDocument xmlDoc) {
			return DeserializeObject(type, xmlDoc.OuterXml);
		}
	}
}