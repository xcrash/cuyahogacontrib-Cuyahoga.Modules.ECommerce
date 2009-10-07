using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    /// <summary>
    /// Summary description for ProductSummary.
    /// </summary>
    [Serializable]
    public class ProductSummary : IItemDetails {

        private string _description = "";
        private string _itemCode = "";
        private string _productID;
        private List<Image> _productImages = new List<Image>();
        private string _name = "";

        public ProductSummary() {
        }

        public ProductSummary(string itemCode, string description) {
            ItemCode = itemCode;
            Description = description;
        }

        [XmlAttribute("id")]
        public string ProductID {
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

        public List<Image> ImageList {
            get {
                return _productImages;
            }
            set {
                _productImages = value;
            }
        }
    }
}
