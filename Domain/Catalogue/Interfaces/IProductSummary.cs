using System;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IProductSummary {
      
        string Description { get; set; }
        string ShortDescription { get; set; }
        string AdditionalInformation { get; set; }
        string ItemCode { get; set; }
        string ProductGroup { get; set; }
        string ProductFamily { get; set; }
        string Features { get; set; }
        long ProductID { get; set; }
        List<IImage> ProductImages { get; set; }
        short StockedIndicator { get; set; }
        string Name { get; set; }
        List<ProductSynonym> SynonymList { get; set; }
        decimal Price { get; set; }
        string PriceDescription { get; set; }
        bool IsKit { get; set; }

        bool IsNewFamily(string oldFamily);
    }
}
