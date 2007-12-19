namespace Cuyahoga.Modules.ECommerce.Web.Controls {

    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Data.SqlClient;
    using log4net;
    using Cuyahoga.Modules.ECommerce.Core;
    using Cuyahoga.Modules.ECommerce.Domain;
    using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
    using Cuyahoga.Modules.ECommerce.Util.Interfaces;
    using Cuyahoga.Modules.ECommerce.Util;
    using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;

    using Cuyahoga.Web.UI;

    using System.Web.UI.WebControls;

    /// <summary>
    ///		Summary description for CatNav.
    /// </summary>
    public class ProductList : LocalizedModuleConsumerControl {

        public enum DisplayMode {
            PlainList,
            KitList,
            SearchGlobal,
            SearchProductLine
        }

        public const string CSS_CLASS_EVEN = "even";

        protected System.Web.UI.WebControls.Repeater rptProducts;
        private CatalogueUrlHelper _urlHelper = null;

        protected string CatImageUrl = "";
        protected string CatImageAltText = "";

        private CatalogueViewModule _mod;
        private DisplayMode _displayMode = DisplayMode.PlainList;
        private int _itemCounter = 0;

        protected CatalogueUrlHelper UrlHelper {
            get {
                if (_urlHelper == null) {
                    _urlHelper = new CatalogueUrlHelper(CatMod);
                }
                return _urlHelper;
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

        public DisplayMode Mode {
            get { return _displayMode; }
            set { _displayMode = value; }
        }

        protected string ItemClass {
            get { return ((_itemCounter % 2 == 0) ? CSS_CLASS_EVEN : ""); }
        }

  

        private void Page_Load(object sender, System.EventArgs e) {
        }

        public void BindProductList(List<IProductSummary> productList) {
            try {

                rptProducts.DataSource = productList;
                rptProducts.DataBind();
                rptProducts.Visible = (rptProducts.Items.Count > 0);

            } catch (System.Threading.ThreadAbortException error) {
                LogManager.GetLogger(GetType()).Error(error);
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void rptProducts_ItemDataBound(object source, RepeaterItemEventArgs e) {

            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) {
                return;
            }

            //Alternate items displayed differently?
            _itemCounter++;

            try {

         

                IProductSummary ps = e.Item.DataItem as IProductSummary;

                ScaledPriceProcessor priceScaler = new ScaledPriceProcessor();

                Literal litPrice = (Literal)e.Item.FindControl("litPrice");
                litPrice.Text = HtmlFormatUtils.FormatMoney(new Money(priceScaler.GetScaledPrice(ps))) + " " + ps.PriceDescription;

  
                    RenderProductNameAndDescription(e, ps);
                    RenderProductImage(e, ps);
                  


                    _itemCounter = 0;

              

            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

  
  

        private void RenderProductImage(RepeaterItemEventArgs e, IProductSummary ps) {

            Image imgProduct = (Image)e.Item.FindControl("imgProduct");

            if (ps.ProductImages.Count > 0) {
                
                IImage image = ps.ProductImages[0];
                imgProduct.ImageUrl = WebHelper.GetImagePathWeb() + image.Url;

                if (image.Width != null) {
                    imgProduct.Width = new Unit(Convert.ToInt32(image.Width));
                }

                if (image.Height != null) {
                    imgProduct.Height = new Unit(Convert.ToInt32(image.Height));
                }

                imgProduct.Visible = true;
            }
        }

        private void RenderProductNameAndDescription(RepeaterItemEventArgs e, IProductSummary ps) {

            Literal litProductName = (Literal)e.Item.FindControl("litProductName");
            HyperLink hplProductName = (HyperLink)e.Item.FindControl("hplProductName");
            Literal litDescription = (Literal)e.Item.FindControl("litDescription");

            
                litDescription.Text = ps.ShortDescription;
                hplProductName.Text = ps.Name;
                hplProductName.NavigateUrl = UrlHelper.GetProductUrl(ps.ProductID);
                hplProductName.Visible = true;
          
  
                litProductName.Visible = true;
          
        }

        private string GetProductLine(IProductSummary summary) {
            //This really isn't very efficient!
            try {
                using (SpHandler sph = new SpHandler("getProductLineForProduct", new SqlParameter("@productID", summary.ProductID))) {
                    sph.ExecuteReader();
                    if (sph.DataReader.Read()) {
                        return sph.DataReader["categoryname"].ToString();
                    }
                }
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }

            return "";
        }

        protected string GetAttrVal(IProductSummary summary, string arributeReference) {
            try {
                return Product.GetProductAttributeValueByReference(summary.ProductID, arributeReference);
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
                return "";
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
            rptProducts.ItemDataBound += new RepeaterItemEventHandler(rptProducts_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);
        }

        protected void AddToBasket(object sender, EventArgs e) {
            try {
                TextBox txtProdID = ((WebControl)sender).Parent.FindControl("txtProdID") as TextBox;

                if (txtProdID != null) {

                    int prodID = Int32.Parse(txtProdID.Text);
                    IStoreContext context = WebStoreContext.Current;

                    CatMod.CommerceService.AddItem(context, prodID, null, 1);
                    CatMod.CommerceService.RefreshBasket(context);
                }

            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }
        #endregion
    }
}