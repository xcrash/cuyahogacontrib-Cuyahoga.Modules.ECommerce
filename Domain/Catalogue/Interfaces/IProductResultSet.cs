using System;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IProductResultSet {
        ISearchMetaData MetaData { get; set; }
        List<IProductSummary> ProductList { get; set; }
    }
}
