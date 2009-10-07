using System;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Util.Promotion {

	/// <summary>
	/// Summary description for SimpleProductPromotion.
	/// </summary>
	public class SimpleBasketLinePromotion : GenericPromotion, IBasketLinePromotion {

		public SimpleBasketLinePromotion() : base() {
		}

		public SimpleBasketLinePromotion(IPromotionData data) : base(data) {
		}

		#region IBasketLinePromotion Members

		public Money CalculatePriceReduction(IBasketLine basketLine) {
			return GenericPromotion.CalculateDefaultPriceReduction(basketLine.LinePrice, basketLine.Quantity, PromotionBase);
		}

		#endregion
	}
}