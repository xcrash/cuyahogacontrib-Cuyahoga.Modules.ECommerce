using System;
using System.Web;
using log4net;
using System.Collections;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

    /*
     * Payments need sorting because they appear to do very similar things
     */

    public interface IPayment {
        string TransactionReference { get; set; }
        Money PaymentAmount { get; set; }
        PaymentStatus TransactionStatus { get; set; }
        DateTime PaymentDate { get; set; }
        IBasket Basket { get; set;}
    }

    /// <summary>
    /// Summary description for IElectronicPayment.
    /// </summary>
    public interface IElectronicPayment : IPayment {

        string LocalRequestReference { get; set; }
		IDictionary CustomValues { get; set; }
		UserDetails UserInfo { get; set; }
		string AuthorisationCode { get; set; }
		string Description {get; set;}

    }
}