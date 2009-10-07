using System;
namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {

    [Obsolete("To be replaced by IProduct")]
    public interface IRelatedProducts {
        string AccessoryDescription { get; set; }
        string AccessoryName { get; set; }
        string AccessoryPartNo { get; set; }
        string Category { get; set; }
        string CategoryDescription { get; set; }
        int CategoryID { get; set; }
        Image Image { get; set; }
        string LanguageCode { get; set; }
        string MainPartNo { get; set; }
        string RelationshipTypeID { get; set; }
    }
}
