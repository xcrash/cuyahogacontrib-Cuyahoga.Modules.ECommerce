namespace Cuyahoga.Modules.ECommerce.Web.Admin.Controls
{
    using System;
    using System.Collections;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Cuyahoga.Web.Admin.Controls;
    using Cuyahoga.Web.Admin;
    using Cuyahoga.Core.Domain;
    using Cuyahoga.Web.Admin.UI;
    using Cuyahoga.Web.UI;
    using Cuyahoga.Core.Util;
	/// <summary>
	///		Summary description for Navigation.
	/// </summary>
	public class Navigation : System.Web.UI.UserControl
	{
        private ModuleAdminBasePage _page;
        protected System.Web.UI.WebControls.HyperLink hplCatBrowser;
        protected System.Web.UI.WebControls.HyperLink hplAccountCreate;
    
        protected System.Web.UI.WebControls.HyperLink hplAccountList;
        protected System.Web.UI.WebControls.HyperLink hplAccountSearch;
        protected System.Web.UI.WebControls.HyperLink hplCategoryAdd;
        protected System.Web.UI.WebControls.HyperLink hplDocuments;
        
        protected System.Web.UI.WebControls.HyperLink hplOrders;
        protected System.Web.UI.WebControls.HyperLink hplOrderSearch;
        protected System.Web.UI.WebControls.HyperLink hplProductAdd;
        protected HyperLink hplProductImport;
        protected HyperLink hplProductExport;
        protected System.Web.UI.WebControls.HyperLink hplPromotionAdd;
        protected System.Web.UI.WebControls.HyperLink hplPromotionList;
    
		private void Page_Load(object sender, System.EventArgs e) {
          
            this._page = (ModuleAdminBasePage)this.Page;
           
           // hplAccountCreate.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/AccountCreate.aspx{0}", this._page.GetBaseQueryString());
           // hplAccountList.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/AccountList.aspx{0}", this._page.GetBaseQueryString());
           // hplAccountSearch.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/AccountSearch.aspx{0}", this._page.GetBaseQueryString());

         //   hplDocuments.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/DocumentManager.aspx{0}", this._page.GetBaseQueryString());
           
           
            //hplOrders.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/Orders.aspx{0}", this._page.GetBaseQueryString());
           // hplOrderSearch.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/OrderSearch.aspx{0}", this._page.GetBaseQueryString());
           // hplProductExport.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/CSVExport.aspx{0}", this._page.GetBaseQueryString());
           
          //  hplProductImport.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/CSVImport.aspx{0}", this._page.GetBaseQueryString());
           
            //hplProductAdd.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/ProductAdd.aspx{0}", this._page.GetBaseQueryString());
          //  hplCatBrowser.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/CatalogueBrowser.aspx{0}", this._page.GetBaseQueryString());
           // hplCategoryAdd.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/CategoryAdd.aspx{0}", this._page.GetBaseQueryString());

            //hplPromotionList.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/PromotionList.aspx{0}", this._page.GetBaseQueryString());
           // hplPromotionAdd.NavigateUrl = String.Format("~/Modules/ECommerce/Admin/PromotionAdd.aspx{0}", this._page.GetBaseQueryString());
		}   

       



		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
