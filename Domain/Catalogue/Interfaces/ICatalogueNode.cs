using System;
using System.Collections;
using Cuyahoga.Modules.ECommerce.UI;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {

    public interface ITrailItem {
        long NodeID { get; set; }
        string Name { get; set; }
    }

    public interface ICategory : ITrailItem, IStyleable, ISortable {

        string Description { get; set; }
        IImage Image { get; set; }
        string BannerImageUrl { get; set; }
        IFlashAnimation Flash { get; set; }
        long ParentNodeID { get; set; }
        IList Links { get; set;}
    }
}
