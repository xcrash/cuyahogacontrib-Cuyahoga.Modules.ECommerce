using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Domain {

    /// <summary>
    /// </summary>
    [Serializable]
    public class Product {

        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private long _productid;
        private string _itemcode;
        private string _productname;
        private string _productdescription;

        private string _shortproductdescription;
        private string _additionalInformation;
        private string _features;
        private int _stocklevel;
        private bool _ispublished;

        private decimal _baseprice;
        private string _basepricedescription = "";
        private DateTime _inserttimestamp;
        private DateTime _updatetimestamp;
        private IList _images;
        private IList _cats;
        private IList _attributes;
        private IList _skus;
        private IList _relatedProducts;
        private IList _documents;
        private IList _synonyms;

        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public Product() {

            _itemcode = null;
            _productname = null;
            _productdescription = null;
            _additionalInformation = null;
            _stocklevel = 0;
            _ispublished = false;
            _baseprice = 0;
            _inserttimestamp = DateTime.Now;
            _updatetimestamp = DateTime.Now;
            _images = null;
            _documents = null;
            _synonyms = null;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Internal Accessors for NHibernate

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual long _Productid {
            get { return _productid; }
            set { _productid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual string _ItemCode {
            get { return _itemcode; }
            set { _itemcode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual string _ProductName {
            get { return _productname; }
            set { _productname = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual string _ProductDescription {
            get { return _productdescription; }
            set { _productdescription = value; }
        }

        protected internal virtual string _AdditionalInformation {
            get { return _additionalInformation; }
            set { _additionalInformation = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual int _StockLevel {
            get { return _stocklevel; }
            set { _stocklevel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual bool _IsPublished {
            get { return _ispublished; }
            set { _ispublished = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual decimal _BasePrice {
            get { return _baseprice; }
            set { _baseprice = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        protected internal virtual DateTime _Inserttimestamp {
            get { return _inserttimestamp; }
            set { _inserttimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual DateTime _Updatetimestamp {
            get { return _updatetimestamp; }
            set { _updatetimestamp = value; }
        }

        protected internal virtual IList _Images {
            get { return _images; }
            set { _images = value; }
        }

        protected internal virtual IList _Documents {
            get { return _documents; }
            set { _documents = value; }
        }

        protected internal virtual IList _Synonyms {
            get { return _synonyms; }
            set { _synonyms = value; }
        }


        #endregion // Internal Accessors for NHibernate

        #region Public Properties

        public virtual IList Attributes {
            get { return _attributes; }
            set { _attributes = value; }
        }

        public virtual IList Categories {
            get { return _cats; }
            set { _cats = value; }
        }

        public virtual IList Skus {
            get { return _skus; }
            set { _skus = value; }
        }

        public virtual IList RelatedProducts {
            get { return _relatedProducts; }
            set { _relatedProducts = value; }
        }

        public virtual IList Documents {
            get { return _documents; }
            set { _documents = value; }
        }

        public virtual IList Synonyms {
            get { return _synonyms; }
            set { _synonyms = value; }
        }


        /// <summary>
        /// 
        /// </summary>		
        public virtual long ProductID {
            get { return _productid; }
            set { _isChanged |= (_productid != value); _productid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string ItemCode {
            get { return _itemcode; }
            set {
                if (value != null)
                    if (value.Length > 128)
                        throw new ArgumentOutOfRangeException("Invalid value for ItemCode", value, value.ToString());

                _isChanged |= (_itemcode != value); _itemcode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string ProductName {
            get { return _productname; }
            set {
                if (value != null)
                    if (value.Length > 128)
                        throw new ArgumentOutOfRangeException("Invalid value for ProductName", value, value.ToString());

                _isChanged |= (_productname != value); _productname = value;
            }
        }



        /// <summary>
        /// 
        /// </summary>		
        public virtual string Features {
            get { return _features; }
            set {
                if (value != null)
                    if (value.Length > 1024)
                        throw new ArgumentOutOfRangeException("Invalid value for Features", value, value.ToString());

                _isChanged |= (_features != value); _features = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string ProductDescription {
            get { return _productdescription; }
            set {
                if (value != null)
                    if (value.Length > 4000)
                        throw new ArgumentOutOfRangeException("Invalid value for ProductDescription", value, value.ToString());

                _isChanged |= (_productdescription != value); _productdescription = value;
            }
        }

        public virtual string ShortProductDescription {
            get { return _shortproductdescription; }
            set {
                if (value != null)
                    if (value.Length > 1024)
                        throw new ArgumentOutOfRangeException("Invalid value for ShortProductDescription", value, value.ToString());

                _isChanged |= (_shortproductdescription != value); _shortproductdescription = value;
            }
        }

        public virtual string AdditionalInformation {
            get { return _additionalInformation; }
            set {
                if (value != null)
                    if (value.Length > 1024)
                        throw new ArgumentOutOfRangeException("Invalid value for  _additionalInformation", value, value.ToString());

                _isChanged |= (_additionalInformation != value); _additionalInformation = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual int StockLevel {
            get { return _stocklevel; }
            set { _isChanged |= (_stocklevel != value); _stocklevel = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual bool IsPublished {
            get { return _ispublished; }
            set { _isChanged |= (_ispublished != value); _ispublished = value; }
        }


        /// <summary>
        /// 
        /// </summary>		
        public virtual decimal BasePrice {
            get { return _baseprice; }
            set { _isChanged |= (_baseprice != value); _baseprice = value; }
        }

        public virtual string BasePriceDescription {
            get { return _basepricedescription; }
            set { _isChanged |= (_basepricedescription != value); _basepricedescription = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime InsertTimestamp {
            get { return _inserttimestamp; }
            set { _isChanged |= (_inserttimestamp != value); _inserttimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime UpdateTimestamp {
            get { return _updatetimestamp; }
            set { _isChanged |= (_updatetimestamp != value); _updatetimestamp = value; }
        }

        /// <summary>
        /// Returns whether or not the object has changed it's values.
        /// </summary>
        public virtual bool IsChanged {
            get { return _isChanged; }
        }

        public virtual IList Images {
            get { return _images; }
            set { _images = value; }
        }

        /// <summary>
        /// Returns whether or not the object has changed it's values.
        /// </summary>
        public virtual bool IsDeleted {
            get { return _isDeleted; }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// mark the item as deleted
        /// </summary>
        public virtual void MarkAsDeleted() {
            _isDeleted = true;
            _isChanged = true;
        }


        #endregion

        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj) {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            Product castObj = (Product)obj;
            return (castObj != null) &&
                (this._productid == castObj.ProductID);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _productid.GetHashCode();
            return hash;
        }
        #endregion

        public virtual string GetProductAttributeValue(string attributeReference) {
            return GetProductAttributeValueByReference(ProductID, attributeReference);
        }

        public static string GetProductAttributeValueByReference(long productID, string attributeReference) {
            try {
                using (SpHandler sph = new SpHandler("getProductAttributeValueByReference", new SqlParameter("@productID", productID), new SqlParameter("@attributeReference", GetCleanReference(attributeReference)))) {

                    sph.ExecuteReader();

                    if (sph.DataReader.Read()) {
                        return sph.DataReader["optionname"] as string;
                    }
                }
            } catch (Exception e) {
                LogManager.GetLogger(typeof(Product)).Error(e);
            }

            return "";
        }

        public virtual void SetProductAttributeValue(string attributeReference, string attributeValue) {
            SetProductAttributeValueByReference(ProductID, attributeReference, attributeValue);
        }

        public  static void SetProductAttributeValueByReference(long productID, string attributeReference, string attributeValue) {
            try {
                using (SpHandler sph = new SpHandler("setProductAttributeValueByReference", new SqlParameter("@productID", productID), new SqlParameter("@attributeReference", GetCleanReference(attributeReference)), new SqlParameter("@attributeValue", attributeValue.Trim()))) {
                    sph.ExecuteNonQuery();
                }
            } catch (Exception e) {
                LogManager.GetLogger(typeof(Product)).Error(e);
            }
        }

        private static string GetCleanReference(string attributeReference) {
            return attributeReference.Trim().ToLower();
        }

        public virtual IProductSummary CreateProductSummary() {
            IProductSummary summary = new ProductSummary();
            PopulateProductSummary(summary);
            return summary;
        }

        public virtual void PopulateProductSummary(IProductSummary product) {

            product.Description = ProductDescription;
            product.ItemCode = ItemCode;
            product.Name = ProductName;
            product.ProductID = Convert.ToInt64(ProductID);
            product.PriceDescription = BasePriceDescription;
            product.Price = BasePrice;
            product.ShortDescription = ShortProductDescription;
            product.AdditionalInformation = AdditionalInformation;
            product.Features = Features;
        }

        public virtual IProductSummary CreateFullProductSummary() {

            IProductSummary product = CreateProductSummary();
            PopulateFullProductSummary(product);

            return product;
        }

        public virtual void PopulateFullProductSummary(IProductSummary product) {

            foreach (ProductSynonym s in Synonyms) {
                product.SynonymList.Add(s);
            }

            foreach (ProductImage epi in Images) {
                IImage image = new Domain.Catalogue.Image();
                ImageHelper.CopyImage(epi, image);
                product.ProductImages.Add(image);
            }

            if (StockLevel > 0) {
                product.StockedIndicator = 1;
            }
        }
    }
}