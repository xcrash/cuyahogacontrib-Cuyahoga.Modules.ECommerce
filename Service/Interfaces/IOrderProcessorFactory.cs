using System;

using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Service.OrderProcessor {

	/// <summary>
	/// Might be superfluous - use a creation by reference instead? i.e. GetInstance("RefreshCommand", basket);
	/// </summary>
	public interface IOrderProcessorFactory {

        void SetMinimumDeliveryCharge(decimal charge);
		/// <summary>Called when a basket is refreshed</summary>
		IOrderProcessor GetRefreshProcessor();

		/// <summary>Called when an order is closed</summary>
		IOrderProcessor GetCloseProcessor();

        /// <summary>Gets a generic processor by key</summary>
        IOrderProcessor GetProcessor(string key);
	}
}