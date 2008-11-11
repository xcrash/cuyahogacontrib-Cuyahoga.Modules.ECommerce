using System;
using System.Xml.Serialization;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    /// <summary>
    /// Summary description for Image.
    /// </summary>
    [Serializable]
    public class Image {

        private string _url = "";
        private string _altText = "";
        private short _width;
        private bool _widthSpecified;
        private short _height;
        private bool _heightSpecified;
        private string _resourceID;

        public Image() {
        }

        public Image(string url, string altText) {
            Url = url;
            Description = altText;
        }

        [XmlAttribute("id")]
        public string ResourceID {
            get { return _resourceID; }
            set { _resourceID = value; }
        }

        [XmlAttribute("url")]
        public string Url {
            get {
                return _url;
            }
            set {
                _url = value;
            }
        }

        [XmlAttribute("alt")]
        public string Description {
            get {
                return _altText;
            }
            set {
                _altText = value;
            }
        }

        [XmlAttribute("width")]
        public short Width {
            get {
                return _width;
            }
            set {
                _width = value;
                _widthSpecified = (value > 0);
            }
        }

        [XmlIgnore]
        public bool WidthSpecified {
            get { return _widthSpecified; }
            set { _widthSpecified = value; }
        }

        [XmlAttribute("height")]
        public short Height {
            get {
                return _height;
            }
            set {
                _height = value;
                _heightSpecified = (value > 0);
            }
        }

        [XmlIgnore]
        public bool HeightSpecified {
            get { return _heightSpecified; }
            set { _heightSpecified = value; }
        }
    }
}
