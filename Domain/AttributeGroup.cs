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
	public class AttributeGroup 
	{
		#region Private Members
		private bool _isChanged;
		private bool _isDeleted;
		private int _attributegroupid; 
		private string _attributegroupname; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public AttributeGroup()
		{
			_attributegroupid = 0; 
			_attributegroupname = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Internal Accessors for NHibernate
		
		/// <summary>
		/// 
		/// </summary>
		protected internal virtual int _AttributeGroupid
		{
			get { return _attributegroupid; }
			set { _attributegroupid = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		protected internal virtual string _AttributeGroupName
		{
			get { return _attributegroupname; }
			set { _attributegroupname = value; }
		}
		
		#endregion // Internal Accessors for NHibernate 

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual int AttributeGroupid
		{
			get { return _attributegroupid; }
			set { _isChanged |= (_attributegroupid != value); _attributegroupid = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public virtual string AttributeGroupName
		{
			get { return _attributegroupname; }
			set	
			{
				if ( value != null )
					if( value.Length > 50)
						throw new ArgumentOutOfRangeException("Invalid value for AttributeGroupName", value, value.ToString());
				
				_isChanged |= (_attributegroupname != value); _attributegroupname = value;
			}
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
			AttributeGroup castObj = (AttributeGroup)obj; 
			return ( castObj != null ) &&
				( this._attributegroupid == castObj.AttributeGroupid );
		}
		
		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			
			int hash = 57; 
			hash = 27 * hash * _attributegroupid.GetHashCode();
			return hash; 
		}
		#endregion
		
	}
}
