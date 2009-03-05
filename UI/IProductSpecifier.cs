using System;
using System.Collections.Generic;
using Igentics.Common.ECommerce.DataTransferObjects;

namespace Igentics.Common.ECommerce.Interfaces {

    /// <summary>
    /// Indicates this class can uniquely identify a product, including its variations
    /// </summary>
    public interface IProductSpecifier {
        string ItemCode { get; set; }
        List<AttributeOptionChoice> OptionList { get; set; }
    }
}
