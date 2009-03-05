using System;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

	/// <summary>
	/// Summary description for ProductResultSet.
	/// </summary>
	public class ProductResultSet : IProductResultSet {

        private ISearchMetaData _metaData = new SearchMetaData();
        private List<IProductSummary> _productList = new List<IProductSummary>();
        
        public ProductResultSet() {
		}

        public ISearchMetaData MetaData {
            get { return _metaData; }
            set { _metaData = value; }
        }

		[XmlArrayItem(Type = typeof(ProductSummary))]
        public List<IProductSummary> ProductList {
            get { return _productList; }
            set { _productList = value; }
        }
	}
}