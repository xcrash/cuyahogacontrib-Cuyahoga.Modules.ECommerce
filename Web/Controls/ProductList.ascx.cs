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
    using Cuyahoga.Modules.ECommerce.Util.Enums;
    using Cuyahoga.Web.UI;
    using Koutny.WebControls;
    using System.Web.UI.WebControls;


    public class ProductList : LocalizedModuleConsumerControl {

        public enum DisplayMode {
            PlainList,
            KitList,
            SearchGlobal,
            SearchProductLine,
            Grid,
            List
        }

        protected HyperLink hplImagePopUp;


        public DropDownList ddlPage;
        public DropDownList ddlResultsPerPage;
        public DropDownList ddlSortBy;
        public DropDownList ddlDisplayMode;

        protected Literal litProductCount;
        protected Literal litPaginationFrom;
        protected Literal litPaginationTo;
        protected Literal litCategoryDescription;
        protected Literal litCategoryName;
        protected Panel pnlFilter;

        public const string CSS_CLASS_EVEN = "even";

        protected System.Web.UI.WebControls.Repeater rptProductsList;
        protected System.Web.UI.WebControls.Repeater rptProductsGrid;

        private CatalogueUrlHelper _urlHelper = null;

        protected string BannerImageUrl = "";
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

        public int TotalResults {
            get {
                if (ViewState["TotalResults"] != null) {
                    return (int)ViewState["TotalResults"];
                }
                return 0;
            }

            set {
                ViewState["TotalResults"] = value;
            }
        }

        protected Panel pnlCatImage;

        private List<IProductSummary> products;

        private void Page_Load(object sender, System.EventArgs e) {
            CreateListNavigation();
            pnlFilter.Visible = false;
        }

        public void CreateListNavigation() {
            int i = 1;
            ddlPage.Items.Clear();
            try {
                if (Convert.ToInt32(ddlResultsPerPage.SelectedValue) <= TotalResults) {

                    while (i <= (TotalResults / Convert.ToInt32(ddlResultsPerPage.SelectedValue))) {
                        ddlPage.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
                        i++;
                    }
                    string r = Request[ddlPage.UniqueID];

                    if (r != null)

                        ddlPage.SelectedValue = r;
                } else {
                    ddlPage.Items.Add(new ListItem("1", "1"));
                }
            } catch (Exception ex) {
                LogManager.GetLogger(GetType()).Info(ex);
            }

        }

        public void BindProductList(List<IProductSummary> productList, ICategory node) {
            try {
                products = productList;
                rptProductsGrid.DataSource = productList;
                rptProductsGrid.DataBind();
                rptProductsGrid.Visible = true;

                pnlFilter.Visible = (productList.Count > 0);

                litCategoryDescription.Text = node.Description;
                litCategoryName.Text = node.Name;
                litProductCount.Text = Convert.ToString(TotalResults);
                if (Convert.ToInt32(ddlResultsPerPage.SelectedValue) <= TotalResults) {
                    litPaginationFrom.Text = Convert.ToString(((Convert.ToInt32(ddlResultsPerPage.SelectedValue) * Convert.ToInt32(ddlPage.SelectedValue) + 1) - Convert.ToInt32(ddlResultsPerPage.SelectedValue)));
                } else {
                    litPaginationFrom.Text = "1";
                }

                int to = (Convert.ToInt32(ddlResultsPerPage.SelectedValue) * Convert.ToInt32(ddlPage.SelectedValue));
                litPaginationTo.Text = Convert.ToString(to);

                if (to > TotalResults) {
                    litPaginationTo.Text = Convert.ToString(TotalResults);
                }
                if (pnlCatImage != null) {
                    if (node.BannerImageUrl != null && !string.IsNullOrEmpty(node.BannerImageUrl)) {
                        pnlCatImage.Visible = true;
                        BannerImageUrl = WebHelper.GetImagePathWeb() + node.BannerImageUrl;
                        CatImageAltText = node.Image.AltText;
                    }
                }

            } catch (System.Threading.ThreadAbortException error) {
                LogManager.GetLogger(GetType()).Error(error);
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void rptProductsGrid_ItemDataBound(object source, RepeaterItemEventArgs e) {

            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) {
                return;
            }

            Literal litHeader = (Literal)e.Item.FindControl("rowHeader");
            Literal litFooter = (Literal)e.Item.FindControl("rowFooter");

            //Alternate items displayed differently?
            _itemCounter++;

            try {

                IProductSummary ps = e.Item.DataItem as IProductSummary;
                ScaledPriceProcessor priceScaler = new ScaledPriceProcessor();

                Literal litPrice = (Literal)e.Item.FindControl("litPrice");
                litPrice.Text = HtmlFormatUtils.FormatMoney(new Money(ps.Price)) + " " + ps.PriceDescription;

                RenderProductNameAndDescription(e, ps);
                RenderProductImage(e, ps);
                _itemCounter = 0;

                if (coulmnCounter < WebHelper.GetMaxCoulmnsPerRow() || coulmnCounter == 0) {
                    litHeader.Visible = litFooter.Visible = false;
                    coulmnCounter++;
                } else {
                    litHeader.Visible = litFooter.Visible = true;
                    coulmnCounter = 0;
                }

            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void rptProductsList_ItemDataBound(object source, RepeaterItemEventArgs e) {

            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) {
                return;
            }

            Literal litHeader = (Literal)e.Item.FindControl("rowHeader");
            Literal litFooter = (Literal)e.Item.FindControl("rowFooter");

            //Alternate items displayed differently?
            _itemCounter++;

            try {
                IProductSummary ps = e.Item.DataItem as IProductSummary;
                ScaledPriceProcessor priceScaler = new ScaledPriceProcessor();

                Literal litPrice = (Literal)e.Item.FindControl("litPrice");
                litPrice.Text = HtmlFormatUtils.FormatMoney(new Money(ps.Price)) + " " + ps.PriceDescription;

                RenderProductNameAndDescription(e, ps);
                RenderProductImage(e, ps);
                _itemCounter = 0;

            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void RenderProductImage(RepeaterItemEventArgs e, IProductSummary ps) {

            Image imgProduct = (Image)e.Item.FindControl("imgProduct");

            imgProduct.Visible = true;
            imgProduct.ImageUrl = WebHelper.GetImagePathWeb() + "blackberry8100_1.jpg"; //default Image

            if (ps.ProductImages.Count > 0) {
                IImage image = ps.ProductImages[0];
                imgProduct.ImageUrl = WebHelper.GetImagePathWeb() + image.Url;
            }
        }

        private void RenderProductNameAndDescription(RepeaterItemEventArgs e, IProductSummary ps) {

            HyperLink hplProductName = (HyperLink)e.Item.FindControl("hplProductName");
            Literal litDescription = (Literal)e.Item.FindControl("litDescription");

            litDescription.Text = ps.ShortDescription;
            hplProductName.Text = ps.Name;
            hplProductName.NavigateUrl = UrlHelper.GetProductUrl(ps.ProductID);
            hplProductName.Visible = true;
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
            rptProductsList.ItemDataBound += new RepeaterItemEventHandler(rptProductsList_ItemDataBound);
            rptProductsGrid.ItemDataBound += new RepeaterItemEventHandler(rptProductsGrid_ItemDataBound);
            ddlDisplayMode.SelectedIndexChanged += new EventHandler(ddlDisplayMode_SelectedIndexChanged);
            ddlResultsPerPage.SelectedIndexChanged += new EventHandler(ddlResultsPerPage_SelectedIndexChanged);
            ddlSortBy.SelectedIndexChanged += new EventHandler(ddlSortBy_SelectedIndexChanged);
            ddlPage.SelectedIndexChanged += new EventHandler(ddlPage_SelectedIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);
        }

        void ddlPage_SelectedIndexChanged(object sender, EventArgs e) {



        }

        void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e) {

        }

        void ddlResultsPerPage_SelectedIndexChanged(object sender, EventArgs e) {

        }



        void ddlDisplayMode_SelectedIndexChanged(object sender, EventArgs e) {

            if (ddlDisplayMode.SelectedValue == Convert.ToString(DisplayMode.Grid)) {
                rptProductsList.Visible = false;
                rptProductsGrid.DataSource = products;
                rptProductsGrid.DataBind();
                rptProductsGrid.Visible = true;
            } else {
                rptProductsList.Visible = true;
                rptProductsList.DataSource = products;
                rptProductsList.DataBind();
                rptProductsGrid.Visible = false;
            }
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