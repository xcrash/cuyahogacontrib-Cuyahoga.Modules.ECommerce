/*

insert license info here

*/

using System;

namespace Cuyahoga.Modules.ECommerce.Domain
{
	/// <summary>
	///	Generated by MyGeneration using the Serdar's NHibernate Object Mapping 1.1 template (based on Gustavo's) - serdar@argelab.net
	/// </summary>
	[Serializable]
	public class TaxZone 
	{
		#region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private short _taxzoneid; 
		private string _taxzonename; 
		private DateTime _inserttimestamp; 
		private DateTime _updatetimestamp; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public TaxZone()
		{
			_taxzoneid = 0; 
			_taxzonename = null; 
			_inserttimestamp = DateTime.Now; 
			_updatetimestamp = DateTime.Now; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Internal Accessors for NHibernate
		
		/// <summary>
		/// 
		/// </summary>
		protected internal virtual short _TaxZoneid
		{
			get { return _taxzoneid; }
			set { _taxzoneid = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		protected internal virtual string _TaxZoneName
		{
			get { return _taxzonename; }
			set { _taxzonename = value; }
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

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual short TaxZoneid
		{
			get { return _taxzoneid; }
			set { _isChanged |= (_taxzoneid != value); _taxzoneid = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string TaxZoneName
		{
			get { return _taxzonename; }
			set	
			{
				if ( value != null )
					if( value.Length > 128)
						throw new ArgumentOutOfRangeException("Invalid value for TaxZoneName", value, value.ToString());
				
				_isChanged |= (_taxzonename != value); _taxzonename = value;
			}
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
			TaxZone castObj = (TaxZone)obj; 
			return ( castObj != null ) &&
				( this._taxzoneid == castObj.TaxZoneid );
		}
		
		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			
			int hash = 57; 
			hash = 27 * hash * _taxzoneid.GetHashCode();
			return hash; 
		}
		#endregion
		
	}
}
