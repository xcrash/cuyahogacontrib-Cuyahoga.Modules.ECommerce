using System;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface ICategoryView : IKeywords {
        List<ITrailItem> BreadCrumbTrail { get; set; }
        List<ICategory> ChildNodes { get; set; }
        ICategory CurrentNode { get; set; }
        List<IProductSummary> ProductList { get; set; }
    }
}
