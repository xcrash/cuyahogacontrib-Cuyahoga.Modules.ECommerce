using System;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IProduct : IProductSummary, IAttributeSource {
        List<IActiveProductAttribute> ActiveAttributeList { get; set; }
        List<IRelatedDocument> DocumentList { get; set; }
        List<string> FeatureList { get; set; }
        List<IProductGroup> ProductGroupList { get; set; }
        List<IProductAttribute> StaticAttributeList { get; set; }
        List<IRelatedProducts> CrossSellList { get; set; }
        List<IRelatedProducts> UpSellList { get; set; }
    }
}
