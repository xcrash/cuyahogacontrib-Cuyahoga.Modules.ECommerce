using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Igentics.Common.ECommerce.DataTransferObjects;
using Igentics.Soa.Commerce.Core.Interfaces;

namespace Igentics.Common.ECommerce.Messages {

    /// <summary>
    /// Contains most of the information required to show a catalogue navigation page
    /// </summary>
    [Serializable]
    public class CategoryView : IKeywords {

        private Category _currentNode = null;
        private List<TrailItem> _breadCrumbTrail;
        private List<Category> _childNodes;
        private List<ProductSummary> _productList;
        private string _keyWords;

        public CategoryView() {
        }

        public string Keywords {
            get { return _keyWords; }
            set { _keyWords = value; }
        }

        public Category CurrentCategory {
            get { return _currentNode; }
            set { _currentNode = value; }
        }

        /// <summary>
        /// A list of catalogue nodes from the root node up to this nodes parent
        /// </summary>
        [XmlArrayItem(Type = typeof(TrailItem))]
        public List<TrailItem> BreadCrumbTrail {
            get { return _breadCrumbTrail; }
            set { _breadCrumbTrail = value; }
        }

        /// <summary>
        /// Children of this node, if any
        /// </summary>
        [XmlArrayItem(Type = typeof(Category), IsNullable = true)]
        public List<Category> ChildCategoryList {
            get { return _childNodes; }
            set { _childNodes = value; }
        }

        /// <summary>
        /// Products belonging to this catalogue node, if any
        /// </summary>
        [XmlArrayItem(Type = typeof(ProductSummary), ElementName = "Product", IsNullable = true)]
        public List<ProductSummary> ProductList {
            get { return _productList; }
            set { _productList = value; }
        }
    }
}
