using System;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IProductGroup {
        string Name { get; set; }
        List<IProductSummary> ProductList { get; set; }
        string RelationshipType { get; set; }
    }
}
