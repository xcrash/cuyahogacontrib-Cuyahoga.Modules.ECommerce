namespace Cuyahoga.Modules.ECommerce.Web.Views {
    using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
    using System;
    using System.Collections.Generic;
    using Cuyahoga.Modules.ECommerce.Util.Enums;
    using log4net;
    using Cuyahoga.Web.Controls;
    using Cuyahoga.Modules.ECommerce.Core;
    using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
    using Cuyahoga.Web.UI;
    using System.Web.UI.WebControls;
    using Cuyahoga.Modules.ECommerce.Util;
    using Cuyahoga.Modules.ECommerce.Util.Interfaces;

    /// <summary>
    ///		Summary description for CatNav.
    /// </summary>
    public class ProdInfo : BaseModuleControl {

        protected System.Web.UI.WebControls.LinkButton btnAddToBasket;
        protected System.Web.UI.WebControls.TextBox txtQuantity;

        protected Label lblDescription;
        protected Label lblTitle;
        protected Literal litPrice;
        protected System.Web.UI.WebControls.Image imgStockedIcon;
        
        protected Cuyahoga.Modules.ECommerce.Web.Controls.RelatedProducts ctlRelatedProducts;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.RelatedDocuments ctlRelatedDocuments;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.Skus ctlSkus;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.BreadCrumbTrail ctlBreadCrumb;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.MenuTabs ctlMenuTab;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.ProductImages ctlImages; 
        
        protected CatalogueUrlHelper UrlHelper;
        protected IProduct Product;

        public List<IActiveProductAttribute> ActiveAttributeList;

        private List<IActiveProductAttribute> ImageAttributesList = new List<IActiveProductAttribute>();
        private List<IActiveProductAttribute> TableAttributesList = new List<IActiveProductAttribute>();
        private List<IActiveProductAttribute> LinkAttributesList = new List<IActiveProductAttribute>();
        private List<IActiveProductAttribute> CheckBoxAttributesList = new List<IActiveProductAttribute>();
        private List<IActiveProductAttribute> TextAttributesList = new List<IActiveProductAttribute>();
        private List<IActiveProductAttribute> RadioAttributesList = new List<IActiveProductAttribute>();

        protected PlaceHolder plhAttributes;

        protected Repeater rptImageAttributes;
        protected Repeater rptLinkAttributes;
        protected Repeater rptTableAttributes;
        protected Repeater rptCheckBoxAttributes;
        protected Repeater rptTextAttributes;
        protected Repeater rptRadioAttributes;

        ECommerceModule catModule;

        private const string STOCKED_ICON = "../images/stocked.gif";
        private const string OUT_OF_STOCK_ICON = "../images/notinstock.gif";

        public string GetProductID(string itemCode){

            return catModule.CatalogueViewer.GetECommerceProductByItemCode(catModule.Section.Node.Site.Id, catModule.Section.Node.Culture, itemCode).ProductID.ToString();
        }

        protected string GetProductUrl(IRelatedProducts product) {
            return UrlHelper.GetProductUrl(product.AccessoryPartNo);
        }
       
        private void Page_Load(object sender, System.EventArgs e) {

            catModule = Module as ECommerceModule;
            UrlHelper = new CatalogueUrlHelper(catModule);

            if (catModule.ProductID != 0) {

               
                try {
                    IProductView prodView = null;

                    if (catModule.ProductID > 0) {
                        prodView = catModule.CatalogueViewer.GetProductView(catModule.Section.Node.Site.Id, catModule.Section.Node.Culture, catModule.ProductID);
                    } 
                   
                    if (prodView != null) {

                        Product = prodView.ProductDetails;
                        
                        ActiveAttributeList = Product.ActiveAttributeList;
                        ctlRelatedProducts.crossSellList = Product.CrossSellList;
                        ctlRelatedProducts.upSellList = Product.UpSellList;
                        ctlSkus.SkuList = Product.SkuList;
                        ctlRelatedDocuments.documentList = Product.DocumentList;
                        ctlImages.imageList = Product.ProductImages;
                        ctlBreadCrumb.RenderBreadCrumbTrail(prodView.BreadCrumbTrail);

                        DisplayAttributes();

                        lblDescription.Text = Product.Description;
                        lblTitle.Text = Product.Name;

                      

                        litPrice.Text = HtmlFormatUtils.FormatMoney(new Money(Product.Price));

                        if (Product.StockedIndicator == 1) { // why the hell is this not a bool?
                            imgStockedIcon.ImageUrl = STOCKED_ICON;
                        } else {
                            imgStockedIcon.ImageUrl = OUT_OF_STOCK_ICON;
                        }
                    }
                } catch (System.Threading.ThreadAbortException) {
                } catch (Exception f) {
                    LogManager.GetLogger(GetType()).Error(f);
                }
            } else {
                //Shouldn't need to access this again
                // Product = catModule.CatalogueViewer.GetProduct(catModule.Section.Node.Site.Id, catModule.Section.Node.Culture, catModule.ProductID);
            }
        }


        public void DisplayAttributes() {
            if (ActiveAttributeList != null) {
                if (ActiveAttributeList.Count > 0) {

                    foreach (IActiveProductAttribute a in ActiveAttributeList) {

                        if (a.DataType == AttributeType.Image.ToString()) {
                            ImageAttributesList.Add(a);
                        }

                        if (a.DataType == AttributeType.DropDown.ToString()) {
                            AddAttributeDropDown(a);
                        }

                        if (a.DataType == AttributeType.Table.ToString()) {
                            TableAttributesList.Add(a);
                        }

                        if (a.DataType == AttributeType.Link.ToString()) {
                            LinkAttributesList.Add(a);
                        }

                        if (a.DataType == AttributeType.CheckBox.ToString()) {
                            CheckBoxAttributesList.Add(a);
                        }

                        if(a.DataType == AttributeType.Text.ToString()){
                            TextAttributesList.Add(a);
                        }

                        if (a.DataType == AttributeType.Radio.ToString()) {
                            RadioAttributesList.Add(a);
                        }
                    }

                    if (CheckBoxAttributesList.Count > 0) {
                        rptCheckBoxAttributes.DataSource = CheckBoxAttributesList;
                        rptCheckBoxAttributes.DataBind();
                    }

                    if (ImageAttributesList.Count > 0) {
                        rptImageAttributes.DataSource = ImageAttributesList;
                        rptImageAttributes.DataBind();
                    }

                    if (LinkAttributesList.Count > 0) {
                        rptLinkAttributes.DataSource = LinkAttributesList;
                        rptLinkAttributes.DataBind();
                    }

                    if (TableAttributesList.Count > 0) {
                        rptTableAttributes.DataSource = TableAttributesList;
                        rptTableAttributes.DataBind();
                    }

                    if(TextAttributesList.Count > 0){
                        rptTextAttributes.DataSource = TextAttributesList;
                        rptTextAttributes.DataBind();
                    }

                    if (RadioAttributesList.Count > 0) {
                        rptRadioAttributes.DataSource = RadioAttributesList;
                        rptRadioAttributes.DataBind();
                    }
                }
            }
        }


        public void AddAttributeDropDown(IActiveProductAttribute a) {
            Label lblName = new Label();
            lblName.Text = "<h3>" + a.Name + "</h3>";

            Label lblSpacing = new Label();
            lblSpacing.Text = "<br/><br/> "; //ewww

            plhAttributes.Controls.Add(lblName);

            System.Web.UI.WebControls.DropDownList ddl = new System.Web.UI.WebControls.DropDownList();
            ddl.ID = a.Name;

            foreach (IAttributeOption o in a.AttributeOptionList) {
                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem();
                li.Text = o.PickListValue + " Price: " + o.Price;
                li.Value = o.ShortCode;
                ddl.Items.Add(li);
            }

            plhAttributes.Controls.Add(ddl);
            plhAttributes.Controls.Add(lblSpacing);
        }

        private void rptImages_ItemDataBound(object sender, RepeaterItemEventArgs e) {


            try {

                RepeaterItem item = e.Item;
                ActiveProductAttribute a = e.Item.DataItem as ActiveProductAttribute;

                if (item != null) {

                    DataList nestedTable = item.FindControl("dlTableAttribute") as DataList;

                    //if you have mutiple table attributes with different formats you will have to define multiple DataList's and pick the one you want.
                    if (nestedTable != null) {
                        nestedTable.DataSource = a.AttributeOptionList;
                        nestedTable.DataBind();
                    }
                }

            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void rptLinks_ItemDataBound(object sender, RepeaterItemEventArgs e) {


            try {

                RepeaterItem item = e.Item;
                ActiveProductAttribute a = e.Item.DataItem as ActiveProductAttribute;

                if (item != null) {

                    DataList nestedTable = item.FindControl("dlTableAttribute") as DataList;

                    //if you have mutiple table attributes with different formats you will have to define multiple DataList's and pick the one you want.
                    if (nestedTable != null) {
                        nestedTable.DataSource = a.AttributeOptionList;
                        nestedTable.DataBind();
                    }
                }

            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }


        private void rptTables_ItemDataBound(object sender, RepeaterItemEventArgs e) {

            try {

                RepeaterItem item = e.Item;
                ActiveProductAttribute a = e.Item.DataItem as ActiveProductAttribute;

                if (item != null) {

                    DataList nestedTable = item.FindControl("dlTableAttribute") as DataList;
                    Label lblTableAttribute = item.FindControl("lblTableAttribute") as Label;
                    //if you have mutiple table attributes with different formats you will have to define multiple DataList's and pick the one you want.
                    if (nestedTable != null) {
                        nestedTable.DataSource = a.AttributeOptionList;
                        nestedTable.DataBind();
                        lblTableAttribute.Text = "<h3>" + a.Name + "</h3>";
                    }
                }

            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void rptRadioButton_ItemDataBound(object sender, RepeaterItemEventArgs e) {

            try {

                RepeaterItem item = e.Item;
                ActiveProductAttribute a = (ActiveProductAttribute)item.DataItem;

                if (item != null) {

                    RadioButtonList nestedRepeater = item.FindControl("rblCheckBoxList") as RadioButtonList;
                    Label lblRadioAttribute = item.FindControl("lblRadioAttribute") as Label;

                    if (nestedRepeater != null) {
                        foreach (AttributeOption ao in a.AttributeOptionList) {
                             ListItem li = new ListItem();
                             li.Text = ao.PickListValue;
                             li.Value = ao.ShortCode;
                             nestedRepeater.Items.Add(li); 
                        }
                       
                        
                        lblRadioAttribute.Text = "<h3>" + a.Name + "</h3>";
                    }
                }

            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void rptCheckBoxes_ItemDataBound(object sender, RepeaterItemEventArgs e) {

            try {

                RepeaterItem item = e.Item;
                ActiveProductAttribute a = (ActiveProductAttribute)item.DataItem;

                if (item != null) {

                    CheckBoxList nestedRepeater = item.FindControl("cblCheckBoxList") as CheckBoxList;
                    Label lblCheckBoxAttribute = item.FindControl("lblCheckBoxAttribute") as Label;

                    if (nestedRepeater != null) {
                        foreach (AttributeOption ao in a.AttributeOptionList) {
                            ListItem li = new ListItem();
                            li.Text = ao.PickListValue;
                            li.Value = ao.ShortCode;
                            nestedRepeater.Items.Add(li);
                        }
                        lblCheckBoxAttribute.Text = "<h3>" + a.Name + "</h3>";
                    }
                }

            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }


        private void rptText_ItemDataBound(object sender, RepeaterItemEventArgs e) {

            try {

                RepeaterItem item = e.Item;
                ActiveProductAttribute a = (ActiveProductAttribute)item.DataItem;

                if (item != null) {

                   
                    Literal litText = item.FindControl("litText") as Literal;
                    Literal litValue = item.FindControl("litValue") as Literal;
                    Literal litAttributePrice = item.FindControl("litAttributePrice") as Literal;
                    litText.Text = a.Name;
                    litValue.Text = a.AttributeOptionList[0].PickListValue.ToString();
                   

                    
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
        /// 
        private void InitializeComponent() {
            InitComp();
        }

        private void InitComp() {

            if (ctlBreadCrumb != null) {
                ctlBreadCrumb.Module = Module;
            }

            if (ctlMenuTab != null) {
                ctlMenuTab.Module = Module;
            }



            if (btnAddToBasket != null) {
                btnAddToBasket.Click += new EventHandler(AddToBasket_Click);
            }
            rptLinkAttributes.ItemDataBound += new RepeaterItemEventHandler(rptLinks_ItemDataBound);
            rptCheckBoxAttributes.ItemDataBound += new RepeaterItemEventHandler(rptCheckBoxes_ItemDataBound);
            rptTableAttributes.ItemDataBound += new RepeaterItemEventHandler(rptTables_ItemDataBound);
            rptImageAttributes.ItemDataBound += new RepeaterItemEventHandler(rptImages_ItemDataBound);
            rptTextAttributes.ItemDataBound += new RepeaterItemEventHandler(rptText_ItemDataBound);
            rptRadioAttributes.ItemDataBound += new RepeaterItemEventHandler(rptRadioButton_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        void AddToBasket_Click(object sender, EventArgs e) {

            CatalogueViewModule catModule = Module as CatalogueViewModule;

            int quantity = 1;

            if (txtQuantity != null && !string.IsNullOrEmpty(txtQuantity.Text)) {
                try {
                    quantity = Int32.Parse(txtQuantity.Text);
                } catch {
                }
            }

            List<IAttributeSelection> selectedAttributes = GatherSelectedOptions();
            IStoreContext context = WebStoreContext.Current;
            catModule.CommerceService.AddItem(context, catModule.ProductID, selectedAttributes, quantity);
            catModule.CommerceService.RefreshBasket(context);
        }

        private List<IAttributeSelection> GatherSelectedOptions() {
            List<IAttributeSelection> selectedAttributes = new List<IAttributeSelection>();
         //   selectedAttributes = AddCheckBoxOptions(selectedAttributes);
         //   selectedAttributes = AddTableAttributes(selectedAttributes);
         //   selectedAttributes = AddDropDownAttributes(selectedAttributes);
            return selectedAttributes;

        }

        private List<IAttributeSelection> AddDropDownAttributes(List<IAttributeSelection> selectedAttributes) {
            throw new Exception("The method or operation is not implemented.");
        }

        private List<IAttributeSelection> AddTableAttributes(List<IAttributeSelection> selectedAttributes) {
            throw new Exception("The method or operation is not implemented.");
        }

        private List<IAttributeSelection> AddCheckBoxOptions(List<IAttributeSelection> selectedAttributes) {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}