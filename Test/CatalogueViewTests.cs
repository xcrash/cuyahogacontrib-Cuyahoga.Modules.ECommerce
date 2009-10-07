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
    public class CatalogueViewTests : TestBase {

        private const int PRODUCT_ID = 101;
        private const string PRODUCT_SKU = "18-013-013";
        private const int STORE_ID = 1;
        private const string CULTURE_CODE = "en-GB";

        [Test]
        public void TestGetRootCatalogueNode() {

            ICatalogueViewService service = ServiceFactory.GetService(typeof(ICatalogueViewService)) as ICatalogueViewService;
            ICategoryView nodeView = service.GetRootCategoryView(STORE_ID, CULTURE_CODE);
            Assert.IsNotNull(nodeView);
            Assert.IsNotNull(nodeView.CurrentNode);

            IList<ICategory> children = service.GetCategoryView(STORE_ID, CULTURE_CODE, nodeView.CurrentNode.NodeID).ChildNodes;
            Assert.IsTrue(children.Count > 0, "No children");
        }

        [Test]
        [Ignore("Serialization doesn't work yet")]
        public void TestSerializeCatalogueNodeView() {

            ICatalogueViewService service = ServiceFactory.GetService(typeof(ICatalogueViewService)) as ICatalogueViewService;
            ICategoryView nodeView = service.GetRootCategoryView(STORE_ID, CULTURE_CODE);
            Assert.IsNotNull(nodeView);
            Assert.IsNotNull(nodeView.CurrentNode);

            XmlDocument xml = XMLUtils.SerializeObject(nodeView);
            LogManager.GetLogger(GetType()).Debug(xml.OuterXml);

            IList<ICategory> children = service.GetCategoryView(STORE_ID, CULTURE_CODE, nodeView.CurrentNode.NodeID).ChildNodes;

            xml = XMLUtils.SerializeObject(children[0]);
            LogManager.GetLogger(GetType()).Debug(xml.OuterXml);

            Assert.IsNotNull(xml);
        }

        [Test]
        public void TestFindProduct() {

            ICatalogueViewService service = ServiceFactory.GetService(typeof(ICatalogueViewService)) as ICatalogueViewService;
            IList<IProductSummary> results = service.FindProducts(STORE_ID, CULTURE_CODE, "o");
            Assert.IsTrue(results.Count > 0);

            IProductView view = service.GetProductView(STORE_ID, CULTURE_CODE, results[0].ProductID);
            Assert.IsNotNull(view);
        }

        [Test]
        [Ignore("Serialization doesn't work yet")]
        public void TestSerialiseProduct() {

            ICatalogueViewService service = ServiceFactory.GetService(typeof(ICatalogueViewService)) as ICatalogueViewService;
            IList<IProductSummary> results = service.FindProducts(STORE_ID, CULTURE_CODE, "o");

            XmlDocument xml = XMLUtils.SerializeObject(results[0]);
            LogManager.GetLogger(GetType()).Debug(xml.OuterXml);

            xml = XMLUtils.SerializeObject(service.GetProductView(STORE_ID, CULTURE_CODE, results[0].ProductID));
            LogManager.GetLogger(GetType()).Debug(xml.OuterXml);
        }
    }
}