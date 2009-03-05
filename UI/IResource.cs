using System;

namespace Igentics.Common.ECommerce.Interfaces {

    /// <summary>
    /// A resource that may be accessed via the internet
    /// </summary>
    public interface IResource {
        string ResourceID { get; set; }
        string Description { get; set; }
        string Url { get; set; }
    }

    public interface IMimeResource {
        string MimeType { get; set; }
    }
}
