using System;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Util.Interfaces {

	/// <summary>
	/// Summary description for IItemLine.
	/// </summary>
	public interface IItemLine : IItemDetails {
		int Quantity {get; set; }
	}
}