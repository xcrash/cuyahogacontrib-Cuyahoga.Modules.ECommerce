using System;
using System.Collections.Generic;

using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Service.Translation;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service {
    
    public interface ICommerceService {

        /*
        IBasketLine AddItem(IStoreContext context, string itemCode, IList<IAttributeSelection> optionList, int quantity);
        IBasketLine AddItem(IStoreContext context, string itemCode, int quantity);
        IBasketLine AddItem(IBasket basket, string itemCode, int quantity);
         */
        //IBasketLine AddItem(IStoreContext context, Product product, IList<IAttributeSelection> optionList, int quantity);
        IBasketLine AddItem(IStoreContext context, Product product, int quantity);
        IBasketLine AddItem(IStoreContext context, long productID, IList<IAttributeSelection> optionList, int quantity);

        void RemoveItem(IStoreContext context, long lineID);

        void AmendItemQuantity(IStoreContext context, long lineID, int newQuantity);

        void ChangeCurrencyCode(IStoreContext context, string currencyCode);
        //void ChangeCurrencyCode(IBasket basket, string currencyCode);

        bool CloseCurrentOrder(IStoreContext context, ITextTranslator translator);
        bool CloseOrder(IBasket order, ITextTranslator translator);

        bool SubmitCurrentOrder(IStoreContext context, ITextTranslator translator);
        bool SubmitOrder(IBasket order, ITextTranslator translator);

        //IBasket CreateBasket(IStoreContext context);
        IBasket GetOrCreateBasket(IStoreContext context);
        //IBasket GetBasket(long basketID);
        IBasket GetCurrentBasket(IStoreContext context);
        IBasket GetLastOrder(IStoreContext context);

        void EmptyBasket(IStoreContext context);


        /// <summary>
        /// Recalculate relevant costs, charges etc and save these changes
        /// </summary>
        /// <param name="context"></param>
        //void RefreshBasket(IBasket basket);

        /// <summary>
        /// Recalculate relevant costs, charges etc and save these changes
        /// </summary>
        /// <param name="context"></param>
        void RefreshBasket(IStoreContext context);

        //void SetCurrentBasket(IStoreContext context, IBasket basket);
        //void SetLastOrder(IStoreContext context, IBasket order);
    }
}
