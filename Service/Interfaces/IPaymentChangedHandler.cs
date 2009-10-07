using System;
using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	//Handler for payment change events. Useful when payment providers require access to the calling application
	public interface IPaymentChangedHandler {
		void HandleChange(object source, IElectronicPayment payment);
	}
}