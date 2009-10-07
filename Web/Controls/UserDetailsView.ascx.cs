namespace Cuyahoga.Modules.ECommerce.Web.Controls {

	using System;

    using Cuyahoga.Modules.ECommerce.Domain;
	using Cuyahoga.Modules.ECommerce.Util.Interfaces;
	using Cuyahoga.Modules.ECommerce.Service.Translation;
    using Cuyahoga.Modules.ECommerce.Util;
    using Cuyahoga.Core.Domain;

	/// <summary>
	/// </summary>
	public class UserDetailsView : TranslatedControl {

		protected IUserDetails UserDetails;
        protected UserDetail UserDetailsAlt;
		protected WebStoreContext StateInfo;

		private void Page_Load(object sender, System.EventArgs e) {
		}

		public void BindUserDetails(User user) {
			UserDetails = new UserDecorator(user);
            UserDetailsAlt = null;
		}

        public void BindUserDetails(IUserDetails user) {
            UserDetails = user;
            UserDetailsAlt = user as UserDetail;
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
		}
		#endregion
	}
}