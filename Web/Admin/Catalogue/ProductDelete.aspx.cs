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

namespace Cuyahoga.Modules.ECommerce.Web.Admin {

    public class ProductDelete : ModuleAdminBasePage {

        public const string PARAM_CONFIRMED = "confirmed";
        public const string PARAM_PRODUCT_ID = "pid";

        private ModuleAdminBasePage _page;
        private int _productID;
        private string _confirmed;
        
        protected Label lblDeletion;
        protected Domain.Product product = null;
        protected BreadCrumb ctlBreadCrumb;

        public int ProductID {
            get {
                return _productID;
            }
            set {
                _productID = value;
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
                ProductID = Int32.Parse(Request.Params[PARAM_PRODUCT_ID]);
                Confirmed = Request.Params[PARAM_CONFIRMED];
            } catch { }

            if (Confirmed == "true") {

                CatalogueViewModule controller = Module as CatalogueViewModule;

                if (ProductID > 0) {
                    product = controller.CatalogueViewer.GetECommerceProduct(controller.Section.Node.Site.Id, controller.Section.Node.Culture, ProductID);
                    ctlBreadCrumb.RenderBreadCrumbTrail(controller.CatalogueViewer.GetProductView(controller.Section.Node.Site.Id, controller.Section.Node.Culture, ProductID).BreadCrumbTrail);
                }

                string message = "The product was unable to be deleted. Please try again.";

                if (product != null) {
                    
                    product.IsPublished = false;

                    if (controller.EditService.SaveProduct(controller.Section.Node.Site.Id, product)) {
                        message = "The product was deleted successfully";
                    }
                }

                Response.Redirect(CatalogueBrowser.GetBrowserUrlForProduct(this, controller, ProductID, message));

            } else {
                //ask them to confirm
                Response.Redirect(String.Format("~/Modules/ECommerce/Admin/Catalogue/ConfirmDelete.aspx{0}", this._page.GetBaseQueryString()) + "&pid=" + ProductID);
            }
        }
    }
}