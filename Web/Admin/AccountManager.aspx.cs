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

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Accounts {
    public class AccountManager : ModuleAdminBasePage {
        private ModuleAdminBasePage _page;

        protected System.Web.UI.WebControls.HyperLink hplListAccount;
        protected System.Web.UI.WebControls.HyperLink hplSearchAccount;
        protected System.Web.UI.WebControls.HyperLink hplAddAccount;

        protected void Page_Load(object sender, EventArgs e) {
            this._page = (ModuleAdminBasePage)this.Page;

            hplListAccount.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Accounts/AccountList.aspx{0}", this._page.GetBaseQueryString());
            hplSearchAccount.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Accounts/AccountSearch.aspx{0}", this._page.GetBaseQueryString());
            hplAddAccount.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Accounts/AccountCreate.aspx{0}", this._page.GetBaseQueryString());
        }
    }
}
