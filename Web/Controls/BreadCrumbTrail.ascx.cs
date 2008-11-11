namespace Cuyahoga.Modules.ECommerce.Web.Controls {

    using System.Collections.Generic;
	using Cuyahoga.Modules.ECommerce.Core;
    using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

	/// <summary>
	///		Summary description for BreadCrumbTrail.
	/// </summary>
    public class BreadCrumbTrail : LocalizedModuleConsumerControl {

		protected System.Web.UI.WebControls.Repeater rptBreadCrumb;
		protected CatalogueUrlHelper UrlHelper;

        public void RenderBreadCrumbTrail(List<ITrailItem> nodeList) {
            UrlHelper = new CatalogueUrlHelper(Module as ECommerceModule);
            rptBreadCrumb.DataSource = nodeList;
            rptBreadCrumb.DataBind();
        }
	}
}