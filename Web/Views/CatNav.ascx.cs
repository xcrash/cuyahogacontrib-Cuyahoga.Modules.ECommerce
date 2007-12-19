namespace Cuyahoga.Modules.ECommerce.Web.Views {

    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Web.UI.WebControls;

    using log4net;

    using Cuyahoga.Modules.ECommerce.Core;
    using Cuyahoga.Modules.ECommerce.Domain;
    using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
    using Cuyahoga.Modules.ECommerce.Util.Interfaces;
    using Cuyahoga.Modules.ECommerce.Util;
    using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
    using CatalogueNode = Cuyahoga.Modules.ECommerce.Domain.Catalogue.CatalogueNode;
    using Cuyahoga.Web.UI;

    /// <summary>
    ///		Summary description for CatNav.
    /// </summary>
    public class CatNav : BaseModuleControl {

        protected System.Web.UI.WebControls.Repeater rptCategories;
        protected System.Web.UI.WebControls.Repeater rptCategoriesProdList;
        protected System.Web.UI.WebControls.Panel pnlSummary;
        protected System.Web.UI.WebControls.Panel pnlCatImage;
        protected System.Web.UI.WebControls.Label lblTitle;
        protected System.Web.UI.WebControls.Label lblDescription;

        protected Cuyahoga.Modules.ECommerce.Web.Controls.BreadCrumbTrail ctlBreadCrumb;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.MenuTabs ctlMenuTabs;
        protected CatalogueUrlHelper UrlHelper;

        protected bool IsRootNode;
        protected bool IsSubCategory;
        protected bool IsSubSubCategory;
        protected bool IsProductList;
        protected bool IsCategory;

        protected string CatImageUrl = "";
        protected string CatImageAltText = "";
        private CatalogueViewModule _mod;
        protected PlaceHolder plhKitLink;
        protected HyperLink hplKitLink;
        protected Label lblKitDescription;
        protected Image imgKit;

        protected Controls.ProductList ctlProductList;

        private CatalogueViewModule CatMod {
            get {
                if (_mod == null) {
                    _mod = Module as CatalogueViewModule;
                }
                return _mod;
            }
        }



        private void Page_Load(object sender, System.EventArgs e) {

            UrlHelper = new CatalogueUrlHelper(CatMod);

            try {
                ICatalogueNodeView cat;

                if (CatMod.NodeID > 0) { //gets top level categories if no node supplied
                    cat = CatMod.CatalogueViewer.GetCatalogueNodeView(CatMod.Section.Node.Site.Id, CatMod.Section.Node.Culture, CatMod.NodeID);
                } else {
                    cat = CatMod.CatalogueViewer.GetRootCatalogueNodeView(CatMod.Section.Node.Site.Id, CatMod.Section.Node.Culture);
                }

                if (cat != null) {

                 

                    List<ICatalogueNode> categoryList = cat.ChildNodes;
                    List<IProductSummary> productList = cat.ProductList;

                    //If there is just one product, jump to that product page
                    if (categoryList.Count == 0 && productList.Count == 1) {
                        string productUrl = new CatalogueUrlHelper(CatMod).GetProductUrl((IProductSummary)productList[0]);
                        Response.Redirect(productUrl);
                        return;
                    }

                    if (categoryList.Count > 0) {
                        rptCategories.DataSource = categoryList;
                        rptCategories.DataBind();
                    } else {
                        ctlProductList.BindProductList(productList);
                    }
                    if (ctlBreadCrumb != null) {
                        ctlBreadCrumb.RenderBreadCrumbTrail(cat.BreadCrumbTrail);
                     }

                        if (pnlCatImage != null) {
                            if (cat.CurrentNode.Image != null && !string.IsNullOrEmpty(cat.CurrentNode.Image.Url)) {
                                pnlCatImage.Visible = true;
                                CatImageUrl = WebHelper.GetImagePathWeb() + cat.CurrentNode.Image.Url;
                                CatImageAltText = cat.CurrentNode.Image.AltText;
                            }
                        }
                  
                        if (lblTitle != null) {
                            lblTitle.Text = cat.CurrentNode.Name;
                        }
                        if (lblDescription != null) {
                            lblDescription.Text = cat.CurrentNode.Description;
                        }
                    }
                

            } catch (System.Threading.ThreadAbortException error) {
                LogManager.GetLogger(GetType()).Error(error);
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
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

            if (ctlMenuTabs != null) {
                ctlMenuTabs.Module = Module;
            }

            if (ctlProductList != null) {
                ctlProductList.Module = Module;
            }

            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }
}