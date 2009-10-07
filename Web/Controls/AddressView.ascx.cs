namespace Cuyahoga.Modules.ECommerce.Web.Controls {

	using System;
	using System.Web.UI.WebControls;

	using Cuyahoga.Modules.ECommerce.Util.Interfaces;
	using Cuyahoga.Modules.ECommerce.Util;
	using Cuyahoga.Modules.ECommerce.Service.Translation;
	using Cuyahoga.Modules.ECommerce.Util.Location;

	/// <summary>
	///		Summary description for AddressWuc.
	/// </summary>
	public class AddressView : TranslatedControl {

		private IAddressFormatter _addressFormatter;

		protected Literal litInvoiceAddress;
		protected Literal litDeliveryAddress;

        public void BindInvoiceAddress(IAddress address) {

            string addressText = _addressFormatter.FormatAddress(address);
            litInvoiceAddress.Text = (!string.IsNullOrEmpty(addressText)) ? "<table>" + addressText + "</table>" : "";

            if (litDeliveryAddress.Text.Length == 0) {
                litDeliveryAddress.Text = litInvoiceAddress.Text;
            }
        }

        public  void BindDeliveryAddress(IAddress address) {
            string addressText = _addressFormatter.FormatAddress(address);
            litDeliveryAddress.Text = (!string.IsNullOrEmpty(addressText)) ? "<table>" + addressText + "</table>" : "";
        }

		private void Page_Load(object sender, System.EventArgs e) {
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

			WebStoreContext context = WebStoreContext.Current;
			_addressFormatter = new AddressFormatter("<tr><td>", "</td></tr>");

		}
		#endregion
	}
}