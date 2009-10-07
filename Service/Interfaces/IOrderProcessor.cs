using System;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {

	/// <summary>
	/// Summary description for ICommand.
	/// </summary>
	public interface IOrderProcessor {
		void Process(IBasket order);
	}
}