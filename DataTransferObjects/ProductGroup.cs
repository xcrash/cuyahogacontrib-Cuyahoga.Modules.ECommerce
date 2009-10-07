using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.UI;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    [Serializable]
    public class ProductGroup : IHideable {

		public const string RELATIONSHIP_TYPE_ACCESSORY = "accessory";
        public const string RELATIONSHIP_TYPE_CROSS_SELL = "cross-sell";
        public const string RELATIONSHIP_TYPE_UP_SELL = "up-sell";

		private  string _name = "";
		private string _relationshipType = "";
        private List<ProductSummary> _productList = new List<ProductSummary>();
        private bool _isVisible = true;
        private bool _isVisibleSpecified;

		public ProductGroup() {
		}

        public ProductGroup(string name, string relationshipType) {
            _relationshipType = relationshipType;
            _name = name;
        }

        [XmlAttribute("visible")]
        public bool Visible {
            get { return _isVisible; }
            set { _isVisible = value; _isVisibleSpecified = !value; }
        }

        [XmlIgnore]
        public bool IsVisibleSpecified {
            get { return _isVisibleSpecified; }
            set { _isVisibleSpecified = value; }
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

		[XmlAttribute("type")]
		public string RelationshipType {
			get {
				return _relationshipType;
			}
			set {
				_relationshipType = value;
			}
		}

		[XmlArrayItem(Type = typeof(ProductSummary), ElementName = "Product")]
        public List<ProductSummary> ProductList {
			get {
				return _productList;
			}
			set {
				_productList = value;
			}
		}
	}
}
