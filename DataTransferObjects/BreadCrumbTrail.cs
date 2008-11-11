using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Igentics.Common.ECommerce.DataTransferObjects {

    [Serializable]
    public class BreadCrumbTrailContainer {

        private bool _isCurrent;
        private bool _isCurrentSpecified;

        private List<TrailItem> _breadCrumbTrail = new List<TrailItem>();

        [XmlAttribute("current")]
        public bool IsCurrent {
            get { return _isCurrent; }
            set { _isCurrent = _isCurrentSpecified = value; }
        }

        [XmlIgnore]
        public bool IsCurrentSpecified {
            get { return _isCurrentSpecified; }
            set { _isCurrentSpecified = value; }
        }

        [XmlArrayItem(Type = typeof(TrailItem))]
        public List<TrailItem> BreadCrumbTrail {
            get { return _breadCrumbTrail; }
            set { _breadCrumbTrail = value; }
        }
    }
}
