using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {

    /// <summary>
    /// Does nothing
    /// </summary>
    class NullOrderProcessor : IOrderProcessor {

        #region IOrderProcessor Members

        public void Process(Cuyahoga.Modules.ECommerce.Util.Interfaces.IBasket order) {
        }

        #endregion
    }
}
