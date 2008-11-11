using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

using Cuyahoga.Core.Domain;
using Cuyahoga.Web.UI;
using Cuyahoga.Web.Util;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using log4net;

namespace Cuyahoga.Modules.ECommerce.Web.Controls {

    public class AlbionMenu : System.Web.UI.UserControl {

        private const long PRODUCT_PAGE = 19; //maybe should be nodeID
        private Cuyahoga.Web.UI.PageEngine _page;

        protected System.Web.UI.WebControls.PlaceHolder plhNodes;
        protected CatalogueUrlHelper ECommerceURLHelper;

        protected void Page_Load(object sender, EventArgs e) {

            PageEngine engine = Page as PageEngine;

            if (engine != null) {
                this._page = engine;
                RenderMenu();
            }
        }

        private void RenderMenu() {

            HtmlGenericControl mainList = new HtmlGenericControl("ul");
            mainList.Attributes.Add("id", "nav");
            int nodeIndex = 0;

            if (this._page.RootNode.ShowInNavigation && this._page.RootNode.ViewAllowed(this._page.CuyahogaUser)) {
                mainList.Controls.Add(BuildListItemFromNode(this._page.RootNode, nodeIndex++));
            }

            foreach (Node node in this._page.RootNode.ChildNodes) {

                if (node.ShowInNavigation && node.ViewAllowed(this._page.CuyahogaUser)) {

                    HtmlControl listItem = BuildListItemFromNode(node, nodeIndex++);

                    if (node.Id == PRODUCT_PAGE) {
                        //add category submenu

                        HtmlGenericControl subList = new HtmlGenericControl("ul");
                        subList.Attributes.Add("class", "adxm menu");
                        List<CategoryNode> cList = GetProductCategories();
                        
                        foreach (CategoryNode c in cList) {
                            subList.Controls.Add(BuildListItemFromCategory(c));
                        }

                        listItem.Controls.Add(subList);
                    }


                    mainList.Controls.Add(listItem);
                }
            }

            if (this._page.CuyahogaUser != null
                && this._page.CuyahogaUser.HasPermission(AccessLevel.Administrator)) {
                HtmlGenericControl listItem = new HtmlGenericControl("li");
                listItem.Attributes.Add("id", "menuadmin");
                HyperLink hpl = new HyperLink();
                hpl.NavigateUrl = this._page.ResolveUrl("~/Admin");
                hpl.Text = "Admin";
                listItem.Controls.Add(hpl);
                mainList.Controls.Add(listItem);
            }
            this.plhNodes.Controls.Add(mainList);
        }

        private HtmlControl BuildListItemFromCategory(CategoryNode cNode) {

            HtmlGenericControl listItem = new HtmlGenericControl("li");

            HyperLink hpl = new HyperLink();
            hpl.NavigateUrl = UrlHelper.GetSiteUrl() + "/29/section.aspx/view/catnav/node/" + cNode.NodeID;
            hpl.Text = cNode.Name;
            listItem.Controls.Add(hpl);
            return listItem;
        }

        private HtmlControl BuildListItemFromNode(Node node, int nodeIndex) {

            HtmlGenericControl listItem = new HtmlGenericControl("li");
            listItem.Attributes.Add("id", "menu" + node.ShortDescription.ToLower());

            HyperLink hpl = new HyperLink();
            hpl.NavigateUrl = UrlHelper.GetUrlFromNode(node);
            UrlHelper.SetHyperLinkTarget(hpl, node);
            hpl.Text = node.Title;

            // Little dirty trick to highlight the active item :)
            if (node.Id == this._page.ActiveNode.Id) {
                hpl.CssClass = "Active";
            }
            listItem.Controls.Add(hpl);
            return listItem;
        }

        private List<CategoryNode> GetProductCategories() {
            try {
                List<CategoryNode> categoryList = new List<CategoryNode>();
                using (SpHandler sph = new SpHandler("getProductLines")) {

                    sph.ExecuteReader();

                    while (sph.DataReader.Read()) {
                        CategoryNode c = new CategoryNode();
                        c.NodeID = Convert.ToInt64(sph.DataReader["categoryID"]);
                        c.Name = sph.DataReader["categoryName"] as string;
                        categoryList.Add(c);
                    }
                    return categoryList;
                }
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
            }

            return null;
        }
    }
}