using System;
using System.Xml.Serialization;
using Igentics.Common.ECommerce.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;

namespace Igentics.Common.ECommerce.DataTransferObjects {

	/// <summary>
	/// Summary description for RelatedDocument.
	/// </summary>
    [Serializable]
    public class Resource : IResource, IMimeResource {

		private  string _description;
        private string _type;
		private  string _url;
        private string _resourceID;

		public Resource() {
		}

		public Resource(string url, string description) {
			Url = url;
			Description = description;
		}

        [XmlAttribute("id")]
        public string ResourceID {
            get { return _resourceID; }
            set { _resourceID = value; }
        }
        
        [XmlAttribute("description")]
		public string Description {
			get {
				return _description;
			}
			set {
				_description = value;
			}
		}

		[XmlAttribute("url")]
		public string Url {
			get {
				return _url;
			}
			set {
				_url = value;
                if (value != null && value.LastIndexOf('.') > 0) {
                    MimeType = MimeHelper.GetMimeType(value.Substring(value.LastIndexOf('.') + 1));
                }
			}
		}

        [XmlAttribute("mimeType")]
        public string  MimeType {
            get {
                return _type;
            }
            set {
                _type = value;
            }
        }
	}
}