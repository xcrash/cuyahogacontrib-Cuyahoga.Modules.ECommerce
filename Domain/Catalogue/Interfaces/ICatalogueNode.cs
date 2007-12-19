using System;
using System.Collections;
namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface ICatalogueNode : UI.IStyleable {
        string Description { get; set; }
        IImage Image { get; set; }
        string Name { get; set; }
        long NodeID { get; set; }
        long ParentNodeID { get; set; }
        short SortOrder { get; set; }
        IList Links { get; set;}
        IList Kits { get; set;}
        string KitDescription { get; set; }
        string KitPicture { get; set; }
        decimal? PriceChangePercent { get; set; }
    }
}
