using System;

using Cuyahoga.Web.UI;




using Cuyahoga.Modules.ECommerce.DataAccess;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Delivery {
    public partial class ConfigureWeightBandingState  : ModuleAdminBasePage {
        private ModuleAdminBasePage _page; 
        protected void Page_Load(object sender, EventArgs e) {

            this._page = (ModuleAdminBasePage)this.Page;
            Response.Redirect(String.Format("~/Modules/ECommerce/Admin/Delivery/WeightBanding/ConfigureWeightBanding.aspx{0}", this._page.GetBaseQueryString()));
       
        }
    }
}   
       
