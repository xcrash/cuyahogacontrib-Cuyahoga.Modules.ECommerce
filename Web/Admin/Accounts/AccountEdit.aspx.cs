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
using Cuyahoga.Modules.ECommerce.Web.Controls;
using Cuyahoga.Modules.ECommerce.Util.Location;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {
    public partial class AccountEdit : ModuleAdminBasePage {

        public int AccountID {
            get {
                return _accountID;
            }

            set {
                _accountID = value;
            }
        }

        public const string ACCOUNT_ID = "accountID";

        private int _accountID;
        
        protected AddressEdit addressEditor;
        protected UserDetailsEdit userDetailsEditor;
        protected Domain.UserDetail userDetails;
        protected User u;
        protected Label lblSaveMessage;
        protected LinkButton lnkSave;

        protected void Page_Load(object sender, EventArgs e) {

            this.lnkSave.Click += new EventHandler(lnkSave_Click);

            if (!IsPostBack) {

                try {
                    AccountID = Int32.Parse(Request.Params[ACCOUNT_ID]);
                } catch {
                }

                CatalogueViewModule controller = Module as CatalogueViewModule;
                addressEditor.SetAvailableCountries(controller.AccountService.GetCountries());

                userDetails = controller.AccountService.GetAccount(AccountID);
                AddressHelper.CopyAddress(userDetails.Address, addressEditor);

                u = (Cuyahoga.Core.Domain.User)base.CoreRepository.GetObjectById(typeof(Cuyahoga.Core.Domain.User), AccountID);
                userDetailsEditor.CultureCode = controller.Section.Node.Culture;
                userDetailsEditor.EmailAddress = u.Email;
                userDetailsEditor.FirstName = u.FirstName;
                userDetailsEditor.LastName = u.LastName;
            }
        }

        public void lnkSave_Click(object sender, EventArgs e) {

            if (Save()) {
                lblSaveMessage.Text = "Success";
                userDetailsEditor.Visible = addressEditor.Visible = lnkSave.Visible = false;
            } else {
                lblSaveMessage.Text = "Saving Failed";
            }
        }


        public bool Save() {

            CatalogueViewModule controller = Module as CatalogueViewModule;
            try {
                AccountID = Int32.Parse(Request.Params[ACCOUNT_ID]);
            } catch {
            }

            userDetails = controller.AccountService.GetAccount(AccountID);
            AddressHelper.CopyAddress(addressEditor, userDetails.Address);

            u = (Cuyahoga.Core.Domain.User)base.CoreRepository.GetObjectById(typeof(Cuyahoga.Core.Domain.User), AccountID);
            u.Email = userDetailsEditor.EmailAddress;
            u.FirstName = userDetailsEditor.FirstName;
            u.LastName = userDetailsEditor.LastName;

            if (controller.AccountService.SaveAccountDetails(userDetails)) {
                try {
                    base.CoreRepository.UpdateObject(u);
                    return true;
                } catch (Exception e) {
                    LogManager.GetLogger(GetType()).Error(e);
                }
            }
            return false;
        }
    }
}