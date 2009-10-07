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

        private List<ITrailItem> _breadCrumbTrail = new List<ITrailItem>();
        private IProduct _productDetails = null;
        private string _keyWords;

        public ProductView() {
		}

		/// <summary>
		/// A list of catalogue nodes from the root node up to this products parent node
		/// </summary>
		[XmlArrayItem(Type = typeof(TrailItem))]
        public List<ITrailItem> BreadCrumbTrail {
            get { return _breadCrumbTrail; }
            set { _breadCrumbTrail = value; }
        }

        public IProduct ProductDetails {
            get { return _productDetails; }
            set { _productDetails = value; }
        }

        public string Keywords {
            get { return _keyWords; }
            set { _keyWords = value; }
        }  
    }
}