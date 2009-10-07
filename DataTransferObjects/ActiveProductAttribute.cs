using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    [Serializable]
    public class ActiveProductAttribute : Attribute {

		public ActiveProductAttribute() : base() {
		}

        public ActiveProductAttribute(string name, string attrValue)
            : base(name, attrValue) {
        }
		
        private List<Option> attributeOptionList = null;

        [XmlArrayItem(Type = typeof(Option), IsNullable = true)]
        public List<Option> OptionList {
			get {
				if (attributeOptionList == null) {
                    attributeOptionList = new List<Option>();
				}
				return attributeOptionList;
			}
			set {
				attributeOptionList = value;
			}
		}
	}
}
