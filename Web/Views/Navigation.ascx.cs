namespace Cuyahoga.Modules.ECommerce.Web.Views {
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
    /// <summary>
    ///		Summary description for CatNav.
    /// </summary>
    public class Navigatyion : BaseModuleControl {

        protected System.Web.UI.WebControls.Repeater rptMenu;
        protected System.Web.UI.WebControls.Repeater rptSecondaryMenu;
        protected System.Web.UI.WebControls.Repeater rptSecondarySubMenu;

        protected CatalogueUrlHelper UrlHelper;
        protected PlaceHolder plhSecondaryMenuHeader;

        private const long PRODUCT_PAGE = 5; //maybe should be nodeID
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

            HtmlGenericControl subList = new HtmlGenericControl("ul");
            subList.Attributes.Add("id", "TJK_dropDownMenu");
            List<CategoryNode> cList = GetProductCategories();

            foreach (CategoryNode c in cList) {
                subList.Controls.Add(BuildListItemFromCategory(c));
            }

            this.plhNodes.Controls.Add(subList);
        }

        private HtmlControl BuildListItemFromCategory(CategoryNode cNode) {

            HtmlGenericControl listItem = new HtmlGenericControl("li");

            HyperLink hpl = new HyperLink();
            hpl.NavigateUrl = Cuyahoga.Web.Util.UrlHelper.GetSiteUrl() + "/5/section.aspx/view/catnav/node/" + cNode.NodeID;
            hpl.Text = cNode.Name;
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