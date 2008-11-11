namespace Cuyahoga.Modules.ECommerce.Web.Controls {

	using System;
	using System.Web.UI.WebControls;

	using Cuyahoga.Modules.ECommerce.Util.Interfaces;
	using Cuyahoga.Modules.ECommerce.Util;
	using Cuyahoga.Modules.ECommerce.Service.Translation;

    using Cuyahoga.Modules.ECommerce.Core;

	/// <summary>
	///		Summary description for AddressWuc.
	/// </summary>
	public class OrderView : LocalizedModuleConsumerControl {

		protected BasketDecorator Order;
		protected IOrderHeader Header;
		protected WebStoreContext StateInfo;
		protected Repeater rptBasketLines;

		private void Page_Load(object sender, System.EventArgs e) {
		}

		public void BindOrder(IBasket order) {

            ECommerceModule mod = Module as ECommerceModule;

			Order = new BasketDecorator(order);
			Header = order.OrderHeader;

			rptBasketLines.DataSource = Order.GetStandardItems();
			rptBasketLines.DataBind();
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

			StateInfo = WebStoreContext.Current;

			//If you can see this, you can't see the summary
			StateInfo.NotifyHideBasketSummary(null);
		}
		#endregion
	}
}