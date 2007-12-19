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
using Cuyahoga.Modules.ECommerce.Util;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Web.Admin {

    public partial class CategoryEdit : ModuleAdminBasePage {

        public const string PARAM_CATEGORY_ID = "cat";
        public const string PARAM_PARENT_CATEGORY_ID = "parentCategory";
        public const int CATEGORY_ID_NEW = 0;

        private long _catID = CATEGORY_ID_NEW;
        private long _parentCategoryID = CATEGORY_ID_NEW;

        protected TextBox txtCategoryName;
        protected LinkButton lnkSave;
        protected TextBox txtDescription;
        protected TextBox txtKitDescription;
        protected WebImageMaker wim;

        protected TextBox txtCss;
        protected TextBox txtPriceChangePercent;
        protected Label lblCss;
        protected Label lblSavingMessage;
        protected Label lblImage;
        protected Label lblDescription;
        protected Label lblKitImage;
        protected Label lblKitDescription;
        protected Label lblName;
        protected Label lblPriceChangePercent;

        private ICatalogueNode cat;
        protected string catImageUrl;
        protected string kitImageUrl;

        protected Repeater rptPages;
        protected Repeater rptKits;
        public CatalogueViewModule controller;
        protected DropDownList ddlNodeList;

        protected TextBox txtTitle;
        protected WebImageMaker wimLinkImage;
        protected WebImageMaker wimKitImage;
        protected LinkButton btnAddPage;
        protected BreadCrumb ctlBreadCrumb;

        public long CatID {
            get {
                return _catID;
            }
            set {
                _catID = value;
            }
        }

        public long ParentNodeID {
            get {
                return _parentCategoryID;
            }
            set {
                _parentCategoryID = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

            this.Title = "Category Editor";

            ReadRequestParams();

            rptPages.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(rptSections_ItemCommand);
            btnAddPage.Click += new EventHandler(btnAddPage_Click);
            controller = Module as CatalogueViewModule;

            ICatalogueNodeView nodeView = null;
            
            if (CatID != CATEGORY_ID_NEW) {
                nodeView = controller.CatalogueViewer.GetCatalogueNodeView(1, "en-GB", CatID);
                cat = nodeView.CurrentNode;
            } else {
                if (!IsPostBack) {
                    nodeView = controller.CatalogueViewer.GetCatalogueNodeView(1, "en-GB", ParentNodeID);
                    ctlBreadCrumb.SetLinkOnCurrentNode = true;
                    ctlBreadCrumb.RenderBreadCrumbTrail(nodeView.BreadCrumbTrail);
                }
            }

            if (cat != null) {

                if (cat.Image != null) {
                    catImageUrl = cat.Image.Url;
                }

                rptKits.DataSource = cat.Kits;
                rptKits.DataBind();

                if (!IsPostBack) {

                    IList rootNodes = Section.Node.Site.RootNodes;
                    ddlNodeList.Items.Clear();
                    ddlNodeList.Items.Add(new ListItem("-- select --", ""));

                    foreach (Node rootNode in rootNodes) {
                        AddChildNodes(ddlNodeList, rootNode);
                    }
                    BindPages();

                    txtCategoryName.Text = cat.Name;
                    txtDescription.Text = cat.Description.ToString();
                    txtKitDescription.Text = cat.KitDescription;
                    txtCss.Text = cat.CssClass;
                    txtPriceChangePercent.Text = (cat.PriceChangePercent != null) ? cat.PriceChangePercent.ToString() : "";

                    if (cat.Image.Height != null) wim.ImageHeight = cat.Image.Height.ToString();
                    if (cat.Image.Width != null) wim.ImageWidth = cat.Image.Width.ToString();
                    wim.ImageUrl = WebHelper.GetImagePathWeb() + cat.Image.Url;
                    wim.WorkingDirectory = WebHelper.GetImageWorkingDirectory();
                    wimLinkImage.WorkingDirectory = wim.WorkingDirectory;

                    wimKitImage.WorkingDirectory = wim.WorkingDirectory;
                    wimKitImage.ImageUrl = WebHelper.GetImagePathWeb() + cat.KitPicture;

                    ctlBreadCrumb.RenderBreadCrumbTrail(nodeView.BreadCrumbTrail);
                }
            }

            if (ParentNodeID != 0) {
                this.lnkSave.Text = "Save Sub Category";
            } else {
                this.lnkSave.Text = "Save Category";
            }

            this.lnkSave.Click += new EventHandler(lnkSave_Click);
        }

        private void ReadRequestParams() {
            try {
                CatID = Int32.Parse(Request.Params[PARAM_CATEGORY_ID]);
            } catch { }

            if (CatID == CATEGORY_ID_NEW) {
                try {
                    ParentNodeID = Int32.Parse(Request.Params[PARAM_PARENT_CATEGORY_ID]);
                } catch { }
            }
        }

        private void AddChildNodes(DropDownList ddlList, Node parentNode) {

            ddlList.Items.Add(new ListItem(GetListItemText(parentNode), parentNode.Id.ToString()));

            foreach (Node child in parentNode.ChildNodes) {
                AddChildNodes(ddlList, child);
            }
        }

        private string GetListItemText(Node node) {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < node.Level; i++) {
                sb.Append("...");
            }
            return sb.ToString() + node.Title;
        }


        private void lnkSave_Click(object sender, EventArgs e) {

            Category newCategory = GetUpdatedCategory();

            if (controller.EditService.SaveCategory(1, newCategory)) {

                Response.Redirect(CatalogueBrowser.GetBrowserUrl(this, newCategory.CategoryID, "Your category has been Saved"));

            } else {
                lblSavingMessage.Text = "Saving failed";
            }
        }

        public Category GetUpdatedCategory() {

            ReadRequestParams();

            Category newCategory;
            if (CatID != CATEGORY_ID_NEW) {
                newCategory = controller.CatalogueViewer.GetCategory(1, "en-GB", CatID);
            } else {
                newCategory = new Category();
                newCategory.IsPublished = true;
                newCategory.ParentCategory = controller.CatalogueViewer.GetCategory(1, "en-GB", ParentNodeID);
            }

            newCategory.CategoryDescription = txtDescription.Text;
            newCategory.CategoryName = txtCategoryName.Text;
            newCategory.KitDescription = txtKitDescription.Text;
            newCategory.CssClass = txtCss.Text;

            try {
                newCategory.PriceChangePercent = Convert.ToDecimal(txtPriceChangePercent.Text);
            } catch {
                newCategory.PriceChangePercent = null;
            }

            if (wim.ImageHeight != null && wim.WebImagePath != null) {
                newCategory.Height = Convert.ToInt16(wim.ImageHeight);
                newCategory.Width = Convert.ToInt16(wim.ImageWidth);
                newCategory.Url = WebHelper.GetImageUrl(wim.WebImagePath);
            }

            if (wimKitImage.ImageHeight != null && wimKitImage.WebImagePath != null) {
                newCategory.KitPicture = WebHelper.GetImageUrl(wimKitImage.WebImagePath);
            }

            return newCategory;
        }

        private void btnAddPage_Click(object sender, EventArgs e) {

            if (!string.IsNullOrEmpty(ddlNodeList.SelectedValue)) {

                int nodeID = Int32.Parse(ddlNodeList.SelectedValue);

                CategoryLink cl = new CategoryLink();
                cl.CategoryID = CatID;
                cl.ImageUrl = WebHelper.GetImageUrl(wimLinkImage.WebImagePath);
                cl.NodeID = nodeID;
                cl.Title = txtTitle.Text;

                try {
                    controller.EditService.SaveCategoryLink(1, cl);
                } catch (Exception ex) {
                    LogManager.GetLogger(GetType()).Error(ex);
                    ShowError(ex.Message);
                }

                BindPages();
            }
        }

        private void rptSections_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e) {

            long linkID = Int64.Parse(e.CommandArgument.ToString());

            try {
                CategoryLink c = controller.CatalogueViewer.GetCategoryLink(linkID);
                base.CoreRepository.DeleteObject(c);
            } catch (Exception ex) {
                LogManager.GetLogger(GetType()).Error(ex);
                ShowError(ex.Message);
            }

            BindPages();
        }

        private void BindPages() {
            Category c = controller.CatalogueViewer.GetCategory(1, "", CatID);
            rptPages.DataSource = c.Links;
            rptPages.DataBind();
        }
    }
}