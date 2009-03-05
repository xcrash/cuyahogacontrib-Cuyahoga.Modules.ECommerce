using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Igentics.Common.ECommerce.DataTransferObjects;
using Igentics.Soa.Commerce.Core.Interfaces;

namespace Igentics.Common.ECommerce.Messages {

	/// <summary>
	/// Contains most of the information required to show a product catalogue page
	/// </summary>
    [Serializable]
    public class ProductView : IKeywords {

        private List<BreadCrumbTrailContainer> _breadCrumbTrail;
        private Product _productDetails = null;
        private string _keyWords;

        public ProductView() {
        }

        public string Keywords {
            get { return _keyWords; }
            set { _keyWords = value; }
        }
        
		/// <summary>
		/// A list of catalogue nodes from the root node up to this products parent node
		/// </summary>
        [XmlArrayItem(Type = typeof(BreadCrumbTrailContainer))]
        public List<BreadCrumbTrailContainer> BreadCrumbTrailList {
            get { return _breadCrumbTrail; }
            set { _breadCrumbTrail = value; }
        }

        public Product ProductDetails {
            get { return _productDetails; }
            set { _productDetails = value; }
        } 
    }
}
