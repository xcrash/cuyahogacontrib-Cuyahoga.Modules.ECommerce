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

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Delivery {
    public class DeliveryMethods : ModuleAdminBasePage {
        protected Repeater rptDeliveryMethods;
        private ModuleAdminBasePage _page;
        protected void Page_Load(object sender, EventArgs e) {
            this._page = (ModuleAdminBasePage)this.Page;
            CatalogueViewModule controller = Module as CatalogueViewModule;
            rptDeliveryMethods.DataSource = controller.DeliveryService.GetAllDeliveryMethods();
            rptDeliveryMethods.DataBind();
        }

        private void rptDeliveryMethods_ItemDataBound(object source, RepeaterItemEventArgs e) {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType !=
                ListItemType.AlternatingItem)
                return;

            HyperLink hplEdit = (HyperLink)e.Item.FindControl("hplEdit");
            Image imgStatus = (Image)e.Item.FindControl("imgStatus");

            DeliveryType dt = e.Item.DataItem as DeliveryType;
            if(dt.Status){
               imgStatus.ImageUrl = "~/Admin/Images/Accept.png";
            } else {
               imgStatus.ImageUrl = "~/Admin/Images/cancel.png";
            }
                //The DeliveryType name has to be same as the folder name.
            hplEdit.NavigateUrl = String.Format(dt.Name + "/Configure" + dt.Name + ".aspx{0}", this._page.GetBaseQueryString());
         
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
            rptDeliveryMethods.ItemDataBound += new RepeaterItemEventHandler(rptDeliveryMethods_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }
}
