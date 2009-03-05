using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {

	/// <summary>
	/// Determines the pre-tax price for this basket
	/// </summary>
	public class SubtotalOrderProcessor : IOrderProcessor {

        public SubtotalOrderProcessor() {
		}

		public void Process(IBasket basket) {

			basket.SubTotal.Amount = 0;

			foreach (IBasketLine line in basket.BasketItemList) {
				switch (line.ItemType) {
					case BasketItemType.CreditNote:
					case BasketItemType.Discount:
					case BasketItemType.Voucher:
					case BasketItemType.OtherCredit:
						basket.SubTotal.Subtract(line.LinePrice);
						break;
					default:
						basket.SubTotal.Add(line.LinePrice);
						break;
				}
			}
		}
	}
}