using System.Collections;
using System.Collections.Generic;

using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

	public class ProductGroup : IProductGroup {

		public const string RELATIONSHIP_TYPE_ACCESSORY = "Accessory";
		public const string RELATIONSHIP_TYPE_RELATED_ITEM = "Related Item";

		private  string _name = "";
		private string _relationshipType = "";
        private List<IProductSummary> _productList = new List<IProductSummary>();

		public ProductGroup() {
		}

		[XmlAttribute]
		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

		[XmlAttribute]
		public string RelationshipType {
			get {
				return _relationshipType;
			}
			set {
				_relationshipType = value;
			}
		}

		[XmlArrayItem(Type = typeof(ProductSummary))]
        public List<IProductSummary> ProductList {
			get {
				return _productList;
			}
			set {
				_productList = value;
			}
		}
	}
}