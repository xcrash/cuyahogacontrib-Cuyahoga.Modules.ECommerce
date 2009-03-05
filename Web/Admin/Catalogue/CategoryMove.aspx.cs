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


namespace Cuyahoga.Modules.ECommerce.Web.Admin {

    public class CategoryMove : ModuleAdminBasePage {

        public const string PARAM_CATEGORY_ID = "cat";
        public const string PARAM_DIRECTION = "direction";

        private long _catID;
        private string _direction;
        private CatalogueViewModule controller;

        public long CatID {
            get {
                return _catID;
            }
            set {
                _catID = value;
            }
        }

        public string Direction {
            get {
                return _direction;
            }
            set {
                _direction = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e) {
            try {
                CatID = Int32.Parse(Request.Params[PARAM_CATEGORY_ID]);
            } catch {
            }

            try {
                Direction = Request.Params[PARAM_DIRECTION];
            } catch {
            }

            controller = Module as CatalogueViewModule;
            Category cat = controller.CatalogueViewer.GetCategory(controller.Section.Node.Site.Id, controller.Section.Node.Culture, CatID);

            switch (Direction) {
                case "Up":
                    MoveUp(cat);
                    break;
                case "Down":
                    MoveDown(cat);
                    break;
                default:
                    break;
            }

            Response.Redirect(Request.UrlReferrer.ToString());
        }

        private void MoveUp(Category cat) {

            ICategoryView nodeView = controller.CatalogueViewer.GetCategoryView(controller.Section.Node.Site.Id, controller.Section.Node.Culture, cat.ParentCategory.CategoryID);
            int storeID = controller.Section.Node.Site.Id;

            //Check the sort orders make sense
            short catIndex = GetNodeIndex(cat, nodeView);

            //Make sure it's not the top one
            if (catIndex == 0) {
                return;
            }

            SwapCategories(nodeView, (short) (catIndex - 1));
        }

        private void MoveDown(Category cat) {

            ICategoryView nodeView = controller.CatalogueViewer.GetCategoryView(controller.Section.Node.Site.Id, controller.Section.Node.Culture, cat.ParentCategory.CategoryID);

            //Check the sort orders make sense
            short catIndex = GetNodeIndex(cat, nodeView);

            //Make sure it's not the bottom one
            if (catIndex == (nodeView.ChildNodes.Count - 1)) {
                return;
            }

            SwapCategories(nodeView, catIndex);
        }

        private short GetNodeIndex(Category cat, ICategoryView nodeView) {

            short sortOrder = Category.SORT_ORDER_MIN;
            int storeID = controller.Section.Node.Site.Id;
            short catIndex = 0;

            //Perform quick sanity check on sort orders whilst we're at it
            foreach (ICategory node in nodeView.ChildNodes) {

                if (node.SortOrder != sortOrder) {
                    Category catLoop = controller.CatalogueViewer.GetCategory(storeID, "", node.NodeID);
                    catLoop.SortOrder = sortOrder;
                    controller.EditService.MoveCategory(storeID, catLoop);
                }

                //Work out index of our node in list
                if (node.NodeID == cat.NodeID) {
                    catIndex = (short) (sortOrder - Category.SORT_ORDER_MIN);
                }

                sortOrder++;
            }

            return catIndex;
        }

        private void SwapCategories(ICategoryView nodeView, short catIndex) {

            int storeID = controller.Section.Node.Site.Id;

            //Swap nth item with (n + 1)th item
            Category currentCat = controller.CatalogueViewer.GetCategory(storeID, "", nodeView.ChildNodes[catIndex].NodeID);
            Category nextCat = controller.CatalogueViewer.GetCategory(storeID, "", nodeView.ChildNodes[catIndex + 1].NodeID);

            short tmpSortOrder = currentCat.SortOrder;

            currentCat.SortOrder = nextCat.SortOrder;
            controller.EditService.MoveCategory(storeID, currentCat);

            nextCat.SortOrder = tmpSortOrder;
            controller.EditService.MoveCategory(storeID, nextCat);
        }
    }
}