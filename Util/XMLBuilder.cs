using System.Xml;

namespace Cuyahoga.Modules.ECommerce.Util {
    
	public class XmlBuilder {

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
	}
}