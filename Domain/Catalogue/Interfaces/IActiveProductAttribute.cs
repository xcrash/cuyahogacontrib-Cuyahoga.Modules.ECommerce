using System;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IActiveProductAttribute : IProductAttribute {
        List<IAttributeOption> AttributeOptionList { get; set; }       
    }
}
