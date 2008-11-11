using System;
namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {

    public class ImageHelper {

        private ImageHelper() { }

        public static void CopyImage(IImage source, IImage destination) {
            destination.AltText = source.AltText;
            destination.Height = source.Height;
            destination.Url = source.Url;
            destination.Width = source.Width;
            destination.ImageType = source.ImageType;
            destination.HeightSpecified = (source.Height != 0);
            destination.WidthSpecified = (source.Width != 0);
        }
    }

    public interface IImage : IDisplayObject {
        short ImageType { get; set;}
    }
}
