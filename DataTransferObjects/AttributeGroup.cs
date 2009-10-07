using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    public enum DisplayType {
        Tabular,
        List,
        Hidden
    }

    [Serializable]
    public class AttributeGroup {

        #region IAttributeGroup Members
        private string _name = "";
        private string _ID = "";
        private DisplayType _displayType;

        private List<Attribute> _attributeList;

        public AttributeGroup() {
        }

        public AttributeGroup(string name, string id) {
            _ID = id;
            _name = name;
            _attributeList = new List<Attribute>();
        }

        [XmlAttribute("display")]
        public DisplayType Display {
            get { return _displayType; }
            set { _displayType = value; }
        }

        [XmlAttribute("name")]
        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }

        [XmlAttribute("id")]
        public string AttributeID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }

        public List<Attribute> AttributeList {
            get { return _attributeList; }
            set { _attributeList = value; }
        }
        #endregion
    }
}
