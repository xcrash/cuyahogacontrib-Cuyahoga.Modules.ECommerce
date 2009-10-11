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
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using System.Collections.Generic;
using FredCK.FCKeditorV2;

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Controls {

    public partial class BaseProductEditor : System.Web.UI.UserControl {

        public FCKeditor fckDescription = new FCKeditor();
        public FCKeditor fckKitComprises = new FCKeditor();
        public FCKeditor fckShortDescription = new FCKeditor();
        public FCKeditor fckFeatures = new FCKeditor();

        public IProduct product;
        public ICategory catNode;

        public TextBox txtProductName;
        public TextBox txtItemCode;
        public TextBox txtProductID;
        public TextBox txtStock;
        public TextBox txtPrice;
        public TextBox txtPriceDescription;
        

        public TextBox txtFinish;
        public TextBox txtWeight;

        protected void Page_Load(object sender, EventArgs e) {

            fckDescription.BasePath
                = fckFeatures.BasePath
                = fckShortDescription.BasePath
                = this.Page.ResolveUrl("~/Support/FCKEditor/");

            if (!IsPostBack && product != null) {


                txtItemCode.Text = product.ItemCode;
                txtProductName.Text = product.Name;
                txtProductID.Text = product.ProductID.ToString();
                txtPriceDescription.Text = product.PriceDescription;
                txtItemCode.Text = product.ItemCode;
                txtPrice.Text = String.Format("{0:f2}", product.Price);

                fckDescription.Value = product.Description;
                fckFeatures.Value = product.Features;
                fckShortDescription.Value = product.ShortDescription;

                txtFinish.Text = product.GetValue("finish_type");
                txtWeight.Text = product.GetValue("weight_kg");
            }
        }

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);
        }
    }
}