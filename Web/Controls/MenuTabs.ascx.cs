namespace Cuyahoga.Modules.ECommerce.Web.Controls {
    using Cuyahoga.Web.UI;
    using Cuyahoga.Core.Util;
    using Cuyahoga.Core.Service;
    using Cuyahoga.Core.Domain;
    using Cuyahoga.Core.Service.Membership;
    using Cuyahoga.Core.Security;
    using Cuyahoga.Core;
    using Cuyahoga.Web;


    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.SessionState;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;


    using Cuyahoga.Modules.ECommerce.Core;
    using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
    using log4net;

    using System.Web.UI.WebControls.WebParts;

    using System.Collections.Generic;

 
    /// <summary>
    ///		Summary description for MenuTabs.
    /// </summary>
    public class MenuTabs : LocalizedModuleConsumerControl {

        protected System.Web.UI.WebControls.Repeater rptMenu;
        protected System.Web.UI.WebControls.Repeater rptSecondaryMenu;
        protected System.Web.UI.WebControls.Repeater rptSecondarySubMenu;

        protected CatalogueUrlHelper UrlHelper;
        protected PlaceHolder plhSecondaryMenuHeader;

        public List<ICatalogueNode> breadcrumb;
        

        private long _nodeID;
        private long _realNode;
         public long CurrentNode {

            get {
                if (ViewState["CurrentNode"] != null) {
                    return (long)ViewState["CurrentNode"];
                } return 0;
            }

            set {
                ViewState["CurrentNode"] = value;
            }
        }

        public long PreviousNode {

            get {
                if (ViewState["PreviousNode"] != null) {
                    return (long)ViewState["PreviousNode"];
                } return 0;
            }

            set {
                ViewState["PreviousNode"] = value;
            }
        }

        public bool NodeSet {

            get {
                if (ViewState["NodeSet"] != null) {
                    return (bool)ViewState["NodeSet"];
                } return true;
            }

            set {
                ViewState["NodeSet"] = value;
            }
        }

        public bool FirstNode {

            get {
                if (ViewState["FirstNode"] != null) {
                    return (bool)ViewState["FirstNode"];
                } return true;
            }

            set {
                ViewState["FirstNode"] = value;
            }
        }


        public bool SubCategories {

            get {
                if (ViewState["SubCategories"] != null) {
                    return (bool)ViewState["SubCategories"];
                } return false;
            }

            set {
                ViewState["SubCategories"] = value;
            }
        }

        public long NodeID {
            get {
                return _nodeID;
            }
            set {
                _nodeID = value;
            }
        }

        public long RealNode {
            get {
                return _realNode;
            }
            set {
                _realNode = value;
            }
        }



        private void Page_Load(object sender, System.EventArgs e) {


            
            if (!IsPostBack) {
            
                CatalogueViewModule controller = Module as CatalogueViewModule;
                UrlHelper = new CatalogueUrlHelper(controller);


                //RenderMenuTabs();
            }
        }

        public bool IsAdmin() {
            if (this.Page.User.Identity.IsAuthenticated) {
                User cuyahogaUser = this.Page.User.Identity as User;
                if (cuyahogaUser.HasPermission(AccessLevel.Administrator)) {
                   return  true;
                }
            }

            return false;
        }

        public void RenderMenuTabs() {

            try {

                CatalogueViewModule controller = Module as CatalogueViewModule;
                UrlHelper = new CatalogueUrlHelper(controller);
                
                ICatalogueNodeView view;
                ICatalogueNodeView root;
                ICatalogueNodeView subs;
                List<ICatalogueNode> newCategoryList = new List<ICatalogueNode>();

                if (controller.ProductID > 0) {
                    //determine productCategory
                    root = controller.CatalogueViewer.GetRootCatalogueNodeView(controller.Section.Node.Site.Id, controller.Section.Node.Culture);
                   view = controller.CatalogueViewer.GetCatalogueNodeView(controller.Section.Node.Site.Id, controller.Section.Node.Culture, breadcrumb[1].NodeID);
                    subs = controller.CatalogueViewer.GetCatalogueNodeView(controller.Section.Node.Site.Id, controller.Section.Node.Culture, view.CurrentNode.ParentNodeID);

                    foreach (ICatalogueNode node in root.ChildNodes) {
                        newCategoryList.Add(node);
                        if (view.CurrentNode.ParentNodeID == node.NodeID) {
                            foreach (ICatalogueNode childNode in subs.ChildNodes) {
                                newCategoryList.Add(childNode);
                            }
                        }
                    }


                    RealNode = view.CurrentNode.NodeID;
                    CurrentNode = view.CurrentNode.ParentNodeID;
                    rptMenu.DataSource = newCategoryList;
                    rptMenu.DataBind();
                } else {


                    if (controller.NodeID > 0) {

                        view = controller.CatalogueViewer.GetCatalogueNodeView(controller.Section.Node.Site.Id, controller.Section.Node.Culture, controller.NodeID);
                        root = controller.CatalogueViewer.GetRootCatalogueNodeView(controller.Section.Node.Site.Id, controller.Section.Node.Culture);

                        if (view != null) {


                            if (view.ChildNodes.Count > 0) {
                                controller.NodeID = view.CurrentNode.NodeID;

                                foreach (ICatalogueNode node in root.ChildNodes) {
                                    newCategoryList.Add(node);
                                    if (view.CurrentNode.NodeID == node.NodeID) {
                                        //add all the child categories in
                                        foreach (ICatalogueNode childNode in view.ChildNodes) {
                                            newCategoryList.Add(childNode);
                                        }
                                    }

                                }

                                CurrentNode = view.CurrentNode.NodeID;
                                rptMenu.DataSource = newCategoryList;
                                rptMenu.DataBind();

                            } else {
                                subs = controller.CatalogueViewer.GetCatalogueNodeView(controller.Section.Node.Site.Id, controller.Section.Node.Culture, view.CurrentNode.ParentNodeID);

                                foreach (ICatalogueNode node in root.ChildNodes) {
                                    newCategoryList.Add(node);
                                    if (view.CurrentNode.ParentNodeID == node.NodeID) {
                                        foreach (ICatalogueNode childNode in subs.ChildNodes) {
                                            newCategoryList.Add(childNode);
                                        }
                                    }
                                }
                                RealNode = view.CurrentNode.NodeID;
                                CurrentNode = view.CurrentNode.ParentNodeID;
                                rptMenu.DataSource = newCategoryList;
                                rptMenu.DataBind();
                            }
                        }

                    } else {
                        view = controller.CatalogueViewer.GetRootCatalogueNodeView(controller.Section.Node.Site.Id, controller.Section.Node.Culture);
                        //show top level stuff only
                        if (view != null) {
                            controller.NodeID = view.CurrentNode.NodeID;
                            rptMenu.DataSource = view.ChildNodes;
                            rptMenu.DataBind();
                        }
                    }

                }

            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
            }
        }

        public string SetPrevious(ICatalogueNode node) {
            PreviousNode = Convert.ToInt64(node.ParentNodeID);
            NodeSet = true; ;
            return "";
        }

        public string SetNode(){
            FirstNode = false;
            NodeSet = false;
            return "";
        }

        public bool NodeHasNotBeenSet() {
            
                return NodeSet;
        }

        public bool IsFirstNode() {
            return FirstNode;
        }

        public bool HadSubCategories() {
            return SubCategories;
        }

        public string SetHadSubCategories(bool b) {
            SubCategories = b;
            return "";
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

