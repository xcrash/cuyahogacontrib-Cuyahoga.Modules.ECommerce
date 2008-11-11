using System;

namespace Cuyahoga.Modules.ECommerce.Util.Enums {

	public enum PricingStatus : int {
		/// <summary>No attempt made yet to get price</summary>
		NotChecked = 0,
		/// <summary>Pricing returned from back office</summary>
		OK = 1,
		/// <summary>Item not found in back office</summary>
		NotFound = 2,
		/// <summary>Item found, but no price availabile</summary>
		NotAvailable = 3,
		/// <summary>Back office not available</summary>
		BackOfficeDown = 4,
		/// <summary>Back office not available</summary>
		Obsolete = 5,
		/// <summary>This item has been replaced by another item (supplied)</summary>
		Replaced = 6,
		/// <summary>This item is found, but has a zero list and/or nett price</summary>
		ZeroPrice = 7
	}
}