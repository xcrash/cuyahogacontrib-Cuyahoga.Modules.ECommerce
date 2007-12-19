using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {
    public class Sku : ISku {
        #region ISku Members
        private string _code;
        public string Code {
            get {
                return _code;
            }
            set {
                _code = value;
            }
        }

        #endregion
    }
}
