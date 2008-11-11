using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    [Serializable]
    public class OrderHeader {

        private List<CustomField> _customFields;

        [XmlArrayItem(typeof(CustomField))]
        public List<CustomField> CustomFieldList {
            get { return _customFields; }
            set { _customFields = value; }
        }
    }
}
