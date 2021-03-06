using System;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Util.Promotion {

	/// <summary>
	/// Provides a price reduction for a basket
	/// </summary>
	public interface IBasketPromotion : IPromotion {
		Money CalculatePriceReduction(IBasket basket);
	}

}