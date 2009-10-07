using System;

using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Service;

namespace Cuyahoga.Modules.ECommerce.Web.Views {

	/// <summary>
	/// Summary description for BasketCommon.
	/// </summary>
	public class BasketCommon : GenericModuleControl {

        private ECommerceModule _emod;

		protected ICommerceService Commerce {
			get {
				return EModule.CommerceService;
			}
		}

        public ECommerceModule EModule {
            get {
                if (_emod == null) {
                    _emod = Module as ECommerceModule;
                }
                return _emod;
            }
        }

		protected BasketDecorator CurrentBasket {
			get {
                IBasket basket = EModule.CommerceService.GetCurrentBasket(WebStoreContext.Current);
                if (basket != null) {
                    return new BasketDecorator(basket);
                } else {
                    return null;
                }
			}
		}

		protected bool AllowAddToBasket() {
			return EModule.Rules.AllowAddToBasket(WebStoreContext.Current.CurrentUser);
		}

		protected bool AllowPlaceOrder() {
            return EModule.Rules.AllowPlaceOrder(WebStoreContext.Current.CurrentUser);
		}
	}
}
