using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Cuyahoga.Modules.ECommerce.Service.Email;
using Cuyahoga.Modules.ECommerce.Service.Translation;

using Igentics.Common.ECommerce;
using Igentics.Common.ECommerce.DataTransferObjects;

namespace Igentics.Soa.Commerce.Core.Service.OrderProcessor {

    public class OrderConfirmationEmailSender : BaseEmailSender {

        public OrderConfirmationEmailSender(ITemplateEngine engine, IEmailTransport sender, ITextTranslatorFactory translatorFactory) : base(engine, sender, translatorFactory) {
		}

        #region IOrderProcessor Members

        public override ProcessStatusMessage Process(Basket order) {
            //ToEmail = order.Header
            return base.Process(order);
        }

        #endregion
    }
}