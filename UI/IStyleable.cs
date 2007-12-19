using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.UI {

    /// <summary>
    /// Defines a UI element that can be styled using CSS
    /// </summary>
    public interface IStyleable {
        string CssClass { get; set; }
    }
}