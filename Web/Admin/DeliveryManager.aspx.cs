using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cuyahoga.Web.UI;
using Cuyahoga.Core.Util;

using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Domain;
using System.Collections.Generic;
using Koutny.WebControls;
using Cuyahoga.Modules.ECommerce.Core;
using System.IO;
using Cuyahoga.Modules.ECommerce.DataAccess;
using log4net;


namespace Cuyahoga.Modules.ECommerce.Web.Admin {
    public class DeliveryManager : ModuleAdminBasePage {
        protected HyperLink hplList;
            private ModuleAdminBasePage _page; 
        protected void Page_Load(object sender, EventArgs e) {
        
            this._page = (ModuleAdminBasePage)this.Page;
            hplList.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Delivery/DeliveryMethods.aspx{0}", this._page.GetBaseQueryString());
        }
    }
}
