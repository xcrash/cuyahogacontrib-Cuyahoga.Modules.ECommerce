namespace Cuyahoga.Modules.ECommerce.Web.Views {

	using System;
	using System.Collections;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	using Cuyahoga.Modules.ECommerce.Util.Enums;
	using Cuyahoga.Modules.ECommerce.Util.Interfaces;
	using Cuyahoga.Modules.ECommerce.Util;
	using log4net;

	/// <summary>
	///		Summary description for Basket Summary.
	/// </summary>
	public class BasketView : BasketCommon {

        protected WebStoreContext StateInfo;

        protected Repeater rBasket;
        protected LinkButton btnEmpty;

        protected PlaceHolder phMessage;
        protected PlaceHolder phBasket;
        public Literal litStatusMessage;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                RefreshVisibility();
                RenderBasket(CurrentBasket);
            }
        }

        private void RenderBasket(BasketDecorator basket) {

            if (basket != null && phBasket.Visible) {

                //If you can see this, you can't see the summary
                StateInfo.NotifyHideBasketSummary(null);

                try {

                    ArrayList lines = basket.GetStandardItems();
                    rBasket.DataSource = lines;
                    rBasket.DataBind();

                    //Now put in the quantities and translated alt text on the buttons
                    if (rBasket.Items.Count == lines.Count) {
                        for (int i = 0; i < lines.Count; i++) {
                            TextBox txtQuantity = (TextBox)rBasket.Items[i].FindControl("txtQuantity");
                            if (txtQuantity != null) {
                                txtQuantity.Text = ((IBasketLine)lines[i]).Quantity.ToString();
                            }

                            DecorateUpdateButtons(i);
                        }
                    }
                } catch (Exception ex) {
                    LogManager.GetLogger(GetType()).Error(ex);
                }
            }
        }

        protected virtual void DecorateUpdateButtons(int i) {

            ImageButton btnUpdateQuantity = (ImageButton)rBasket.Items[i].FindControl("btnUpdateQuantity");
            btnUpdateQuantity.AlternateText = GetText("update quantity");

            ImageButton btnRemoveItem = (ImageButton)rBasket.Items[i].FindControl("btnRemoveItem");
            btnRemoveItem.AlternateText = GetText("remove item");
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

            StateInfo = WebStoreContext.Current;
            StateInfo.OnBasketChanged += new WebStoreContext.BasketChangedHandler(BasketSummary_OnBasketChanged);

            btnEmpty.Text = GetText("empty");
            btnEmpty.Click += new EventHandler(btnEmpty_Click);
        }
        #endregion

        private void BasketSummary_OnBasketChanged(object sender, IBasket basket) {
            RenderBasket(new BasketDecorator(basket));
        }


        protected void btnUpdateQuantity_Click(object sender, ImageClickEventArgs e) {
            try {
                if (CurrentBasket != null) {

                    RepeaterItem item = (RepeaterItem)((System.Web.UI.Control)sender).Parent;
                    long lineID = GetSelectedBaketLineID(item);

                    if (lineID > 0) {

                        TextBox txtQuantity = (TextBox)item.FindControl("txtQuantity");
                        int quantity = Int32.Parse(txtQuantity.Text);

                        IStoreContext context = WebStoreContext.Current;
                        Commerce.AmendItemQuantity(context, lineID, quantity);
                        Commerce.RefreshBasket(context);
                    }
                }
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private long GetSelectedBaketLineID(RepeaterItem item) {

            TextBox box = item.FindControl("txtLineID") as TextBox;

            if (box != null) {
                try {
                    return Int64.Parse(box.Text);
                } catch { }
            }

            return 0;
        }

        protected void btnRemoveItem_Click(object sender, ImageClickEventArgs e) {
            try {
                if (CurrentBasket != null) {

                    RepeaterItem item = (RepeaterItem)((System.Web.UI.Control)sender).Parent;
                    long lineID = GetSelectedBaketLineID(item);

                    if (lineID > 0) {
                        IStoreContext context = WebStoreContext.Current;
                        Commerce.RemoveItem(context, lineID);
                        Commerce.RefreshBasket(context);
                    }
                }
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void btnEmpty_Click(object sender, EventArgs e) {
            if (CurrentBasket != null) {
                IStoreContext context = WebStoreContext.Current;
                Commerce.EmptyBasket(context);
                Commerce.RefreshBasket(context);
            }
        }

        protected void RefreshVisibility() {
            phMessage.Visible = IsEmpty;
            phBasket.Visible = !phMessage.Visible;
        }

        protected bool IsEmpty {
            get {
                return (CurrentBasket == null || CurrentBasket.IsEmpty);
            }
        }
    }
}