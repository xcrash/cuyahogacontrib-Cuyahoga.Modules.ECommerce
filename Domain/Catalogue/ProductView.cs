using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

	/// <summary>
	/// Contains most of the information required to show a product catalogue page
	/// </summary>
	public class ProductView : IProductView {

        private List<ICatalogueNode> _breadCrumbTrail = new List<ICatalogueNode>();
        private IProduct _productDetails = null;
        private List<Domain.Product> _productsInFamily = null;
        public ProductView() {
		}

		/// <summary>
		/// A list of catalogue nodes from the root node up to this products parent node
		/// </summary>
		[XmlArrayItem(Type = typeof(CatalogueNode))]
        public List<ICatalogueNode> BreadCrumbTrail {
            get { return _breadCrumbTrail; }
            set { _breadCrumbTrail = value; }
        }

        public IProduct ProductDetails {
            get { return _productDetails; }
            set { _productDetails = value; }
        }

        public List<Domain.Product> ProductsInFamily {
            get {return  _productsInFamily; }
            set { _productsInFamily = value; }
        }

  
    }
}