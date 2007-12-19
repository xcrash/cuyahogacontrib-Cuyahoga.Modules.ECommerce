using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

    /// <summary>
    /// Summary description for Product.
    /// </summary>
    public class Product : ProductSummary, IProduct {

        private List<IProductGroup> _productGroupList = new List<IProductGroup>();
        private List<IRelatedDocument> _documentList = new List<IRelatedDocument>();
        private List<IProductAttribute> _staticAttributeList = new List<IProductAttribute>();
        private List<IActiveProductAttribute> _activeAttributeList = new List<IActiveProductAttribute>();
        private List<string> _featureList = new List<string>();
        private List<ISku> _skuList = new List<ISku>();
        private List<IRelatedProducts> _upSellList = new List<IRelatedProducts>();
        private List<IRelatedProducts> _crossSellList = new List<IRelatedProducts>();
        private string _priceDescription;
        private decimal _price;

        public Product() {
        }

        [XmlArrayItem(Type = typeof(RelatedProducts))]
        public List<IRelatedProducts> UpSellList {
            get { return _upSellList; }
            set { _upSellList = value; }
        }

        [XmlArrayItem(Type = typeof(RelatedProducts))]
        public List<IRelatedProducts> CrossSellList {
            get { return _crossSellList; }
            set { _crossSellList = value; }
        }

        [XmlArrayItem(Type = typeof(RelatedDocument))]
        public List<IRelatedDocument> DocumentList {
            get { return _documentList; }
            set { _documentList = value; }
        }

        [XmlArrayItem(Type = typeof(ProductAttribute))]
        public List<IProductAttribute> StaticAttributeList {
            get { return _staticAttributeList; }
            set { _staticAttributeList = value; }
        }

        [XmlArrayItem(Type = typeof(ActiveProductAttribute))]
        public List<IActiveProductAttribute> ActiveAttributeList {
            get { return _activeAttributeList; }
            set { _activeAttributeList = value; }
        }

        [XmlArrayItem(Type = typeof(string), ElementName = "Feature")]
        public List<string> FeatureList {
            get { return _featureList; }
            set { _featureList = value; }
        }

        [XmlArrayItem(Type = typeof(Sku), ElementName = "Sku")]
        public List<ISku> SkuList {
            get { return _skuList; }
            set { _skuList = value; }
        }

        [XmlArrayItem(Type = typeof(ProductGroup))]
        public List<IProductGroup> ProductGroupList {
            get { return _productGroupList; }
            set { _productGroupList = value; }
        }

        [XmlArrayItem(Type = typeof(decimal))]
        public decimal Price {
            get { return _price; }
            set { _price = value; }
        }

        [XmlArrayItem(Type = typeof(string))]
        public string PriceDescription {
            get { return _priceDescription; }
            set { _priceDescription = value; }
        }

        #region IAttributeSource Members
        public string GetValue(string referenceName) {
            return Domain.Product.GetProductAttributeValueByReference(ProductID, referenceName);
        }

        public void SetValue(string referenceName, string val) {
            Domain.Product.SetProductAttributeValueByReference(ProductID, referenceName, val);
        }
        #endregion
    }
}