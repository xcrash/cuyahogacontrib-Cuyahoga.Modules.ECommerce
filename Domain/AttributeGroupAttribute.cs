

using System;
using System.Collections;
namespace Cuyahoga.Modules.ECommerce.Domain
{
	/// <summary>
	///	Generated by MyGeneration using the Serdar's NHibernate Object Mapping 1.1 template (based on Gustavo's) - serdar@argelab.net
	/// </summary>
	[Serializable]
	public class AttributeGroupAttribute 
	{
		#region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private short _attributegroupid; 
		private int _attributeid; 
		private DateTime _inserttimestamp; 
		private DateTime _updatetimestamp;
        private Attribute _attribute;
        private AttributeGroup _attributeGroup;
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public AttributeGroupAttribute()
		{
			_attributegroupid = 0; 
			_attributeid = 0; 
			_inserttimestamp = DateTime.Now; 
			_updatetimestamp = DateTime.Now;
            _attribute = null;
            _attributeGroup = null;
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		

		#region Public Properties

        public virtual Attribute Attribute {
            get { return _attribute; }
            set { _attribute = value; }
        }

        public virtual AttributeGroup AttributeGroup{
            get { return _attributeGroup; }
            set { _attributeGroup = value; }
        }	


		/// <summary>
		/// 
		/// </summary>		
		public virtual short AttributeGroupid
		{
			get { return _attributegroupid; }
			set { _attributegroupid = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual int Attributeid
		{
			get { return _attributeid; }
			set { _isChanged |= (_attributeid != value); _attributeid = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime Inserttimestamp
		{
			get { return _inserttimestamp; }
			set { _isChanged |= (_inserttimestamp != value); _inserttimestamp = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual DateTime Updatetimestamp
		{
			get { return _updatetimestamp; }
			set { _isChanged |= (_updatetimestamp != value); _updatetimestamp = value; }
		}
			
		/// <summary>
		/// Returns whether or not the object has changed it's values.
		/// </summary>
		public virtual bool IsChanged
		{
			get { return _isChanged; }
		}
		
		/// <summary>
		/// Returns whether or not the object has changed it's values.
		/// </summary>
		public virtual bool IsDeleted
		{
			get { return _isDeleted; }
		}
		
		#endregion 
		
		
		#region Public Functions
		
		/// <summary>
		/// mark the item as deleted
		/// </summary>
		public virtual void MarkAsDeleted()
		{
			_isDeleted = true;
			_isChanged = true;
		}
		
		
		#endregion
		#region Internal Accessors for NHibernate
				
				/// <summary>
				/// 
				/// </summary>
				protected internal virtual short _AttributeGroupid
				{
					get { return _attributegroupid; }
					set { _attributegroupid = value; }
				}
				
				/// <summary>
				/// 
				/// </summary>
				protected internal virtual int _Attributeid
				{
					get { return _attributeid; }
					set { _attributeid = value; }
				}
				
				/// <summary>
				/// 
				/// </summary>
				protected internal virtual DateTime _Inserttimestamp
				{
					get { return _inserttimestamp; }
					set { _inserttimestamp = value; }
				}
				
				/// <summary>
				/// 
				/// </summary>
				protected internal virtual DateTime _Updatetimestamp
				{
					get { return _updatetimestamp; }
					set { _updatetimestamp = value; }
				}
				
		#endregion // Internal Accessors for NHibernate 
		
                #region Equals And HashCode Overrides
                /// <summary>
                /// local implementation of Equals based on unique value members
                /// </summary>
                public override bool Equals(object obj) {
                    if (this == obj) return true;
                    if ((obj == null) || (obj.GetType() != this.GetType())) return false;
                    AttributeGroupAttribute castObj = (AttributeGroupAttribute)obj;
                    return (castObj != null) &&
                        (this._attributegroupid == castObj.AttributeGroupid) &&
                        (this._attributeid == castObj.Attributeid);
                }

                /// <summary>
                /// local implementation of GetHashCode based on unique value members
                /// </summary>
                public override int GetHashCode() {

                    int hash = 57;
                    hash = 27 * hash * _attributegroupid.GetHashCode();
                    hash = 27 * hash * _attributeid.GetHashCode();
                    return hash;
                }
                #endregion
		
	}
}
