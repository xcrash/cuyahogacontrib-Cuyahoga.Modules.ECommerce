using System;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

	/// <summary>
	/// Summary description for Image.
	/// </summary>
	public class Image : IImage {

		private  string _url = "";
		private  string _altText = "";
        private  short? _width;
		private  short? _height;

		public Image() {
		}

		public Image(string url, string altText) {
			Url = url;
			AltText = altText;
		}

		[XmlAttribute]
		public string Url {
			get {
				return _url;
			}
			set {
				_url = value;
			}
		}

		[XmlAttribute]
		public string AltText {
			get {
				return _altText;
			}
			set {
				_altText = value;
			}
		}

		[XmlAttribute]
		public short? Width {
			get {
				return _width;
			}
			set {
				_width = value;
			}
		}

		//need these
		[XmlAttribute]
        public short? Height
        {
			get {
				return _height;
			}
			set {
				_height = value;
			}
		}
		//both to be null
	}
}