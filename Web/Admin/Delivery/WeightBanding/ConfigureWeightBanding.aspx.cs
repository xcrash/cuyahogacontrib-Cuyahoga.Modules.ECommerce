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

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Delivery {
    public class ConfigureWeightBanding : ModuleAdminBasePage {
        protected Image imgCountryStatus;
        protected Image imgCountyStatus;

        protected LinkButton lbCountryStatus;
        protected LinkButton lbCountyStatus;

        private DeliveryType county;
        private DeliveryType country;

        private CatalogueViewModule controller;
        protected Label lblMessage;
        protected HyperLink hplView;
        private ModuleAdminBasePage _page;
        protected HyperLink hplDeliveryMainMenu;

        protected void Page_Load(object sender, EventArgs e) {
            controller = Module as CatalogueViewModule;
            this._page = (ModuleAdminBasePage)this.Page;

            hplDeliveryMainMenu.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/DeliveryManager.aspx{0}", this._page.GetBaseQueryString());


            county = controller.DeliveryService.GetDeliveryType("WeightBandingState");
            country = controller.DeliveryService.GetDeliveryType("WeightBandingCountry");
            lbCountryStatus.Click += new EventHandler(lbCountryStatus_Click);
            lbCountyStatus.Click += new EventHandler(lbCountyStatus_Click);
            UpdateStatus();
            hplView.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Delivery/WeightBanding/WeightBandingList.aspx{0}", this._page.GetBaseQueryString());
        }

        private void UpdateStatus() {
            if (country.Status) {
                imgCountryStatus.ImageUrl = "~/Admin/Images/Accept.png";
                lbCountryStatus.Text = "Disable weight banding on a country basis";
            } else {
                imgCountryStatus.ImageUrl = "~/Admin/Images/Cancel.png";
                lbCountryStatus.Text = "Enable weight banding on a country basis";
            }

            if (county.Status) {
                imgCountyStatus.ImageUrl = "~/Admin/Images/Accept.png";
                lbCountyStatus.Text = "Disable weight banding on a county basis";
            } else {
                lbCountyStatus.Text = "Enable weight banding on a county basis";
                imgCountyStatus.ImageUrl = "~/Admin/Images/Cancel.png";

            }
        }
        void lbCountryStatus_Click(Object sender, EventArgs e) {
            country.Status = (!country.Status);
            controller.DeliveryService.Save(country);
            lblMessage.Text = "Your change has been saved";
            UpdateStatus();
        }


        void lbCountyStatus_Click(Object sender, EventArgs e) {
            county.Status = (!county.Status);
            controller.DeliveryService.Save(county);
            lblMessage.Text = "Your change has been saved";
            UpdateStatus();
        }

    }
}
