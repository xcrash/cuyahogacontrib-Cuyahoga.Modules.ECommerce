using System;
using System.Xml.Serialization;

namespace Igentics.Common.ECommerce.DataTransferObjects {

	/// <summary>
	/// Summary description for ProductAttribute.
	/// </summary>
    [Serializable]
    public class Attribute : INameValue {

		private string _name;
		private string _value;
        private string _attributeID;

		public Attribute() {
		}

        public Attribute(string name, string attrValue) {
            this.Value = attrValue;
            this.Name = name;
            if (!string.IsNullOrEmpty(name)) {
                this.AttributeID = System.Text.RegularExpressions.Regex.Replace(name.ToLower().Trim(), "[^\\w]", "_").Replace("__", "_");
            }
        }

        public Attribute(string id, string name, string attrValue) : this(name, attrValue) {
            this.AttributeID = id;
        }

        [XmlAttribute("id")]
        public string AttributeID {
            get { return _attributeID; }
            set { _attributeID = value; }
        }

        public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

		public virtual string Value {
			get {
				return _value;
			}
			set {
				_value = value;
			}
		}
	}
}
