using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using Igentics.Common.ECommerce;
using Igentics.Common.ECommerce.DataTransferObjects;
using Igentics.Soa.Commerce.Core.Util.Enums;

namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

    public interface IBandedItem {
        decimal Boundary { get; set; }
    }

    public class BandComparator : IComparer {
        #region IComparer Members

        public int Compare(object x, object y) {

            IBandedItem ix = x as IBandedItem;
            IBandedItem iy = y as IBandedItem;

            if (ix == null && iy == null) return 0;
            if (ix == null) return -1;
            if (iy == null) return 1;

            return Decimal.Compare(ix.Boundary, iy.Boundary);
        }

        #endregion
    }

    public class BandedDeliveryChargeCalculator : IOrderProcessor {

        private class BandedCharge : IBandedItem {

            private decimal _boundary;
            private Money _charge;

            public BandedCharge() {
            }

            public BandedCharge(decimal boundary, Money charge) {
                _boundary = boundary;
                _charge = charge;
            }

            public decimal Boundary {
                get { return _boundary; }
                set { _boundary = value; }
            }

            public Money Charge {
                get { return _charge; }
                set { _charge = value; }
            }
        }

        private ArrayList _chargeList;
        private string _currencyCode;

        public BandedDeliveryChargeCalculator(string currencyCode, IDictionary chargeAmounts) {

            _currencyCode = currencyCode;
            ArrayList chargeList = new ArrayList();

            foreach (object key in chargeAmounts.Keys) {
                decimal bandBoundary = Convert.ToDecimal(key);
                chargeList.Add(new BandedCharge(bandBoundary, new Money(currencyCode, Convert.ToDecimal(chargeAmounts[key]))));
            }

            ChargeList = chargeList;
        }

        public ArrayList ChargeList {
            get { return _chargeList; }
            set { _chargeList = value; if (value != null) _chargeList.Sort(new BandComparator()); }
        }

        #region IOrderProcessor Members

        public ProcessStatusMessage Process(Basket order) {

            if (order.SubTotal != null) {

                foreach (BandedCharge charge in _chargeList) {

                    decimal bandBoundary = charge.Boundary;
                    if (order.SubTotal.Amount > bandBoundary) {
                        order.DeliveryCharge = new Money(charge.Charge);
                    }
                }
            }

            return new ProcessStatusMessage(ProcessStatus.Success);
        }
        #endregion
    }
}