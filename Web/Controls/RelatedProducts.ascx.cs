
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


    public partial class RelatedProducts : LocalizedModuleConsumerControl {
       
        protected System.Web.UI.WebControls.Repeater rptCrossSell;
        protected System.Web.UI.WebControls.Repeater rptUpSell;

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

        protected void Page_Load(object sender, EventArgs e) {
            
        }

        public void RenderRelatedProducts(IProduct product) {
            UrlHelper = new CatalogueUrlHelper(CatMod);
            if (product != null && product.CrossSellList != null) {
               
                if (product.CrossSellList.Count > 0) {
                    rptCrossSell.DataSource = product.CrossSellList;
                    rptCrossSell.DataBind();
                }
            }
            if (product != null && product.UpSellList != null) {
                if (product.UpSellList.Count > 0) {
                    rptUpSell.DataSource = product.UpSellList;
                    rptUpSell.DataBind();
                }
            }
        }

        public string GetProductID(string itemCode) {

            return "625";
            //catModule.CatalogueViewer.GetECommerceProductByItemCode(catModule.Section.Node.Site.Id, catModule.Section.Node.Culture, itemCode).ProductID.ToString();
        }
    }
}