using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Service.Email;
using Cuyahoga.Modules.ECommerce.Service.Translation;
using Cuyahoga.Modules.ECommerce.Util.Location;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {

    public class AlbionOrderCloseProcessor : BaseEmailProcessor {

        public AlbionOrderCloseProcessor(ITemplateEngine engine, Cuyahoga.Core.Service.Email.IEmailSender sender)
            : base(engine, sender) {
        }

        #region IOrderProcessor Members

        public override void Process(IBasket order) {

            ITextTranslator translator = TranslatorUtils.GetTextTranslator(order.GetType(), order.CultureCode);
            Hashtable data = new Hashtable();

            IUserDetails user = (order.UserDetails != null) ? new UserDecorator(order.UserDetails) : order.AltUserDetails;

            data.Add("order", new BasketDecorator(order));
            data.Add("header", order.OrderHeader);
            data.Add("user", user);
            data.Add("isCreditCardOrder", order.OrderHeader.PaymentMethod == PaymentMethodType.CreditCard);

            IAddressFormatter addrf = new AddressFormatter();
            if (order.OrderHeader.InvoiceAddress != null) {
            
                data.Add("invoiceAddress", addrf.FormatAddress(order.OrderHeader.InvoiceAddress, "<br>", translator.CultureCode));

                if (order.OrderHeader.DeliveryAddress != null && !AddressHelper.AreSame(order.OrderHeader.InvoiceAddress, order.OrderHeader.DeliveryAddress)) {
                    data.Add("deliveryAddress", addrf.FormatAddress(order.OrderHeader.DeliveryAddress, "<br>", translator.CultureCode));
                }
            }

            SendEmail(data, translator, TemplateName, SenderEmail, user.EmailAddress, SubjectTag, IsHtml);
        }

        #endregion
    }
}