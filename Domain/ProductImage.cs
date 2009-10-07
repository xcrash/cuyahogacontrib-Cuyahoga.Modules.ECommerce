/*

insert license info here

*/

using System;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain {

    /// <summary>
    ///	Generated by MyGeneration using the Serdar's NHibernate Object Mapping 1.1 template (based on Gustavo's) - serdar@argelab.net
    /// </summary>
    [Serializable]
    public class ProductImage : IImage {

        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private int _imageid;
        private Product _productid;
        private string _imageurl;
        private short? _width;
        private short? _height;
        private string _alttext;
        private short _imagetype;
        private DateTime _inserttimestamp;
        private DateTime _updatetimestamp;
        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public ProductImage() {
            _imageid = 0;
            _productid = null;
            _imageurl = null;
            _width = 0;
            _height = 0;
            _alttext = null;
            _imagetype = 0;
            _inserttimestamp = DateTime.Now;
            _updatetimestamp = DateTime.Now;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Internal Accessors for NHibernate

        /// <summary>
        /// 
        /// </summary>
        internal virtual int _Imageid {
            get { return _imageid; }
            set { _imageid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual Product _Productid {
            get { return _productid; }
            set { _productid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual string _ImageUrl {
            get { return _imageurl; }
            set { _imageurl = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual short? _Width {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual short? _Height {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual string _AltText {
            get { return _alttext; }
            set { _alttext = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual short _ImageType {
            get { return _imagetype; }
            set { _imagetype = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual DateTime _Inserttimestamp {
            get { return _inserttimestamp; }
            set { _inserttimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal virtual DateTime _Updatetimestamp {
            get { return _updatetimestamp; }
            set { _updatetimestamp = value; }
        }

        #endregion // Internal Accessors for NHibernate

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>		
        public virtual int Imageid {
            get { return _imageid; }
            set { _isChanged |= (_imageid != value); _imageid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual Product ProductID {
            get { return _productid; }
            set { _isChanged |= (_productid != value); _productid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
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
        public virtual short Width {
            get { return (_width != null) ? Convert.ToInt16(_width) : (short) 0; }
            set { _isChanged |= (_width != value); _width = value; }
        }

        public bool WidthSpecified {
            get { return _width != null; }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual short Height {
            get { return (_height != null) ? Convert.ToInt16(_height) : (short) 0; }
            set { _isChanged |= (_height != value); _height = value; }
        }

        public bool HeightSpecified {
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

        /// <summary>
        /// 
        /// </summary>		
        public virtual short ImageType {
            get { return _imagetype; }
            set { _isChanged |= (_imagetype != value); _imagetype = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime Inserttimestamp {
            get { return _inserttimestamp; }
            set { _isChanged |= (_inserttimestamp != value); _inserttimestamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual DateTime Updatetimestamp {
            get { return _updatetimestamp; }
            set { _isChanged |= (_updatetimestamp != value); _updatetimestamp = value; }
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
            ProductImage castObj = (ProductImage)obj;
            return (castObj != null) &&
                (this._imageid == castObj.Imageid);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _imageid.GetHashCode();
            return hash;
        }
        #endregion
    }
}