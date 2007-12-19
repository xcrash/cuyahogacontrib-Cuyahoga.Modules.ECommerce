namespace Cuyahoga.Modules.ECommerce.Web.Views {

    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Web.UI.WebControls;

    using log4net;

    using Cuyahoga.Modules.ECommerce.Core;
    using Cuyahoga.Modules.ECommerce.Domain;
    using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
    using Cuyahoga.Modules.ECommerce.Util.Interfaces;
    using Cuyahoga.Modules.ECommerce.Util;
    using Cuyahoga.Modules.ECommerce.Service.OrderProcessor;
    using Cuyahoga.Web.UI;

    /// <summary>
    ///		Summary description for CatNav.
    /// </summary>
    public class Search : BaseModuleControl {

        public const string QS_SEARCH = "st";
        public const string QS_PRODUCT_LINE = "pl";

        protected CatalogueUrlHelper UrlHelper;
        private CatalogueViewModule _mod;

        protected Controls.ProductList ctlProductList;
        protected PlaceHolder phNoResults;

        private CatalogueViewModule CatMod {
            get {
                if (_mod == null) {
                    _mod = Module as CatalogueViewModule;
                }
                return _mod;
            }
        }

        private void Page_Load(object sender, System.EventArgs e) {

            UrlHelper = new CatalogueUrlHelper(CatMod);
            phNoResults.Visible = true;

            try {

                string searchTerm = Request[QS_SEARCH];
                string prodLineString = Request[QS_PRODUCT_LINE];

                List<IProductSummary> results;

                if (!string.IsNullOrEmpty(prodLineString)) {
                    ctlProductList.Mode = Cuyahoga.Modules.ECommerce.Web.Controls.ProductList.DisplayMode.SearchProductLine;
                    results = CatMod.CatalogueViewer.FindProducts(searchTerm, Convert.ToInt64(prodLineString));
                } else {
                    ctlProductList.Mode = Cuyahoga.Modules.ECommerce.Web.Controls.ProductList.DisplayMode.SearchGlobal;
                    results = CatMod.CatalogueViewer.FindProducts(searchTerm);
                }

                ctlProductList.BindProductList(results);
                phNoResults.Visible = (results.Count == 0);

            } catch (System.Threading.ThreadAbortException error) {
                LogManager.GetLogger(GetType()).Error(error);
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
                phNoResults.Visible = true;
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

            if (ctlProductList != null) {
                ctlProductList.Module = Module;
            }

            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }
}