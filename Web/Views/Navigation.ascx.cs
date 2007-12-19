namespace Cuyahoga.Modules.ECommerce.Web.Views {
    using System;
    using Cuyahoga.Modules.ECommerce.Core;
    using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
    using log4net;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.Collections.Generic;
    using System.Collections;
    using Cuyahoga.Web.UI;
    /// <summary>
    ///		Summary description for CatNav.
    /// </summary>
    public class Navigatyion : BaseModuleControl {

        protected System.Web.UI.WebControls.Repeater rptMenu;
        protected System.Web.UI.WebControls.Repeater rptSecondaryMenu;
        protected System.Web.UI.WebControls.Repeater rptSecondarySubMenu;

        protected CatalogueUrlHelper UrlHelper;
        protected PlaceHolder plhSecondaryMenuHeader;

        private void Page_Load(object sender, System.EventArgs e) {

            if (!IsPostBack) {

                CatalogueViewModule controller = Module as CatalogueViewModule;
                UrlHelper = new CatalogueUrlHelper(controller);


                RenderMenuTabs();
            }
        }

        public void RenderMenuTabs() {

            try {

                CatalogueViewModule controller = Module as CatalogueViewModule;
                UrlHelper = new CatalogueUrlHelper(controller);
                ICatalogueNodeView root;
             

                    //determine productCategory
                    root = controller.CatalogueViewer.GetRootCatalogueNodeView(controller.Section.Node.Site.Id, controller.Section.Node.Culture);
                 
                     
                        //show top level stuff only
                        if (root != null) {
                            controller.NodeID = root.CurrentNode.NodeID;
                            rptMenu.DataSource = root.ChildNodes;
                            rptMenu.DataBind();
                        }

            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
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



            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }
}