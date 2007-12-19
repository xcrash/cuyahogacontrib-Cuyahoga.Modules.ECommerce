using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Castle.Facilities.NHibernateIntegration;

using Cuyahoga.Core;
using Cuyahoga.Modules.ECommerce.DataAccess;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;

using NHibernate;
using log4net;
using DbProduct = Cuyahoga.Modules.ECommerce.Domain.Product;

namespace Cuyahoga.Modules.ECommerce.Service {

    public class CatalogueViewService : ICatalogueViewService, INHibernateModule {

        private ISessionManager _sessionManager;
        private IExtCommonDao _dao;

        public CatalogueViewService(ISessionManager sessionManager, IExtCommonDao dao) {
            _sessionManager = sessionManager;
            _dao = dao;
        }

        private void AddCategoryKits(Category category) {

            IList kitList = new ArrayList();

            try {

                using (SpHandler sph = new SpHandler("getCategoryKits", new SqlParameter("@categoryID", category.CategoryID))) {

                    sph.ExecuteReader();
                    IDataReader record = sph.DataReader;

                    while (record.Read()) {

                        IKitSummary summary = new KitSummary();

                        summary.Name = Convert.ToString(record["productName"]);
                        summary.AltText = summary.Name;
                        summary.ItemCode = Convert.ToString(record["itemCode"]);
                        summary.ProductID = Convert.ToInt64(record["ProductID"]);
                        summary.Url = Convert.ToString(record["imageUrl"]);

                        try {
                            summary.Width = Convert.ToInt16(record["width"]);
                        } catch { }

                        try {
                            summary.Height = Convert.ToInt16(record["height"]);
                        } catch { }

                        kitList.Add(summary);
                    }
                }
            } catch (Exception ex) {
                LogManager.GetLogger(GetType()).Error(ex);
            }

            category.Kits = kitList;
        }

        #region ICatalogueViewService Members

        public ICatalogueNode GetRootCatalogueNode(int storeID, string cultureCode) {

            ICatalogueNode node = new CatalogueNode();
            IQuery query = this._sessionManager.OpenSession().CreateQuery("from Category where parentcategoryID IS Null AND isPublished = 1 order by sortOrder");

            IList cats = query.List();

            if (cats.Count > 0) {
                foreach (Category epc in cats) {

                    node.Name = epc.CategoryName;
                    node.NodeID = Convert.ToInt32(epc.CategoryID);
                    node.Description = epc.CategoryDescription;

                    IImage image = new Domain.Catalogue.Image();
                    ImageHelper.CopyImage(epc, image);
                    node.Image = image;
                }
            }

            return node;
        }

        public ICatalogueNodeView GetRootCatalogueNodeView(int storeID, string cultureCode) {
            ICatalogueNodeView nodeView = new CatalogueNodeView();
            nodeView.CurrentNode = GetRootCatalogueNode(storeID, cultureCode);
            nodeView.ChildNodes = GetCatalogueNodeChildren(storeID, cultureCode, nodeView.CurrentNode.NodeID);
            nodeView.BreadCrumbTrail = GetCategoryBreadCrumb(nodeView.CurrentNode);
            return nodeView;
        }

        public List<ICatalogueNode> GetCatalogueNodeChildren(int storeID, string cultureCode, long parentNodeID) {

            IQuery query = this._sessionManager.OpenSession().CreateQuery("from Category where parentcategoryID = :id AND (categoryID <> :id ) AND isPublished = 1 order by sortOrder");

            query.SetInt64("id", parentNodeID);
            IList cats2 = query.List();

            List<ICatalogueNode> children = new List<ICatalogueNode>();

            if (cats2.Count > 0) {

                foreach (Category epc in cats2) {
                    if (epc.IsPublished) {
                        ICatalogueNode node = new CatalogueNode();

                        node.Name = epc.CategoryName;
                        node.NodeID = Convert.ToInt64(epc.CategoryID);
                        node.ParentNodeID = Convert.ToInt64(epc.ParentCategory.CategoryID);
                        node.Description = epc.CategoryDescription;
                        node.CssClass = epc.CssClass;
                        IImage image = new Domain.Catalogue.Image();
                        ImageHelper.CopyImage(epc, image);
                        node.Image = image;
                        node.SortOrder = epc.SortOrder;
                        node.PriceChangePercent = epc.PriceChangePercent;
                        children.Add(node);
                    }
                }
            }

            return children;
        }

        public ICatalogueNode GetCatalogueNode(int storeID, string cultureCode, long nodeID) {

            ICatalogueNode node = new CatalogueNode();
            Category cat = (Category)_dao.GetObjectById(typeof(Category), nodeID);

            if (cat != null) {

                AddCategoryKits(cat);

                node.Name = cat.CategoryName;
                node.NodeID = Convert.ToInt64(cat.CategoryID);

                if (cat.ParentCategory != null) {
                    node.ParentNodeID = Convert.ToInt32(cat.ParentCategory.CategoryID);
                }

                node.Kits = cat.Kits;
                node.KitPicture = cat.KitPicture;
                node.KitDescription = cat.KitDescription;
                node.CssClass = cat.CssClass;
                node.Description = cat.CategoryDescription;
                node.Links = cat.Links;
                node.PriceChangePercent = cat.PriceChangePercent;

                if (node.Image == null) {
                    node.Image = new Domain.Catalogue.Image();
                }

                ImageHelper.CopyImage(cat, node.Image);
            }

            return node;
        }

        public Category GetCategory(int storeID, string cultureCode, string catName) {
            Category cat = _dao.GetObjectByDescription(typeof(Category), "_CategoryName", catName) as Category;
            AddCategoryKits(cat);
            return cat;
        }

        public Category GetCategory(int storeID, string cultureCode, long nodeID) {
            Category cat = (Category)_dao.GetObjectById(typeof(Category), nodeID);
            AddCategoryKits(cat);
            return cat;
        }

        public ICatalogueNodeView GetCatalogueNodeView(int storeID, string cultureCode, long nodeID) {

            ICatalogueNodeView nodeView = new CatalogueNodeView();
            nodeView.CurrentNode = GetCatalogueNode(storeID, cultureCode, nodeID);
            nodeView.ChildNodes = GetCatalogueNodeChildren(storeID, cultureCode, nodeID);
            nodeView.BreadCrumbTrail = GetCategoryBreadCrumb(nodeView.CurrentNode);

            if (nodeView.ChildNodes.Count < 1) {

                IQuery pQuery = this._sessionManager.OpenSession().CreateQuery("from ProductCategory WHERE categoryID = :id Order By sortOrder");
                pQuery.SetInt64("id", nodeID);
                IList productCategories = pQuery.List();

                //im sure this is repeated
                foreach (ProductCategory pc in productCategories) {
                    if (pc.Product.IsPublished) {
                        nodeView.ProductList.Add(pc.Product.CreateFullProductSummary());
                    }
                }
            }

            return nodeView;
        }

        public IProductSummary GetProductSummary(int storeID, string cultureCode, long productID) {

            IProduct product = new Domain.Catalogue.Product();

            DbProduct ep = GetECommerceProduct(storeID, cultureCode, productID);

            ep.PopulateProductSummary(product);

            foreach (ProductSynonym s in ep.Synonyms) {
                product.SynonymList.Add(s);
            }
            if (ep.StockLevel > 0) {
                product.StockedIndicator = 1;
            }

            return product;
        }

        public IProduct GetProduct(int storeID, string cultureCode, long productID) {

            DbProduct eProduct = GetECommerceProduct(storeID, cultureCode, productID);
            IProduct product = new Domain.Catalogue.Product();

            eProduct.PopulateProductSummary(product);

            product.CrossSellList = GetRelatedProducts(eProduct, "CrossSell");
            product.UpSellList = GetRelatedProducts(eProduct, "UpSell");
            product.ActiveAttributeList = GetProductAttributes(product);
            product.ProductImages = GetProductImages(eProduct);
            product.DocumentList = GetProductDocuments(eProduct);
            product.SkuList = GetProductSkus(eProduct);

            foreach (ProductSynonym s in eProduct.Synonyms) {
                product.SynonymList.Add(s);
            }

            return product;
        }
        #region utility functions

        public IList GetECommerceProductsByCategory(int storeID, string cultureCode, long categoryID) {
            IQuery query = this._sessionManager.OpenSession().CreateQuery("from ProductCategory  where categoryID = :id order by SortOrder");
            query.SetInt64("id", categoryID);
            IList productList = query.List();
            return productList;
        }


        public DbProduct GetECommerceProduct(int storeID, string cultureCode, long productID) {
            return _dao.GetObjectById(typeof(DbProduct), productID) as DbProduct;
            //this may have to change due to kits -- unless I put kits in their own Table
        }

        public DbProduct GetECommerceProduct(int storeID, string cultureCode, string productName) {
            return _dao.GetObjectByDescription(typeof(DbProduct), "_ProductName", productName) as DbProduct;
        }

        public DbProduct GetECommerceProductByItemCode(int storeID, string cultureCode, string itemCode) {
            return _dao.GetObjectByDescription(typeof(DbProduct), "_ItemCode", itemCode) as DbProduct;
        }

        public Domain.AttributeOptionValue GetAttributeOptionValue(int storeID, string cultureCode, string optionName) {
            return _dao.GetObjectByDescription(typeof(AttributeOptionValue), "_OptionName", optionName) as AttributeOptionValue;
        }

        public List<IActiveProductAttribute> GetProductAttributes(IProduct ep) {

            List<IActiveProductAttribute> attributeList = new List<IActiveProductAttribute>();
            IActiveProductAttribute active = null;
            int lastAttributeID = 0;
            int count = 0;
            //Replaced with a stored procedure to make it far, far quicker
            using (SpHandler sph = new SpHandler("getProductAttributes", new SqlParameter("@productID", ep.ProductID))) {

                sph.ExecuteReader();

                while (sph.DataReader.Read()) {

                    int attributeID = Convert.ToInt32(sph.DataReader["attributeID"]);

                    //Create a new attribute as it is found. Relies on values returned grouped by attributeid
                    if (attributeID != lastAttributeID) {
                        count++;
                        if (lastAttributeID != 0) {
                            attributeList.Add(active);
                        }

                        active = new ActiveProductAttribute();
                        active.DataType = Convert.ToString(sph.DataReader["type"]);
                        active.BaseUnit = Convert.ToString(sph.DataReader["baseUnit"]);
                        active.Name = Convert.ToString(sph.DataReader["attributeDescription"]);

                        lastAttributeID = attributeID;
                    }

                    if (active != null) {

                        //Add options to the current attribute
                        IAttributeOption option = new AttributeOption();

                        option.PickListValue = Convert.ToString(sph.DataReader["optionName"]);
                        option.ShortCode = Convert.ToString(sph.DataReader["optionID"]);
                        option.Url = Convert.ToString(sph.DataReader["optionData"]);
                        option.Price = Convert.ToDecimal(sph.DataReader["optionPrice"]);

                        active.AttributeOptionList.Add(option);
                    }
                }

                if (count > attributeList.Count) {
                    attributeList.Add(active);
                }
            }

            

            return attributeList;
        }

        public List<IRelatedProducts> GetRelatedProducts(DbProduct ep, string typeOfRelation) {

            List<IRelatedProducts> relatedProducts = new List<IRelatedProducts>();

            using (SpHandler sph = new SpHandler("getFamilyRelatedProducts", new SqlParameter("@productID", ep.ProductID), new SqlParameter("@relationshipType", typeOfRelation))) {

                sph.ExecuteReader();

                while (sph.DataReader.Read()) {

                    IRelatedProducts rp = new RelatedProducts();

                    rp.AccessoryPartNo = sph.DataReader["itemcode"].ToString();
                    rp.AccessoryDescription = sph.DataReader["productdescription"].ToString();
                    rp.AccessoryName = sph.DataReader["productname"].ToString();
                    relatedProducts.Add(rp);
                }
            }

            return relatedProducts;
        }

        public List<IImage> GetProductImages(Domain.Product ep) {

            List<IImage> imageList = new List<IImage>();

            foreach (ProductImage epi in ep.Images) {
                IImage image = new Domain.Catalogue.Image();
                ImageHelper.CopyImage(epi, image);
                imageList.Add(image);
            }

            return imageList;
        }


        public List<IRelatedDocument> GetProductDocuments(Domain.Product ep) {

            List<IRelatedDocument> documentList = new List<IRelatedDocument>();

            foreach (ProductDocument epd in ep.Documents) {
                IRelatedDocument doc = new RelatedDocument();
                doc.Name = epd.Document.DocumentName;
                doc.Url = epd.Document.FilePath;
                doc.Type = epd.Document.Type.TypeName;
                doc.CssClass = epd.Document.Type.CssClass;
                doc.DocumentID = epd.DocumentID;
                documentList.Add(doc);
            }

            return documentList;
        }

        public List<ISku> GetProductSkus(Domain.Product ep) {

            List<ISku> skuList = new List<ISku>();

            foreach (Productsku sku in ep.Skus) {
                ISku s = new Sku();
                s.Code = sku.Sku;
                skuList.Add(s);
            }

            return skuList;
        }

        public List<ICatalogueNode> GetCategoryBreadCrumb(ICatalogueNode node) {

            List<ICatalogueNode> trail = new List<ICatalogueNode>();
            Category catNode = node as Category;

            if (catNode == null) {
                catNode = _dao.GetObjectById(typeof(Category), node.NodeID) as Category;
            }

            while (catNode != null) {
                trail.Add(catNode);
                catNode = catNode.ParentCategory;
            }

            trail.Reverse();
            return trail;
        }

        public List<ICatalogueNode> GetProductBreadCrumbTrail(long productID) {

            List<ICatalogueNode> trail = new List<ICatalogueNode>();

            IQuery pQuery = this._sessionManager.OpenSession().CreateQuery("from ProductCategory WHERE productID = :id");
            pQuery.SetInt64("id", productID);
            IList productCategories = pQuery.List();

            if (productCategories.Count > 0) {
                Category bottomCategory = _dao.GetObjectById(typeof(Category), ((ProductCategory)productCategories[0]).CategoryID) as Category;
                return GetCategoryBreadCrumb(bottomCategory);
            }

            return trail;
        }
        #endregion

        public IProductView GetProductView(int storeID, string cultureCode, long productID) {

            IProductView productView = new ProductView();

            productView.ProductDetails = GetProduct(storeID, cultureCode, productID);
            productView.BreadCrumbTrail = GetProductBreadCrumbTrail(productID);
            productView.ProductsInFamily = GetProductsInFamily(productView.ProductDetails.ProductFamily);

            return productView;
        }

        private List<Domain.Product> GetProductsInFamily(string p) {

            List<Domain.Product> pList = new List<Domain.Product>();

            if (!string.IsNullOrEmpty(p)) {

                IQuery fQuery = this._sessionManager.OpenSession().CreateQuery("from Product where ProductFamily = :family order by ItemCode");
                fQuery.SetString("family", p);
                IList iList = fQuery.List();
             
                foreach (Domain.Product product in iList) {
                    pList.Add(product);
                }
            }

            return pList;
        }

        public List<Document> GetDocuments(int storeID, string cultureCode) {
            List<Document> docList = new List<Document>();
            IQuery dQuery = this._sessionManager.OpenSession().CreateQuery(" from Document");
            IList iList = dQuery.List();
            foreach (Document d in iList) {
                docList.Add(d);
            }

            return docList;
        }

        #endregion

        public List<IActiveProductAttribute> GetCatalogueAttributes(int storeID, string cultureCode) {

            IQuery aQuery = this._sessionManager.OpenSession().CreateQuery(" from AttributeGroupAttribute");
            IList aList = aQuery.List();

            List<Domain.Catalogue.Interfaces.IActiveProductAttribute> attributes = new List<IActiveProductAttribute>();
            foreach (AttributeGroupAttribute active in aList) {

                Domain.Catalogue.ActiveProductAttribute a = new Domain.Catalogue.ActiveProductAttribute();
                a.Name = active.Attribute.AttributeReference;
                a.Group.ID = active.AttributeGroupid;
                a.Group.Name = active.AttributeGroup.AttributeGroupName;
                a.DataType = active.Attribute.AttributeType.Name;

                foreach (AttributeOptionValue aov in active.Attribute.Options) {
                    AttributeOption option = new AttributeOption();
                    option.ShortCode = aov.OptionID.ToString();
                    option.PickListValue = aov.OptionName;
                    a.AttributeOptionList.Add(option);
                }

                attributes.Add(a);

            }

            return attributes;
        }

        public List<Cuyahoga.Modules.ECommerce.Domain.Product> GetAllProducts(int storeID, string cultureCode) {
            List<Cuyahoga.Modules.ECommerce.Domain.Product> pList = new List<Cuyahoga.Modules.ECommerce.Domain.Product>();
            IQuery pQuery = this._sessionManager.OpenSession().CreateQuery("from Product");
            IList iList = pQuery.List();
            foreach (Cuyahoga.Modules.ECommerce.Domain.Product p in iList) {
                pList.Add(p);
            }
            return pList;
        }

        public Document GetDocument(int storeID, string culture, long DocumentID) {
            return _dao.GetObjectById(typeof(Document), DocumentID) as Document;
        }

        public Category GetCategoryByProductCode(long productCode) {
            IQuery pQuery = this._sessionManager.OpenSession().CreateQuery("from Product where ProductID = :code");
            pQuery.SetInt64("code", productCode);
            IList results = pQuery.List();
            if (results.Count > 0) {
                Cuyahoga.Modules.ECommerce.Domain.Product p = (Cuyahoga.Modules.ECommerce.Domain.Product)results[0];
                ProductCategory pc = (ProductCategory)p.Categories[0];
                Category cat = GetCategory(0, "", pc.CategoryID);
                return cat.ParentCategory;

            }
            return null;
        }

        public CategoryLink GetCategoryLink(long id) {
            return (CategoryLink)_dao.GetObjectById(typeof(CategoryLink), id);
        }

        public List<IProductSummary> FindProducts(string searchTerm) {
            return FindProducts(searchTerm, 0);
        }

        public List<IProductSummary> FindProducts(string searchTerm, long nodeID) {

            List<IProductSummary> pList = new List<IProductSummary>();
            searchTerm = searchTerm.Replace("'", "''");

            IQuery pQuery;

            if (nodeID > 0) {
                pQuery = this._sessionManager.OpenSession().CreateSQLQuery(
                        "select {p.*} from ECommerce_Product p"
                        + " inner join ECommerce_ProductCategory pc on p.productid = pc.productid"
                        + " inner join vwProductLineCategoryLookup cl on cl.categoryid = pc.categoryid"
                        + " where"
                        + ((!string.IsNullOrEmpty(searchTerm)) ? " (itemCode like '" + searchTerm + "%' or productName like '%" + searchTerm + "%') and" : "")
                        + " cl.productlinecategoryid = " + nodeID
                        + " order by p.productfamily, p.itemcode"
                        , "p", typeof(DbProduct));
            } else {
                pQuery = this._sessionManager.OpenSession().CreateSQLQuery(
                        "select {p.*} from ECommerce_Product p"
                        + " inner join ECommerce_ProductCategory pc on p.productid = pc.productid"
                        + " inner join vwProductLineCategoryLookup cl on cl.categoryid = pc.categoryid"
                        + " inner join ECommerce_Category c on cl.productlinecategoryid = c.categoryid"
                        + ((!string.IsNullOrEmpty(searchTerm)) ? " where (itemCode like '" + searchTerm + "%' or productName like '%" + searchTerm + "%')" : "")
                        + " order by c.sortorder, c.categoryname, p.productfamily, p.itemcode"
                        , "p", typeof(DbProduct));
            }

            pQuery.SetMaxResults(100); //some arbitrary largeish number
            IList iList = pQuery.List();

            foreach (DbProduct p in iList) {
                pList.Add(p.CreateProductSummary());
            }

            return pList;
        }
    }
}