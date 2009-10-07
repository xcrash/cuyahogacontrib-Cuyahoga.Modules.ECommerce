using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Enums;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {

    /// <summary>
    /// Calculates the total weight of the order and applies a delivery charge based upon a weight band
    /// </summary>
    public class WeightBandingOrderProcessor : IOrderProcessor {

        public const string ATTRIBUTE_REFERENCE_WEIGHT_KG = "weight_kg";

        #region IOrderProcessor Members

        public void Process(IBasket order) {

            BasketDecorator dec = order as BasketDecorator;
            if (dec == null) {
                dec = new BasketDecorator(order);
            }

            //Remove any existing delivery charge
            dec.RemoveItems(BasketItemType.DeliveryCharge);

            decimal totalWeight = 0;

            foreach (IBasketLine line in order.BasketItemList) {

                BasketItem item = line as BasketItem;

                if (item != null && item.Product != null) {
                    totalWeight += line.Quantity * GetProductWeight(item.Product.ProductID);
                }
            }

            string countryCode = "GB";
            if (order.OrderHeader != null && (order.OrderHeader.DeliveryAddress != null || order.OrderHeader.InvoiceAddress != null)) {
                if (order.OrderHeader.DeliveryAddress != null) {
                    countryCode = order.OrderHeader.DeliveryAddress.CountryCode;
                } else {
                    countryCode = order.OrderHeader.InvoiceAddress.CountryCode;
                }
            }

            decimal cost = GetDeliveryChargeByWeight(countryCode, totalWeight);

            if (cost > 0 && order.BasketItemList.Count > 0) {
                //Where does this magic item come from?
                // We should have a variable in order header / basket that stores Delivery Charges. Not bodging it as an item.
                order.AddItem(BasketItemType.DeliveryCharge, "Delivery Charge", cost);
            }
        }
        #endregion

        private decimal GetProductWeight(long productID) {
            try {
                string val = Product.GetProductAttributeValueByReference(productID, ATTRIBUTE_REFERENCE_WEIGHT_KG);
                if (!string.IsNullOrEmpty(val)) {
                    return Convert.ToDecimal(val);
                }
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
            }

            return 0;
        }

        private decimal GetDeliveryChargeByWeight(string countryCode, decimal weight) {
            try {
                using (SpHandler sph = new SpHandler("getDeliveryChargeByWeight", new SqlParameter("@countrycode", countryCode), new SqlParameter("@weight", weight))) {

                    object o = sph.ExecuteScalar();

                    if (o != null) {
                        return Convert.ToDecimal(o);
                    }
                }
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
            }

            return 0;
        }
    }
}