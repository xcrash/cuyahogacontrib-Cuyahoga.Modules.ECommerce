
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
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;

namespace Cuyahoga.Modules.ECommerce.Web.Controls {

    public class Attributes : LocalizedModuleConsumerControl {

        protected CatalogueUrlHelper UrlHelper;

        private CatalogueViewModule _mod;
        private CatalogueViewModule CatMod {
            get {
                if (_mod == null) {
                    _mod = Module as CatalogueViewModule;
                }
                return _mod;
            }
        }

        protected System.Web.UI.WebControls.Repeater rptDocuments;

        protected void Page_Load(object sender, EventArgs e) {
        }

        public void RenderAttributes(IProduct product) {

            UrlHelper = new CatalogueUrlHelper(CatMod);

            if (product != null && product.DocumentList != null) {
                if (product.DocumentList.Count > 0) {
                    rptDocuments.DataSource = product.DocumentList;
                    rptDocuments.DataBind();
                }
            }
        }


        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
   
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            InitComp();
        }

        private void InitComp() {


                this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }
}