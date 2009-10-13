using System;
using System.Collections.Generic;
using Cuyahoga.Modules.ECommerce.Util.Enums;
using log4net;
using Cuyahoga.Web.Controls;
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;
using Cuyahoga.Web.UI;
using System.Web.UI.WebControls;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;


namespace Cuyahoga.Modules.ECommerce.Web.Controls {

    public partial class RelatedDocuments : LocalizedModuleConsumerControl {

        protected CatalogueUrlHelper UrlHelper;

        private CatalogueViewModule _mod;
        private CatalogueViewModule CatMod {
            get {
                if (_mod == null) {
                    _mod = Module as CatalogueViewModule;
                }
                return _mod;
            }
        }

        protected System.Web.UI.WebControls.Repeater rptDocuments;

        protected void Page_Load(object sender, EventArgs e) {
        }

        public void RenderDocuments(IProduct product) {
            UrlHelper = new CatalogueUrlHelper(CatMod);
            if (product != null && product.DocumentList != null) {
                if (product.DocumentList.Count > 0) {
                    rptDocuments.DataSource = product.DocumentList;
                    rptDocuments.DataBind();
                }
            }
        }
    }
}