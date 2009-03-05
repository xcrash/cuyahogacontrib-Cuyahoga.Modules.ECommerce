using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Util.Promotion {

    /// <summary>
    /// Defines how a promotion should be interpreted
    /// </summary>
    public enum PromotionType : int {

        /// <summary>Applied to a single product</summary>
        ProductPromotion = 1,
        /// <summary>Applied to a whole basket</summary>
        BasketPromotion = 2,
        /// <summary>Applied to all products in a catalogue section (shallow)</summary>
        CatalogueSectionPromotion = 3,
        /// <summary>Applied to a single customer</summary>
        CustomerPromotion = 4,
        /// <summary>Applied to a defined group of customers</summary>
        CustomerGroupPromotion = 5,
        /// <summary>Applied to all customers sharing the same account number</summary>
        AccountNumberPromotion = 6

    }
}
