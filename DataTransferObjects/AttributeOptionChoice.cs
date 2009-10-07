using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Igentics.Common.ECommerce.Interfaces;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    public interface IOptionChoice {
        string AttributeID { get; set; }
        string OptionID { get; set; }
    }
    
    public class AttributeOptionChoice : IOptionChoice {

        private string _optionID;
        private string _optionText;
        private string _attributeID;
        private string _attributeName;

        public AttributeOptionChoice() {
        }

        public AttributeOptionChoice(string attributeId, string optionID) {
            AttributeID = attributeId;
            OptionID = optionID;
        }

        [XmlAttribute("id")]
        public string AttributeID {
            get { return _attributeID; }
            set { _attributeID = value; }
        }

        [XmlAttribute("attr-name")]
        public string AttributeName {
            get {
                return _attributeName;
            }
            set {
                _attributeName = value;
            }
        }

        [XmlAttribute("value")]
        public string OptionID {
            get {
                return _optionID;
            }
            set {
                _optionID = value; ;
            }
        }

        [XmlAttribute("option-name")]
        public string OptionText {
            get { return _optionText; }
            set { _optionText = value; }
        }
    }
}
