using System;
using System.Collections.Generic;
using System.Net.Mail;
namespace Cuyahoga.Modules.ECommerce.Service.Email {
   
    public interface IEmailTransport {
        void Send(MailMessage message);
    }
}
