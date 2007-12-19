using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {

    class SimpleOrderProcessorFactory : IOrderProcessorFactory {
        private decimal _minimumDeliveryCharge;
        private IOrderProcessor _refreshProcessor;
        private IOrderProcessor _closeProcessor;

        public SimpleOrderProcessorFactory() {
            
            CompositeOrderProcessor refreshProcessor = new CompositeOrderProcessor();
            refreshProcessor.Add(new ItemCodeCondenserCommand());
            /* All of these processors should be configurable via the admin 
                I think we should pass all the processors we want to use in via the constuctor - maybe only useful for delivery and charges tho.
             * Alternatively, have a generic Delivery Processor which call's all the enabled delivery processors.
             *  What happens if a customer has a choice of two delivery methods? Would need to pass in which one is slected and only apply that and remove  all other delivery charges
                At the
             * LC - 29.10.2007
             */
            //refreshProcessor.Add(new SimplePriceProcessor());
            refreshProcessor.Add(new ScaledPriceProcessor());
            refreshProcessor.Add(new WeightBandingOrderProcessor()); 
            refreshProcessor.Add(new MinimumDeliveryChargeOrderProcessor(_minimumDeliveryCharge));
            refreshProcessor.Add(new SubtotalOrderProcessor());
            refreshProcessor.Add(new SimpleUkVatProcessor()); 

            _refreshProcessor = refreshProcessor;

            CompositeOrderProcessor closeProcessor = new CompositeOrderProcessor();
            closeProcessor.Add(GetProcessor("order.confirmation"));
            _closeProcessor = closeProcessor;
        }

        #region IOrderProcessorFactory Members

        public void SetMinimumDeliveryCharge(decimal charge){
            _minimumDeliveryCharge = charge;
        }

        public IOrderProcessor GetRefreshProcessor() {
            return _refreshProcessor;
        }

        public IOrderProcessor GetCloseProcessor() {
            return _closeProcessor;
        }

        public IOrderProcessor GetProcessor(string key) {
            return Util.ServiceFactory.GetService(key) as IOrderProcessor;
        }
        #endregion
    }
}
