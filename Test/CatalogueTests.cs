using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

using Cuyahoga.Modules.ECommerce.Service;

using System.Collections;

using Castle.Facilities.NHibernateIntegration;
using Castle.MicroKernel;
using Castle.Windsor;

using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;

using Cuyahoga.Core.Domain;
using Cuyahoga.Core.Service;
using Cuyahoga.Core.DataAccess;
using Castle.Windsor.Configuration.Interpreters;

using Cuyahoga.Web.Components;
using Cuyahoga.Modules.ECommerce.DataAccess;

using NHibernate;

using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;

namespace Cuyahoga.Modules.ECommerce.Test {

    [TestFixture]
    class CatalogueTests : TestBase {

        private const int PRODUCT_ID = 101;
        private const string PRODUCT_SKU = "18-013-013";
        private const int STORE_ID = 1;
        private const string CULTURE_CODE = "en-GB";

        [Test]
        public void TestGetRootCatalogueNode() {

            ICatalogueViewService service = Container[typeof(ICatalogueViewService)] as ICatalogueViewService;
            ICatalogueNode node = service.GetRootCatalogueNode(STORE_ID, CULTURE_CODE);
            Assert.IsNotNull(node);

            IList<ICatalogueNode> children = service.GetCatalogueNodeChildren(STORE_ID, CULTURE_CODE, node.NodeID);
            Assert.IsTrue(children.Count > 0, "No children");
        }

        /*
        [Test]
        public void TestFindProductSummaries() {
            ICatalogueViewService service = new GwsCatalogueViewService();
            IProductResultSet results = service.FindProductSummaries(STORE_ID, CULTURE_CODE, "valve", 10, 1, 1, -1);
            Assert.IsTrue(results.MetaData.ResultCount > 0);
            Assert.IsTrue(results.ProductList.Count > 0);
        }

        [Test]
        public void TestGetBreadCrumbTrailID() {
            ICatalogueViewService service = new GwsCatalogueViewService();
            List<ICatalogueNode> nodeList = service.GetBreadCrumbTrail(STORE_ID, CULTURE_CODE, PRODUCT_ID);
            Assert.IsTrue(nodeList.Count > 0);
        }

        [Test]
        public void TestGetBreadCrumbTrailSku() {
            ICatalogueViewService service = new GwsCatalogueViewService();
            List<ICatalogueNode> nodeList = service.GetBreadCrumbTrail(STORE_ID, CULTURE_CODE, PRODUCT_SKU);
            Assert.IsTrue(nodeList.Count > 0);
        }

        [Test]
        public void TestGetProductSku() {
            ICatalogueViewService service = GwsCatalogueViewService();
            IProduct product = service.GetProduct(STORE_ID, CULTURE_CODE, PRODUCT_SKU);
            Assert.IsTrue(product.StaticAttributeList.Count > 0);
        }

        [Test]
        public void TestGetProductID() {
            ICatalogueViewService service = new GwsCatalogueViewService();
            IProduct product = service.GetProduct(STORE_ID, CULTURE_CODE, PRODUCT_ID);
            Assert.IsTrue(product.StaticAttributeList.Count > 0);
        }
         */
    }
}
