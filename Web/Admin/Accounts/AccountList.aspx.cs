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

using Cuyahoga.Core;
using Cuyahoga.Core.Domain;
using Cuyahoga.Core.Service;
using Cuyahoga.Core.Search;

using Cuyahoga.Core.Communication;


using Guild.WebControls;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Web.Admin.Controls;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using System.Collections.Generic;
using FredCK.FCKeditorV2;
using Koutny.WebControls;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {
    public partial class AccountList : ModuleAdminBasePage {

        public IList accountList;
        protected RepeaterPager repItemsPager;
        protected void Page_Load(object sender, EventArgs e) {

            CatalogueViewModule controller = Module as CatalogueViewModule;

            accountList = base.CoreRepository.GetAll((typeof(Cuyahoga.Core.Domain.User)));
            //(Cuyahoga.Core.Domain.User)base.CoreRepository.GetAll((typeof(Cuyahoga.Core.Domain.User)));
                repItemsPager.DataSource = accountList;
                repItemsPager.DataBind();
                User u = new User();
            
            
        }

        protected void PageChanged(object sender, RepeaterPageChangedEventArgs e) {
            repItemsPager.PageIndex = e.NewPageIndex;
            repItemsPager.DataSource = accountList;
            repItemsPager.DataBind();
        }
    }
}
