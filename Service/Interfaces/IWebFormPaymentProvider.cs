using System;
using System.Web;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	/// <summary>
	/// Marks a class as one which transfers the user to a remote payment page
	/// </summary>
	public interface IWebFormPaymentProvider : IPaymentProvider {

		/// <summary>
		/// Transfers automatically
		/// </summary>
		void TransferClientToPaymentPage(IElectronicPayment payment, PaymentRequestTypes paymentType);

		/// <summary>
		/// Transfers the client to the payment page
		/// </summary>
		/// <param name="isDebug">If true, doesn submit automatically</param>
		void TransferClientToPaymentPage(IElectronicPayment payment, PaymentRequestTypes paymentType, bool isDebug);

        IElectronicPayment ProcessAuthPaymentResponse(HttpRequest postBackData);
        IElectronicPayment ProcessPreAuthPaymentResponse(HttpRequest postBackData);
        IElectronicPayment ProcessPostAuthPaymentResponse(HttpRequest postBackData);
        IElectronicPayment ProcessRefundResponse(HttpRequest postBackData);
        IElectronicPayment ProcessSettlePaymentResponse(HttpRequest postBackData);
        IElectronicPayment ProcessVoidPaymentResponse(HttpRequest postBackData);
	}
}