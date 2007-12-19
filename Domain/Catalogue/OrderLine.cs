using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {
    public class OrderLine : IOrderLine {
        OrderHeader orderHeader { get; set; }
        List<IProduct> itemList { get; set;}
    }
}
