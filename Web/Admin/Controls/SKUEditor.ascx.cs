using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using Cuyahoga.Web.Admin.Controls;
//using Cuyahoga.Web.Admin;
using Cuyahoga.Core.Domain;
//using Cuyahoga.Web.Admin.UI;
using Cuyahoga.Web.UI;
using Cuyahoga.Core.Util;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Core;
namespace Cuyahoga.Modules.ECommerce.Web.Admin.Controls {

    public partial class SKUEditor : LocalizedModuleConsumerControl {

        protected PlaceHolder plhSkuEditor;
        public IProduct product;

        protected void Page_Load(object sender, EventArgs e) {

        }
    }
}