using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IAttributeGroup {
        string Name { get; set; }
        long ID { get; set; }
    }
}
