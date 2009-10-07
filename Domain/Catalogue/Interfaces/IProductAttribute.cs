using System;
namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {

    public interface IProductAttribute {
        string BaseUnit { get; set; }
        string DataType { get; set; }
        string Name { get; set; }
        string Value { get; set; }
        IAttributeGroup Group { get; set; }
    }
}
