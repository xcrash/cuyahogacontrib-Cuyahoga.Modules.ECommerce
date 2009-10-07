using System;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

	public class AttributeOption : IAttributeOption {

		private string _pickListValue = "";
		private string _shortCode = "";
        private decimal _price = 0;
        private string _url = "";

		public AttributeOption() {
		}

		[XmlAttribute]
		public string PickListValue {
			get {
				return _pickListValue;
			}
			set {
				_pickListValue = value;
			}
		}

		[XmlAttribute]
		public string ShortCode {
			get {
				return _shortCode;
			}
			set {
				_shortCode = value;
			}
		}

        [XmlAttribute]
        public decimal Price {
            get {
                return _price;
            }
            set {
                _price = value;
            }
        }

        [XmlAttribute]
        public string Url {
            get {
                return _url;
            }
            set {
                _url = value;
            }
        }

	}
}