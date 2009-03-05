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
using Cuyahoga.Modules.ECommerce.Web.Admin.Controls;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {

    public partial class CatalogueBrowser : ModuleAdminBasePage {

        public const string PARAM_CATEGORY_ID = "cat";
        public const string PARAM_PRODUCT_ID = "pid";
        public const string PARAM_MESSAGE = "msg";

        private const int PROD_DEPTH = 2;
        private ICategoryView cat = null;

        protected Repeater rptCategories;
        protected Repeater rptProducts;
        protected CatalogueUrlHelper UrlHelper;
        protected CatalogueViewModule controller;
        protected BreadCrumb ctlBreadCrumb;
        protected Label lblMessage;
        protected Panel pnlMessage;

        private long _catID;
        private long _productID;

        public bool IsRoot {
            get { return (ParentID == 0); }
            set { ParentID = (value) ? 0 : ParentID; }
        }

        public long CatID {
            get { return _catID; }
            set { _catID = value; }
        }

        public long ProductID {
            get { return _productID; }
            set { _productID = value; }
        }

        public long ParentID {
            get {
                if (ViewState["ParentID"] != null) {
                    return (long)ViewState["ParentID"];
                } return 0;
            }
            set {
                ViewState["ParentID"] = value;
            }
        }

        public static string GetBrowserUrl(ModuleAdminBasePage page, long categoryID, string message) {
            return page.ResolveUrl("~/Modules/ECommerce/Admin/Catalogue/CatalogueBrowser.aspx" + page.GetBaseQueryString()
                + "&" + CatalogueBrowser.PARAM_CATEGORY_ID + "=" + categoryID
                + ((!string.IsNullOrEmpty(message)) ? ("&" + CatalogueBrowser.PARAM_MESSAGE + "=" + page.Server.UrlEncode(message)) : ""));
        }

        public static string GetBrowserUrlForProduct(ModuleAdminBasePage page, CatalogueViewModule module, long productID, string message) {

            long catID = 0;

            if (productID > 0) {

                Product product = module.CatalogueViewer.GetECommerceProduct(1, "", productID);

                if (product != null && product.Categories != null && product.Categories.Count > 0) {
                    ProductCategory pc = (ProductCategory)product.Categories[0];
                    catID = pc.CategoryID;
                }
            }

            return GetBrowserUrl(page, catID, message);
        }

        protected void Page_Load(object sender, EventArgs e) {

            string msg = Request[PARAM_MESSAGE];
            if (!string.IsNullOrEmpty(msg)) {
                lblMessage.Text = msg;
                pnlMessage.Visible = true;
            }

            CatalogueViewModule controller = Module as CatalogueViewModule;
            UrlHelper = new CatalogueUrlHelper(controller);

            try {
                CatID = Int32.Parse(Request.Params[PARAM_CATEGORY_ID]);
            } catch { }
            try {
                ProductID = Int32.Parse(Request.Params[PARAM_PRODUCT_ID]);
            } catch { }

            if (CatID > 0) {
                cat = controller.CatalogueViewer.GetCategoryView(controller.Section.Node.Site.Id, controller.Section.Node.Culture, CatID);
            } else {
                cat = controller.CatalogueViewer.GetRootCategoryView(controller.Section.Node.Site.Id, controller.Section.Node.Culture);
                CatID = cat.CurrentNode.NodeID;
                IsRoot = true;
            }

            if (cat != null) {

                List<ICategory> categoryList = cat.ChildNodes;
                List<IProductSummary> productList = cat.ProductList;

                ParentID = cat.CurrentNode.ParentNodeID;

                rptCategories.DataSource = categoryList;
                rptCategories.DataBind();

                rptProducts.DataSource = productList;
                rptProducts.DataBind();

                ctlBreadCrumb.RenderBreadCrumbTrail(cat.BreadCrumbTrail);
            }
        }

        protected bool ShowAddProduct {
            get {
                return (cat != null && (cat.BreadCrumbTrail.Count - 1) == PROD_DEPTH);
            }
        }

        protected bool ShowAddCategory {
            get {
                return !ShowAddProduct;
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            InitComp();
        }

        private void InitComp() {

            if (ctlBreadCrumb != null) {
                ctlBreadCrumb.Module = Module;
            }

            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

    }
}