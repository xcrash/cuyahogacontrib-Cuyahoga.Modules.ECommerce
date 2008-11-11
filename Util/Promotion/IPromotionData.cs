using System;

namespace Cuyahoga.Modules.ECommerce.Util.Promotion {

	/// <summary>
	/// Summary description for IPromotionData.
	/// </summary>
	public interface IPromotionData : IPromotion {

		int AuxData { get; set; }
		int PromotionAmount { get; set; }
		PromotionAmountType PromotionAmountType { get; set; }
	
	}
}