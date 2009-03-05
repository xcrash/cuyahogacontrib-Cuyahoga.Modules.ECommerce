using System;
namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IRelatedDocument : UI.IStyleable {
        string Description { get; set; }
        string Name { get; set; }
        string Url { get; set; }
        string Type { get; set; }
        long DocumentID { get; set; } //handy for when passing documents around
    }
}
