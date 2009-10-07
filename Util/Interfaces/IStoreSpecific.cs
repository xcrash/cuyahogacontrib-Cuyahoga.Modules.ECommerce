using System;

namespace Cuyahoga.Modules.ECommerce.Util.Interfaces {

	/// <summary>
	/// Shows this object has store-specific options
	/// </summary>
	public interface IStoreSpecific {
		int StoreID {get; set;}
	}
}