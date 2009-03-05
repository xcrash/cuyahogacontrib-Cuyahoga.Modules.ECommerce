using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.UI {

    /// <summary>
    /// Defines a UI element that can be styled using CSS
    /// </summary>
    public interface IStyleable {
        string Style { get; set; }
    }

    /// <summary>
    /// Defines a UI element that can be rendered using a named template
    /// </summary>
    public interface ITemplateable {
        string Template { get; set; }
    }

    /// <summary>
    /// Defines a UI element that may be sorted
    /// </summary>
    public interface ISortable {
        short SortOrder { get; set;}
        bool SortOrderSpecified { get; set; }
    }

    public interface IHideable {
        bool Visible { get; set; }
    }
}