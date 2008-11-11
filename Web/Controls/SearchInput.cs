namespace Cuyahoga.Modules.ECommerce.Web.Controls {

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
    using Cuyahoga.Web.UI;
    using Cuyahoga.Web.Util;
    using Cuyahoga.Modules.ECommerce.Web.Views;

    using System.Web.UI.WebControls;

    /// <summary>
    /// </summary>
    public class SearchInput : System.Web.UI.UserControl {

        private const string CAT_ID_NONE = "";

        protected System.Web.UI.WebControls.DropDownList ddlProductLine;
        protected System.Web.UI.WebControls.TextBox txtSearch;
        protected System.Web.UI.WebControls.Button btnSearch;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                //This could be quite expensive!
                BindProductLineDdl();

                //Populate the fields
                try {
                    txtSearch.Text = Request[Search.QS_SEARCH].ToString();
                } catch { }
                try {
                    ddlProductLine.SelectedValue = Request[Search.QS_PRODUCT_LINE].ToString();
                } catch { }
            }
        }

        private void BindProductLineDdl() {
            using (SpHandler sph = new SpHandler("getProductLines")) {

                ddlProductLine.Items.Clear();
                ddlProductLine.Items.Add(new ListItem("--- select---", CAT_ID_NONE));

                sph.ExecuteReader();
                while (sph.DataReader.Read()) {
                    ddlProductLine.Items.Add(new ListItem(sph.DataReader[1].ToString(), sph.DataReader[0].ToString()));
                }
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
            InitComp();
        }

        private void InitComp() {
            if (btnSearch != null) {
                btnSearch.Click += new EventHandler(btnSearch_Click);
            }
            this.Load += new System.EventHandler(this.Page_Load);
        }

        void btnSearch_Click(object sender, EventArgs e) {
            string url = UrlHelper.GetSiteUrl() + "/Search.aspx?" + Search.QS_SEARCH + "=" + Server.UrlEncode(txtSearch.Text)
                + ((ddlProductLine.SelectedValue != CAT_ID_NONE) ? ("&" + Search.QS_PRODUCT_LINE + "=" + ddlProductLine.SelectedValue) : "");
            Response.Redirect(url);
        }
        #endregion
    }
}