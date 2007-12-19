using System;
using Cuyahoga.Core.Domain;

namespace Cuyahoga.Modules.ECommerce.Util.Interfaces {

	/// <summary>
	/// Business rules to apply
	/// </summary>
	public interface IBasketRules {
		bool ShowPrices(User user);
        bool AllowAddToBasket(User user);
        bool AllowModifyBasket(User user, IBasket basket);
        bool AllowPlaceOrder(User user);
	}
}