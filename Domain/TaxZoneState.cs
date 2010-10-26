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
	public class TaxZoneState 
	{
		#region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private short _stateid; 
		private TaxZone _taxzoneid; 
		private DateTime _inserttimestamp; 
		private DateTime _updatetimestamp; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public TaxZoneState()
		{
			_stateid = 0; 
			_taxzoneid =  null; 
			_inserttimestamp = DateTime.Now; 
			_updatetimestamp = DateTime.Now; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Internal Accessors for NHibernate
		
		/// <summary>
		/// 
		/// </summary>
		protected internal virtual short _Stateid
		{
			get { return _stateid; }
			set { _stateid = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		protected internal virtual TaxZone _TaxZoneid
		{
			get { return _taxzoneid; }
			set { _taxzoneid = value; }
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
		public virtual short Stateid
		{
			get { return _stateid; }
			set { _isChanged |= (_stateid != value); _stateid = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual TaxZone TaxZoneid
		{
			get { return _taxzoneid; }
			set { _isChanged |= (_taxzoneid != value); _taxzoneid = value; }
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
			TaxZoneState castObj = (TaxZoneState)obj; 
			return ( castObj != null ) &&
				( this._stateid == castObj.Stateid );
		}
		
		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			
			int hash = 57; 
			hash = 27 * hash * _stateid.GetHashCode();
			return hash; 
		}
		#endregion
		
	}
}
