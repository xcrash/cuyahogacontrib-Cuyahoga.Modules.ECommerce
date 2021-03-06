

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
    public class Document  {

        #region Private Members
        private bool _isChanged;
        private bool _isDeleted;
        private long _documentid;
        private int _typeid;
        private string _documentname;
        private string _filepath;
        private bool _ispublished;
        private DateTime _inserttimestamp;
        private DateTime _updatetimestamp;
        private DocumentType _type;

        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public Document() {
            _documentid = 0;
            _typeid = 0;
            _type = null;
 
            _documentname = null;
            _filepath = null;
            _ispublished = true;
            _inserttimestamp = DateTime.Now;
            _updatetimestamp = DateTime.Now;
        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Internal Accessors for NHibernate

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual long _DocumentID {
            get { return _documentid; }
            set { _documentid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual string _DocumentName {
            get { return _documentname; }
            set { _documentname = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual string _FilePath {
            get { return _filepath; }
            set { _filepath = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal virtual bool _IsPublished {
            get { return _ispublished; }
            set { _ispublished = value; }
        }

        protected internal virtual int _TypeID {
            get { return _typeid; }
            set { _typeid = value; }
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
        #endregion // Internal Accessors for NHibernate

        #region Public Properties

        public virtual DocumentType Type {
            get { return _type; }
            set { _isChanged |= (_type != value); _type = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual long DocumentID {
            get { return _documentid; }
            set { _isChanged |= (_documentid != value); _documentid = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string DocumentName {
            get { return _documentname; }
            set {
                if (value != null)
                    if (value.Length > 128)
                        throw new ArgumentOutOfRangeException("Invalid value for DocumentName", value, value.ToString());

                _isChanged |= (_documentname != value); _documentname = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public virtual string FilePath {
            get { return _filepath; }
            set {
                if (value != null)
                    if (value.Length > 1024)
                        throw new ArgumentOutOfRangeException("Invalid value for FilePath", value, value.ToString());

                _isChanged |= (_filepath != value); _filepath = value;
            }
        }

      	
        public virtual bool IsPublished {
            get { return _ispublished; }
            set { _isChanged |= (_ispublished != value); _ispublished = value; }
        }


        public virtual int TypeID {
            get { return _typeid; }
            set { _isChanged |= (_typeid != value); _typeid = value; }
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
            Category castObj = (Category)obj;
            return (castObj != null) &&
                (this._documentid == castObj.CategoryID);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode() {

            int hash = 57;
            hash = 27 * hash * _documentid.GetHashCode();
            return hash;
        }
        #endregion
    }
}