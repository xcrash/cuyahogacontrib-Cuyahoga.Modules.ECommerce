using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.UI;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    /// <summary>
    /// Summary description for Product.
    /// </summary>
    [Serializable]
    public class Product : ProductSummary, IStyleable, ITemplateable {

        private List<ProductGroup> _productGroupList;
        private List<Resource> _documentList;
        private List<AttributeGroup> _staticAttributeList;
        private List<ActiveProductAttribute> _activeAttributeList;
        private PriceAvailability _priceAvailability;
        
        private string _style = null;
        private string _template = null;

        public Product() {
        }

        public PriceAvailability PriceAvailability {
            get { return _priceAvailability; }
            set { _priceAvailability = value; }
        }

        [XmlArrayItem(Type = typeof(Resource), IsNullable = true)]
        public List<Resource> ResourceList {
            get { return _documentList; }
            set { _documentList = value; }
        }

        [XmlArrayItem(Type = typeof(AttributeGroup), IsNullable = true)]
        public List<AttributeGroup> AttributeGroupList {
            get { return _staticAttributeList; }
            set { _staticAttributeList = value; }
        }

        [XmlArrayItem(Type = typeof(ActiveProductAttribute), IsNullable = true)]
        public List<ActiveProductAttribute> ActiveAttributeList {
            get { return _activeAttributeList; }
            set { _activeAttributeList = value; }
        }

        [XmlArrayItem(Type = typeof(ProductGroup), IsNullable = true)]
        public List<ProductGroup> ProductGroupList {
            get { return _productGroupList; }
            set { _productGroupList = value; }
        }

        [XmlAttribute("style")]
        public string Style {
            get { return _style; }
            set { _style = value; }
        }

        [XmlAttribute("template")]
        public string Template {
            get { return _template; }
            set { _template = value; }
        }
    }
}
