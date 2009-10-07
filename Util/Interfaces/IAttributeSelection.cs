using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Util {

    public interface IAttributeSelection {
        int AttributeID { get; set;}
        string OptionValue { get; set;}
    }

    public class AttributeSelection : IAttributeSelection {

        private int _attributeID;
        private string _attributeOption;

        public int AttributeID {
            get { return _attributeID; }
            set { _attributeID = value; }
        }

        public string OptionValue {
            get { return _attributeOption; }
            set { _attributeOption = value; }
        }
    }
}
