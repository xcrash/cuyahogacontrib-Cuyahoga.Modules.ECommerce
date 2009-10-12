using System;
using System.Collections;

using Cuyahoga.Core;
using Cuyahoga.Core.Search;
using Cuyahoga.Modules.ECommerce.Domain;
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Modules.ECommerce.Service;
using Cuyahoga.Modules.ECommerce.Service.Email;
using Cuyahoga.Core.Service;
using Cuyahoga.Core.Service.Email;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util.Enums;
using Cuyahoga.Core.Communication;
using Cuyahoga.Core.Service.Membership;

namespace Cuyahoga.Modules.ECommerce {

    /// <summary>
    /// Summary description for CatalogueViewModule.
    /// </summary>
    public class CatalogueViewModule : GenericModule, INHibernateModule {//, ISearchable {

        public const string PARAM_PRODUCT_ID = "pid";
        public const string PARAM_ITEM_CODE = "itemcode";
        public const string PARAM_PRODUCT_SKU = "sku";
        public const string PARAM_NODE_ID = "node";
        public const string PARAM_CATEGORY_ID = "cat";
        public const string PARAM_KIT_ID = "kit";
        public const string PARAM_IMAGE_ID = "imageID";
        public const string PARAM_MARKET = "market";
        public const string PARAM_LANGUAGE = "language";

        private long _imageID;
        private long _nodeID;
        private long _productID;
        private int _catID;
        private string _sku = "";
        private string _itemCode = "";
        private string _displayMode = string.Empty;
        private ICatalogueViewService _catalogueService;
        private ICommerceService _commerceService;
        private IAccountService _accountService;
        private ICatalogueModificationService _editService;
        private IOrderService _orderService;
        private IEmailSender _emailSender;
        private IDeliveryService _deliveryService;
        private ICultureService _cultureService;
        private IUserService _userService;

        public CatalogueViewModule(ICatalogueViewService catatalogueService, ICommerceService commerceService, ICatalogueModificationService editService, IAccountService accountService, IOrderService orderService, IEmailSender emailSender, IDeliveryService deliveryService, ICultureService cultureService, IUserService userService) {
            _catalogueService = catatalogueService;
            _commerceService = commerceService;
            _editService = editService;
            _accountService = accountService;
            _orderService = orderService;
            _emailSender = emailSender;
            _deliveryService = deliveryService;
            _cultureService = cultureService;
            _userService = userService;
            ParsePathInfo();
        }

        public override void ReadSectionSettings() {
            base.ReadSectionSettings();
            if (base.Section.Settings["DISPLAY_MODE"] != null) {
                _displayMode = base.Section.Settings["DISPLAY_MODE"].ToString();
            }
        }

        public override string CurrentViewControlPath {
            get {
                if (!string.IsNullOrEmpty(_displayMode) && base.CurrentViewControlPath == DefaultViewControlPath) {
                    return GetViewBase() + _displayMode + ".ascx";
                } else {
                    return base.CurrentViewControlPath;
                }
            }
        }

        protected override void ParsePathInfo() {

            base.ParsePathInfo();

            if (RequestParameters != null && RequestParameters.Count > 0) {
                try {
                    NodeID = Int64.Parse(RequestParameters[PARAM_NODE_ID]);
                } catch { }

                try {
                    ImageID = Int32.Parse(RequestParameters[PARAM_IMAGE_ID]);
                } catch { }

                try {
                    ProductID = Int32.Parse(RequestParameters[PARAM_PRODUCT_ID]);
                } catch { }

                try {
                    CatID = Int32.Parse(RequestParameters[PARAM_CATEGORY_ID]);
                } catch { }

                try {
                    ItemCode = RequestParameters[PARAM_ITEM_CODE];
                } catch { }

                try {
                    Sku = RequestParameters[PARAM_PRODUCT_SKU];
                } catch { }
            } 
        }

       

        public ICatalogueViewService CatalogueViewer {
            get {
                return _catalogueService;
            }
        }

        public ICommerceService CommerceService {
            get {
                return _commerceService;
            }
        }

        public ICatalogueModificationService EditService
        {
            get
            {
                return _editService;
            }
        }

        public IEmailSender EmailService {
            get {
                return _emailSender;
            }
        }

        public IDeliveryService DeliveryService {
            get {
                return _deliveryService;
            }
        }


        public IAccountService AccountService {
            get {
                return _accountService;
            }
        }

        public IOrderService OrderService {
            get {
                return _orderService;
            }
        }


        public ICultureService CultureService {
            get {
                return _cultureService;
            }
        }

        public IUserService UserService {
            get {
                return _userService;
            }
        }

        public long NodeID {
            get {
                return _nodeID;
            }
            set {
                _nodeID = value;
            }
        }


        public long ImageID {
            get {
                return _imageID;
            }
            set {
                _imageID = value;
            }
        }

        public int CatID {
            get {
                return _catID;
            }
            set {
                _catID = value;
            }
        }

        public long ProductID {
            get {
                return _productID;
            }
            set {
                _productID = value;
            }
        }

        public string ItemCode {
            get {
                return _itemCode;
            }
            set {
                _itemCode = value;
            }
        }

        public string Sku {
            get {
                return _sku;
            }
            set {
                _sku = value;
            }
        }

        public string CatalogueUrlRoot {
            get {
                return "/"; // ermmm what?
            }
        }

        public override string CacheKey {
            get {
                return ViewName + "__" + NodeID + "__" + ProductID + "__" + Sku;
            }
        }

        #region ISearchable Members
        public SearchContent[] GetAllSearchableContent() {

            int storeID = Section.Node.Site.Id;
            string cultureCode = Section.Node.Culture;
            int maxDepth = 5;

            ArrayList searchableContent = new ArrayList();
            CatalogueUrlHelper helper = new CatalogueUrlHelper(this);
            AddRecursiveContent(0, maxDepth, CatalogueViewer.GetRootCategoryView(storeID, cultureCode), searchableContent, helper, storeID, cultureCode);
            return (SearchContent[])searchableContent.ToArray(typeof(SearchContent));
        }

        private void AddRecursiveContent(int currentLevel, int maxLevel, ICategoryView node, ArrayList searchableContent, CatalogueUrlHelper helper, int storeID, string cultureCode) {
            if (this.CurrentViewControlPath == "Modules/ECommerce/Views/Navigation.ascx") return;
     
            if (node == null || currentLevel > maxLevel) return;

            searchableContent.Add(CreateSearchContent("Node", node.CurrentNode, helper));

            

            foreach (IProductSummary product in node.ProductList) {
             
                searchableContent.Add(CreateSearchContentName(node.CurrentNode.Name, product, helper));

                foreach(ProductSynonym ps in product.SynonymList){
                 searchableContent.Add(CreateSearchContentSynonym(node.CurrentNode.Name, product, ps, helper));
                
                }
            }

            foreach (ICategory childNode in node.ChildNodes) {
                try {
                    AddRecursiveContent(currentLevel + 1, maxLevel, CatalogueViewer.GetCategoryView(storeID, cultureCode, childNode.NodeID), searchableContent, helper, storeID, cultureCode);
                   
                } catch (Exception e) {
                    log4net.LogManager.GetLogger(GetType()).Error(e);
                }
            }
        }

        public event Cuyahoga.Core.Search.IndexEventHandler ContentCreated;

        protected void OnContentCreated(IndexEventArgs e) {
            if (ContentCreated != null) {
                ContentCreated(this, e);
            }
        }

        public event Cuyahoga.Core.Search.IndexEventHandler ContentDeleted;

        protected void OnContentDeleted(IndexEventArgs e) {
            if (ContentDeleted != null) {
                ContentDeleted(this, e);
            }
        }

        public event Cuyahoga.Core.Search.IndexEventHandler ContentUpdated;

        protected void OnContentUpdated(IndexEventArgs e) {
            if (ContentUpdated != null) {
                ContentUpdated(this, e);
            }
        }


        private SearchContent CreateSearchContentSynonym(string category, IProductSummary product, ProductSynonym ps, CatalogueUrlHelper urlHelper) {
            
            SearchContent sc = new SearchContent();
            sc.Title = product.ItemCode + "-" + product.Name;
           
            sc.Contents = ps.AlternativePhrase; 
        
            sc.Author = "simon";
            sc.ModuleType = this.Section.ModuleType.Name;
            sc.Path = urlHelper.GetProductUrl(product);
            sc.Category = category;
            sc.Site = (this.Section.Node != null ? this.Section.Node.Site.Name : String.Empty);
            sc.DateCreated = DateTime.Now;
            sc.DateModified = DateTime.Now;
            sc.SectionId = this.Section.Id;

            if (sc.Title == null || sc.Title.Length == 0) {
                sc.Title = product.Name;
            }

            if (sc.Contents == null || sc.Contents.Length == 0) {
                sc.Contents = "Item " + (sc.Title);
            }

            sc.Summary = sc.Contents;
            
            return sc;
        }


        private SearchContent CreateSearchContent(string category, IProductSummary product, CatalogueUrlHelper urlHelper) {

            SearchContent sc = new SearchContent();
            sc.Title = product.Name;

            sc.Contents = product.Name; 
        
            sc.Author = "simon";
            sc.ModuleType = this.Section.ModuleType.Name;
            sc.Path = urlHelper.GetProductUrl(product);
            sc.Category = category;
            sc.Site = (this.Section.Node != null ? this.Section.Node.Site.Name : String.Empty);
            sc.DateCreated = DateTime.Now;
            sc.DateModified = DateTime.Now;
            sc.SectionId = this.Section.Id;

            if (sc.Title == null || sc.Title.Length == 0) {
                sc.Title = product.Name;
            }

            if (sc.Contents == null || sc.Contents.Length == 0) {
                sc.Contents = "Item " + (sc.Title);
            }

            sc.Summary = sc.Contents;
            
            return sc;
        }


        private SearchContent CreateSearchContentName(string category, IProductSummary product, CatalogueUrlHelper urlHelper) {

            SearchContent sc = new SearchContent();
            sc.Title = product.Name;

            sc.Contents = product.Name;

            sc.Author = "simon";
            sc.ModuleType = this.Section.ModuleType.Name;
            sc.Path = urlHelper.GetProductUrl(product);
            sc.Category = category;
            sc.Site = (this.Section.Node != null ? this.Section.Node.Site.Name : String.Empty);
            sc.DateCreated = DateTime.Now;
            sc.DateModified = DateTime.Now;
            sc.SectionId = this.Section.Id;

            if (sc.Title == null || sc.Title.Length == 0) {
                sc.Title = product.Name;
            }

            if (sc.Contents == null || sc.Contents.Length == 0) {
                sc.Contents = "Item " + (sc.Title);
            }

            sc.Summary = sc.Contents;

            return sc;
        }


        private SearchContent CreateSearchContent(string category, ICategory node, CatalogueUrlHelper urlHelper) {

            SearchContent sc = new SearchContent();
            sc.Title = node.Name;

            sc.Contents = node.Description;
            sc.Author = "simon";
            sc.ModuleType = this.Section.ModuleType.Name;
            sc.Path = urlHelper.GetCatalogueNodeUrl(node);
            sc.Category = category;
            sc.Site = (this.Section.Node != null ? this.Section.Node.Site.Name : String.Empty);
            sc.DateCreated = DateTime.Now;
            sc.DateModified = DateTime.Now;
            sc.SectionId = this.Section.Id;

            if (sc.Title == null || sc.Title.Length == 0) {
                sc.Title = "NODE " + node.NodeID;
            }

            if (sc.Contents == null || sc.Contents.Length == 0) {
                sc.Contents = "Desc " + (sc.Title);
            }

            sc.Summary = sc.Contents;

            return sc;
        }
        #endregion
    }
}
