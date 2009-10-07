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
using Cuyahoga.Core.Service.Membership;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce {

    /// <summary>
    /// Summary description for CatalogueViewModule.
    /// </summary>
    public class ECommerceModule : CatalogueViewModule, INHibernateModule {

        private ICommerceDao _dao;
        private IExtCommonDao _commonDao;
        private List<IPaymentProvider> _paymentProviders;
        private IPaymentProvider _paymentProvider;
        private IBasketRules _rules;

        public ECommerceModule(ICatalogueViewService catatalogueService, ICommerceService commerceService, ICatalogueModificationService editService, IAccountService accountService, IOrderService orderService, IEmailSender emailSender, IDeliveryService deliveryService, ICultureService cultureService, IUserService userService) : base(catatalogueService, commerceService, editService, accountService, orderService, emailSender, deliveryService, cultureService, userService) {
        }

        public ECommerceModule(ICommerceService commerceService, ICatalogueViewService catatalogueService, ICommerceDao dao, IExtCommonDao commonDao, IBasketRules rules, ICatalogueModificationService editService, IAccountService accountService, IOrderService orderService, IEmailSender emailSender, IDeliveryService deliveryService, ICultureService cultureService, IUserService userService)
            : base(catatalogueService, commerceService, editService, accountService, orderService, emailSender, deliveryService, cultureService, userService) {

            _dao = dao;
            _commonDao = commonDao;
            _rules = rules; 
            
            //we now get payment providers from the DB to make it more user friendly
            //PaymentProviders = CommerceService.GetEnabledPaymentProviders();
           

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

            set {
                _paymentProvider = value;
            }
        }

        public List<IPaymentProvider> PaymentProviders {
            get {
                return _paymentProviders;
            }

            set {
                _paymentProviders = value;
            }
        }

        public IBasketRules Rules {
            get {
                return _rules;
            }
        }
    }
}
