using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Util.Enums {

    /// <summary>
    /// Standard statuses for orders and baskets
    /// </summary>
    public enum OrderStatus : int {
        /// <summary>A basket that has no known status</summary>
        Unknown = 0,
        /// <summary>A basket that has not been submitted as an order</summary>
        Basket = 2,
        /// <summary>An order not yet purchased</summary>
        OrderedNotPaid = 3,
        /// <summary>An order that has been purchased</summary>
        OrderedPaid = 4,
        /// <summary>An order purchased, but not shipped</summary>
        Processing = 5,
        /// <summary>An order purchased and shipped</summary>
        Shipped = 6,
        /// <summary>An order that has been purchased and acknowledged by the back office</summary>
        OrderedPaidAcknowledged = 7,
        /// <summary>An order that has been purchased and acknowledged by the back office</summary>
        Cancelled = 8,
        /// <summary>An order has been paid but the bank raised it as referred</summary>
        OrderedReferred = 9,
        /// <summary>An order has been paid and Acknowledged but the bank raised it as referred</summary>
        OrderedReferredAcknowledged = 10,
        /// <summary>An order has submitted for account payment</summary>
        OrderedSubmittedForAccountPayment = 11
    }
}
