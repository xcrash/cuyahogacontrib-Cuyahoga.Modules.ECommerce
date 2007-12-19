using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue {

    public class Promotion : IPromotion {

        private  List<IProduct> _crossSellList;
        private  List<IProduct> _upSellList;

        #region IPromotion Members

        public List<IProduct> CrossSellList {
            get {
                return _crossSellList;
            }
            set {
                _crossSellList = value;
            }
        }

        public List<IProduct> UpSellList {
            get {
               return _upSellList;
            }
            set {
                _upSellList = value;
            }
        }

        #endregion
    }
}
