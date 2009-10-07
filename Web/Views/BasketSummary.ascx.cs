namespace Cuyahoga.Modules.ECommerce.Web.Views {

    using System;
    using System.Collections;
    using System.Web.UI.WebControls;

    using Cuyahoga.Modules.ECommerce.Util.Interfaces;
    using Cuyahoga.Modules.ECommerce.Util;
    using log4net;

    /// <summary>
    ///		Summary description for Basket Summary.
    /// </summary>
    public class BasketSummary : BasketCommon {

        protected Literal litBasketTotal;
        protected Panel pnlDetails;
        protected Panel pnlEmpty;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                DisplayContents(CurrentBasket);
            }
        }

        private void DisplayContents(IBasket basket) {

            Visible = true;

            if (basket != null) {

                BasketDecorator decorator = basket as BasketDecorator;

                if (decorator == null) {
                    decorator = new BasketDecorator(basket);
                }

                if (!decorator.IsEmpty) {
                    RenderBasket(CurrentBasket);
                    pnlDetails.Visible = true;
                }
            }

            pnlEmpty.Visible = !pnlDetails.Visible;
        }

        protected virtual void RenderBasket(IBasket basket) {
            try {

                if (basket != null) {

                    BasketDecorator decoratedBasket = new BasketDecorator(basket);
                    litBasketTotal.Text = HtmlFormatUtils.FormatMoney(decoratedBasket.StandardItemPrice);
                    ArrayList standardItems = decoratedBasket.GetStandardItems();

                    if (standardItems.Count > 0) {

                        int itemCounter = 0;

                        for (int i = 0; i < standardItems.Count; i++) {
                            itemCounter += ((IBasketLine)standardItems[i]).Quantity;
                        }

                        ItemCount = itemCounter;
                    }
                }

            } catch (System.Threading.ThreadAbortException) {
            } catch (Exception ex) {
                LogManager.GetLogger(GetType()).Error(ex);
            }
        }

        protected int ItemCount {
            get {
                try {
                    return Convert.ToInt32(ViewState["itemCount"]);
                } catch {
                    return 0;
                }
            }
            set {
                ViewState["itemCount"] = value;
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

            WebStoreContext context = WebStoreContext.Current;
            this.Load += new System.EventHandler(this.Page_Load);

            Visible = (context.BasketID > 0 && !context.IsBasketEmpty);

            context.OnBasketChanged += new WebStoreContext.BasketChangedHandler(BasketSummary_OnBasketChanged);
            context.OnHideBasketSummary += new Cuyahoga.Modules.ECommerce.Util.WebStoreContext.BasketChangedHandler(state_OnHideBasketSummary);
        }
        #endregion

        protected virtual void BasketSummary_OnBasketChanged(object sender, IBasket basket) {
            DisplayContents(basket);
        }

        protected virtual void state_OnHideBasketSummary(object sender, IBasket basket) {
            Visible = false;
        }
    }
}