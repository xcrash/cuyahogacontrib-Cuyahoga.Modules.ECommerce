using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Util;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {
    
    public class ScaledPriceProcessor : IOrderProcessor {

        public const decimal PRICE_RES = 0.05M;

        #region IOrderProcessor Members

        public void Process(IBasket order) {

            foreach (IBasketLine line in order.BasketItemList) {

                BasketItem item = line as BasketItem;

                if (item != null && item.Status != PricingStatus.OK) {

                    decimal unitPrice = item.Product.BasePrice;

                    if (item.OptionList != null) {
                        foreach (BasketItemAttribute attribute in item.OptionList) {
                            unitPrice += attribute.OptionPrice;
                        }
                    }

                    item.UnitLinePrice = new Money(order.CurrencyCode, GetScaledPrice(item.Product.ProductID, unitPrice));
                    item.Status = PricingStatus.OK;
                }
            }
        }

        #endregion

        public decimal GetScaledPrice(IProductSummary product) {
            return GetScaledPrice(product.ProductID, product.Price);
        }

        public decimal GetScaledPrice(long productID, decimal originalPrice) {

            decimal priceChangePercent = GetProductPriceChange(productID);

            if (priceChangePercent != 0) {
                //Rounded to nearest 5p after scaling
                originalPrice = Math.Round(originalPrice * (100 + priceChangePercent) / (100 * PRICE_RES)) * PRICE_RES;
            }

            return originalPrice;
        }

        public decimal GenerateScaledPrice(decimal change, decimal originalPrice) {

            decimal priceChangePercent = change;

            if (priceChangePercent != 0) {
                //Rounded to nearest 5p after scaling
                originalPrice = Math.Round(originalPrice * (100 + priceChangePercent) / (100 * PRICE_RES)) * PRICE_RES;
            }

            return originalPrice;

        }

        private decimal GetProductPriceChange(long productID) {
            try {
                using (SpHandler sph = new SpHandler("getProductPriceChange", new SqlParameter("@productID", productID))) {

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

        public void SaveProductPriceChange(long productID, decimal price) {
            try {
                using (SpHandler sph = new SpHandler("saveProductPriceChange", new SqlParameter("@productID", productID), new SqlParameter("@newPrice", price))) {

                    object o = sph.ExecuteScalar();

                }
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
            }
        }
    }
}