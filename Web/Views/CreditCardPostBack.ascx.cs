namespace Cuyahoga.Modules.ECommerce.Web.Views {

	using System;

	using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;
	using Cuyahoga.Modules.ECommerce.Util;
    using Cuyahoga.Modules.ECommerce.Service;
    using Cuyahoga.Modules.ECommerce.Service.Translation;
    using Cuyahoga.Modules.ECommerce.DataAccess;
    using Cuyahoga.Modules.ECommerce.Core;
    using log4net;

	/// <summary>
	///		Summary description for Basket Summary.
	/// </summary>
    public class CreditCardPostBack : LocalizedModuleConsumerControl {

		private void Page_Load(object sender, System.EventArgs e) {

			if (!IsPostBack) {

				IWebFormPaymentProvider provider = ServiceFactory.GetService(typeof(IPaymentProvider)) as IWebFormPaymentProvider;
                ICommerceDao dao = ServiceFactory.GetService(typeof(ICommerceDao)) as ICommerceDao;
                ICommerceService service = ServiceFactory.GetService(typeof(ICommerceService)) as ICommerceService;

                if (provider == null || dao == null || service == null) {
                    LogManager.GetLogger(GetType()).Warn("Unable to create services for CC Postback");
                    return;
                }

				IElectronicPayment payment = provider.ProcessAuthPaymentResponse(Request);

                BasketDecorator order = new PaymentHelper(dao).ProcessReceivedPayment(payment, this);
                if (order != null) {
                    service.CloseOrder(order.Basket, TranslatorUtils.GetTextTranslator(GetType(), order.CultureCode));
                }
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
			initComp();
		}

		private void initComp() {
            this.Load += new System.EventHandler(this.Page_Load);
        }
		#endregion
    }
}