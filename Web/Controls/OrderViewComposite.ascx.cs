namespace Cuyahoga.Modules.ECommerce.Web.Controls {

	using System;
	using System.Web.UI.WebControls;

	using Cuyahoga.Modules.ECommerce.Util.Interfaces;
	using Cuyahoga.Modules.ECommerce.Util;
	using Cuyahoga.Modules.ECommerce.Service.Translation;

	/// <summary>
	///		Summary description for AddressWuc.
	/// </summary>
	public class OrderViewComposite : TranslatedControl {

		protected AddressView ctlAddressView;
		protected OrderView ctlOrderView;
		protected UserDetailsView ctlUserView;

		private void Page_Load(object sender, System.EventArgs e) {
		}

		public virtual void BindOrder(IBasket order) {

			if (ctlUserView != null) {
                if (order.UserDetails != null) {
                    ctlUserView.BindUserDetails(order.UserDetails);
                } else {
                    ctlUserView.BindUserDetails(order.AltUserDetails);
                }
			}

			if (ctlAddressView != null) {
                
                bool showAddress = false;
                
                if (order.OrderHeader.InvoiceAddress != null) {
					ctlAddressView.BindInvoiceAddress(order.OrderHeader.InvoiceAddress);
                    showAddress = true;
				}
				if (order.OrderHeader.DeliveryAddress != null) {
					ctlAddressView.BindDeliveryAddress(order.OrderHeader.DeliveryAddress);
                    showAddress = true;
				}

                ctlAddressView.Visible = showAddress;
			}

			if (ctlOrderView != null) {
				ctlOrderView.BindOrder(order);
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