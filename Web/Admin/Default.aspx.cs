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

    public partial class _Default : ModuleAdminBasePage {

        private ECommerceModule _module;
        private ModuleAdminBasePage _page;

        protected System.Web.UI.WebControls.HyperLink hplOrderList;
        protected System.Web.UI.WebControls.HyperLink hplAttributes;
        protected System.Web.UI.WebControls.HyperLink hplCat;

        protected void Page_Load(object sender, EventArgs e) {

            this._module = base.Module as ECommerceModule;
            this._page = (ModuleAdminBasePage)this.Page;

            hplOrderList.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Orders/Orders.aspx{0}", this._page.GetBaseQueryString());

            hplCat.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Catalogue/CatalogueBrowser.aspx{0}", this._page.GetBaseQueryString());
            hplAttributes.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Catalogue/AttributeManager.aspx{0}", this._page.GetBaseQueryString());
        }
    }
}