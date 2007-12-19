using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections;

using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce.Domain;
using DbProduct = Cuyahoga.Modules.ECommerce.Domain.Product;
namespace Cuyahoga.Modules.ECommerce.Service {

    public interface ICatalogueViewService {

        #region Catalogue nodes
        /// <summary>
        /// Gets the root level catalogue node
        /// </summary>
        /// <returns></returns>
        ICatalogueNode GetRootCatalogueNode(int storeID, string cultureCode);

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
        ICatalogueNode GetCatalogueNode(int storeID, string cultureCode, long nodeID);

        /// <summary>
        /// Gets the catalogue node information
        /// </summary>
        /// <param name="nodeID">id of catalogue node</param>
        /// <returns></returns>

        ICatalogueNodeView GetCatalogueNodeView(int storeID, string cultureCode, long nodeID);
        Category GetCategory(int storeID, string cultureCode, long nodeID);

        /// <summary>
        /// Gets all immediate children for a catalogue node
        /// </summary>
        /// <param name="parentNodeID">id of parent catalogue node</param>
        /// <returns></returns>
        [return: XmlArrayItem(ElementName = "CatalogueSection", Type = typeof(CatalogueNode))]
        List<ICatalogueNode> GetCatalogueNodeChildren(int storeID, string cultureCode, long parentNodeID);
        #endregion

        List<Document> GetDocuments(int storeID, string cultureCode);

        Document GetDocument(int storeID, string culture, long DocumentID);

        Category GetCategoryByProductCode(long productCode);

        CategoryLink GetCategoryLink(long id);

        #region Attributes

        /// <summary>
        /// Gets the catalogue node information
        /// </summary>
        /// <param name="nodeID">id of catalogue node</param>
        /// <returns></returns>
        List<Domain.Catalogue.Interfaces.IActiveProductAttribute> GetCatalogueAttributes(int storeID, string cultureCode);
        Domain.AttributeOptionValue GetAttributeOptionValue(int storeID, string cultureCode, string optionName);

        List<ICatalogueNode> GetCategoryBreadCrumb(ICatalogueNode node);
        List<ICatalogueNode> GetProductBreadCrumbTrail(long productID);

        #endregion

        #region Product Related
        /// <summary>
        /// Gets limited product information
        /// </summary>
        /// <param name="productID">id of product to be returned</param>
        /// <returns></returns>
        IProductSummary GetProductSummary(int storeID, string cultureCode, long productID);

        /// <summary>
        /// Gets full product information.
        /// If this is an active part, the attribute values will be null
        /// </summary>
        /// <param name="productID">id of product to be returned</param>
        /// <returns></returns>
        IProduct GetProduct(int storeID, string cultureCode, long productID);

        /// <summary>
        /// Gets full product information and related information
        /// If this is an active part, the attribute values will be null
        /// </summary>
        /// <param name="productID">id of product to be returned</param>
        /// <returns></returns>
        IProductView GetProductView(int storeID, string cultureCode, long productID);

        Category GetCategory(int storeID, string cultureCode, string catName);

        /// <summary>
        /// Finds a list of products matching the search term
        /// </summary>
        /// <param name="searchTerm">text to find</param>
        /// <returns></returns>
        List<IProductSummary> FindProducts(string searchTerm);

        /// <summary>
        /// Finds a list of products matching the search term from a specific node or child
        /// </summary>
        /// <param name="searchTerm">text to find</param>
        /// <param name="nodeID">Node or parent node of category</param>
        /// <returns></returns>
        List<IProductSummary> FindProducts(string searchTerm, long nodeID);

        //What are all these??
        DbProduct GetECommerceProduct(int storeID, string cultureCode, long productID);
        DbProduct GetECommerceProduct(int storeID, string cultureCode, string productName);
        DbProduct GetECommerceProductByItemCode(int storeID, string cultureCode, string itemCode);
        IList GetECommerceProductsByCategory(int storeID, string cultureCode, long categoryID);

        //Are you mad?? -- YES -- BUT in a good way
        List<Cuyahoga.Modules.ECommerce.Domain.Product> GetAllProducts(int storeID, string cultureCode);
        #endregion
    }
}