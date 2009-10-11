using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

    /// <summary>
    /// Summary description for ProductSummary.
    /// </summary>
    public class ProductSummary : IProductSummary {

        private string _description = "";
        private string _shortDescription = "";
        private string _itemCode = "";
        private long _productID = 0;
        private List<IImage> _productImages = new List<IImage>();
        private string _name = "";
        private short _stockedIndicator = 0;
        private string _additionalInformation = "";
        private string _features = "";
        private string _productGroup = "";
        private List<ProductSynonym> _synonymList = new List<ProductSynonym>();
        private string _priceDescription;
        private decimal _price;

        public ProductSummary() {
        }

        public ProductSummary(string itemCode, string description) {
            ItemCode = itemCode;
            Description = description;
        }

        public List<ProductSynonym> SynonymList {
            get { return _synonymList; }
            set { _synonymList = value; }
        }

        [XmlAttribute]
        public long ProductID {
            get {
                return _productID;
            }
            set {
                _productID = value;
            }
        }

        public string Description {
            get { return _description; }
            set { _description = value; }
        }

        public string ShortDescription {
            get { return _shortDescription; }
            set { _shortDescription = value; }
        }


        public string AdditionalInformation {
            get { return _additionalInformation; }
            set { _additionalInformation = value; }
        }

        public string Features {
            get { return _features; }
            set { _features = value; }
        }

        public string Name {
            get { return _name; }
            set { _name = value; }
        }
        
        /// <summary>
        /// Either a real part number for a static part, 
        /// or a particular instance of a configured active part
        /// </summary>
        public string ItemCode {
            get {
                return _itemCode;
            }
            set {
                _itemCode = value;
            }
        }

        public string ProductGroup {
            get {
                return _productGroup;
            }
            set {
                _productGroup = value;
            }
        }

        public List<IImage> ProductImages {
            get {
                return _productImages;
            }
            set {
                _productImages = value;
            }
        }

        public short StockedIndicator {
            get {
                return _stockedIndicator;
            }
            set {
                _stockedIndicator = value;
            }
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
    }
}