using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Core.Domain;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Summary description for GenericBasketRules.
	/// </summary>
	public class GenericBasketRules : IBasketRules {

		public GenericBasketRules() {
		}

		#region IBasketRules Members

        public bool ShowPrices(User user) {
            return true;
		}

		public bool AllowAddToBasket(User user) {
            return true;
        }

        public bool AllowModifyBasket(User user, IBasket basket) {
            return (basket != null
                && (basket.UserDetails == null || (user != null && basket.UserDetails.Id == user.Id)));
        }

        public bool AllowPlaceOrder(User user) {
            return true;
        }
		#endregion
	}
}