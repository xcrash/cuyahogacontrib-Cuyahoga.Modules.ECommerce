using System;
using System.Xml.Serialization;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    public interface INameValue {
        string Name { get; set;}
        string Value { get; set; }
    }

    [Serializable]
    public class CustomField : INameValue {

        private string _name;
        private string _value;
        private bool _visibleSpecified;
        private bool _visible = true;

        public CustomField() {
        }

        public CustomField(string name, string value) {
            _name = name;
            _value = value;
            _visible = true;
        }

        [XmlAttribute("name")]
        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        [XmlAttribute("value")]
        public string Value {
            get { return _value; }
            set { _value = value; }
        }

        [XmlIgnore]
        public bool VisibleSpecified {
            get { return _visibleSpecified; }
            set { _visibleSpecified = value; }
        }

        [XmlAttribute("visible")]
        public bool Visible {
            get { return _visible; }
            set { _visible = value; _visibleSpecified = value; }
        }
    }
}
