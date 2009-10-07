using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {
   public class AttributeGroup : IAttributeGroup {
        #region IAttributeGroup Members

        private string _name = "";
        private long _ID = 0;

        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }

        public long ID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }

        #endregion
    }
}
