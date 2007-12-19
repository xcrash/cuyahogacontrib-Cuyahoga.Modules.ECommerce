using System;
using System.Collections;
using System.Collections.Generic;

using Castle.Facilities.NHibernateIntegration;

using Cuyahoga.Core;
using Cuyahoga.Modules.ECommerce.DataAccess;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Castle.Services.Transaction;
using NHibernate;
using Cuyahoga.Modules.ECommerce.Service;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Service {
    [Transactional]
    public class CatalogueModificationService : ICatalogueModificationService, INHibernateModule {

        private ISessionManager _sessionManager;
        private IExtCommonDao _dao;

        public CatalogueModificationService(ISessionManager sessionManager, IExtCommonDao dao) {
            _sessionManager = sessionManager;
            _dao = dao;
        }

        #region ICatalogueModificationService Members

        public bool SaveProduct(int storeID, Domain.Product p) {
            try {

                this._sessionManager.OpenSession().SaveOrUpdate(p);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool SaveProductAttribute(int storeID, ProductAttributeOptionValue paov) {
            try {

                this._sessionManager.OpenSession().Save(paov);
                this._sessionManager.OpenSession().Flush();

            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;

        }

        public bool DeleteProductAttribute(int storeID, ProductAttributeOptionValue paov) {

            try {
                this._sessionManager.OpenSession().Delete(paov);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool SaveProductImage(int storeID, ProductImage pi) {

            try {
                this._sessionManager.OpenSession().SaveOrUpdate(pi);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool DeleteProductImage(int storeID, ProductImage pi) {
            try {
                this._sessionManager.OpenSession().Delete(pi);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool SaveProductRelation(int storeID, ProductRelation pr) {
            try {
                this._sessionManager.OpenSession().Save(pr);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool DeleteProductRelation(int storeID, ProductRelation pr) {
            try {
                this._sessionManager.OpenSession().Delete(pr);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool SaveCategory(int storeID, Category cat) {
            try {
                this._sessionManager.OpenSession().SaveOrUpdateCopy(cat);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool DeleteCategory(int storeID, Category cat) {
            try {
                this._sessionManager.OpenSession().Delete(cat);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool SaveCategoryLink(int storeID, CategoryLink cat) {
            try {
                this._sessionManager.OpenSession().SaveOrUpdateCopy(cat);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool DeleteCategoryLink(int storeID, CategoryLink cat) {
            try {
                this._sessionManager.OpenSession().Delete(cat);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool SaveObject(object o) {
            try {
                this._sessionManager.OpenSession().SaveOrUpdateCopy(o);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool DeleteObject(object o) {
            try {
                this._sessionManager.OpenSession().Delete(o);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool MoveCategory(int storeID, Category cat) {
            try {
                this._sessionManager.OpenSession().SaveOrUpdateCopy(cat);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool MoveProduct(int storeID, ProductCategory pc) {
            try {
                this._sessionManager.OpenSession().SaveOrUpdateCopy(pc);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool DeleteProductDocument(int storeID, string cultureCode, ProductDocument pd) {
            try {
                this._sessionManager.OpenSession().Delete(pd);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }
        public bool SaveProductDocument(int storeID, string cultureCode, ProductDocument pd) {
            try {

                this._sessionManager.OpenSession().SaveOrUpdateCopy(pd);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool SaveDocument(int storeID, string cultureCode, Document d) {

            try {

                this._sessionManager.OpenSession().SaveOrUpdate(d);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool DeleteDocument(int storeID, string cultureCode, Document d) {
            try {
                this._sessionManager.OpenSession().Delete(d);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool SaveProductSynonym(int storeID, string cultureCode, ProductSynonym ps) {
            try {
                this._sessionManager.OpenSession().SaveOrUpdateCopy(ps);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool DeleteProductSynonym(int storeID, string cultureCode, ProductSynonym ps) {
            try {
                this._sessionManager.OpenSession().Delete(ps);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public short GetHighestSortOrder(List<ICatalogueNode> catList) {

            short highest = 0;

            try {
                foreach (ICatalogueNode cat in catList) {
                    if (cat.SortOrder > highest) {
                        highest = cat.SortOrder;
                    }
                }

            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
            }
            return highest;
        }

        public long GetHighestProductOrder(long nodeID) {

            long highest = 0;

            try {

                IQuery query = this._sessionManager.OpenSession().CreateQuery("from ProductCategory where CategoryID = :id order By SortOrder Desc");
                query.SetInt64("id", nodeID);
                query.SetFirstResult(0);
                query.SetMaxResults(1);
                IList pcList = query.List();

                foreach (ProductCategory pc in pcList) {
                    highest = pc.SortOrder;
                }

            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
            }

            return highest;
        }

        public bool SaveProductCategory(int storeID, string cultureCode, ProductCategory pc) {
            try {
                this._sessionManager.OpenSession().SaveOrUpdateCopy(pc);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }

        public bool DeleteProductCategory(int storeID, string cultureCode, ProductCategory pc) {
            try {
                this._sessionManager.OpenSession().Delete(pc);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }
        /* FIXME */
        public bool DeleteProduct(int storeID, string cultureCode, Cuyahoga.Modules.ECommerce.Domain.Product p) {
            try {
                this._sessionManager.OpenSession().Delete(p);
                this._sessionManager.OpenSession().Flush();
            } catch (Exception e) {
                LogManager.GetLogger(GetType()).Error(e);
                return false;
            }

            return true;
        }
        #endregion
    }
}