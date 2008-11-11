using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Igentics.Common.ECommerce.DataTransferObjects {

	/// <summary>
	/// Summary description for ProductResultSet.
	/// </summary>
    [Serializable]
    public class ProductResultSet {

        private SearchMetaData _metaData = new SearchMetaData();
        private List<ProductSummary> _productList = new List<ProductSummary>();
        
        public ProductResultSet() {
		}

        public SearchMetaData MetaData {
            get { return _metaData; }
            set { _metaData = value; }
        }

		[XmlArrayItem(Type = typeof(ProductSummary), ElementName = "Product")]
        public List<ProductSummary> ProductList {
            get { return _productList; }
            set { _productList = value; }
        }
	}
}
