using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using System.Web.Caching;

namespace Cuyahoga.Modules.ECommerce.Service {

    public class CachedCatalogueViewService : ICatalogueViewService {
    
        private ICatalogueViewService _service;

        private const string KEY_TYPE_CAT_VIEW = "CatView";
        private const string KEY_TYPE_PROD_VIEW = "ProdView";

        private string GetCacheKey(string keyType, int storeId, string cultureCode, long id) {
            return "CEC__CV__" + keyType + "__" + storeId + "__" + cultureCode + "__" + id;
        }

        public CachedCatalogueViewService(ICatalogueViewService service) {
            _service = service;
        }

        #region ICatalogueViewService Members

        private Cache GetCache() {
            HttpContext ctx = HttpContext.Current;
            if (ctx == null) return null;
            return ctx.Cache;
        }

        public ICategoryView GetRootCategoryView(int storeId, string cultureCode) {
            
            string key = GetCacheKey(KEY_TYPE_CAT_VIEW, storeId, cultureCode, 0);

            Cache c = GetCache();
            ICategoryView view = c[key] as ICategoryView;

            if (view == null) {
                view = _service.GetRootCategoryView(storeId, cultureCode);
                if (view != null) {
                    c[key] = view;
                }
            }

            return view;
        }

        public ICategoryView GetCategoryView(int storeId, string cultureCode, long nodeID) {

            string key = GetCacheKey(KEY_TYPE_CAT_VIEW, storeId, cultureCode, nodeID);

            Cache c = GetCache();
            ICategoryView view = c[key] as ICategoryView;

            if (view == null) {
                view = _service.GetCategoryView(storeId, cultureCode, nodeID);
                if (view != null) {
                    c[key] = view;
                }
            }

            return view;
        }

        public ICategoryView GetCategoryView(int storeId, string cultureCode, long nodeID, int offset, int limit, string orderBy) {
            throw new Exception("The method or operation is not implemented.");
        }

        public int GetProductCount(int storeId, string cultureCode, long nodeID) {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<IActiveProductAttribute> GetCatalogueAttributes(int storeId, string cultureCode) {
            throw new Exception("The method or operation is not implemented.");
        }

        public IProductView GetProductView(int storeId, string cultureCode, long productID) {

            string key = GetCacheKey(KEY_TYPE_PROD_VIEW, storeId, cultureCode, productID);

            Cache c = GetCache();
            IProductView view = c[key] as IProductView;

            if (view == null) {
                view = _service.GetProductView(storeId, cultureCode, productID);
                if (view != null) {
                    c[key] = view;
                }
            }

            return view;
        }

        public List<IProductSummary> FindProducts(int storeId, string cultureCode, string searchTerm) {
            throw new Exception("The method or operation is not implemented.");
        }

        public void NotifyProductChange(int storeId, string cultureCode, long productId) {
            string key = GetCacheKey(KEY_TYPE_PROD_VIEW, storeId, cultureCode, productId);
            GetCache().Remove(key);
        }

        public void NotifyCategoryChange(int storeId, string cultureCode, long categoryId) {
            string key = GetCacheKey(KEY_TYPE_CAT_VIEW, storeId, cultureCode, categoryId);
            GetCache().Remove(key);
        }

        public Cuyahoga.Modules.ECommerce.Domain.Product GetECommerceProduct(int storeId, string cultureCode, long productID) {
            throw new Exception("The method or operation is not implemented.");
        }

        public Cuyahoga.Modules.ECommerce.Domain.Product GetECommerceProduct(int storeId, string cultureCode, string productName) {
            throw new Exception("The method or operation is not implemented.");
        }

        public Cuyahoga.Modules.ECommerce.Domain.Product GetECommerceProductByItemCode(int storeId, string cultureCode, string itemCode) {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Collections.IList GetECommerceProductsByCategory(int storeId, string cultureCode, long categoryID) {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Cuyahoga.Modules.ECommerce.Domain.Document> GetDocuments(int storeId, string cultureCode) {
            throw new Exception("The method or operation is not implemented.");
        }

        public Cuyahoga.Modules.ECommerce.Domain.Document GetDocument(int storeId, string culture, long DocumentID) {
            throw new Exception("The method or operation is not implemented.");
        }

        public Cuyahoga.Modules.ECommerce.Domain.Category GetCategoryByProductCode(long productCode) {
            throw new Exception("The method or operation is not implemented.");
        }

        public Cuyahoga.Modules.ECommerce.Domain.Category GetCategory(int storeId, string cultureCode, long id) {
            throw new Exception("The method or operation is not implemented.");
        }

        public Cuyahoga.Modules.ECommerce.Domain.Category GetCategory(int storeId, string cultureCode, string catName) {
            throw new Exception("The method or operation is not implemented.");
        }

        public Cuyahoga.Modules.ECommerce.Domain.CategoryLink GetCategoryLink(long id) {
            throw new Exception("The method or operation is not implemented.");
        }

        public Cuyahoga.Modules.ECommerce.Domain.Catalogue.Image GetImageByItemCode(int storeId, string cultureCode, string itemCode) {
            throw new Exception("The method or operation is not implemented.");
        }

        public Cuyahoga.Modules.ECommerce.Domain.Catalogue.Image GetImageById(long imageID) {
            throw new Exception("The method or operation is not implemented.");
        }

        public Cuyahoga.Modules.ECommerce.Domain.AttributeOptionValue GetAttributeOptionValue(int storeId, string cultureCode, string optionName) {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}