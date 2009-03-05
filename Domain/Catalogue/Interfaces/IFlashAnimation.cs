using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IFlashAnimation {
        string FlashQuality { get; set; }
        string FlashBackgroundColour { get; set; }
        string FlashAltText { get; set; }
        short? FlashHeight { get; set; }
        string FlashUrl { get; set; }
        short? FlashWidth { get; set; }
    }
}
