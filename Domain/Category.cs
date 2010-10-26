using System;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using System.Collections;
namespace Cuyahoga.Modules.ECommerce.Domain {


    [Serializable]
    public class Category : IImage, ICategory{

        public const short SORT_ORDER_MIN = 1;

        #region Private Members
        private string _bannerImageurl;
        private bool _isChanged;
        private bool _isDeleted;
        private long _categoryid;
        private string _categoryname;
        private string _categorydescription;
        private Category _parentcategoryid;
        private short _sortorder;
        private bool _sortOrderSpecified;
        private bool _ispublished;

        private string _imageurl;
        private short? _width;
        private short? _height;
        private short _imageType;
        private string _alttext;
        private string _cssClass;
        private DateTime _inserttimestamp;
        private DateTime _updatetimestamp;
        private IList _links;

        private decimal? _priceChangePercent;
   
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public Category() {
            _categoryid = 0;
            _categoryname = null;
            _categorydescription = null;
            _parentcategoryid = null;

            _ispublished = true;
            _imageurl = null;
            _width = null;
            _height = null;
            _alttext = null;
            _cssClass = null;
            _inserttimestamp = DateTime.Now;
            _updatetimestamp = DateTime.Now;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Internal Accessors for NHibernate

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual long _Categoryid {
            get { return _categoryid; }
            set { _categoryid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual string _CategoryName {
            get { return _categoryname; }
            set { _categoryname = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual string _CategoryDescription {
            get { return _categorydescription; }
            set { _categorydescription = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual Category _ParentCategoryid {
            get { return _parentcategoryid; }
            set { _parentcategoryid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual short _SortOrder {
            get { return _sortorder; }
            set { _sortorder = value; }
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
        protected internal virtual string _ImageUrl {
            get { return _imageurl; }
            set { _imageurl = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        protected internal virtual short? _Width {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual short? _Height {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual string _AltText {
            get { return _alttext; }
            set { _alttext = value; }
        }

        protected internal virtual string _CssClass {
            get { return _cssClass; }
            set { _cssClass = value; }
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

        protected internal virtual string _BannerImageUrl {
            get { return _bannerImageurl; }
            set { _bannerImageurl = value; }
        }
        
    
        #endregion // Internal Accessors for NHibernate

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>		
        public virtual long CategoryID {
            get { return _categoryid; }
            set { _isChanged |= (_categoryid != value); _categoryid = value; }
        }

        public virtual decimal? PriceChangePercent {
            get { return _priceChangePercent; }
            set { _priceChangePercent = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string CategoryName {
            get { return _categoryname; }
            set {
                if (value != null)
                    if (value.Length > 128)
                        throw new ArgumentOutOfRangeException("Invalid value for CategoryName", value, value.ToString());

                _isChanged |= (_categoryname != value); _categoryname = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string CategoryDescription {
            get { return _categorydescription; }
            set {
                if (value != null)
                    if (value.Length > 1024)
                        throw new ArgumentOutOfRangeException("Invalid value for CategoryDescription", value, value.ToString());

                _isChanged |= (_categorydescription != value); _categorydescription = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual Category ParentCategory {
            get { return _parentcategoryid; }
            set { _isChanged |= (_parentcategoryid != value); _parentcategoryid = value; }
        }



        /// <summary>
        /// 
        /// </summary>		
        public virtual short SortOrder {
            get { return _sortorder; }
            set { _isChanged |= (_sortorder != value); _sortorder = value; }
        }

        [XmlIgnore]
        public virtual bool SortOrderSpecified {
            get { return _sortOrderSpecified; }
            set { _sortOrderSpecified = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual bool IsPublished {
            get { return _ispublished; }
            set { _isChanged |= (_ispublished != value); _ispublished = value; }
        }

        /// <summary>
        /// Returns whether or not the object has changed it's values.
        /// </summary>
        public virtual bool IsChanged {
            get { return _isChanged; }
        }

        /// <summary>
        /// Returns whether or not the object has changed it's values.
        /// </summary>
        public virtual bool IsDeleted {
            get { return _isDeleted; }
        }
        public virtual IList Links {
            get { return _links; }
            set { _links = value; }
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

        public virtual string BannerImageUrl {
            get { return _bannerImageurl; }
            set {
                if (value != null)
                    if (value.Length > 128)
                        throw new ArgumentOutOfRangeException("Invalid value for BannerImageUrl", value, value.ToString());

                _isChanged |= (_bannerImageurl != value); _bannerImageurl = value;
            }
        }

        public virtual string Url {
            get { return _imageurl; }
            set {
                if (value != null)
                    if (value.Length > 128)
                        throw new ArgumentOutOfRangeException("Invalid value for ImageUrl", value, value.ToString());

                _isChanged |= (_imageurl != value); _imageurl = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual short ImageType {
            get { return _imageType; }
            set { _isChanged |= (_imageType != value); _imageType = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual short Width {
            get { return (_width != null) ? Convert.ToInt16(_width) : (short) 0; }
            set { _isChanged |= (_width != value); _width = value; }
        }

        public virtual bool WidthSpecified {
            get { return _width != null; }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual short Height {
            get { return (_height != null) ? Convert.ToInt16(_height) : (short)0; }
            set { _isChanged |= (_height != value); _height = value; }
        }

        public virtual bool HeightSpecified {
            get { return _height != null; }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string AltText {
            get { return _alttext; }
            set {
                if (value != null)
                    if (value.Length > 128)
                        throw new ArgumentOutOfRangeException("Invalid value for AltText", value, value.ToString());

                _isChanged |= (_alttext != value); _alttext = value;
            }
        }

        public virtual string CssClass {
            get { return _cssClass; }
            set {
                if (value != null)
                    if (value.Length > 128)
                        throw new ArgumentOutOfRangeException("Invalid value for cssClass", value, value.ToString());

                _isChanged |= (_cssClass != value); _cssClass = value;
            }
        }

        public virtual string Style {
            get {
                return _cssClass;
            }
            set {
                _cssClass = value;
            }
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

        #endregion

        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj) {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            Category castObj = (Category)obj;
            return (castObj != null) &&
                (this._categoryid == castObj.CategoryID);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _categoryid.GetHashCode();
            return hash;
        }
        #endregion

        #region ICatalogueNode Members

        public virtual string Description {
            get {
                return CategoryDescription;
            }
            set {
                CategoryDescription = value;
            }
        }

        public virtual IImage Image {
            get {
                return this;
            }
            set {
                this.AltText = value.AltText;
                this.Height = value.Height;
                this.Url = value.Url;
                this.Width = value.Width;
                this.ImageType = value.ImageType;
            }
        }


        public virtual string Name {
            get {
                return CategoryName;
            }
            set {
                CategoryName = value;
            }
        }

        public virtual long NodeID {
            get {
                return CategoryID;
            }
            set {
                CategoryID = value;
            }
        }

        public virtual long ParentNodeID {
            get {
                if (ParentCategory != null) {
                    return ParentCategory._Categoryid;
                }
                return 0;
            }
            set {
                if (_parentcategoryid != null) {
                    _parentcategoryid._Categoryid = value;
                }
            }
        }
        #endregion
    }
}