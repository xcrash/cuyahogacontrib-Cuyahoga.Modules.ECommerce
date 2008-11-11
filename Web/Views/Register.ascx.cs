
using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using log4net;
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Modules.ECommerce.Util.Location;
using Cuyahoga.Web.UI;
using System.Web.UI.WebControls;

namespace Cuyahoga.Modules.ECommerce.Web.Views {
    public partial class Register : Cuyahoga.Modules.ECommerce.Core.GenericModuleControl {

        private ECommerceModule _emod = null;

        protected ECommerceModule EModule {
            get {
                if (_emod == null) {
                    _emod = Module as ECommerceModule;
                }
                return _emod;
            }
        }


        protected Cuyahoga.Modules.ECommerce.Web.Controls.UserDetailsEdit ctlUser;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.AddressEdit ctlUserAddress;
        protected Label lbMessage;
        protected LinkButton btnRegister;

        protected void Page_Load(object sender, EventArgs e) {

        }

        private void btnRegister_Click(object sender, EventArgs e) {
            if (ValidateDetails()) {
                try {
                    PerformRegistration();
                } catch (Exception ex) {
                    LogManager.GetLogger(GetType()).Debug(ex);
                    DisplayErrorMessage();
                }
            }

        }

        private void DisplaySuccessMessage() {
            lbMessage.Text = GetText("You have been registerd on the store.");
            lbMessage.CssClass = Convert.ToString(CssStyles.Success);
        }

        private void DisplayErrorMessage() {
            lbMessage.Text = GetText("There was an error registering your account");
            lbMessage.CssClass = Convert.ToString(CssStyles.Error);
        }

        private bool ValidateDetails() {
            throw new Exception("The method or operation is not implemented.");
        }

        private void PerformRegistration() {
            try {
                UserDetail detail = new UserDetail();
                UserDetailsHelper.CopyUserDetails(ctlUser, detail);
                AddressHelper.CopyAddress(ctlUserAddress, detail.Address);

                EModule.CommonDao.SaveOrUpdateObject(detail);

                ctlUser.Visible = ctlUserAddress.Visible = false;

            } catch (Exception ex) {
                LogManager.GetLogger(GetType()).Debug(ex);
            }
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
            initComp();
        }

        private void initComp() {

            this.Load += new System.EventHandler(this.Page_Load);

            //Make sure the child controls know about the base module
            if (ctlUserAddress != null) ctlUserAddress.Module = Module;
            if (ctlUser != null) ctlUser.Module = Module;

        }


        #endregion
    }
}