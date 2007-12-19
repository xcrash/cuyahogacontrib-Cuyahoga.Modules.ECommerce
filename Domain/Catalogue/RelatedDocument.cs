using System;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

	/// <summary>
	/// Summary description for RelatedDocument.
	/// </summary>
	public class RelatedDocument : IRelatedDocument {

		private  string _name;
		private  string _description;
        private string _type;
		private  string _url;
        private string _cssClass;
        private long _documentID;

		public RelatedDocument() {
		}

		public RelatedDocument(string url, string name, string description) {
			Url = url;
			Name = name;
			Description = description;
		}

		[XmlAttribute]
		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

		[XmlAttribute]
		public string Description {
			get {
				return _description;
			}
			set {
				_description = value;
			}
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
        public string  Type {
            get {
                return _type;
            }
            set {
                _type = value;
            }
        }

        [XmlAttribute]
        public string CssClass {
            get {
                return _cssClass;
            }
            set {
                _cssClass = value;
            }
        }

        [XmlAttribute]
        public long DocumentID {
            get {
                return _documentID;
            }
            set {
                _documentID = value;
            }
        }
	}
}