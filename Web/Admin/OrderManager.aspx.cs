using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cuyahoga.Web.UI;
using Cuyahoga.Core.Util;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {
    public class OrderManager : ModuleAdminBasePage {

        private ModuleAdminBasePage _page;


        protected System.Web.UI.WebControls.HyperLink hplOrderList;
        protected System.Web.UI.WebControls.HyperLink hplOrderSearch;
        protected System.Web.UI.WebControls.HyperLink hplOrderStats;

        protected void Page_Load(object sender, EventArgs e) {
            this._page = (ModuleAdminBasePage)this.Page;
            hplOrderList.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Orders/Orders.aspx{0}", this._page.GetBaseQueryString());
            hplOrderSearch.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Orders/OrderSearch.aspx{0}", this._page.GetBaseQueryString());
            hplOrderStats.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Orders/OrderStats.aspx{0}", this._page.GetBaseQueryString());

        }
    }
}
