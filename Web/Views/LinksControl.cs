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

namespace Cuyahoga.Modules.ECommerce.Web.Views {
   public class LinksControl : GenericModule, INHibernateModule{
         public const string PARAM_PRODUCT_ID = "pid";
        public const string PARAM_PRODUCT_SKU = "sku";
        public const string PARAM_NODE_ID = "node";
        public const string PARAM_CATEGORY_ID = "cat";

        private long _nodeID;
        private string _displayMode = string.Empty;

        private ICatalogueViewService _catalogueService;

        public LinksControl(ICatalogueViewService catatalogueService) {
            _catalogueService = catatalogueService;

            ParsePathInfo();
        }

        public override void ReadSectionSettings() {
            base.ReadSectionSettings();
            if (base.Section.Settings["DISPLAY_MODE"] != null) {
                _displayMode = base.Section.Settings["DISPLAY_MODE"].ToString();
            }
        }//test

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
                    NodeID = Int32.Parse(RequestParameters[PARAM_NODE_ID]);
                } catch { }
            }
        }

        public ICatalogueViewService CatalogueViewer {
            get {
                return _catalogueService;
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
    }
}
