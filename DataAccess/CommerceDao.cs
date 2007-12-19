using System;
using System.Collections;

using Castle.Facilities.NHibernateIntegration;
using Castle.Services.Transaction;

using Cuyahoga.Core.Domain;

using Cuyahoga.Modules.ECommerce.DataAccess;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util.Location;

namespace Cuyahoga.Modules.ECommerce.DataAccess {

    [Transactional]
    public class CommerceDao : ICommerceDao {

        private ISessionManager _sessionManager;
        private IExtCommonDao _commonDao;

        public CommerceDao(ISessionManager sessionManager, IExtCommonDao commonDao) {
            _sessionManager = sessionManager;
            _commonDao = commonDao;
        }

        #region ICommerceDao Members

        public IBasket CreateBasket(IStoreContext context) {
            Basket basket = new Basket(context);
            _commonDao.SaveOrUpdateObject(basket);
            return basket;
        }

        public IBasket FindBasket(long basketID) {
            return _commonDao.GetObjectById(typeof(Basket), basketID) as Basket;
        }

        public IBasket FindOrder(long orderID) {

            NHibernate.IQuery query = _sessionManager.OpenSession().CreateQuery("from Basket b where b.OrderHeader.OrderHeaderID = :orderID");
            query.SetInt64("orderID", orderID);
            IList results = query.List();

            if (results != null && results.Count == 1) {
                return results[0] as IBasket;
            }

            return null;
        }

        public IBasketLine FindBasketLine(long basketLineID) {
            return _commonDao.GetObjectById(typeof(BasketItem), basketLineID) as BasketItem;
        }

        public IList FindOrderByDateRange(int storeID, string username, DateTime startDate, DateTime endDate) {
            throw new Exception("The method or operation is not implemented.");
        }

        [Transaction(TransactionMode.RequiresNew)]
        public void Save(IBasket basket) {

            Basket b = basket as Basket;

            if (b != null) {
                _commonDao.SaveOrUpdateObject(b);
            }
        }

        public IBasketLine CreateBasketLine(IBasket basket) {

            Basket b = basket as Basket;

            if (b != null) {
                BasketItem item = new BasketItem();
                item.Basket = b;
                return item;
            }

            return null;
        }

        public void Save(IBasketLine basketLine) {
            _commonDao.SaveOrUpdateObject(basketLine as BasketItem);
        }

        public IOrderHeader CreateOrderHeader(IBasket basket) {
            
            OrderHeader header = new OrderHeader();
            header.CreatedDate = DateTime.Now;

            return header;
        }

        public void Save(IOrderHeader header) {
            _commonDao.SaveOrUpdateObject(header);
        }

        public IAddress CreateAddress() {

            Address address = new Address();
            address.InsertTimestamp = DateTime.Now;
            address.UpdateTimestamp = DateTime.Now;

            return address;
        }

        public void Save(IAddress address) {
            _commonDao.SaveOrUpdateObject(address);
        }

        public IList FindCountryList(int storeID) {
            throw new Exception("The method or operation is not implemented.");
        }

        public IUserDetails CreateUserDetails(User user) {

            UserDetail userDetails = new UserDetail();
            userDetails.UserID = user.Id;

            return userDetails;
        }

        public IUserDetails FindUserDetails(long userID) {
            return _commonDao.GetObjectById(typeof(UserDetail), userID) as UserDetail;
        }

        public IUserDetails FindUserDetails(int storeID, string username) {
            return _commonDao.GetObjectById(typeof(UserDetail), 0) as UserDetail;
        }

        public void Save(IUserDetails user) {
            _commonDao.SaveOrUpdateObject(user);
        }

        public IPaymentRecord CreatePaymentRecord(IBasket basket) {
            Payment payment = new Payment();
            //do some stuff
            return payment;
        }

        public IList FindPayments(long orderID) {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList FindPayments(string transactionReference) {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Save(IPaymentRecord payment) {
            _commonDao.SaveOrUpdateObject(payment);
        }

        #endregion
    }
}
