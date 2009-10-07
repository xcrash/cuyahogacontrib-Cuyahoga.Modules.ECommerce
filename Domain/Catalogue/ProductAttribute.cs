using System;
using System.Collections;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

	/// <summary>
	/// Summary description for ProductAttribute.
	/// </summary>
	public class ProductAttribute : IProductAttribute {

		public const string DATA_TYPE_STRING = "string";
		public const string DATA_TYPE_BOOLEAN = "boolean";
		public const string DATA_TYPE_NUMERIC = "number";

		private  string _name = "";
		private  string _value = "";
		private  string _baseUnit = "";
		private  string _type = "";
        private IAttributeGroup _group = new AttributeGroup();
		public ProductAttribute() {
		}


		public ProductAttribute(string name, string attrValue, string attrType) {
			this.DataType = attrType;
			this.Value = attrValue;
			this.Name = name;
		}

		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

        public IAttributeGroup Group {
            get {
                return _group;
            }
            set {
                _group = value;
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

		public string BaseUnit {
			get {
				return _baseUnit;
			}
			set {
				_baseUnit = value;
			}
		}

		public string DataType {
			get {
				return _type;
			}
			set {
				_type = value;
			}
		}
	}
}