using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
namespace Cuyahoga.Modules.ECommerce.Service
{
    public interface ICatalogueModificationService
    {
        bool SaveProduct(int storeID, Domain.Product p);
        bool SaveProductAttribute(int storeID, ProductAttributeOptionValue paov);
        bool DeleteProductAttribute(int storeID, ProductAttributeOptionValue paov);
        bool SaveProductImage(int storeID, ProductImage pi);
        bool DeleteProductImage(int storeID, ProductImage pi);
        bool SaveProductRelation(int storeID, ProductRelation pr);
        bool DeleteProductRelation(int storeID, ProductRelation pr);
        bool SaveCategory(int storeID, Category cat);
        bool DeleteCategoryLink(int storeID, CategoryLink cat);
        bool SaveCategoryLink(int storeID, CategoryLink cat);
        bool DeleteCategory(int storeID, Category cat);
        bool DeleteProductDocument(int storeID, string cultureCode, ProductDocument pd);
        bool DeleteDocument(int storeID, string cultureCode, Document d);
        bool SaveProductDocument(int storeID, string cultureCode, ProductDocument pd);
        bool SaveDocument(int storeID, string cultureCode, Document d);
        bool SaveProductSynonym(int storeID, string cultureCode, ProductSynonym ps);
        bool DeleteProductSynonym(int storeID, string cultureCode, ProductSynonym ps);
        bool MoveCategory(int storeID, Category cat); //can just use save?
        bool MoveProduct(int storeID, ProductCategory pc);
        bool SaveProductCategory(int storeID, string cultureCode, ProductCategory pc);
        bool DeleteProductCategory(int storeID, string cultureCode, ProductCategory pc);
        bool DeleteProduct(int storeID, string cultureCode, Product p);
        short GetHighestSortOrder(List<ICategory> catList);
        long GetHighestProductOrder(long nodeID);

        bool SaveObject(object o);
        bool DeleteObject(object o);
    }
}
