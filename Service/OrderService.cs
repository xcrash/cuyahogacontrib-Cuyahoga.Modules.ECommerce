using System;
using System.Collections;
using System.Collections.Generic;

using Castle.Facilities.NHibernateIntegration;

using Cuyahoga.Core;
using Cuyahoga.Modules.ECommerce.DataAccess;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce.Util.Enums;

using NHibernate;
using DbProduct = Cuyahoga.Modules.ECommerce.Domain.Product;


namespace Cuyahoga.Modules.ECommerce.Service {

    public class OrderService : IOrderService, INHibernateModule {

        private ISessionManager _sessionManager;
        private ICommerceDao _dao;

        public OrderService(ISessionManager sessionManager, ICommerceDao dao) {
            _sessionManager = sessionManager;
            _dao = dao;
        }

        public Basket GetOrderDetail(long orderID) {
            return _dao.FindOrder(orderID) as Basket;
        }

        public List<Basket> GetAllOrders(int storeID, string cultureCode) {

            List<Basket> oh = new List<Basket>();
            IQuery query = this._sessionManager.OpenSession().CreateQuery("from Basket");
            IList oList = query.List();

            foreach (Basket orderHeader in oList) {
                oh.Add(orderHeader);
            }

            return oh;
        }


        public bool UpdateOrderHeader(OrderHeader oh) {
            try {
                this._sessionManager.OpenSession().SaveOrUpdate(oh);
                this._sessionManager.OpenSession().Flush();
            } catch {
                return false;
            }
            return true;
        }

        public IList<Basket> GetCompletedOrders(int pageSize, int pageNumber) {

            List<Basket> orderList = new List<Basket>();

            IQuery query = this._sessionManager.OpenSession().CreateQuery(GetCompletedOrderQuery() + " order by b.CreatedDate desc");
            query.SetFirstResult(pageSize * pageNumber);
            query.SetMaxResults(pageSize);
            IList oList = query.List();

            foreach (Basket basket in oList) {
                orderList.Add(basket);
            }

            return orderList;
        }

        public int GetCompletedOrdersCount() {
            IQuery query = this._sessionManager.OpenSession().CreateQuery("select count(*) " + GetCompletedOrderQuery());
            return Convert.ToInt32(query.UniqueResult());
        }

        private string GetCompletedOrderQuery() {
            return "from Basket b where b.OrderHeader is not null and (b.AltUserDetails is not null or b.UserDetails is not null)"
                + " and ("
                    + "(b.OrderHeader._PaymentMethodID = " + ((short)PaymentMethodType.CreditCard)
                        + " and b.OrderHeader._OrderStatusID = " + ((int) OrderStatus.OrderedPaid) + ")"
                    + " or "
                    + "(b.OrderHeader._PaymentMethodID = " + ((short)PaymentMethodType.PurchaseOrderInvoice)
                        + " and b.OrderHeader._OrderStatusID = " + ((int)OrderStatus.OrderedSubmittedForAccountPayment) + ")"
                    + ")"
                ;
        }
    }
}