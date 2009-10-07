using Cuyahoga.Modules.ECommerce.Core;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Web.HttpModules;
using Cuyahoga.Modules.ECommerce.Util;
using log4net;
using Cuyahoga.Core.Service;
using Cuyahoga.Core;
using Cuyahoga.Core.Security;

namespace Cuyahoga.Modules.ECommerce.Web.Controls {
    public partial class Login : LocalizedModuleConsumerControl {

        protected System.Web.UI.WebControls.TextBox txtUsername;
        protected System.Web.UI.WebControls.TextBox txtPassword;
        protected System.Web.UI.WebControls.Label lblError;

        public string RedirectTo {
            get {
                if (ViewState["redirectTo"] != null) {
                    return (string)ViewState["redirectTo"];
                } else {
                    return "Default.aspx";
                }
            }

            set {
                ViewState["redirectTo"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void PerformLogin(object sender, EventArgs e) {
            
            ECommerceModule mod = Module as ECommerceModule;

            IStoreContext context = WebStoreContext.Current;
           
            AuthenticationModule am = (AuthenticationModule)this.Context.ApplicationInstance.Modules["AuthenticationModule"];

            if (am.AuthenticateUser(txtUsername.Text, txtPassword.Text, false)) {
               Cuyahoga.Core.Domain.User user = (Cuyahoga.Core.Domain.User)Context.User.Identity;
               WebStoreContext.Current.WebStoreUser = mod.AccountService.GetWebStoreUser(user.Id);
               Context.Response.Redirect(Page.ResolveUrl(RedirectTo), false);
            } else {
                this.lblError.Text = "Invalid username or password.";
                this.lblError.Visible = true;
            }		
        }
    }
}