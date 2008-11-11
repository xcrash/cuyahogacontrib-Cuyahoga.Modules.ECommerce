using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Igentics.Common.ECommerce.DataTransferObjects;
using Igentics.Common.ECommerce;

namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

    public class SimpleCurrencyConverter : IOrderProcessor {

        private IDictionary<string, decimal> _exchangeRates;

        public SimpleCurrencyConverter(IDictionary exchangeRates) {

            _exchangeRates = new Dictionary<string, decimal>();

            foreach (string key in exchangeRates.Keys) {
                _exchangeRates.Add(key, Convert.ToDecimal(exchangeRates[key]));
            }
        }

        public ProcessStatusMessage Process(Basket order) {

            if (order == null) return new ProcessStatusMessage(ProcessStatus.Error, "Null Order");

            foreach (BasketItem item in order.BasketItemList) {

                if (item.UnitPrice == null) continue;

                if (item.UnitPrice.CurrencyCode != order.CurrencyCode) {
                    item.UnitPrice = new Money(
                        order.CurrencyCode,
                        item.UnitPrice.Amount * GetExchangeRate(item.UnitPrice.CurrencyCode, order.CurrencyCode)
                        );
                    item.LinePrice = new Money(item.UnitPrice);
                    item.LinePrice.Multiply(item.Quantity);
                }
            }

            return new ProcessStatusMessage(ProcessStatus.Success);
        }

        protected virtual decimal GetExchangeRate(string sourceCurrencyCode, string destinationCurrencyCode) {

            if (string.Compare(sourceCurrencyCode, destinationCurrencyCode, true) == 0) return 1.0M;

            decimal sourceRate = _exchangeRates[sourceCurrencyCode];
            decimal destinationRate = _exchangeRates[destinationCurrencyCode];

            return destinationRate / sourceRate;
        }
    }
}