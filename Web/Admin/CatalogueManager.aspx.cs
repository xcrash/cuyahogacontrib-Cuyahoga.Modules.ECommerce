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

namespace Cuyahoga.Modules.ECommerce.Web.Admin {

    public class CatalogueManager : ModuleAdminBasePage {

        private ModuleAdminBasePage _page;

        protected HyperLink hplBrowse;
        protected HyperLink hplExport;
        protected HyperLink hplImport;

        protected void Page_Load(object sender, EventArgs e) {

            this._page = (ModuleAdminBasePage)this.Page;

            hplBrowse.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Catalogue/CatalogueBrowser.aspx{0}", this._page.GetBaseQueryString());

            if (hplExport != null) {
                hplExport.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Catalogue/CSVExport.aspx{0}", this._page.GetBaseQueryString());
            }

            if (hplImport != null) {
                hplImport.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Catalogue/CSVImport.aspx{0}", this._page.GetBaseQueryString());
            }
        }
    }
}