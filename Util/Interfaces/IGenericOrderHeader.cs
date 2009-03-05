using System;

using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Util.Location;

namespace Cuyahoga.Modules.ECommerce.Util.Interfaces {

	/// <summary>
	/// Summary description for IGenericOrderHeader.
	/// </summary>
	public interface IOrderHeader {
	
		long OrderHeaderID {get; set;}
		string PurchaseOrderNumber {get; set;}
		DateTime OrderedDate {get; set;}
		string Comment {get; set;}

		OrderStatus Status {get; set;}
       
		PaymentMethodType PaymentMethod {get; set;}
		
		IAddress InvoiceAddress {get; set;}
		IAddress DeliveryAddress {get; set;}
	}
}