
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
       
        public List<IRelatedProducts> crossSellList;
        public List<IRelatedProducts> upSellList;

        protected System.Web.UI.WebControls.Repeater rptCrossSell;
        protected System.Web.UI.WebControls.Repeater rptUpSell;

        protected CatalogueUrlHelper UrlHelper;

        protected void Page_Load(object sender, EventArgs e) {
            if (crossSellList != null) {
                if (crossSellList.Count > 0) {
                    rptCrossSell.DataSource = crossSellList;
                    rptCrossSell.DataBind();
                }
            }
            if (upSellList != null) {
                if (upSellList.Count > 0) {

                    rptUpSell.DataSource = upSellList;
                    rptUpSell.DataBind();
                }
            }
        }

        public string GetProductID(string itemCode) {

            return "625";//catModule.CatalogueViewer.GetECommerceProductByItemCode(catModule.Section.Node.Site.Id, catModule.Section.Node.Culture, itemCode).ProductID.ToString();
        }

    }
}