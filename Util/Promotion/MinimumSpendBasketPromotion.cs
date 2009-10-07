using System;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Util.Promotion {

	/// <summary>
	/// Summary description for BasketMinimumSpendPromotion.
	/// </summary>
	public class MinimumSpendBasketPromotion : GenericPromotion, IBasketPromotion {

		public MinimumSpendBasketPromotion() : base() {
		}

		public MinimumSpendBasketPromotion(IPromotionData data) : base(data) {
		}

		#region IBasketPromotion Members

		public Money CalculatePriceReduction(IBasket basket) {

			//Spent a lot?
			if (basket.SubTotal.Amount > PromotionBase.AuxData) {
				return AllowedDiscount(GenericPromotion.CalculateDefaultPriceReduction(basket.SubTotal, 1, PromotionBase), basket);
			}

			//No?
			return new Money(basket.CurrencyCode, 0);
		}

		#endregion

	}
}