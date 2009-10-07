using System;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Util.Promotion {

	/// <summary>
	/// Summary description for SimpleBasketPromotion.
	/// </summary>
	public class SimpleBasketPromotion : GenericPromotion, IBasketPromotion {
	
		public SimpleBasketPromotion() : base() {
		}

		public SimpleBasketPromotion(IPromotionData data) : base(data) {
		}

		#region IBasketPromotion Members

		public Money CalculatePriceReduction(IBasket basket) {
			return AllowedDiscount(GenericPromotion.CalculateDefaultPriceReduction(basket.SubTotal, 1, PromotionBase), basket);
		}

		#endregion

	}
}