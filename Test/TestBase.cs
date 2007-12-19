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

namespace Cuyahoga.Modules.ECommerce.Test {

    [TestFixture]
    public class TestBase {

        private IWindsorContainer _container;

        [SetUp]
        public void SetUp() {
            _container = new CuyahogaContainer();
            SessionFactoryHelper helper = new SessionFactoryHelper(_container.Kernel);
            helper.AddAssembly(typeof(Cuyahoga.Modules.ECommerce.Domain.Basket).Assembly);
        }

        [TearDown]
        public void TearDown() {
            if (Container != null) {
                Container.Dispose();
            }
        }

        protected IWindsorContainer Container {
            get {
                return _container;
            }
        }
    }
}