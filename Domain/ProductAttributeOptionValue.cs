using System;
using System.Collections;
namespace Cuyahoga.Modules.ECommerce.Domain
{
	/// <summary>
	///	Generated by MyGeneration using the Serdar's NHibernate Object Mapping 1.1 template (based on Gustavo's) - serdar@argelab.net
	/// </summary>
	[Serializable]
	public class ProductAttributeOptionValue 
	{
		#region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private long _productid; 
		private long _optionvalueid; 
		private decimal _optionprice; 
		private string _optionvaluecode; 
		private short _sortorder;
        private AttributeOptionValue _optionDetails;
		private DateTime _inserttimestamp; 
		private DateTime _updatetimestamp;
      
      
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public ProductAttributeOptionValue()
		{
			_productid = 0; 
			_optionvalueid = 0; 
			_optionprice = 0; 
			_optionvaluecode = null; 
			_sortorder = 0; 
			_inserttimestamp = DateTime.Now;
            _updatetimestamp = DateTime.Now;
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Internal Accessors for NHibernate
		
		/// <summary>
		/// 
		/// </summary>
		internal virtual long _Productid
		{
			get { return _productid; }
			set { _productid = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		internal virtual long _OptionValueid
		{
			get { return _optionvalueid; }
			set { _optionvalueid = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		internal virtual decimal _OptionPrice
		{
			get { return _optionprice; }
			set { _optionprice = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		internal virtual string _OptionValueCode
		{
			get { return _optionvaluecode; }
			set { _optionvaluecode = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		internal virtual short _SortOrder
		{
			get { return _sortorder; }
			set { _sortorder = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		internal virtual DateTime _Inserttimestamp
		{
			get { return _inserttimestamp; }
			set { _inserttimestamp = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		internal virtual DateTime _Updatetimestamp
		{
			get { return _updatetimestamp; }
			set { _updatetimestamp = value; }
		}

 
		
		#endregion // Internal Accessors for NHibernate 

		#region Public Properties

     
		/// <summary>
		/// 
		/// </summary>		
		public virtual long ProductID
		{
			get { return _productid; }
			set { _isChanged |= (_productid != value); _productid = value; }
		}

        public virtual AttributeOptionValue OptionDetails {
            get { return _optionDetails; }
            set { _isChanged |= (_optionDetails != value); _optionDetails = value; }
        }
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual long OptionValueid
		{
			get { return _optionvalueid; }
			set { _isChanged |= (_optionvalueid != value); _optionvalueid = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual decimal OptionPrice
		{
			get { return _optionprice; }
			set { _isChanged |= (_optionprice != value); _optionprice = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string OptionValueCode
		{
			get { return _optionvaluecode; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for OptionValueCode", value, value.ToString());
				
				_isChanged |= (_optionvaluecode != value); _optionvaluecode = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual short SortOrder
		{
			get { return _sortorder; }
			set { _isChanged |= (_sortorder != value); _sortorder = value; }
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
		
		
		#region Equals And HashCode Overrides
		/// <summary>
		/// local implementation of Equals based on unique value members
		/// </summary>
		public override bool Equals( object obj )
		{
			if( this == obj ) return true;
			if( ( obj == null ) || ( obj.GetType() != this.GetType() ) ) return false;
			ProductAttributeOptionValue castObj = (ProductAttributeOptionValue)obj; 
			return ( castObj != null ) &&
				( this._productid == castObj.ProductID ) &&
				( this._optionvalueid == castObj.OptionValueid );
		}
		
		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			
			int hash = 57; 
			hash = 27 * hash * _productid.GetHashCode();
			hash = 27 * hash * _optionvalueid.GetHashCode();
			return hash; 
		}
		#endregion
		
	}
}