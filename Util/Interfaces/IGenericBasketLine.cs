using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Enums;

namespace Cuyahoga.Modules.ECommerce.Util.Interfaces {

	public interface IBasketLine : IItemLine {

		/// <summary>
		/// Database reference for this object
		/// </summary>
		long BasketItemID {get; set;}
		
		/// <summary>
		/// The total price before tax for this object
		/// </summary>
		Money LinePrice {get; set;}

        /// <summary>
        /// The total price before tax for this object
        /// </summary>
        Money TaxPrice { get; set;}
        
        /// <summary>
		/// The total price before tax for this object per pricing unit
		/// </summary>
		Money UnitLinePrice {get; set;}

		/// <summary>
		/// Identifies type of item: standard, tax, charge, discount etc
		/// </summary>
		BasketItemType ItemType {get; set;}

		/// <summary>
		/// Identifies whether this item has been priced correctly
		/// </summary>
		PricingStatus Status {get; set;}

	}
}