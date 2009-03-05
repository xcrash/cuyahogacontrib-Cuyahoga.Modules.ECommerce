using System;
using System.Collections;

using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Core.Domain;

namespace Cuyahoga.Modules.ECommerce.Util.Interfaces {

    /// <summary>
    /// Provides the bare minimum functionality for a basket
    /// </summary>
    public interface IBasket {

        IOrderHeader OrderHeader { get; set;}

        /// <summary>
        /// Database reference for this object
        /// </summary>
        long BasketID { get; set;}

        /// <summary>
        /// The date when this basket was first created
        /// </summary>
        DateTime CreatedDate { get; set;}

        /// <summary>
        /// Culture code used for currency, date and number formatting
        /// </summary>
        string CultureCode { get; set;}

        /// <summary>
        /// Currency Code used for all item lines
        /// </summary>
        string CurrencyCode { get; set;}

        /// <summary>
        /// The total price for this basket before taxes
        /// </summary>
        Money SubTotal { get; set;}

        /// <summary>
        /// The total tax paid on this basket
        /// </summary>
        Money TaxPrice { get; set;}

        /// <summary>
        /// A list of items contained within this basket
        /// </summary>
        IList BasketItemList { get; set; }

        /// <summary>
        /// User information to get specific line prices
        /// </summary>
        User UserDetails { get; set;}

        /// <summary>
        /// User information where the user may not be registered
        /// </summary>
        IUserDetails AltUserDetails { get; set;}

        /// <summary>
        /// Adds an item to the basket
        /// </summary>
        /// <param name="itemCode">The unique item code identifying the item to add</param>
        /// <param name="quantity">The quantity to add to the basket</param>
        /// <returns>The basket line to which this item is added</returns>
        /// <remarks>Where an item with the same item code already exists, the quantity of the existing item is increased by quanity</remarks>
        IBasketLine AddItem(string itemCode, int quantity);

        //To be used for delivery ect
        IBasketLine AddItem(BasketItemType itemType, string description, decimal price);

        IBasketLine UpdateItem(BasketItem item, string description, decimal price);

        /// <summary>
        /// Adds an item to the basket
        /// </summary>
        /// <param name="itemCode">The unique item code identifying the item to add</param>
        /// <param name="quantity">The quantity to add to the basket</param>
        /// <returns>The basket line to which this item is added</returns>
        /// <remarks>Where an item with the same item code already exists, the quantity of the existing item is increased by quanity</remarks>
        IBasketLine AddItem(string itemCode, int quantity, BasketItemType itemType, string description, decimal unitPrice);

        /// <summary>
        /// Removes all basketlines containing the supplied item code
        /// </summary>
        /// <param name="itemCode">The unique item code identifying the item to remove</param>
        void RemoveItem(string itemCode);

        /// <summary>
        /// Removes the item from the basket
        /// </summary>
        /// <param name="item">item to be removed</param>
        void RemoveItem(IBasketLine item);

        /// <summary>
        /// Sets the quantity of the supplied item to the new quantity
        /// </summary>
        /// <param name="itemCode">The unique item code identifying the item to add</param>
        /// <param name="newQuantity">New item quantity</param>
        void AmendItemQuantity(string itemCode, int newQuantity);
    }
}