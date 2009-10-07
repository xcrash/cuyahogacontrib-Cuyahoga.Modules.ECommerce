using System;
namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IAttributeOption {
        string PickListValue { get; set; }
        string ShortCode { get; set; }
        decimal Price { get; set; }
        string Url { get; set; }
    }
}
