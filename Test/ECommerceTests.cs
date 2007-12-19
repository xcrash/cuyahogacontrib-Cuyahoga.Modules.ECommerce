using System;
//using System.Collections.Generic;
using System.Collections;
using System.Text;
using NUnit.Framework;

using Castle.Facilities.NHibernateIntegration;
using Castle.MicroKernel;
using Castle.Windsor;

using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;

using Cuyahoga.Core.Domain;
using Cuyahoga.Core.Service;
using Cuyahoga.Core.DataAccess;
using Castle.Windsor.Configuration.Interpreters;

using Cuyahoga.Web.Components;
using Cuyahoga.Modules.ECommerce.DataAccess;

using NHibernate;

using Cuyahoga.Modules.ECommerce.Service;
using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;

namespace Cuyahoga.Modules.ECommerce.Test {

    [TestFixture]
    public class ECommerceTests : TestBase {

        [Test]
        public void TestBasketDao() {

            User user = new User();

            IStoreContext context = new StoreContext(user);
            context.CurrencyCode = "GBP";

            IExtCommonDao common = Container.Kernel[typeof(IExtCommonDao)] as IExtCommonDao;

            ECommerceModule module = new ECommerceModule(
                Container.Kernel[typeof(ICommerceService)] as ICommerceService,
                Container.Kernel[typeof(ICatalogueViewService)] as ICatalogueViewService,
                Container.Kernel[typeof(ICommerceDao)] as ICommerceDao,
                Container.Kernel[typeof(IPaymentProvider)] as IPaymentProvider,
                Container.Kernel[typeof(IBasketRules)] as IBasketRules,
                Container.Kernel[typeof(ICatalogueModificationService)] as ICatalogueModificationService,
                Container.Kernel[typeof(IAccountService)] as IAccountService,
                Container.Kernel[typeof(IOrderService)] as IOrderService
                );

            IBasketLine item1 = module.CommerceService.AddItem(context, 7, null, 3);
            IBasketLine item2 = module.CommerceService.AddItem(context, 8, null, 2);
            module.CommerceService.RefreshBasket(context);

            IBasket b = context.CurrentBasket;

            Assert.IsNotNull(b, "Null basket");
            Assert.AreEqual(2, b.BasketItemList.Count);

            context.CurrentBasket = null;
            b = module.CommerceService.GetCurrentBasket(context);

            Assert.IsNotNull(b, "Not found");
            Assert.IsTrue(b.BasketItemList.Count == 2);

            IBasketLine item3 = module.CommerceService.AddItem(context, 7, null, 3);
            module.CommerceService.RefreshBasket(context);
            Assert.AreEqual(2, b.BasketItemList.Count);

            module.CommerceService.RemoveItem(context, item1.BasketItemID);
            module.CommerceService.RefreshBasket(context);

            context.CurrentBasket = null;
            b = module.CommerceService.GetCurrentBasket(context);

            Assert.IsNotNull(b, "Not found");
            Assert.AreEqual(1, b.BasketItemList.Count);

            decimal tax = b.TaxPrice.Amount;
            decimal expectedTax = b.SubTotal.Amount * 0.175M;

            Assert.IsTrue(tax <= expectedTax * 1.01M && tax >= expectedTax * 0.99M, "Tax is wrong");

            OrderHeader header = new OrderHeader();
            header.CreatedDate = DateTime.Now;
            b.OrderHeader = header;

            module.CommerceService.RefreshBasket(context);

            b = module.CommerceService.GetCurrentBasket(context);
            Assert.IsNotNull(b.OrderHeader, "No header");
            Assert.IsTrue(b.OrderHeader.OrderHeaderID > 0, "No header ID");
        }

        [Test]
        public void TestTax() {
        }
    }
}