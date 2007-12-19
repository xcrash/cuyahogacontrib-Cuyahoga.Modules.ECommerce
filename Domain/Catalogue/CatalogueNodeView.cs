using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

	/// <summary>
	/// Contains most of the information required to show a catalogue navigation page
	/// </summary>
	public class CatalogueNodeView : ICatalogueNodeView {

		public CatalogueNodeView() {
		}

        private ICatalogueNode _currentNode = null;
        private List<ICatalogueNode> _breadCrumbTrail = new List<ICatalogueNode>();
        private List<ICatalogueNode> _childNodes = new List<ICatalogueNode>();
        private List<IProductSummary> _productList = new List<IProductSummary>();
        

        public ICatalogueNode CurrentNode {
            get { return _currentNode; }
            set { _currentNode = value; }
        }

		/// <summary>
		/// A list of catalogue nodes from the root node up to this nodes parent
		/// </summary>
		[XmlArrayItem(Type = typeof(CatalogueNode))]
        public List<ICatalogueNode> BreadCrumbTrail {
            get { return _breadCrumbTrail; }
            set { _breadCrumbTrail = value; }
        }

		/// <summary>
		/// Children of this node, if any
		/// </summary>
		[XmlArrayItem(Type = typeof(CatalogueNode))]
        public List<ICatalogueNode> ChildNodes {
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


	}
}