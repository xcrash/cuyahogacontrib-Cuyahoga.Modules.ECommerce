
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
using Cuyahoga.Web.HttpModules;
using Cuyahoga.Core.Domain;
using System.Threading;
using System.Web.UI.HtmlControls;

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

        private ArrayList _errorList = new ArrayList();


        protected Cuyahoga.Modules.ECommerce.Web.Controls.UserDetailsEdit ctlUser;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.AddressEdit ctlUserAddress;
        protected PlaceHolder plhAdditonalErrors;

        protected Label lbMessage;
        protected LinkButton btnRegister;

        protected void Page_Load(object sender, EventArgs e) {
            CatalogueViewModule controller = Module as CatalogueViewModule;
            ctlUser.CultureCode = ctlUserAddress.CultureCode = controller.Section.Node.Culture;
        }

        private void btnRegister_Click(object sender, EventArgs e) {
            if (ValidateDetails()) {
                try {
                    PerformRegistration();
                    //login the user and redirect them to where they came form.

                    ECommerceModule mod = Module as ECommerceModule;

                    IStoreContext context = WebStoreContext.Current;

                    AuthenticationModule am = (AuthenticationModule)this.Context.ApplicationInstance.Modules["AuthenticationModule"];

					//TODO: implement
                    //am.AuthenticateUser(ctlUser.EmailAddress, ctlUser.Password, false);
                    Cuyahoga.Core.Domain.User user = (Cuyahoga.Core.Domain.User)Context.User.Identity;
                    WebStoreContext.Current.WebStoreUser = mod.AccountService.GetWebStoreUser(user.Id);

                    Response.Redirect(this.Page.Request.UrlReferrer.AbsoluteUri);

                } catch (ThreadAbortException) { } catch (Exception ex) {
                    LogManager.GetLogger(GetType()).Debug(ex);
                    DisplayErrorMessage();
                }
            } else {
                DisplayErrorMessage();
            }

        }

        private void DisplaySuccessMessage() {
            lbMessage.Text = GetText("You have been registerd on the store.");
            lbMessage.CssClass = Convert.ToString(CssStyles.Success);
        }

        private void DisplayErrorMessage() {
            lbMessage.Text = GetText("There was an error registering your account");
            lbMessage.CssClass = Convert.ToString(CssStyles.Error);

            //display additional mesages
            HtmlGenericControl list = new HtmlGenericControl("ul");

            foreach (string s in _errorList) {
                HtmlGenericControl listItem = new HtmlGenericControl("li");
                Literal lit = new Literal();
                lit.Text = s;
                listItem.Controls.Add(lit);
                list.Controls.Add(listItem);
            }

            plhAdditonalErrors.Controls.Add(list);
        }

        private bool ValidateDetails() {

            IList list = EModule.UserService.FindUsersByUsername(ctlUser.EmailAddress);
            if (list != null && list.Count > 0) {
                //user already exists
                AddErrorMessage(GetText("Already registered"));
            }

            if (ctlUser.Password != ctlUser.ConfirmPassword) {
                AddErrorMessage(GetText("passwords_do_not_match"));
            }
            

           return this.Page.IsValid && _errorList.Count == 0; //needs more than this.
        }

        private void AddErrorMessage(string message) {
            _errorList.Add(message);
        }

        //should put this in account service.
        private void PerformRegistration() {
            try {
                UserDetail detail = new UserDetail();
                UserDetailsHelper.CopyUserDetails(ctlUser, detail);
                Address Address = new Address();
                AddressHelper.CopyAddress(ctlUserAddress, Address);
                EModule.CommonDao.SaveObject(Address);
                detail.Address = Address;
                EModule.CommonDao.SaveObject(detail);

               
                User user = new User();
                user.Email = ctlUser.EmailAddress;
                user.UserName = ctlUser.EmailAddress;
                user.FirstName = ctlUser.FirstName;
                user.LastName = ctlUser.LastName;
                user.Password = User.HashPassword(ctlUser.Password);
                user.InsertTimestamp = DateTime.Now;
                user.IsActive = true;
                EModule.CommonDao.SaveObject(user);
                
                ctlUser.Visible = ctlUserAddress.Visible = false;

            } catch (Exception ex) {
                LogManager.GetLogger(GetType()).Debug(ex);
                DisplayErrorMessage();
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
            btnRegister.Click +=new EventHandler(btnRegister_Click);
            //Make sure the child controls know about the base module
            if (ctlUserAddress != null) ctlUserAddress.Module = Module;
            if (ctlUser != null) ctlUser.Module = Module;

        }


        #endregion
    }
}