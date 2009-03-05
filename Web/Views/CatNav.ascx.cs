namespace Cuyahoga.Modules.ECommerce.Web.Views {

    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Web.UI.WebControls;
    using System.Configuration;
    using log4net;

    using Cuyahoga.Modules.ECommerce.Core;
    using Cuyahoga.Modules.ECommerce.Domain;
    using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
    using Cuyahoga.Modules.ECommerce.Util.Interfaces;
    using Cuyahoga.Modules.ECommerce.Util;
    using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
    using CatalogueNode = Cuyahoga.Modules.ECommerce.Domain.Catalogue.CategoryNode;
    using Cuyahoga.Web.UI;

    /// <summary>
    ///		Summary description for CatNav.
    /// </summary>
    public class CatNav : BaseModuleControl {

        protected System.Web.UI.WebControls.Repeater rptCategories;
        protected System.Web.UI.WebControls.Repeater rptCategoriesProdLis;
        protected System.Web.UI.WebControls.Panel pnlCatImage;
        protected System.Web.UI.WebControls.Label lblTitle;
        protected System.Web.UI.WebControls.Label lblDescription;
        protected Panel pnlSummary;

        protected Cuyahoga.Modules.ECommerce.Web.Controls.BreadCrumbTrail ctlBreadCrumb;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.MenuTabs ctlMenuTabs;
        protected CatalogueUrlHelper UrlHelper;

        protected bool IsRootNode;
        protected bool IsSubCategory;
        protected bool IsSubSubCategory;
        protected bool IsProductList;
        protected bool IsCategory;

        protected string BannerImageUrl = "";
        protected string CatImageAltText = "";
        private CatalogueViewModule _mod;
        protected PlaceHolder plhFlashAnimation;
        protected PlaceHolder plhKitLink;
        protected HyperLink hplKitLink;
        protected Label lblKitDescription;
        protected Image imgKit;

        protected Controls.ProductList ctlProductList;

        protected int coulmnCounter {
            get {
                if (ViewState["columnCounter"] != null) {
                    return (int)ViewState["columnCounter"];
                }
                return 0;
            }

            set {
                ViewState["columnCounter"] = value;
            }
        }

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
                ICategoryView cat;

                 ctlProductList.TotalResults = CatMod.CatalogueViewer.GetProductCount(CatMod.Section.Node.Site.Id, CatMod.Section.Node.Culture, CatMod.NodeID);
                ctlProductList.CreateListNavigation();
                int page = 1;
                if (ctlProductList.ddlPage.SelectedValue != String.Empty) {
                    page = Convert.ToInt32(ctlProductList.ddlPage.SelectedValue);
                }

                //I dont like this
                string orderBy = "p.basePrice desc";
                switch (ctlProductList.ddlSortBy.SelectedValue) {
                    case "PriceHighest":
                        orderBy = "p.basePrice desc";
                        break;
                    case "PriceLowest":
                        orderBy = "p.basePrice ASC";
                        break;
                    case "ProductName":
                        orderBy = "p.ProductName ASC";
                        break;
                    default:
                        break;
                }

                int offset = 0;

                try {
                    offset = ((Convert.ToInt32(ctlProductList.ddlResultsPerPage.SelectedValue) * Convert.ToInt32(ctlProductList.ddlPage.SelectedValue) + 1) - Convert.ToInt32(ctlProductList.ddlResultsPerPage.SelectedValue));
                } catch(Exception ex){
                    LogManager.GetLogger(GetType()).Error(ex);
                }
                
                if (CatMod.NodeID > 0) { //gets top level categories if no node supplied
                    cat = CatMod.CatalogueViewer.GetCategoryView(CatMod.Section.Node.Site.Id, CatMod.Section.Node.Culture, CatMod.NodeID, offset, Convert.ToInt32(ctlProductList.ddlResultsPerPage.SelectedValue), orderBy);
                } else {
                    cat = CatMod.CatalogueViewer.GetRootCategoryView(CatMod.Section.Node.Site.Id, CatMod.Section.Node.Culture);
                }              


                
                if (cat != null) {

                    List<ICategory> categoryList = cat.ChildNodes;
                    List<IProductSummary> productList = cat.ProductList;

                    //If there is just one product, jump to that product page
                    if (categoryList.Count == 0 && productList.Count == 1) {
                        string productUrl = new CatalogueUrlHelper(CatMod).GetProductUrl((IProductSummary)productList[0]);
                        Response.Redirect(productUrl);
                        return;
                    }

                    pnlSummary.Visible = false;

                    if (cat.ProductList.Count == 0) {

                        rptCategories.DataSource = categoryList;
                        rptCategories.DataBind();

                        pnlSummary.Visible = true;

                        if (lblTitle != null) {
                            lblTitle.Text = cat.CurrentNode.Name;
                        }
                        if (lblDescription != null) {
                            lblDescription.Text = cat.CurrentNode.Description;
                        }

                        if (pnlCatImage != null) {
                            if (cat.CurrentNode.BannerImageUrl != null && !string.IsNullOrEmpty(cat.CurrentNode.BannerImageUrl)) {
                                pnlCatImage.Visible = true;
                                BannerImageUrl = WebHelper.GetImagePathWeb() + cat.CurrentNode.BannerImageUrl;
                                CatImageAltText = cat.CurrentNode.Image.AltText;
                            }
                        }
                    }

                    if (cat.ProductList.Count > 0) {

                        ctlProductList.BindProductList(productList, cat.CurrentNode);
                        ctlProductList.Visible = true;
                    }

                    if (ctlBreadCrumb != null) {
                        ctlBreadCrumb.RenderBreadCrumbTrail(cat.BreadCrumbTrail);
                    }                  
                }

            } catch (System.Threading.ThreadAbortException error) {
                LogManager.GetLogger(GetType()).Error(error);
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void rptCategories_ItemDataBound(object source, RepeaterItemEventArgs e) {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) {
                return;
            }
            Image img = (Image)e.Item.FindControl("imgSubCategory");
            CatalogueNode node = e.Item.DataItem as CatalogueNode;

            if (node.Image != null && node.Image.Url != null) {
                img.ImageUrl = node.Image.Url;
            }

            Literal litHeader = (Literal)e.Item.FindControl("rowHeader");
            Literal litFooter = (Literal)e.Item.FindControl("rowFooter");

            if (coulmnCounter < WebHelper.GetMaxCoulmnsPerRow() || coulmnCounter == 0) {
                litHeader.Visible = litFooter.Visible = false;
                coulmnCounter++;
            } else {
                litHeader.Visible = litFooter.Visible = true;
                coulmnCounter = 0;
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

            rptCategories.ItemDataBound += new RepeaterItemEventHandler(rptCategories_ItemDataBound);

            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }
}