using System;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {
	/// <summary>
	/// Summary description for TestPaymentProvider.
	/// </summary>
	public class TestPaymentProvider : AbstractPaymentProvider {

		public TestPaymentProvider() {
		}

		public override void RequestPreAuthPayment(IElectronicPayment payment) {
			ProcessRequest(payment);
		}

		public override void RequestAuthPayment(IElectronicPayment payment) {
			ProcessRequest(payment);
		}
		
		public override void RequestPostAuthPayment(IElectronicPayment payment) {
			ProcessRequest(payment);
		}
		
		public override void RequestRefund(IElectronicPayment payment) {
			ProcessRequest(payment);
		}
		
		public override void RequestRefund(IElectronicPayment payment, Money refundAmount) {
			ProcessRequest(payment);
		}
		
		public override void RequestVoidPayment(IElectronicPayment payment) {
			ProcessRequest(payment);
		}
		
		public override void RequestSettlePayment(IElectronicPayment payment) {
			ProcessRequest(payment);
		}

		protected virtual void ProcessRequest(IElectronicPayment payment) {

			payment.PaymentDate = DateTime.Now;

			switch (Mode) {
				case TransactionMode.Production:
					throw new NotSupportedException("Cannot simulate live processing");
				case TransactionMode.AlwaysPass:
				case TransactionMode.Test:
					payment.TransactionStatus = PaymentStatus.Approved;
					break;
				case TransactionMode.AlwaysFail:
					payment.TransactionStatus = PaymentStatus.Declined;
					break;
				case TransactionMode.Random:
				default:
					payment.TransactionStatus = GetRandomStatus();
					break;
			}

			//Record this request
			LogRequest(payment);

			if ((payment.TransactionStatus == PaymentStatus.Approved || payment.TransactionStatus == PaymentStatus.Referred) 
				&& payment.TransactionReference.Length == 0) {

				//Make up some stuff
				payment.TransactionReference = "TEST-" + StringUtils.GenerateRandomText(16, "0123456789abcdef") + "-TEST";
				payment.AuthorisationCode = "TEST-" + StringUtils.GenerateRandomText(8, "0123456789abcdef") + "-TEST";
			}
		}

		protected virtual PaymentStatus GetRandomStatus() {

			Random random = new Random(DateTime.Now.Millisecond);
			int number = random.Next(3);

			switch (number) {
				case 0:
					return PaymentStatus.Approved;
				case 1:
					return PaymentStatus.Declined;
				case 2:
					return PaymentStatus.Referred;
				default:
					return PaymentStatus.Other;
			}
		}

	}
}