
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


namespace Cuyahoga.Modules.ECommerce.Web.Controls {
    public partial class Skus : System.Web.UI.UserControl {

        protected System.Web.UI.WebControls.Repeater rptSkus;
        public List<ISku> SkuList;

        protected void Page_Load(object sender, EventArgs e) {
            if (SkuList != null) {
                if (SkuList.Count > 0) {
                    rptSkus.DataSource = SkuList;
                    rptSkus.DataBind();
                }
            }
        }
    }
}