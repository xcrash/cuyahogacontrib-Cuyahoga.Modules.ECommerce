using System;

using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Util.Interfaces {

	/// <summary>
	/// Summary description for IPaymentRecord.
	/// </summary>
	public interface IPaymentRecord : IPayment {

		PaymentMethodType PaymentMethod {get; set;}
		long PaymentID {get; set;}
		short PaymentProviderID {get; set;}

	}
}