using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections;

using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce.Domain;
using DbProduct = Cuyahoga.Modules.ECommerce.Domain.Product;
namespace Cuyahoga.Modules.ECommerce.Service {

    public interface ICatalogueViewService {

        /// <summary>
        /// Gets the root level catalogue node plus related items
        /// </summary>
        /// <returns></returns>
        ICatalogueNodeView GetRootCatalogueNodeView(int storeID, string cultureCode);

        /// <summary>
        /// Gets the catalogue node information
        /// </summary>
        /// <param name="nodeID">id of catalogue node</param>
        /// <returns></returns>

        ICatalogueNodeView GetCatalogueNodeView(int storeID, string cultureCode, long nodeID);

        //Can we ditch these two?
        ICatalogueNodeView GetCatalogueNodeView(int storeID, string cultureCode, long nodeID, int offset, int limit, string orderBy);
        int GetProductCount(int storeID, string cultureCode, long nodeID);

        /// <summary>
        /// Gets the catalogue node information
        /// </summary>
        /// <param name="nodeID">id of catalogue node</param>
        /// <returns></returns>
        List<IActiveProductAttribute> GetCatalogueAttributes(int storeID, string cultureCode);

        /// <summary>
        /// Gets full product information and related information
        /// If this is an active part, the attribute values will be null
        /// </summary>
        /// <param name="productID">id of product to be returned</param>
        /// <returns></returns>
        IProductView GetProductView(int storeID, string cultureCode, long productID);

        /// <summary>
        /// Finds a list of products matching the search term
        /// </summary>
        /// <param name="searchTerm">text to find</param>
        /// <returns></returns>
        List<IProductSummary> FindProducts(int storeID, string cultureCode, string searchTerm);

        //What are all these??
        DbProduct GetECommerceProduct(int storeID, string cultureCode, long productID);
        DbProduct GetECommerceProduct(int storeID, string cultureCode, string productName);
        DbProduct GetECommerceProductByItemCode(int storeID, string cultureCode, string itemCode);
        IList GetECommerceProductsByCategory(int storeID, string cultureCode, long categoryID);

        List<Document> GetDocuments(int storeID, string cultureCode);

        Document GetDocument(int storeID, string culture, long DocumentID);

        Category GetCategoryByProductCode(long productCode);
        Category GetCategory(int storeID, string cultureCode, long nodeID);
        Category GetCategory(int storeID, string cultureCode, string catName);

        CategoryLink GetCategoryLink(long id);

        Image GetImageByItemCode(int storeID, string cultureCode, string itemCode);
        Image GetImageById(long imageID);

        AttributeOptionValue GetAttributeOptionValue(int storeID, string cultureCode, string optionName);
    }
}