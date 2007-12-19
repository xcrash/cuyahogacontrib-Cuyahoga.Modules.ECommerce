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

using Cuyahoga.Core;
using Cuyahoga.Core.Domain;
using Cuyahoga.Core.Service;
using Cuyahoga.Core.Search;

using Cuyahoga.Core.Communication;

using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using System.Collections.Generic;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {

    public class ConfirmDelete : ModuleAdminBasePage {

        public const string PARAM_CATEGORY_ID = "cat";
        public const string PARAM_PRODUCT_ID = "pid";

        protected long catid;
        protected int pid;
        private ModuleAdminBasePage _page;
        protected LinkButton lnkConfirm;
        protected LinkButton lnkCancel;
        protected Controls.BreadCrumb ctlBreadCrumb;

        public string Url {
            get {
                if (ViewState["url"] != null) {
                    return (string)ViewState["url"];
                } return "";
            }
            set {
                ViewState["url"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

            this.Title = "Confirm Delete";
            this._page = (ModuleAdminBasePage)this.Page;

            ReadRequestParameters();

            if (!IsPostBack) {

                CatalogueViewModule mod = Module as CatalogueViewModule;

                if (catid > 0) {                    
                    
                    Category cat = mod.CatalogueViewer.GetCategory(1, "", catid);
                    long parentCatID = (cat != null) ? cat.ParentCategory.NodeID : catid;
                    
                    Url = CatalogueBrowser.GetBrowserUrl(this, parentCatID, "Category delete cancelled");

                } else if (pid > 0) {
                    Url = CatalogueBrowser.GetBrowserUrlForProduct(this, mod, pid, "Product delete cancelled");
                } else {
                    Url = Request.UrlReferrer.ToString();
                }
            }

            ECommerceModule controller = Module as ECommerceModule;
            Service.ICatalogueViewService service = controller.CatalogueViewer;

            if (catid > 0) {
                ICatalogueNode node = new CatalogueNode();
                node.NodeID = catid;
                ctlBreadCrumb.RenderBreadCrumbTrail(service.GetCategoryBreadCrumb(node));
            } else {
                ctlBreadCrumb.RenderBreadCrumbTrail(service.GetProductBreadCrumbTrail(pid));
            }

            lnkConfirm.Click += new System.EventHandler(Confirm_Click);
            lnkCancel.Click += new System.EventHandler(Cancel_Click);
        }

        private void ReadRequestParameters() {
            pid = 0;
            catid = 0;
            try {
                pid = Int32.Parse(Request.Params[PARAM_PRODUCT_ID]);
            } catch { }

            try {
                catid = Int64.Parse(Request.Params[PARAM_CATEGORY_ID]);
            } catch { }
        }

        public void Confirm_Click(object sender, System.EventArgs e) {
            try {
                if (catid != 0) {
                    Response.Redirect(String.Format("~/Modules/ECommerce/Admin/Catalogue/CategoryDelete.aspx{0}", this._page.GetBaseQueryString()) + "&cat=" + Int64.Parse(Request.Params[PARAM_CATEGORY_ID]) + "&Confirmed=true");
                }
            } catch { }

            try {
                if (pid != 0) {
                    Response.Redirect(String.Format("~/Modules/ECommerce/Admin/Catalogue/ProductDelete.aspx{0}", this._page.GetBaseQueryString()) + "&pid=" + Int32.Parse(Request.Params[PARAM_PRODUCT_ID]) + "&Confirmed=true");
                }
            } catch { }
        }

        public void Cancel_Click(object sender, System.EventArgs e) {
            Response.Redirect(Url);
        }
    }
}