using System;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Util.Promotion {

	/// <summary>
	/// Wraps a promotion so it can be extended easily
	/// </summary>
	public class GenericPromotion : IPromotion {

		private IPromotionData _data = null;
	
		public GenericPromotion() {
		}

		public GenericPromotion(IPromotion data) {
		}

		public IPromotionData PromotionBase {
			get {
				return _data;
			}
			set {
				_data = value;
			}
		}

		#region IPromotion Members		
		public static Money CalculateDefaultPriceReduction(Money originalPrice, int itemQuantity, IPromotionData data) {

			Money priceReduction = null;
			Money promotionPrice = new Money(originalPrice, 0);
			promotionPrice.Amount = data.PromotionAmount;
			promotionPrice.Multiply(itemQuantity);

			switch (data.PromotionAmountType) {
				case PromotionAmountType.PercentReduction:
					if (data.PromotionAmount <= 100 && data.PromotionAmount > 0) {
						priceReduction = new Money(originalPrice);
						priceReduction.RoundingMode = RoundingMode.AlwaysRoundUp;
						priceReduction.Multiply(data.PromotionAmount / 100);
					} else {
						priceReduction = originalPrice;
					}
					break;
				case PromotionAmountType.AbsolutePrice:
					priceReduction = new Money(originalPrice);
					priceReduction.Subtract(promotionPrice);
					break;
				case PromotionAmountType.AbsoluteReduction:
					priceReduction = promotionPrice;
					break;
				default:
					LogManager.GetLogger(typeof(GenericPromotion)).Error("Unknown PromotionAmountType");
					break;
			}

			return priceReduction;
		}

		public string Description {
			get {
				return PromotionBase.Description;
			}
			set {
				PromotionBase.Description = value;
			}
		}

		public bool AllowOtherPromotions {
			get {
				return PromotionBase.AllowOtherPromotions;
			}
			set {
				PromotionBase.AllowOtherPromotions = value;
			}
		}

		public bool IsEnabled {
			get {
				return PromotionBase.IsEnabled;
			}
			set {
				PromotionBase.IsEnabled = value;
			}
		}

		public DateTime EndDate {
			get {
				return PromotionBase.EndDate;
			}
			set {
				PromotionBase.EndDate = value;
			}
		}

		public DateTime StartDate {
			get {
				return PromotionBase.StartDate;
			}
			set {
				PromotionBase.StartDate = value;
			}
		}

		public int PromotionID {
			get {
				return PromotionBase.PromotionID;
			}
			set {
				PromotionBase.PromotionID = value;
			}
		}
		#endregion

		protected Money AllowedDiscount(Money discount, IBasket basket) {

			if (AllowOtherPromotions == false) {
				foreach (IBasketLine line in basket.BasketItemList) {
					//if (line.Promotion != null) {
					//	return new Money(discount, 0);
					//}
				}
			}

			return discount;
		}
	}
}