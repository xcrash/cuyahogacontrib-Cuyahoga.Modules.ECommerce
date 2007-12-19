using System;
namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {

    public class ImageHelper {

        private ImageHelper() { }

        public static void CopyImage(IImage source, IImage destination) {
            destination.AltText = source.AltText;
            destination.Height = source.Height;
            destination.Url = source.Url;
            destination.Width = source.Width;
        }
    }

    public interface IImage {
        string AltText { get; set; }
        short? Height { get; set; }
        string Url { get; set; }
        short? Width { get; set; }
    }

    public interface IKitSummary : IImage {
        string ItemCode { get; set; }
        long ProductID { get; set; }
        string Name { get; set; }
    }

    public class KitSummary : Image, IKitSummary {

        private string _itemCode;
        private long _productID;
        private string _name;

        public string ItemCode {
            get { return _itemCode; }
            set { _itemCode = value; }
        }

        public long ProductID {
            get { return _productID; }
            set { _productID = value; }
        }

        public string Name {
            get { return _name; }
            set { _name = value; }
        }
    }
}
