using System.Collections.Specialized;
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce {

	/// <summary>
	/// Summary description for CatalogueUrlHelper.
	/// </summary>
	public class CatalogueUrlHelper {

		private CatalogueViewModule _module;
        public const string VIEW_KITS = "kits";
        public const string VIEW_KIT_PRODUCT_LIST = "kitProductList";
		public const string VIEW_CAT_NAV = "catnav";
		public const string VIEW_PRODUCT = "prodinfo";
        public const string VIEW_IMAGE = "popup";

        public CatalogueUrlHelper(CatalogueViewModule controller) {
            _module = controller;
		}

        public virtual string GetCatalogueNodeUrlForKits(ITrailItem node) {
            NameValueCollection paramList = GetBaseParamList();
            paramList.Add(GenericModule.PARAM_VIEW_NAME, VIEW_KITS);
            paramList.Add(CatalogueViewModule.PARAM_NODE_ID, node.NodeID.ToString());

            return _module.GetUrl(paramList);
        }

        public virtual string GetUrlForKitProductList(long kitID) {
            NameValueCollection paramList = GetBaseParamList();
            paramList.Add(GenericModule.PARAM_VIEW_NAME, VIEW_KIT_PRODUCT_LIST);
            paramList.Add(CatalogueViewModule.PARAM_KIT_ID, kitID.ToString());

            return _module.GetUrl(paramList);
        }

		public virtual string GetCatalogueNodeUrl(ITrailItem node) {
			return GetCatalogueNodeUrl(node.NodeID);
		}

        public virtual string GetCatalogueNodeUrl(long nodeID) {

			NameValueCollection paramList = GetBaseParamList();
			paramList.Add(GenericModule.PARAM_VIEW_NAME, VIEW_CAT_NAV);
			paramList.Add(CatalogueViewModule.PARAM_NODE_ID, nodeID.ToString());

			return _module.GetUrl(paramList);
		}

		private NameValueCollection GetBaseParamList() {
			NameValueCollection paramList = new NameValueCollection();
			return paramList;
		}

        public virtual string GetProductUrl(IProductSummary summary) {
            if (summary != null) {
                return GetProductUrl(summary.ProductID);
            } else {
                try {
                    return GetCatalogueNodeUrl(_module.CatalogueViewer.GetRootCategoryView(_module.Section.Node.Site.Id, _module.Section.Node.Culture).CurrentNode.NodeID);
                } catch {
                    return "";
                }
            }
        }

        public virtual string GetProductUrl(long productID) {

			NameValueCollection paramList = GetBaseParamList();
            paramList.Add(GenericModule.PARAM_VIEW_NAME, VIEW_PRODUCT);
			paramList.Add(CatalogueViewModule.PARAM_PRODUCT_ID, productID.ToString());

			return _module.GetUrl(paramList);
		}

        public virtual string GetProductUrl(string productID) {
            
            NameValueCollection paramList = GetBaseParamList();
            paramList.Add(GenericModule.PARAM_VIEW_NAME, VIEW_PRODUCT);
            paramList.Add(CatalogueViewModule.PARAM_PRODUCT_SKU, productID);

            return _module.GetUrl(paramList);
        }

        public virtual string GetMenuUrl(long nodeID) {

            /*FIXME*/
            //this is a hack due to deadline. we need to change the way views work. If I have a menu view on every page, then you click on a category
            //it will ouput the catalogue on that page with all the other crap. hence the bodge. Basically just gets the view bit of the url and you append it to the something You hard code in the repeater. 
            //Terrible I know...Such is Life..

            NameValueCollection paramList = GetBaseParamList();
            paramList.Add(GenericModule.PARAM_VIEW_NAME, VIEW_CAT_NAV);
            paramList.Add(CatalogueViewModule.PARAM_NODE_ID, nodeID.ToString());

            return _module.GetMenuUrl(paramList);
            
        }

        public virtual string GetImagePopUpUrl(string itemCode) {
            
            NameValueCollection paramList = GetBaseParamList();
            paramList.Add(GenericModule.PARAM_VIEW_NAME, VIEW_IMAGE);
            paramList.Add(CatalogueViewModule.PARAM_ITEM_CODE, itemCode);

            return _module.GetUrl(paramList);
        }
	}
}
