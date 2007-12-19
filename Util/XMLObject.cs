using System;
using System.Collections;
using System.Reflection;
using System.Xml;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	///	The class that contains methods to map xml data to class properties
	/// </summary>
	public class XMLObject {

		private XmlDocument _doc;
		private XmlNode _node;

		public XMLObject() {
			XmlDoc = new XmlDocument();
		}

		public XMLObject(XmlDocument doc) {
			XmlDoc = doc;
		}

		public XMLObject(XmlNode node) {
			Node = node;
		}

		public XMLObject(String xml) {
			if (xml.Trim().StartsWith("<")) {
				XML = xml;
			} else {
				XML = "<" + xml + "/>";
			}
		}

		public XMLObject(object o) {
			LoadObject(o);
		}

		public string this[string propertyName] {
			get {
				try {
					string stringValue = GetStringValue(propertyName);
					if (stringValue == null) {
						stringValue = "";
					}

					return stringValue;

				} catch {
					return "";
				}
			}
			set {
				SetValue(propertyName, value);
			}
		}

		/// <summary>
		/// The name of this object - the element name for the XML
		/// </summary>
		public string Name {
			get {
				return Node.Name;
			}
			set {
				if (XmlDoc.ChildNodes.Count == 0) {
					XmlNode node = (XmlNode) XmlDoc;
					XMLUtils.AppendNewElement(ref node, value);
				} else {
					//This is very dangerous
					XML = XMLUtils.RenameElement(XML, Name, value);
				}
			}
		}

		/// <summary>
		/// A helper property to populate the internal XmlDocument
		/// </summary>
		public string XML {
			get {
				return Node.OuterXml;
			}
			set {
				LoadXml(value);
			}
		}

		public XmlNode Node {
			get {
				if (_node == null) {
					if (_doc.FirstChild != null) {
						_node = _doc.FirstChild;
					} else {
						_node = _doc;
					}
				}
				return _node;
			}
			set {
				_node = value;
				_doc = value.OwnerDocument;
			}
		}

		public XmlDocument XmlDoc {
			get {
				if (_doc == null) {
					_doc = new XmlDocument();
				}
				return _doc;
			}
			set {
				_doc = value;
			}
		}

		/// <summary>
		/// An ArrayList of objects matching the tag name
		/// </summary>
		/// <param name="nodeName"></param>
		/// <returns></returns>
		public ArrayList GetObjects(string nodeName) {

			XmlNodeList nodes = GetNodeList(nodeName);
			ArrayList objects = new ArrayList();

			if (nodes != null && nodes.Count > 0) {
				foreach(XmlNode node in nodes) {
					objects.Add(new XMLObject(node));
				}
			}

			return objects;
		}

		/// <summary>
		/// This method accepts a string containing the path to the XML file. And an object
		/// whose properties are to be filled by the data from the xml file.
		/// </summary>
		/// <param name="obj">the object, whose properties are to be filled</param>
		/// <returns>true if no error occurs, false otherwise</returns>
		public bool PopulateObject(object obj) {

			Type t = null;
			PropertyInfo[] properties = null;
			object currentValue = null;

			try {
				t = obj.GetType();
				properties = t.GetProperties();
				// taking a property at a time ...
				foreach (PropertyInfo proc in properties) {
					// if it's writable
					if (proc.CanWrite) {
						try {
							// then match a value from the XML Node
							currentValue = GetValue(proc.Name, proc.PropertyType.ToString());
							// if there's a value returned,
							if (currentValue != null)
								// then assign the value to the property
								t.InvokeMember(
									proc.Name,
									BindingFlags.Default | BindingFlags.SetProperty,
									null,
									obj,
									new object [] { currentValue }
									);
						} catch(Exception e) {
							throw e;
						}
					}
				}
				return true;

			} catch (Exception ex) {

				throw ex;
			}
		}

		/**
				 * This section of code should be using the ObjectFactory class
				 */
		public String GetStringValue(string xmlName) {
			string value = "";
			try {
				object obj = GetValue(xmlName, "System.String");
				value = obj.ToString();
			} catch {
			}
			return value;
		}

		public byte GetByteValue(string xmlName) {
			object obj = GetValue(xmlName, "System.Byte");
			byte value = 0;
			try {
				value = (byte) obj;
			} catch {
			}
			return value;
		}

		public char GetCharValue(string xmlName) {
			object obj = GetValue(xmlName, "System.Char");
			char value = '\0';
			try {
				value = (char) obj;
			} catch {
			}
			return value;
		}

		public decimal GetDecimalValue(string xmlName) {
			object obj = GetValue(xmlName, "System.Decimal");
			decimal value = 0;
			try {
				value = (decimal) obj;
			} catch {
			}
			return value;
		}

		public double GetDoubleValue(string xmlName) {
			object obj = GetValue(xmlName, "System.Double");
			double value = 0;
			try {
				value = (double) obj;
			} catch {
			}
			return value;
		}
        
		public Int16 GetInt16Value(string xmlName) {
			object obj = GetValue(xmlName, "System.Int16");
			Int16 value = 0;
			try {
				value = (Int16) obj;
			} catch {
			}
			return value;
		}
        
		public Int32 GetInt32Value(string xmlName) {
			object obj = GetValue(xmlName, "System.Int32");
			Int32 value = 0;
			try {
				value = (Int32) obj;
			} catch {
			}
			return value;
		}
        
		public Int64 GetInt64Value(string xmlName) {
			object obj = GetValue(xmlName, "System.Int64");
			Int64 value = 0;
			try {
				value = (Int64) obj;
			} catch {
			}
			return value;
		}
        
		public sbyte GetSByteValue(string xmlName) {
			object obj = GetValue(xmlName, "System.SByte");
			sbyte value = 0;
			try {
				value = (sbyte) obj;
			} catch {
			}
			return value;
		}
        
		public double GetSingleValue(string xmlName) {
			object obj = GetValue(xmlName, "System.Single");
			double value = 0;
			try {
				value = (double) obj;
			} catch {
			}
			return value;
		}
        
		public UInt16 GetUInt16Value(string xmlName) {
			object obj = GetValue(xmlName, "System.UInt16");
			UInt16 value = 0;
			try {
				value = (UInt16) obj;
			} catch {
			}
			return value;
		}
        
		public UInt32 GetUInt32Value(string xmlName) {
			object obj = GetValue(xmlName, "System.UInt32");
			UInt32 value = 0;
			try {
				value = (UInt32) obj;
			} catch {
			}
			return value;
		}
        
		public UInt64 GetUInt64Value(string xmlName) {
			object obj = GetValue(xmlName, "System.UInt64");
			UInt64 value = 0;
			try {
				value = (UInt64) obj;
			} catch {
			}
			return value;
		}
        
		public DateTime GetDateTimeValue(string xmlName) {
			object obj = GetValue(xmlName, "System.DateTime");
			DateTime value = new DateTime(1900, 1, 1);
			try {
				value = (DateTime) obj;
			} catch {
			}
			return value;
		}
      
		public bool GetBooleanValue(string xmlName) {
			object obj = GetValue(xmlName, "System.Boolean");
			bool value = false;
			try {
				value = (bool) obj;
			} catch {
			}
			return value;
		}

		/// <summary>
		/// Sets the text value for an immediate child of the given name.
		/// If an element is not found, a new element is created
		/// </summary>
		/// <param name="xmlName">The element name to be found</param>
		/// <param name="value">object containing the value.
		/// In general, the ToString() value will be applied</param>
		public void SetValue(string xmlName, object value) {

			XmlNode node = GetSingleNode(xmlName);

			if (node != null && node.Name != Node.Name) {
				if (node.ChildNodes.Count == 0) {
					node.AppendChild(node.OwnerDocument.CreateTextNode(value.ToString()));
				} else {
					node.InnerText = value.ToString();
				}
			} else {
				XmlNode rootNode = Node;
				node = XMLUtils.AppendNewElement(ref rootNode, xmlName, value.ToString());
			}

		}

		public void AppendXMLObject(XMLObject obj) {
            
			XmlNode newXml = _doc.ImportNode(obj.Node, true);
			Node.AppendChild(newXml);

		}

		private XmlNode GetSingleNode(string xmlName) {

			XmlNode node = null;
            
			// first select the first element of the name xmlName
			try {
				node = Node.SelectSingleNode(xmlName);
			} catch {}

			if (node == null) {

				try {
                    
					string attrPath = "";
                    
					// if no elements match, then look for attributes
					if (xmlName.StartsWith("//")) {
						//Any attribute of this name
						attrPath = "//*[@" + xmlName + "]";
					} else {
						if (xmlName.StartsWith("/")) {
							//An attribute in the root element
							attrPath = "/*[@" + xmlName + "]";
						} else {
							//Assume an *attribute* for the current element
							attrPath = ".";
						}
					}
					node = Node.SelectSingleNode(attrPath);
				} catch {}
			}

			return node;

		}

		private XmlNodeList GetNodeList(string xmlName) {

			XmlNodeList nodes = null;

			nodes = XmlDoc.SelectNodes("//" + xmlName);

			return nodes;

		}

		/// <summary>
		///		This method will extract a value from the XML node. It first select the first
		///		xml element that match the xmlName specified in the first parameter. If there's
		///		no match, then it will select the first element that has the xmlName specified as
		///		its attribute. Then check to see which type it is, and parse/convert/box to that type.
		/// </summary>
		/// <param name="xmlName">the name of the xml element/attribute to match</param>
		/// <param name="proType">the string indicating which type the xmlName should be boxed to</param>
		/// <returns>the boxed object containing the value from the xml node</returns>
		public object GetValue(string xmlName, string proType) {

			string xmlValue = "";
			XmlNode node;

			try {

				node = GetSingleNode(xmlName);

				//Ignore if it can't be found
				if (node != null) {

					//First see if this is an attribute
					XmlNode attr = node.Attributes[xmlName];

					if (attr != null) {
						xmlValue = attr.Value.Trim();
					} else {
						//Take the text value of this node, as long as it isn't the root node
						//(That would give us the text equivalent of the whole XML)
						if (node != Node) {
							xmlValue = node.InnerText.Trim();
						}
					}
				}

				return ObjectFactory.CreateNewInstance(proType, xmlValue);
				
			} catch {}

			//Default value??
			return null;

		}

		private void LoadXml(string xml) {
			XmlDoc = new XmlDocument();
			XmlDoc.LoadXml(xml);
			_node = null;
		}

		/// <summary>
		/// Function provide detail about the passing object.
		/// </summary>
		public void LoadObject(object extObject) {

			string fieldValue = "";
			string fieldName = "";
			Type objectType;
			PropertyInfo[] infoArray;

			XmlNode doc = new XmlDocument();

			objectType = extObject.GetType();
			infoArray = objectType.GetProperties();

			XmlNode root = (XmlNode) XMLUtils.AppendNewElement(ref doc, objectType.Name);
            
			//Add each property as an element
			foreach(PropertyInfo info in infoArray) {
				try {
					fieldValue = info.GetValue(extObject, null).ToString();
					fieldName = info.Name;
					XMLUtils.AppendNewElement(ref root, fieldName, fieldValue);
				} catch (Exception e) {
					LogManager.GetLogger(GetType()).Debug(e);
				}
			}

			XmlDoc = (XmlDocument) doc;

		}
	}
}