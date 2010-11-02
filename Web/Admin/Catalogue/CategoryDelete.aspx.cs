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
using Cuyahoga.Modules.ECommerce.Web.Admin.Controls;
using Category = Cuyahoga.Modules.ECommerce.Domain.Category;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {

    public class CategoryDelete : ModuleAdminBasePage {

        public const string PARAM_CONFIRMED = "confirmed";
        public const string PARAM_CATEGORY_ID = "cat";

        protected Label lblDeletion;
        protected Category cat = null;
        protected BreadCrumb ctlBreadCrumb;

        private ModuleAdminBasePage _page;
        private string _confirmed;
        private long _catID;

        public long CatID {
            get {
                return _catID;
            }
            set {
                _catID = value;
            }
        }

        public string Confirmed {
            get {
                return _confirmed;
            }
            set {
                _confirmed = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

            this._page = (ModuleAdminBasePage)this.Page;
            try {
                CatID = Int64.Parse(Request.Params[PARAM_CATEGORY_ID]);
                Confirmed = Request.Params[PARAM_CONFIRMED];
            } catch {
            }

            if (Confirmed == "true") {

                CatalogueViewModule controller = Module as CatalogueViewModule;

                if (CatID > 0) {
                    cat = controller.CatalogueViewer.GetCategory(controller.Section.Node.Site.Id, controller.Section.Node.Culture, CatID);
                }

                string message = "The category was unable to be deleted. Please try again.";

                if (cat != null) {

                    ctlBreadCrumb.RenderBreadCrumbTrail(controller.CatalogueViewer.GetCategoryView(controller.Section.Node.Site.Id, controller.Section.Node.Culture, CatID).BreadCrumbTrail);
                    cat.IsPublished = false;

					try
					{
						controller.EditService.SaveCategory(controller.Section.Node.Site.Id, cat);
						message = "The category was deleted successfully";
					}
                    catch(Exception ex)
                    {
                    	message = ex.Message;
                    }
                }

                Response.Redirect(CatalogueBrowser.GetBrowserUrl(this, (cat != null) ? cat.ParentNodeID : CatID, message));

            } else {

                //ask them to confirm
                Response.Redirect(String.Format("~/Modules/ECommerce/Admin/Catalogue/ConfirmDelete.aspx{0}", this._page.GetBaseQueryString()) + "&cat=" + CatID);
            }
        }
    }
}