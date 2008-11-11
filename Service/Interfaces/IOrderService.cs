using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Domain;

namespace Cuyahoga.Modules.ECommerce.Service {

    public interface IOrderService {

        Basket GetOrderDetail(long OrderID);

        //Are you mad??
        List<Basket> GetAllOrders(int storeID, string cultureCode);

        IList<Basket> GetCompletedOrders(int pageSize, int pageNumber);
        int GetCompletedOrdersCount();

        bool UpdateOrderHeader(OrderHeader oh);
    }
}
