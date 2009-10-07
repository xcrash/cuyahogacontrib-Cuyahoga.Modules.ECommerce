using System;
using System.Collections;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

    /// <summary>
    /// Summary description for ProductSummary.
    /// </summary>
    public class RelatedProducts : IRelatedProducts {

        private string _relationshipTypeID = "";
        private Image _image = new Image();
        private string _languageCode = "";
        private string _accessoryPartNo = "";
        private string _accessoryName = "";
        private string _accessoryDescription = "";
        private int _categoryID = 0;
        private string _mainPartNo = "";
        private string _category = "";
        private string _categoryDescription = "";

        public RelatedProducts() {
        }

        [XmlAttribute]
        public string MainPartNo {
            get {
                return _mainPartNo;
            }
            set {
                _mainPartNo = value;
            }
        }

        [XmlAttribute]
        public int CategoryID {
            get {
                return _categoryID;
            }
            set {
                _categoryID = value;
            }
        }

        [XmlAttribute]
        public string AccessoryPartNo {
            get {
                return _accessoryPartNo;
            }
            set {
                _accessoryPartNo = value;
            }
        }

        [XmlAttribute]
        public string AccessoryName {
            get {
                return _accessoryName;
            }
            set {
                _accessoryName = value;
            }
        }

        [XmlAttribute]
        public string RelationshipTypeID {
            get {
                return _relationshipTypeID;
            }
            set {
                _relationshipTypeID = value;
            }
        }

        [XmlAttribute]
        public Image Image {
            get {
                return _image;
            }
            set {
                _image = value;
            }
        }

        [XmlAttribute]
        public string LanguageCode {
            get {
                return _languageCode;
            }
            set {
                _languageCode = value;
            }
        }

        [XmlAttribute]
        public string AccessoryDescription {
            get {
                return _accessoryDescription;
            }
            set {
                _accessoryDescription = value;
            }
        }

        [XmlAttribute]
        public string Category {
            get {
                return _category;
            }
            set {
                _category = value;
            }
        }

        [XmlAttribute]
        public string CategoryDescription {
            get {
                return _categoryDescription;
            }
            set {
                _categoryDescription = value;
            }
        }
    }
}