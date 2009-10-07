using System;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {
    
    public class FlashHelper {

        private FlashHelper() { }

        public static void CopyFlash(IFlashAnimation source, IFlashAnimation destination) {
            destination.FlashAltText = source.FlashAltText;
            destination.FlashHeight = source.FlashHeight;
            destination.FlashUrl = source.FlashUrl;
            destination.FlashWidth = source.FlashWidth;
            destination.FlashQuality = source.FlashQuality;
            destination.FlashBackgroundColour = source.FlashBackgroundColour;
        }
    }

    public class FlashAnimation : IFlashAnimation {

        private string _url = "";
        private string _altText = "";
        private short? _width;
        private short? _height;
        private string _quality = "";
        private string _backgroundColour = "#FFF"; // Sensible?

        public FlashAnimation() {
		}

        public FlashAnimation(string url, string altText) {
            FlashUrl = url;
            FlashAltText = altText;
		}

        #region IFlashAnimation Members

        [XmlAttribute]
        public string FlashQuality {
            get {
                return _quality;
            }
            set {
                _quality = value;
            }
        }

        [XmlAttribute]
        public string FlashBackgroundColour {
            get {
                return _backgroundColour;
            }
            set {
                _backgroundColour = value;
            }
        }

        #endregion

        #region IDisplayObject Members

		[XmlAttribute]
        public string FlashUrl {
			get {
				return _url;
			}
			set {
				_url = value;
			}
		}

		[XmlAttribute]
        public string FlashAltText {
			get {
				return _altText;
			}
			set {
				_altText = value;
			}
		}

		[XmlAttribute]
        public short? FlashWidth {
			get {
				return _width;
			}
			set {
				_width = value;
			}
		}

		//need these
		[XmlAttribute]
        public short? FlashHeight
        {
			get {
				return _height;
			}
			set {
				_height = value;
			}
		}
		//both to be null

        #endregion
    }
}
