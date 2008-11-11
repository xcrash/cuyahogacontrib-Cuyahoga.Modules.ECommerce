using System;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Util.Promotion {

	/// <summary>
	/// Summary description for FreeBasketDeliveryPromotion.
	/// </summary>
	public class FreeDeliveryBasketPromotion : GenericPromotion, IBasketPromotion {
	
		public FreeDeliveryBasketPromotion() : base() {
		}

		public FreeDeliveryBasketPromotion(IPromotionData data) : base(data) {
		}

		#region IBasketPromotion Members

		public Money CalculatePriceReduction(IBasket basket) {
			return AllowedDiscount(new BasketDecorator(basket).DeliveryPrice, basket);
		}
		#endregion

	}
}