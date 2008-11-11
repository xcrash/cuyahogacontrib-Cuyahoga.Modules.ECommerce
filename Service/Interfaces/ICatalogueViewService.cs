using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections;

using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce.Domain;
using DbProduct = Cuyahoga.Modules.ECommerce.Domain.Product;
namespace Cuyahoga.Modules.ECommerce.Service {

    /// <summary>
    /// Provides a coarse-grained interface to catalogue information. Results are intended to simplify
    /// cacheing and use as web services. Results should give a single object that can be used for
    /// most display operations.
    /// </summary>
    public interface ICatalogueViewService {

        /// <summary>
        /// Gets the root level catalogue node plus related items
        /// </summary>
        /// <returns></returns>
        ICategoryView GetRootCategoryView(int storeId, string cultureCode);

        /// <summary>
        /// Gets the catalogue node information
        /// </summary>
        /// <param name="nodeID">id of catalogue node</param>
        /// <returns></returns>
        ICategoryView GetCategoryView(int storeId, string cultureCode, long nodeID);

        /// <summary>
        /// Gets the catalogue node information
        /// </summary>
        /// <param name="nodeID">id of catalogue node</param>
        /// <returns></returns>
        List<IActiveProductAttribute> GetCatalogueAttributes(int storeId, string cultureCode);

        /// <summary>
        /// Gets full product information and related information
        /// If this is an active part, the attribute values will be null
        /// </summary>
        /// <param name="productID">id of product to be returned</param>
        /// <returns></returns>
        IProductView GetProductView(int storeId, string cultureCode, long productID);

        /// <summary>
        /// Finds a list of products matching the search term
        /// </summary>
        /// <param name="searchTerm">text to find</param>
        /// <returns></returns>
        List<IProductSummary> FindProducts(int storeId, string cultureCode, string searchTerm);

        /// <summary>
        /// Tell any cacheing schemes to discard product information about this product
        /// </summary>
        /// <param name="productId"></param>
        void NotifyProductChange(int storeId, string cultureCode, long productId);

        /// <summary>
        /// Tell any cacheing schemes to discard product information about this category
        /// </summary>
        /// <param name="categoryId"></param>
        void NotifyCategoryChange(int storeId, string cultureCode, long categoryId);

        //What are all these??
        DbProduct GetECommerceProduct(int storeId, string cultureCode, long productID);
        DbProduct GetECommerceProduct(int storeId, string cultureCode, string productName);
        DbProduct GetECommerceProductByItemCode(int storeId, string cultureCode, string itemCode);
        IList GetECommerceProductsByCategory(int storeId, string cultureCode, long categoryID);

        List<Document> GetDocuments(int storeId, string cultureCode);

        Document GetDocument(int storeId, string culture, long DocumentID);

        Category GetCategoryByProductCode(long productCode);
        Category GetCategory(int storeId, string cultureCode, long nodeID);
        Category GetCategory(int storeId, string cultureCode, string catName);

        CategoryLink GetCategoryLink(long id);

        Image GetImageByItemCode(int storeId, string cultureCode, string itemCode);
        Image GetImageById(long imageID);

        AttributeOptionValue GetAttributeOptionValue(int storeId, string cultureCode, string optionName);
    }
}