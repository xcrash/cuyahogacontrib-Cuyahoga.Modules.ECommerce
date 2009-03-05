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
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Web.Controls;
using log4net;

using OrderStatusEnum = Cuyahoga.Modules.ECommerce.Util.Enums.OrderStatus;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {

    public partial class OrderDetail : ModuleAdminBasePage {

        protected OrderViewComposite ctlOrderView;
        
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                try {
                    CatalogueViewModule controller = Module as CatalogueViewModule;
                    int orderID = Convert.ToInt32(Request[Orders.ORDER_ID]);

                    Basket basket = null;

                    if (orderID > 0) {
                        basket = controller.OrderService.GetOrderDetail(orderID);
                    }

                    if (basket != null) {
                        ctlOrderView.BindOrder(basket);
                    } else {
                        ctlOrderView.Visible = false;
                    }
                } catch (Exception f) {
                    LogManager.GetLogger(GetType()).Error(f);
                }
            } 
        }
    }
}