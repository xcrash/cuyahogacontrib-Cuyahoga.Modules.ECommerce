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
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Service;
namespace Cuyahoga.Modules.ECommerce.Views {
    public partial class MarketSelector : GenericModuleControl {
        protected DropDownList ddlCurrency;
        protected void Page_Load(object sender, EventArgs e) {
            ddlCurrency.SelectedIndexChanged += new EventHandler(ddlCurrency_SelectedIndexChanged);
        }

        void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e) {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}