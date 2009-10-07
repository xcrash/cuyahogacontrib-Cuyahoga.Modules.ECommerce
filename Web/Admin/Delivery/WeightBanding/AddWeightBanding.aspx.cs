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
using Igentics.Common.Logging;
using Cuyahoga.Modules.ECommerce.Core;

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Delivery.WeightBanding {
    public class AddWeightBanding : ModuleAdminBasePage {

        private CatalogueViewModule controller;
 
        private ModuleAdminBasePage _page;

        protected Label lblCountry;
        protected Label lblMessage;
        protected TextBox txtPrice;
        protected LinkButton lbSave;
        protected TextBox txtWeight;

        protected HyperLink hplDeliveryMainMenu;
        protected HyperLink hplWeightBandingHomePage;
        protected HyperLink hplWeightBandingList;
        protected PlaceHolder plhAdd;
        public string Country {

            get {
                return Request.QueryString["CountryCode"];
            }

        }

        protected void Page_Load(object sender, EventArgs e) {
            controller = Module as CatalogueViewModule;
            lbSave.Click += new EventHandler(lbSave_Click);
            this._page = (ModuleAdminBasePage)this.Page;

            hplDeliveryMainMenu.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/DeliveryManager.aspx{0}", this._page.GetBaseQueryString());
            hplWeightBandingHomePage.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Delivery/WeightBanding/ConfigureWeightBanding.aspx{0}", this._page.GetBaseQueryString());
            hplWeightBandingList.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Delivery/WeightBanding/WeightBandingList.aspx{0}", this._page.GetBaseQueryString());


            if (!IsPostBack) {
                DisplayAddForm();
            }
        }

        private void DisplayAddForm() {

            lblCountry.Text = Country;
            plhAdd.Visible = true;
        }

        void lbSave_Click(object sender, EventArgs e) {

            CountryDeliveryWeight cdw = new CountryDeliveryWeight();
            cdw.Price = Convert.ToDecimal(txtPrice.Text);
            cdw.WeightLevel = Convert.ToDecimal(txtWeight.Text);
            cdw.CountryCode = Country;

            try {
                controller.DeliveryService.Save(cdw);
                lblMessage.Text = "Your new weight band has been saved";
                plhAdd.Visible = false;
            } catch (Exception ex) {
                LogManager.GetLogger(GetType()).Error(ex);
                lblMessage.Text = "Your new weight band has NOT been saved. This is probably because the weight level you entered already exsists for this market.";
            }

        }
    }
}
