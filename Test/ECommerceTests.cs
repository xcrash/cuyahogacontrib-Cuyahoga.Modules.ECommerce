using System;
using System.Collections.Generic;
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
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

using Cuyahoga.Core.Domain;
using Cuyahoga.Core.Service;
using Cuyahoga.Core.Service.Email;
using Cuyahoga.Core.DataAccess;
using Castle.Windsor.Configuration.Interpreters;

using Cuyahoga.Web.Components;
using Cuyahoga.Modules.ECommerce.DataAccess;

using NHibernate;

using Cuyahoga.Modules.ECommerce.Service;
using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;
using Igentics.Common.Logging;
using System.Xml;

namespace Cuyahoga.Modules.ECommerce.Test {

    [TestFixture]
    public class ECommerceTests : TestBase {

        private const int PRODUCT_ID = 101;
        private const string PRODUCT_SKU = "18-013-013";
        private const int STORE_ID = 1;
        private const string CULTURE_CODE = "en-GB";

        [Test]
        public void TestOrderProcessorFactory() {
            object o = ServiceFactory.GetService(typeof(IOrderProcessorFactory));
            Assert.IsNotNull(o);
            IOrderProcessorFactory f = o as IOrderProcessorFactory;
            Assert.IsNotNull(f);
        }

        [Test]
        public void TestBasketDao() {

            User user = new User();

            IStoreContext context = new StoreContext(user);
            context.CurrencyCode = "GBP";

            ECommerceModule module = CreateModule();
            List<IProductSummary> l = (ServiceFactory.GetService(typeof(ICatalogueViewService)) as ICatalogueViewService).FindProducts(1, "en-GB", "o");

            IBasketLine item1 = module.CommerceService.AddItem(context, l[0].ProductID, null, 3);
            IBasketLine item2 = module.CommerceService.AddItem(context, l[1].ProductID, null, 2);
            module.CommerceService.RefreshBasket(context);

            IBasket b = context.CurrentBasket;

            Assert.IsNotNull(b, "Null basket");
            Assert.AreEqual(3, b.BasketItemList.Count);

            context.CurrentBasket = null;
            b = module.CommerceService.GetCurrentBasket(context);

            Assert.IsNotNull(b, "Not found");
            Assert.IsTrue(b.BasketItemList.Count == 3);

            IBasketLine item3 = module.CommerceService.AddItem(context, l[0].ProductID, null, 3);
            module.CommerceService.RefreshBasket(context);
            Assert.AreEqual(3, b.BasketItemList.Count);

            module.CommerceService.RemoveItem(context, item1.BasketItemID);
            module.CommerceService.RefreshBasket(context);

            context.CurrentBasket = null;
            b = module.CommerceService.GetCurrentBasket(context);

            Assert.IsNotNull(b, "Not found");
            Assert.AreEqual(2, b.BasketItemList.Count);

            decimal tax = b.TaxPrice.Amount;
            decimal expectedTax = b.SubTotal.Amount * 0.175M;

            Assert.IsTrue(tax <= expectedTax * 1.01M && tax >= expectedTax * 0.99M, "Tax is wrong");

            OrderHeader header = new OrderHeader();
            header.CreatedDate = DateTime.Now;
            header.Comment = "TEST";
            header.PaymentMethod = Cuyahoga.Modules.ECommerce.Util.Enums.PaymentMethodType.CreditCard;
            header.PurchaseOrderNumber = StringUtils.GenerateRandomText(8);
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