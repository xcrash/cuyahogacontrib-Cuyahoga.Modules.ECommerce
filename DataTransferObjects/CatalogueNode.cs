using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Igentics.Soa.Commerce.Core.Interfaces;
using Cuyahoga.Modules.ECommerce.UI;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    [Serializable]
    public class TrailItem {
        
        private string _nodeName;
        private string _nodeID;

        public TrailItem() {
        }

        public TrailItem(string name, string id) {
            Name = name;
            NodeID = id;
        }

        [XmlAttribute("name")]
        public string Name {
            get {
                return _nodeName;
            }
            set {
                _nodeName = value;
            }
        }

        [XmlAttribute("id")]
        public string NodeID {
            get {
                return _nodeID;
            }
            set {
                _nodeID = value;
            }
        }
    }

    /// <summary>
    /// Summary description for Category.
    /// </summary>
    public class Category : TrailItem, IStyleable, ITemplateable, ISortable {

        private string _parentNodeID;
        private short _sortOrder;
        private bool _sortOrderSpecified;
        private Image _nodeImage;
        private string _nodeDescription;
        private string _style = null;
        private string _template = null;

        private List<CustomField> _customFieldList;
        
        public Category() : base() {
        }

        public Category(string name, string id) : base(name, id) {
        }

        [XmlArrayItem(Type = typeof(CustomField))]
        public List<CustomField> CustomFieldList {
            get { return _customFieldList; }
            set { _customFieldList = value; }
        }

        [XmlIgnore]
        public string ParentNodeID {
            get {
                return _parentNodeID;
            }
            set {
                _parentNodeID = value;
            }
        }

        public Image Image {
            get {
                return _nodeImage;
            }
            set {
                _nodeImage = value;
            }
        }

        [XmlAttribute("description")]
        public string Description {
            get {
                return _nodeDescription;
            }
            set {
                _nodeDescription = value;
            }
        }

        [XmlAttribute("style")]
        public string Style {
            get { return _style; }
            set { _style = value; }
        }

        [XmlAttribute("template")]
        public string Template {
            get { return _template; }
            set { _template = value; }
        }

        [XmlAttribute("order")]
        public short SortOrder {
            get {
                return _sortOrder;
            }
            set {
                _sortOrder = value;
            }
        }

        [XmlIgnore]
        public bool SortOrderSpecified {
            get { return _sortOrderSpecified; }
            set { _sortOrderSpecified = value; }
        }
    }
}
