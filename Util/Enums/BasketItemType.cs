using System;

namespace Cuyahoga.Modules.ECommerce.Util.Enums {

	public enum BasketItemType {
		NotSet = 0,
		/// <summary>Standard physical item</summary>
		StandardItem = 1,
		/// <summary>Charge for delivery</summary>
		DeliveryCharge = 2,
		/// <summary>Amount charged when standard item total is below a threshold</summary>
		SmallOrderCharge = 3,
		/// <summary>Amount charged when adminstrators intervene in a purchase</summary>
		AdminSurcharge = 4,
		/// <summary>Amount deducted from the standard item total</summary>
		Discount = 5,
		/// <summary>Amount charged when standard item total is below a threshold, bringing it up to the threshold value</summary>
		MinimumOrderCharge = 7,
		/// <summary>Voucher whose value is deducted from the total</summary>
		Voucher = 8,
		/// <summary>Trade based refund</summary>
		CreditNote = 9,
		/// <summary>Miscellaneous credit whose value is deducted from the total</summary>
		OtherCredit = 10,
		/// <summary>Miscellaneous charge</summary>
		OtherCharge = 11
	}
}