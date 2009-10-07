using System;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Util.Promotion {

	/// <summary>
	/// Summary description for BuyNGetOneFreeBasketLinePromotion.
	/// </summary>
	public class BuyNGetOneFreeBasketLinePromotion : GenericPromotion, IBasketLinePromotion {

		public BuyNGetOneFreeBasketLinePromotion() : base() {
		}

		public BuyNGetOneFreeBasketLinePromotion(IPromotionData data) : base(data) {
		}

		#region IBasketLinePromotion Members

		public Money CalculatePriceReduction(IBasketLine basketLine) {
			
			/*
			 * Buy N, get one free
			 * i.e. Buy 4, pay for 3
			 */
			try {
				int quantity = basketLine.Quantity / (PromotionBase.AuxData + 1);

				//See how many multiples we have
				if (quantity * (PromotionBase.AuxData + 1) > basketLine.Quantity) {
					quantity--;
				}

				if (quantity > 0) {

					Money discount = new Money(basketLine.LinePrice);
					discount.RoundingMode = RoundingMode.AlwaysRoundUp;

					discount.Divide(basketLine.Quantity);
					discount.Multiply(quantity);

					return discount;

				} else {

					return new Money(0);

				}

			} catch (Exception e) {
				LogManager.GetLogger(GetType()).Error(e);
				return new Money(0);
			}
		}

		#endregion

	}
}