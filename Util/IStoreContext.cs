using System;
using Cuyahoga.Core.Domain;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Util.Interfaces {

    public interface IStoreContext {

        string AppPath { get; }
        long BasketID { get; set; }
        string CurrencyCode { get; set; }
        User CurrentUser { get; }
        bool IsBasketEmpty { get; set; }
        long LastOrderID { get; set; }

        IBasket CurrentBasket { get; set;}
        IBasket LastOrder { get; set;}

        void NotifyBasketChanged(IBasket basket);
        void NotifyHideBasketSummary(IBasket basket);

        event WebStoreContext.BasketChangedHandler OnBasketChanged;
        event WebStoreContext.BasketChangedHandler OnHideBasketSummary;

        int StoreID { get; set; }

    }
}
