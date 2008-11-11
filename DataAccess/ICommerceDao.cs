using System;
using System.Collections;
using System.Collections.Generic;
using Cuyahoga.Core.Domain;
using Cuyahoga.Modules.ECommerce.Util.Location;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;

namespace Cuyahoga.Modules.ECommerce.DataAccess {

	/// <summary>
	/// Summary description for IBasketFactory.
	/// </summary>
	public interface ICommerceDao {

        IBasket CreateBasket(IStoreContext context);
		IBasket FindBasket(long basketID);
        IBasketLine FindBasketLine(long basketLineID);
		IBasket FindOrder(long orderID);

        IList FindOrderByDateRange(int storeID, string username, DateTime startDate, DateTime endDate);
		void Save(IBasket basket);

		IBasketLine CreateBasketLine(IBasket basket);
		void Save(IBasketLine basketLine);

		IOrderHeader CreateOrderHeader(IBasket basket);
		void Save(IOrderHeader header);

		IAddress CreateAddress();
		void Save(IAddress address);

		IList FindCountryList(int storeID);

        IUserDetails CreateUserDetails(User user);
        IUserDetails FindUserDetails(long userID);
        IUserDetails FindUserDetails(int storeID, string username);
        void Save(IUserDetails user);
 
		IPaymentRecord CreatePaymentRecord(IBasket basket);
		IList FindPayments(long orderID);
		IList FindPayments(string transactionReference);
		void Save(IPaymentRecord payment);

        //Used for export - Move!
        List<Cuyahoga.Modules.ECommerce.Domain.Product> GetAllProducts(int storeID, string cultureCode);

	}
}
