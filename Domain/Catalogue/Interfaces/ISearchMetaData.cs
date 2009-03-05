using System;
namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface ISearchMetaData {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int ResultCount { get; set; }
        string SearchTerm { get; set; }
    }
}
