namespace Cuyahoga.Modules.ECommerce.Web.Views {

    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using System.Data.SqlClient;

    using log4net;
    using Cuyahoga.Web.Controls;
    using Cuyahoga.Web.UI;

    using Cuyahoga.Modules.ECommerce.Core;
    using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
    using Cuyahoga.Modules.ECommerce.Util;
    using Cuyahoga.Modules.ECommerce.Util.Interfaces;
    using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
    using Cuyahoga.Modules.ECommerce.Web.Controls;

    using DbProduct = Domain.Product;

    public class ProdInfo : BaseModuleControl {

        protected Cuyahoga.Modules.ECommerce.Web.Controls.BreadCrumbTrail ctlBreadCrumb;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.Attributes ctlAttributes;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.ProductImages ctlImages;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.RelatedDocuments ctlDocuments;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.RelatedProducts ctlRelatedProducts;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.ProductDetails ctlDetails;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.Skus ctlSkus;

        protected TextBox txtProdID;
        protected TextBox txtQuantity;
        protected Literal litStocked;

        protected CatalogueUrlHelper UrlHelper;

        private CatalogueViewModule _mod;
        private CatalogueViewModule CatMod {
            get {
                if (_mod == null) {
                    _mod = Module as CatalogueViewModule;
                }
                return _mod;
            }
        }

        public IProductView prodView = null;

        //resx
        private string ITEM_NOT_STOCKED = " This item is not currently in stock";
        private string ITEM_STOCKED = " This item is currently in stock";

        private void Page_Load(object sender, System.EventArgs e) {

            UrlHelper = new CatalogueUrlHelper(CatMod);

            try {

                if (CatMod.ProductID > 0) {
                    prodView = CatMod.CatalogueViewer.GetProductView(CatMod.Section.Node.Site.Id, CatMod.Section.Node.Culture, CatMod.ProductID);
                } else {
                    DbProduct prod = CatMod.CatalogueViewer.GetECommerceProductByItemCode(CatMod.Section.Node.Site.Id, CatMod.Section.Node.Culture, CatMod.Sku);
                    if (prod != null) {
                        prodView = CatMod.CatalogueViewer.GetProductView(CatMod.Section.Node.Site.Id, CatMod.Section.Node.Culture, prod.ProductID);
                    }
                }
                
                if (prodView != null) {
                    List<IProduct> prodList = new List<IProduct>();
                    prodList.Add(prodView.ProductDetails);
                    
                    RenderProductDetails(prodList);
                    RenderBreadCrumbTrail(prodView);
                    txtProdID.Text = Convert.ToString(prodView.ProductDetails.ProductID);
                    ctlImages.RenderProductImages(prodView.ProductDetails.ProductImages);

                    if (Convert.ToBoolean(prodView.ProductDetails.StockedIndicator)) {
                        litStocked.Text = ITEM_STOCKED;
                    } else {
                        litStocked.Text = ITEM_NOT_STOCKED;
                    }

                }

            } catch (System.Threading.ThreadAbortException) {
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void RenderProductDetails(List<IProduct> prodList) {
            ctlDetails.RenderProductDetails(prodList);
        }

        private void RenderBreadCrumbTrail(IProductView prodView) {
            if (ctlBreadCrumb != null) {
                ctlBreadCrumb.RenderBreadCrumbTrail(prodView.BreadCrumbTrail);
            }
        }

        protected string GetProductUrl(IRelatedProducts product) {
            return UrlHelper.GetProductUrl(product.AccessoryPartNo);
        }

        protected void AddToBasket(object sender, EventArgs e) {
            try {
               

                if (txtProdID != null) {

                    int prodID = Int32.Parse(txtProdID.Text);
                    int quantity = Int32.Parse(txtQuantity.Text);
                    IStoreContext context = WebStoreContext.Current;

                    CatMod.CommerceService.AddItem(context, prodID, null, quantity);
                    CatMod.CommerceService.RefreshBasket(context);
                }

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

            if (ctlRelatedProducts != null) {
                ctlRelatedProducts.Module = Module;
            }

            if (ctlDocuments != null) {
                ctlDocuments.Module = Module;
            }

            if (ctlImages != null) {
                ctlImages.Module = Module;
            }

            if (ctlAttributes != null) {
                ctlAttributes.Module = Module;
            }

            if (ctlDetails != null) {
                ctlDetails.Module = Module;
            }
            
               this.Load += new System.EventHandler(this.Page_Load);
        }


        #endregion
    }
}