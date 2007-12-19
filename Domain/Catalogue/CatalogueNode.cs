using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

    /// <summary>
    /// Summary description for CatalogueNode.
    /// </summary>
    public class CatalogueNode : ICatalogueNode {

        private string nodeName;
        private long nodeID;
        private long parentNodeID;
        private short sortOrder;
        private IImage nodeImage;
        private string nodeDescription;
        private string cssClass;
        private IList links;
        private IList kits;

        private string _kitDescription;
        private string _kitPicture;
        private decimal? _priceChangePercent;

        public CatalogueNode() {
        }

        public CatalogueNode(string name, long id) {
            Name = name;
            NodeID = id;
        }

        [XmlAttribute]
        public decimal? PriceChangePercent {
            get { return _priceChangePercent; }
            set { _priceChangePercent = value; }
        }

        [XmlAttribute]
        public string Name {
            get {
                return nodeName;
            }
            set {
                nodeName = value;
            }
        }

        [XmlAttribute]
        public long NodeID {
            get {
                return nodeID;
            }
            set {
                nodeID = value;
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
        public string CssClass {
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

        [XmlAttribute]
        public IList Links {
            get {

                return links;
            }
            set {
                links = value;
            }
        }

        [XmlAttribute]
        public IList Kits {
            get {

                return kits;
            }
            set {
                kits = value;
            }
        }


        [XmlAttribute]
        public string KitDescription {
            get {
                return _kitDescription;
            }
            set {
                _kitDescription = value;
            }
        }

        [XmlAttribute]
        public string KitPicture {
            get {
                return _kitPicture;
            }
            set {
                _kitPicture = value;
            }
        }

        public static bool IsRootNode(ICatalogueNodeView nodeView) {
            return (nodeView.BreadCrumbTrail.Count == 1);
        }
        public static bool IsSubCategory(ICatalogueNodeView nodeView) {
            return (nodeView.BreadCrumbTrail.Count == 2);
        }
        public static bool IsSubSubCategory(ICatalogueNodeView nodeView) {
            return (nodeView.BreadCrumbTrail.Count == 3);
        }
        public static bool IsProductList(ICatalogueNodeView nodeView) {
            return (nodeView.BreadCrumbTrail.Count == 4);
        }
        public static bool IsCategory(ICatalogueNodeView nodeView) {
            return (IsRootNode(nodeView) || IsSubCategory(nodeView) || IsSubSubCategory(nodeView));
        }
    }
}