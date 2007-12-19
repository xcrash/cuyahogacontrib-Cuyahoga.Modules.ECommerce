
using System.Collections.Generic;
using Cuyahoga.Modules.ECommerce.Core;
using Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces;

namespace Cuyahoga.Modules.ECommerce.Web.Admin.Controls {

    public class BreadCrumb : LocalizedModuleConsumerControl {

        protected System.Web.UI.WebControls.Repeater rptBreadCrumb;
        protected CatalogueUrlHelper UrlHelper;

        private bool _setLinkOnCurrentNode = true;
        private string _nodeSeperator = " &#187;&nbsp;";
        private string _rootNodeName = "Top";
        protected int nodeCount;

        public bool SetLinkOnCurrentNode {
            get { return _setLinkOnCurrentNode; }
            set { _setLinkOnCurrentNode = value; }
        }

        public string NodeSeperator {
            get { return _nodeSeperator; }
            set { _nodeSeperator = value; }
        }

        public string RootNodeName {
            get { return _rootNodeName; }
            set { _rootNodeName = value; }
        }

        public void RenderBreadCrumbTrail(List<ICatalogueNode> nodeList) {
            UrlHelper = new CatalogueUrlHelper(Module as ECommerceModule);
            nodeCount = nodeList.Count;
            rptBreadCrumb.DataSource = nodeList;
            rptBreadCrumb.DataBind();
        }
    }
}