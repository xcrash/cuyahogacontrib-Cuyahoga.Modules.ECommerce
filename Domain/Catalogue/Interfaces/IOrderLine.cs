using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IOrderLine {
    OrderHeader orderHeader { get; set; }
    List<IProduct> itemList { get; set;}

    }
}
