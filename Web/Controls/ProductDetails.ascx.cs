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
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;


namespace Cuyahoga.Modules.ECommerce.Web.Controls {
    public partial class ProductDetails : LocalizedModuleConsumerControl {


        private const string STOCK_AVALIABLE = "";
        private const string STOCK_NOT_AVALIABLE = "";


        protected Repeater rptProductDetails;

        protected void Page_Load(object sender, EventArgs e) {
            
        }

        public void RenderProductDetails(List<IProduct> prodView) {
            rptProductDetails.DataSource = prodView;
            rptProductDetails.DataBind();
        }

        void rptProductDetails_ItemDataBound(object sender, RepeaterItemEventArgs e) {

            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) {
                return;
            }

            IProduct product = e.Item.DataItem as IProduct;

            System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgStockedIndicator");
            Literal litStocked = (Literal)e.Item.FindControl("litStocked");
            Literal litPrice = (Literal)e.Item.FindControl("litPrice");

            litPrice.Text = HtmlFormatUtils.FormatMoney(new Money(product.Price)) + " " + product.PriceDescription;

            if (Convert.ToBoolean(product.StockedIndicator)) {
                img.ImageUrl = STOCK_AVALIABLE;
                litStocked.Text =  GetText("itemstocked");
            } else {
                img.ImageUrl = STOCK_NOT_AVALIABLE;
                litStocked.Text = GetText("itemnotstocked");
            }
        }

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
            rptProductDetails.ItemDataBound += new RepeaterItemEventHandler(rptProductDetails_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);
        }

    }
}