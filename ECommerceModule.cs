using System;
using Cuyahoga.Core;
using Cuyahoga.Core.DataAccess;
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.DataAccess;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Service;
using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;
using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
using Cuyahoga.Core.Service;
using Cuyahoga.Core.Service.Email;

namespace Cuyahoga.Modules.ECommerce {

    /// <summary>
    /// Summary description for CatalogueViewModule.
    /// </summary>
    public class ECommerceModule : CatalogueViewModule, INHibernateModule {

        private ICommerceDao _dao;
        private IExtCommonDao _commonDao;
        private IPaymentProvider _paymentProvider;
        private IBasketRules _rules;

        public ECommerceModule(ICatalogueViewService catatalogueService, ICommerceService commerceService, ICatalogueModificationService editService, IAccountService accountService, IOrderService orderService, IEmailSender emailSender, IDeliveryService deliveryService, ICultureService cultureService) : base(catatalogueService, commerceService, editService, accountService, orderService, emailSender, deliveryService, cultureService) {
        }

        public ECommerceModule(ICommerceService commerceService, ICatalogueViewService catatalogueService, ICommerceDao dao, IExtCommonDao commonDao, IPaymentProvider paymentProvider, IBasketRules rules, ICatalogueModificationService editService, IAccountService accountService, IOrderService orderService, IEmailSender emailSender, IDeliveryService deliveryService, ICultureService cultureService)
            : base(catatalogueService, commerceService, editService, accountService, orderService, emailSender, deliveryService, cultureService) {

            _dao = dao;
            _commonDao = commonDao;
            _paymentProvider = paymentProvider;
            _rules = rules;
        }

        public ICommerceDao CommerceDao {
            get {
                return _dao;
            }
        }

        public IExtCommonDao CommonDao {
            get {
                return _commonDao;
            }
        }

        public IPaymentProvider PaymentProvider {
            get {
                return _paymentProvider;
            }
        }

        public IBasketRules Rules {
            get {
                return _rules;
            }
        }
    }
}
