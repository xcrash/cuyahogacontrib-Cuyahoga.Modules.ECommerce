using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

	public class ActiveProductAttribute : ProductAttribute, IActiveProductAttribute {

		public ActiveProductAttribute() : base() {
	
		}

		
        private List<IAttributeOption> attributeOptionList = null;

		[XmlArrayItem(Type = typeof(AttributeOption))]
        public List<IAttributeOption> AttributeOptionList {
			get {
				if (attributeOptionList == null) {
                    attributeOptionList = new List<IAttributeOption>();
				}
				return attributeOptionList;
			}
			set {
				attributeOptionList = value;
			}
		}
	}
}