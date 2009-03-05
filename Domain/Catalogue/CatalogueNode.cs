using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

    [Serializable]
    public class TrailItem : ITrailItem {

        private string _nodeName;
        private long _nodeID;

        public TrailItem() {
        }

        public TrailItem(string name, long id) {
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
        public long NodeID {
            get {
                return _nodeID;
            }
            set {
                _nodeID = value;
            }
        }
    }

    /// <summary>
    /// Summary description for CatalogueNode.
    /// </summary>
    [Serializable]
    public class CategoryNode : TrailItem, ICategory {

        private long parentNodeID;
        private short sortOrder;
        private bool _sortOrderSpecified;
        private IImage nodeImage;
        private string nodeDescription;
        private string cssClass;
        private IList links;
        private IFlashAnimation nodeFlash;
        private string bannerImageUrl;

        public CategoryNode() {
        }

        public CategoryNode(string name, long id) {
            Name = name;
            NodeID = id;
        }

        [XmlAttribute]
        public string  BannerImageUrl {
            get {
                return bannerImageUrl;
            }
            set {
                bannerImageUrl = value;
            }
        }

        [XmlAttribute]
        public long ParentNodeID {
            get {
                return parentNodeID;
            }
            set {
                parentNodeID = value;
            }
        }

        public IImage Image {
            get {
                return nodeImage;
            }
            set {
                nodeImage = value;
            }
        }

        public IFlashAnimation Flash {
            get {
                return nodeFlash;
            }
            set {
                nodeFlash = value;
            }
        }

        [XmlAttribute]
        public string Description {
            get {
                return nodeDescription;
            }
            set {
                nodeDescription = value;
            }
        }

        [XmlAttribute]
        public string Style {
            get {
                return cssClass;
            }
            set {
                cssClass = value;
            }
        }

        [XmlAttribute]
        public short SortOrder {
            get {
                return sortOrder;
            }
            set {
                sortOrder = value;
            }
        }

        [XmlIgnore]
        public bool SortOrderSpecified {
            get { return _sortOrderSpecified; }
            set { _sortOrderSpecified = value; }
        }

        [XmlAttribute]
        public IList Links {
            get {

                return links;
            }
            set {
                links = value;
            }
        }
    }
}