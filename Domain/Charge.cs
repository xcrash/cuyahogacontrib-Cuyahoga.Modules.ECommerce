using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Domain {
    [Serializable]
    public class Charge {
        private string _chargeName;
        private int _chargeID;

        public Charge() {
            _chargeID = 0;
        }

        public virtual int ChargeID {
            get { return _chargeID; }
            set { _chargeID = value; }
        }

        public virtual string ChargeName {
            get { return _chargeName; }
            set { _chargeName = value; }
        }
    }
}
