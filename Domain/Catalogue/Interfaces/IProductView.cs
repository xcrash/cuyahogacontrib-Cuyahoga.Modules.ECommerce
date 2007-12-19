using System;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IProductView {
        List<ICatalogueNode> BreadCrumbTrail { get; set; }
        IProduct ProductDetails { get; set; }
        List<Domain.Product> ProductsInFamily { get; set;}
    }
}
