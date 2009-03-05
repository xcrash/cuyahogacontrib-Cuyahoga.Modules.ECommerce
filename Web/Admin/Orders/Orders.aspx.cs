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

namespace Cuyahoga.Modules.ECommerce.Web.Admin {

    public partial class Orders : ModuleAdminBasePage {

        public const string ORDER_ID = "orderID";

        protected RepeaterPager repItemsPager;
        
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                CatalogueViewModule controller = Module as CatalogueViewModule;
                IList<Basket>  orderList = controller.OrderService.GetCompletedOrders(repItemsPager.PageSize, 0);
                repItemsPager.VirtualItemsCount = controller.OrderService.GetCompletedOrdersCount();
                repItemsPager.DataSource = orderList;
                repItemsPager.DataBind();
            }
        }

        protected void PageChanged(object sender, RepeaterPageChangedEventArgs e) {

            CatalogueViewModule controller = Module as CatalogueViewModule;

            repItemsPager.PageIndex = e.NewPageIndex;
            repItemsPager.DataSource = controller.OrderService.GetCompletedOrders(repItemsPager.PageSize, repItemsPager.PageIndex); ;
            repItemsPager.DataBind();
        }
    }
}
