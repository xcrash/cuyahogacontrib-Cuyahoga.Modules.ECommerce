namespace Cuyahoga.Modules.ECommerce.Web.Controls {

    using System;
    using System.Web.UI.WebControls;

    using Cuyahoga.Modules.ECommerce.Util.Interfaces;
    using Cuyahoga.Modules.ECommerce.Service.Translation;
    using Cuyahoga.Modules.ECommerce.Util;

    /// <summary>
    ///		Summary description for AddressWuc.
    /// </summary>
    public class UserDetailsEdit : TranslatedControl, IUserDetails {

        protected TextBox txtFirstName;
        protected TextBox txtLastName;
        protected TextBox txtEmailAddress;
        protected TextBox txtTelephoneNumber;
        protected TextBox txtFaxNumber;
        
        protected RequiredFieldValidator rfvFirstName;
        protected RequiredFieldValidator rfvLastName;
        protected RequiredFieldValidator rfvEmailAddress;
        protected RequiredFieldValidator rfvTelephoneNumber;
        protected RegularExpressionValidator regEmailAddress;

        public bool Enabled {
            get {
                try {
                    return (bool)ViewState["enabled"];
                } catch {
                    return true;
                }
            }
            set {
                ViewState["enabled"] = value;
                txtFirstName.Enabled
                    = txtLastName.Enabled
                    = txtEmailAddress.Enabled
                    = txtTelephoneNumber.Enabled
                    = rfvFirstName.Enabled
                    = rfvLastName.Enabled
                    = rfvEmailAddress.Enabled
                    = rfvTelephoneNumber.Enabled
                    = value;
            }
        }

        public void ClearControls() {
            txtFirstName.Text
                = txtLastName.Text
                = txtTelephoneNumber.Text
                = txtEmailAddress.Text = "";
        }

        #region exposed properties

        public string FirstName {
            get {
                if (txtFirstName != null) {
                    return txtFirstName.Text;
                } else {
                    return "";
                }
            }
            set {
                if (txtFirstName != null) {
                    txtFirstName.Text = value;
                }
            }
        }

        public string LastName {
            get {
                if (txtLastName != null) {
                    return txtLastName.Text;
                } else {
                    return "";
                }
            }
            set {
                if (txtLastName != null) {
                    txtLastName.Text = value;
                }
            }
        }

        public string EmailAddress {
            get {
                if (txtEmailAddress != null) {
                    return txtEmailAddress.Text;
                } else {
                    return "";
                }
            }
            set {
                if (txtEmailAddress != null) {
                    txtEmailAddress.Text = value;
                }
            }
        }



        public string TelephoneNumber {
            get {
                if (txtTelephoneNumber != null) {
                    return txtTelephoneNumber.Text;
                } else {
                    return "";
                }
            }
            set {
                if (txtTelephoneNumber != null) {
                    txtTelephoneNumber.Text = value;
                }
            }
        }

        public string FaxNumber {
            get {
                if (txtFaxNumber != null) {
                    return txtFaxNumber.Text;
                } else {
                    return "";
                }
            }
            set {
                if (txtFaxNumber != null) {
                    txtFaxNumber.Text = value;
                }
            }
        }

            
        public string CompanyName {
            get {     
                    return "";
            }
            set {
                
            }
        }

        public long UserID {
            get {
                try {
                    return (long)ViewState["UserID"];
                } catch {
                    return 0;
                }
            }
            set {
                ViewState["UserID"] = value;
            }
        }
        #endregion

        public void BindUserDetails(IUserDetails user) {
            if (user != null) {
                UserDetailsHelper.CopyUserDetails(user, this);
            } else {
                ClearControls();
            }
        }

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                ConfigureValidators();
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
            this.Load += new System.EventHandler(this.Page_Load);
        }

        protected virtual void ConfigureValidators() {

            rfvFirstName.Text
                = rfvLastName.Text
                = rfvEmailAddress.Text
                = rfvTelephoneNumber.Text
                = "<div class=\"error\">" + GetText("required_field") + "</div>";

            regEmailAddress.Text = "<div class=\"error\">" + GetText("invalid_email") + "</div>";
            regEmailAddress.ValidationExpression = StringUtils.REGEXP_VALID_EMAIL_ADDRESS;
        }
        #endregion
    }
}