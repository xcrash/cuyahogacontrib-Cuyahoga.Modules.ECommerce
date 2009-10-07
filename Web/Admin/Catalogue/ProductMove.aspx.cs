using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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

using Cuyahoga.Modules.ECommerce;
using Cuyahoga.Modules.ECommerce.Web.Admin.Controls;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

using Guild.WebControls;

using ProductCategory = Cuyahoga.Modules.ECommerce.Domain.ProductCategory;
using DbProduct = Cuyahoga.Modules.ECommerce.Domain.Product;


namespace Cuyahoga.Modules.ECommerce.Web.Admin {

    public class ProductMove : ModuleAdminBasePage {

        public const string PARAM_PRODUCT_ID = "pid";
        public const string PARAM_DIRECTION = "direction";
        public const string PARAM_CATEGORY_ID = "cat";

        private long _productID;
        private string _direction;
        private long _catID;

        CatalogueViewModule controller;

        public long ProductID {
            get {
                return _productID;
            }
            set {
                _productID = value;
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

        public long CatID {
            get {
                return _catID;
            }
            set {
                _catID = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            try {
                ProductID = Int32.Parse(Request.Params[PARAM_PRODUCT_ID]);
            } catch {
            }

            try {
                CatID = Int32.Parse(Request.Params[PARAM_CATEGORY_ID]);
            } catch {
            }

            try {
                Direction = Request.Params[PARAM_DIRECTION];
            } catch {
            }

            controller = Module as CatalogueViewModule;
            DbProduct product = controller.CatalogueViewer.GetECommerceProduct(Section.Node.Site.Id, controller.Section.Node.Culture, ProductID);

            switch (Direction) {
                case "Up":
                    MoveUp(product);
                    break;
                case "Down":
                    MoveDown(product);
                    break;
                default:
                    break;
            }

            Response.Redirect(Request.UrlReferrer.ToString());
        }

        private void MoveUp(DbProduct product) {

            if (product.Categories.Count == 0) {
                return;
            }

            ProductCategory pc = null;
            
            foreach (ProductCategory productCategory in product.Categories) {
                if (productCategory.CategoryID == CatID) {
                    pc = productCategory;
                }
            }

            IList productCatList = controller.CatalogueViewer.GetECommerceProductsByCategory(controller.Section.Node.Site.Id, controller.Section.Node.Culture, pc.CategoryID);

            //Check the sort orders make sense
            short productCatIndex = GetNodeIndex(product, productCatList);

            //Make sure it's not the top one
            if (productCatIndex == 0) {
                return;
            }

            SwapProducts(productCatList, (short)(productCatIndex - 1));
        }

        private void MoveDown(DbProduct product) {

            if (product.Categories.Count == 0) {
                return;
            }

            ProductCategory pc = null;

            foreach (ProductCategory productCategory in product.Categories) {
                if (productCategory.CategoryID == CatID) {
                    pc = productCategory;
                }
            }

            IList productCatList = controller.CatalogueViewer.GetECommerceProductsByCategory(controller.Section.Node.Site.Id, controller.Section.Node.Culture, pc.CategoryID);

            //Check the sort orders make sense
            short productCatIndex = GetNodeIndex(product, productCatList);

            //Make sure it's not the bottom one
            if (productCatIndex == (productCatList.Count - 1)) {
                return;
            }

            SwapProducts(productCatList, productCatIndex);
        }

        private short GetNodeIndex(DbProduct product, IList productCatList) {

            short sortOrder = Domain.ProductCategory.SORT_ORDER_MIN;
            int storeID = controller.Section.Node.Site.Id;
            short productCatIndex = 0;

            //Perform quick sanity check on sort orders whilst we're at it
            foreach (ProductCategory node in productCatList) {

                if (node.SortOrder != sortOrder) {
                    node.SortOrder = sortOrder;
                    controller.EditService.MoveProduct(storeID, node);
                }

                //Work out index of our node in list
                if (node.ProductID == product.ProductID) {
                    productCatIndex = (short)(sortOrder - Domain.ProductCategory.SORT_ORDER_MIN);
                }

                sortOrder++;
            }

            return productCatIndex;
        }

        private void SwapProducts(IList productCatList, short productCatIndex) {

            int storeID = controller.Section.Node.Site.Id;

            //Swap nth item with (n + 1)th item
            ProductCategory currentCat = productCatList[productCatIndex] as ProductCategory;
            ProductCategory nextCat = productCatList[productCatIndex + 1] as ProductCategory;

            short tmpSortOrder = currentCat.SortOrder;

            currentCat.SortOrder = nextCat.SortOrder;
            controller.EditService.MoveProduct(storeID, currentCat);

            nextCat.SortOrder = tmpSortOrder;
            controller.EditService.MoveProduct(storeID, nextCat);
        }
    }
}