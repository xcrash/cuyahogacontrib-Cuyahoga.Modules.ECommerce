using System;
using System.Xml.Serialization;
using Igentics.Common.ECommerce.Interfaces;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    [Serializable]
    public class Option {

		private string _pickListValue = "";
		private string _shortCode = "";
        private decimal _price = 0;

		public Option() {
		}

		[XmlAttribute("text")]
		public string OptionText {
			get {
				return _pickListValue;
			}
			set {
				_pickListValue = value;
			}
		}

		[XmlAttribute("id")]
		public string OptionID {
			get {
				return _shortCode;
			}
			set {
				_shortCode = value;
			}
		}

        [XmlAttribute("price")]
        public decimal Price {
            get {
                return _price;
            }
            set {
                _price = value;
            }
        }
    }
}
