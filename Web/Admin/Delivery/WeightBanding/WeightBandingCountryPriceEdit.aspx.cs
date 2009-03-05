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

using Cuyahoga.Modules.ECommerce.Core;


namespace Cuyahoga.Modules.ECommerce.Web.Admin.Delivery.WeightBanding {
    public class WeightBandingCountryPriceEdit : ModuleAdminBasePage {
        private CatalogueViewModule controller;
        protected Label lblMessage;
        protected HyperLink hplView;
        private ModuleAdminBasePage _page;

        protected Label lblCountry;
        protected TextBox txtPrice;
        protected Label lblWeightBand;
        protected LinkButton lbSave;

        protected HyperLink hplDeliveryMainMenu;
        protected HyperLink hplWeightBandingHomePage;
        protected HyperLink hplWeightBandingList;

        private decimal _weight = -1;
        public decimal Weight {
            get {
                if (_weight == -1) {
                    return Convert.ToDecimal(Request.QueryString["WeightLevel"]);
                } else {
                    return _weight;
                }

            }
           
        }

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
                DisplayEditForm();
            }
        }

        void lbSave_Click(object sender, EventArgs e) {
            CountryDeliveryWeight cdw = controller.DeliveryService.GetCountryDeliveryWeight(Country, Weight);
            cdw.Price = Convert.ToDecimal(txtPrice.Text);
            try {
                controller.DeliveryService.Save(cdw);
                lblMessage.Text = "Your update has been saved";
                _weight = cdw.WeightLevel;
            } catch(Exception ex) {
                log4net.LogManager.GetLogger(GetType()).Error(ex);
                lblMessage.Text = "Your update has NOT been saved. This is probably because the weight level you entered already exsists for this market.";
            }
            
            DisplayEditForm();
        }

        private void DisplayEditForm() {
            CountryDeliveryWeight cdw = controller.DeliveryService.GetCountryDeliveryWeight(Country, Weight);
            if (cdw != null) {
                txtPrice.Text = cdw.Price.ToString();
                lblWeightBand.Text = cdw.WeightLevel.ToString();
                lblCountry.Text = cdw.CountryCode;
            }
        }
    }
}
