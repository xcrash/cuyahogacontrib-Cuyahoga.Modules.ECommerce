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
using Koutny.WebControls;
namespace Cuyahoga.Modules.ECommerce.Web.Admin.Delivery.WeightBanding {
    public class WeightBandingList : ModuleAdminBasePage {
        protected Repeater rptCountryBandings;
        protected Repeater rptStateWeightBandings;
        private ModuleAdminBasePage _page;
        private CatalogueViewModule controller;
        protected Label lblCountryHeading;
        protected LinkButton lbShowCountry;
        protected DropDownList ddlCountryList;
        protected LinkButton lbStateBandings;
        protected Label lblDelete;

        protected HyperLink hplDeliveryMainMenu;
        protected HyperLink hplWeightBandingHomePage;
        protected HyperLink hplAdd;

        protected void Page_Load(object sender, EventArgs e) {
            this._page = (ModuleAdminBasePage)this.Page;
            controller = Module as CatalogueViewModule;
            hplDeliveryMainMenu.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/DeliveryManager.aspx{0}", this._page.GetBaseQueryString());
            hplWeightBandingHomePage.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Delivery/WeightBanding/ConfigureWeightBanding.aspx{0}", this._page.GetBaseQueryString());
            
            if (!IsPostBack) {
                lbStateBandings.Visible = false;
                ShowCountryList();
                ShowCountryBanding("GB");
                hplAdd.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Delivery/WeightBanding/AddWeightBanding.aspx{0}&CountryCode={1}", this._page.GetBaseQueryString(), "GB");
            }
        }

        private void ShowCountryList() {
           IList countryList = controller.CultureService.GetAllCountries();
           foreach (Country c in countryList) {
               ddlCountryList.Items.Add(new ListItem(c.CountryName, c.CountryCode));
           }
          
        }

        public void ddlCountryList_SelectedIndexChanged(object sender, EventArgs e) {

            ShowCountryBanding(ddlCountryList.SelectedValue);
        }

        private void ShowCountryBanding(string countryCode) {
            rptCountryBandings.DataSource = controller.DeliveryService.GetCountryWeightBandings(countryCode);
            rptCountryBandings.DataBind();
            rptCountryBandings.Visible = true;
            rptStateWeightBandings.Visible = lblCountryHeading.Visible = false;
            hplAdd.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Delivery/WeightBanding/AddWeightBanding.aspx{0}&CountryCode={1}", this._page.GetBaseQueryString(), countryCode);

        }

        void lbStateBandings_Click(Object sender, EventArgs e) {
            LinkButton lb = (LinkButton)sender as LinkButton;
            rptCountryBandings.DataSource = controller.DeliveryService.GetStateWeightBandings(lb.CommandArgument);
            rptCountryBandings.DataBind();
            rptCountryBandings.Visible = false;
            lblCountryHeading.Text = lb.CommandArgument;
        }

      /*  private void rptCountryBandings_ItemDataBound(object source, RepeaterItemEventArgs e) {
         if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType !=
                ListItemType.AlternatingItem)
                return;
            CountryDeliveryWeight cdw = e.Item.DataItem as CountryDeliveryWeight;
            LinkButton lbEditCountryBand = (LinkButton)e.Item.FindControl("lbEditCountryBand");

        }*/

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
            //rptCountryBandings.ItemDataBound += new RepeaterItemEventHandler(rptCountryBandings_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);
            lbStateBandings.Click += new EventHandler(lbStateBandings_Click);
            ddlCountryList.SelectedIndexChanged += new EventHandler(ddlCountryList_SelectedIndexChanged);
            lbShowCountry.Click += new EventHandler(lbShowCountry_Click);
            rptCountryBandings.ItemCommand += new RepeaterCommandEventHandler(rptCountryBandings_ItemCommand);
        }

        void rptCountryBandings_ItemCommand(object source, RepeaterCommandEventArgs e) {
            LinkButton lb = (LinkButton)e.CommandSource as LinkButton;
            if (lb.Text != "Delete") { //bit of a bodge
                Response.Redirect(String.Format("~/Modules/ECommerce/Admin/Delivery/WeightBanding/WeightBandingCountryPriceEdit.aspx{0}&CountryCode={1}&WeightLevel={2}", this._page.GetBaseQueryString(), e.CommandName, e.CommandArgument));
            } else {
                //delete band
                CountryDeliveryWeight cdw = controller.DeliveryService.GetCountryDeliveryWeight(e.CommandName, Convert.ToDecimal(e.CommandArgument));
                controller.DeliveryService.Delete(cdw);
                lblDelete.Text = "The band has been deleted";
                ShowCountryBanding(ddlCountryList.SelectedValue);
            }
        }

     

        void lbShowCountry_Click(object sender, EventArgs e) {
            ShowCountryBanding(ddlCountryList.SelectedValue);
        }
        #endregion


    }
}
