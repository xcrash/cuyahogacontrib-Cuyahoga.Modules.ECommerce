using System;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Util.Promotion {

	/// <summary>
	/// Defines how a promotion amount should be interpreted
	/// </summary>
	public enum PromotionAmountType : int {

		/// <summary>The amount does not affect the list price directly</summary>
		Ignored = 0,
		/// <summary>List price is reduced by this percentage</summary>
		PercentReduction = 1,
		/// <summary>This price replaces the list price</summary>
		AbsolutePrice = 2,
		/// <summary>List price is reduced by this absolute amount</summary>
		AbsoluteReduction = 3

	}
	
	/// <summary>
	/// Provides a price reduction for a basket
	/// </summary>
	public interface IPromotion {

		string Description { get; set; }
		DateTime StartDate { get; set; }
		DateTime EndDate { get; set; }
		bool IsEnabled { get; set; }
		bool AllowOtherPromotions { get; set; }
		int PromotionID { get; set; }
	
	}

}