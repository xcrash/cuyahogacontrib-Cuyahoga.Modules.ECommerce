using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Castle.Facilities.NHibernateIntegration;
using Castle.MicroKernel;
using Castle.Windsor;

using Cuyahoga.Core.Domain;
using Cuyahoga.Core.Service;
using Cuyahoga.Core.DataAccess;
using Castle.Windsor.Configuration.Interpreters;

using Cuyahoga.Web.Components;
using Cuyahoga.Modules.ECommerce.DataAccess;

using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;

using Cuyahoga.Modules.ECommerce.Service;
using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;
using Cuyahoga.Core.Service.Email;


namespace Cuyahoga.Modules.ECommerce.Test {

    [TestFixture]
    public class TestBase {

        [SetUp]
        public void SetUp() {
        }

        [TearDown]
        public void TearDown() {
        }

        protected ECommerceModule CreateModule() {
            return new ECommerceModule(
                        ServiceFactory.GetService(typeof(ICommerceService)) as ICommerceService,
                        ServiceFactory.GetService(typeof(ICatalogueViewService)) as ICatalogueViewService,
                        ServiceFactory.GetService(typeof(ICommerceDao)) as ICommerceDao,
                        ServiceFactory.GetService(typeof(IExtCommonDao)) as IExtCommonDao,
                        ServiceFactory.GetService(typeof(IPaymentProvider)) as IPaymentProvider,
                        ServiceFactory.GetService(typeof(IBasketRules)) as IBasketRules,
                        ServiceFactory.GetService(typeof(ICatalogueModificationService)) as ICatalogueModificationService,
                        ServiceFactory.GetService(typeof(IAccountService)) as IAccountService,
                        ServiceFactory.GetService(typeof(IOrderService)) as IOrderService,
                        ServiceFactory.GetService(typeof(IEmailSender)) as IEmailSender,
                        ServiceFactory.GetService(typeof(IDeliveryService)) as IDeliveryService,
                        ServiceFactory.GetService(typeof(ICultureService)) as ICultureService
                    );
        }
    }
}