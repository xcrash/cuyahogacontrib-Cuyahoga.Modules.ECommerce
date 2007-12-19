
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

    public partial class ProductImages : System.Web.UI.UserControl {
        
        public List<IImage> imageList;
        protected Repeater rptImages;

        protected void Page_Load(object sender, EventArgs e) {
            if (imageList != null) {
                if (imageList.Count > 0) {
                    rptImages.DataSource = imageList;
                    rptImages.DataBind();
                }
            }
        }
    }
}