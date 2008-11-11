using System;
using System.Collections;
using Cuyahoga.Modules.ECommerce.Util;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Service.PaymentProvider {

	public enum PaymentRequestTypes : int {
		ReservePayment = 1,
		ImmediatePayment = 2,
		CollectPayment = 3,
		SettlePayment = 4,
		RefundPayment = 5,
		VoidPayment = 6
	}

	public enum TransactionMode : int {
		/// <summary>This provider is taking live transactions</summary>
		Production = 1,
		/// <summary>This provider accepts transactions but does not process them as live</summary>
		Test = 2,
		/// <summary>Simulation always returning pass</summary>
		AlwaysPass = 3,
		/// <summary>Simulation always returning fail</summary>
		AlwaysFail = 4,
		/// <summary>Simulation returning random responses</summary>
		Random = 5
	}

	/// <summary>
	/// Summary description for IPaymentProvider.
	/// </summary>
	public interface IPaymentProvider : IConfiguredObject {

		string MerchantPassword {get; set;}
		string MerchantID {get; set;}
		TransactionMode Mode {get; set;}

		void RequestPreAuthPayment(IElectronicPayment payment);
		void RequestAuthPayment(IElectronicPayment payment);
		void RequestPostAuthPayment(IElectronicPayment payment);
		void RequestRefund(IElectronicPayment payment);
		void RequestRefund(IElectronicPayment payment, Money refundAmount);
		void RequestVoidPayment(IElectronicPayment payment);
		void RequestSettlePayment(IElectronicPayment payment);

		ILog Logger {get;}

	}
}