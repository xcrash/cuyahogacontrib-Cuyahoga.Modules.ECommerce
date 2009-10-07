using System;
using System.Collections.Generic;
using System.Collections;
using System.Web.UI.WebControls;
using System.Configuration;
using log4net;

using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
using CatalogueNode = Cuyahoga.Modules.ECommerce.Domain.Catalogue.CategoryNode;
using Cuyahoga.Web.UI;

namespace Cuyahoga.Modules.ECommerce.Web.Views {
    public partial class Account : BaseModuleControl {
        protected void Page_Load(object sender, EventArgs e) {

        }
    }
}