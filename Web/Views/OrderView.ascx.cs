namespace Cuyahoga.Modules.ECommerce.Web.Views {

	using System;
	using System.Web.UI.WebControls;

	using Cuyahoga.Modules.ECommerce.Util;
	using Cuyahoga.Modules.ECommerce.Web.Controls;
    using Cuyahoga.Modules.ECommerce.Util.Interfaces;

	/// <summary>
	///	Displays the status of the last order
	/// </summary>
	public class OrderView : BasketCommon {

		protected OrderViewComposite ctlOrderViewComposite;
		protected Label lblMessage;

		private void Page_Load(object sender, System.EventArgs e) {

			if (!IsPostBack) {

				IBasket order = Commerce.GetLastOrder(WebStoreContext.Current);
				
				if (order != null) {
					ctlOrderViewComposite.Visible = true;
					ctlOrderViewComposite.BindOrder(order);
				} else {
					ctlOrderViewComposite.Visible = false;
				}
			}

			if (lblMessage != null) {
				lblMessage.Visible = !ctlOrderViewComposite.Visible;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) {
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
		private void InitializeComponent() {
            this.Load += new System.EventHandler(this.Page_Load);
        }
		#endregion

	}
}