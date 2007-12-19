using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IPromotion {
        List<IProduct> CrossSellList { get; set; }
        List<IProduct> UpSellList { get; set; }
    }
}
//this is bollox - should use the related product stuff