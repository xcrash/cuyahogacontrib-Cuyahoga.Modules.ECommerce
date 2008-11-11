using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {

    //abstraction is the future!
    public interface IDisplayObject {
        string AltText { get; set; }
        short Height { get; set; }
        string Url { get; set; }
        short Width { get; set; }
        bool WidthSpecified { get; set; }
        bool HeightSpecified { get; set; }
    }
}
