using System;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IProductView : IKeywords {
        List<ITrailItem> BreadCrumbTrail { get; set; }
        IProduct ProductDetails { get; set; }
    }
}
