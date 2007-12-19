using System;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface ICatalogueNodeView {
        List<ICatalogueNode> BreadCrumbTrail { get; set; }
        List<ICatalogueNode> ChildNodes { get; set; }
        ICatalogueNode CurrentNode { get; set; }
        List<IProductSummary> ProductList { get; set; }
    }
}
