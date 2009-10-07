using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

	/// <summary>
	/// Contains most of the information required to show a catalogue navigation page
	/// </summary>
	public class CategoryNodeView : ICategoryView {

		public CategoryNodeView() {
		}

        private ICategory _currentNode = null;
        private List<ITrailItem> _breadCrumbTrail = new List<ITrailItem>();
        private List<ICategory> _childNodes = new List<ICategory>();
        private List<IProductSummary> _productList = new List<IProductSummary>();
        private string _keyWords;

        public ICategory CurrentNode {
            get { return _currentNode; }
            set { _currentNode = value; }
        }

		/// <summary>
		/// A list of catalogue nodes from the root node up to this nodes parent
		/// </summary>
		[XmlArrayItem(Type = typeof(TrailItem))]
        public List<ITrailItem> BreadCrumbTrail {
            get { return _breadCrumbTrail; }
            set { _breadCrumbTrail = value; }
        }

		/// <summary>
		/// Children of this node, if any
		/// </summary>
		[XmlArrayItem(Type = typeof(CategoryNode))]
        public List<ICategory> ChildNodes {
            get { return _childNodes; }
            set { _childNodes = value; }
        }

		/// <summary>
		/// Products belonging to this catalogue node, if any
		/// </summary>
		[XmlArrayItem(Type = typeof(ProductSummary))]
        public List<IProductSummary> ProductList {
            get { return _productList; }
            set { _productList = value; }
        }

        public string Keywords {
            get { return _keyWords; }
            set { _keyWords = value; }
        }  
	}
}