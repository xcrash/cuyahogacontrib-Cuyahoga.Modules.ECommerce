namespace Cuyahoga.Modules.ECommerce.Web.Views {

    using System;
    using System.Collections;
    using System.Globalization;
    using System.Web.UI.WebControls;
    using System.Collections.Specialized;
    using System.Web;

    using Cuyahoga.Modules.ECommerce.Util.Interfaces;
    using Cuyahoga.Modules.ECommerce.Util;
    using log4net;
    using Cuyahoga.Modules.ECommerce.Core;
    using Cuyahoga.Modules.ECommerce.Service;

    /// <summary>
    ///		Summary description for Basket Summary.
    /// </summary>
    public class OrderList : GenericModuleControl {

        public const string QS_BASKET_ID = "bid";

        public const string DATE_ANY = "-1";
        public const int MAX_DAYS_HISTORY = 365;
        public const int HISTORY_INCREMENT = 7;

        private ICommerceService _service = null;
        private ECommerceModule _emod = null;

        private IFormatProvider _dateFormatter = null;
        private string _dateFormat = null;

        protected WebStoreContext StateInfo;
        protected PlaceHolder phOrderView;
        protected PlaceHolder phOrderList;
        protected Repeater rptOrderList;
        protected Controls.OrderViewComposite ctlOrderViewComposite;

        protected DropDownList ddlFromDate;
        protected DropDownList ddlToDate;
        protected Button btnSearch;
        protected Button btnBack;

        protected ICommerceService Commerce {
            get {
                return _service;
            }
        }

        protected ECommerceModule EModule {
            get {
                if (_emod == null) {
                    _emod = Module as ECommerceModule;
                    _service = _emod.CommerceService;
                }
                return _emod;
            }
        }

        protected long BasketID {
            get {
                try {
                    return Int64.Parse(EModule.RequestParameters[QS_BASKET_ID]);
                } catch {
                    return 0;
                }
            }
        }

        private void Page_Load(object sender, System.EventArgs e) {

            if (HttpContext.Current.User == null) {
                Response.Redirect(ResolveUrl("~/Login.aspx"));
            }

            if (!IsPostBack) {
                if (ddlFromDate != null) PopulateHistoryDdl(ddlFromDate);
                if (ddlToDate != null) PopulateHistoryDdl(ddlToDate);
                if (btnSearch != null) btnSearch.Text = GetText("search");
                if (btnBack != null) btnBack.Text = GetText("search_again");
            }

            if (BasketID > 0) {
                PopulateOrderDetails();
            } else {
                RenderOrderList();
            }
        }

        protected virtual void PopulateOrderDetails() {

            IBasket basket = EModule.CommerceDao.FindBasket(BasketID);

            if (basket != null && basket.UserDetails.Id == WebStoreContext.Current.CurrentUser.Id) {

                phOrderView.Visible = true;
                phOrderList.Visible = !phOrderView.Visible;

                ctlOrderViewComposite.BindOrder(basket);

            } else {
                RenderOrderList();
            }
        }

        protected virtual string GetOrderUrl(RepeaterItem container) {
            try {

                IBasket order = container.DataItem as IBasket;

                if (order != null) {
                    NameValueCollection nv = new NameValueCollection();
                    nv.Add(QS_BASKET_ID, order.BasketID.ToString());
                    return EModule.GetUrl(nv);
                }
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
            return "";
        }

        private void RenderOrderList() {

            phOrderList.Visible = true;
            phOrderView.Visible = !phOrderList.Visible;

            DateTime now = DateTime.Now;
            DateTime fromDate = now.AddDays(-Int32.Parse(ddlFromDate.SelectedValue));
            DateTime toDate = now.AddDays(-Int32.Parse(ddlToDate.SelectedValue));

            //What happens if they select ANY for one or the other?
            if (ddlFromDate.SelectedValue == DATE_ANY && ddlToDate.SelectedValue == DATE_ANY) {
                fromDate = DateTime.Now.AddYears(-100);
                toDate = now;
            }

            //Make sure from < to
            if (fromDate > toDate) {
                DateTime tmp = toDate;
                toDate = fromDate;
                fromDate = tmp;
            }

            //Search up to very end of to date
            toDate = toDate.AddDays(1);

            IList data = EModule.CommerceDao.FindOrderByDateRange(StateInfo.StoreID, HttpContext.Current.User.Identity.Name, fromDate, toDate);
            IList orderList = new ArrayList();

            //Add lots of extra data
            foreach (IBasket basket in data) {
                orderList.Add(new BasketDecorator(basket));
            }

            rptOrderList.DataSource = orderList;
            rptOrderList.DataBind();

            if (data.Count > 0) {
            }
        }

        protected virtual void PopulateHistoryDdl(ListControl control) {

            control.Items.Add(new ListItem("--- " + GetText("please select") + " ---", DATE_ANY));

            DateTime date = DateTime.Now;
            string dateFormat = GetDateFormat();
            IFormatProvider formatter = GetDateFormatter();

            for (int i = 0; i <= MAX_DAYS_HISTORY; i += HISTORY_INCREMENT) {
                control.Items.Add(new ListItem(date.AddDays(-i).ToString(dateFormat, formatter), i.ToString()));
            }
        }

        private string GetDateFormat() {
            if (_dateFormat == null) {
                try {
                    CultureInfo culture = new CultureInfo(CultureCode);

                    if (culture != null) {
                        _dateFormat = culture.DateTimeFormat.LongDatePattern;
                    }
                } catch (Exception e) {
                    LogManager.GetLogger(GetType()).Error(e);
                }
            }
            return _dateFormat;
        }

        private IFormatProvider GetDateFormatter() {
            if (_dateFormatter == null) {
                try {
                    CultureInfo culture = new CultureInfo(CultureCode);

                    if (culture != null) {
                        _dateFormatter = culture.DateTimeFormat;
                    }
                } catch (Exception e) {
                    LogManager.GetLogger(GetType()).Error(e);
                }
            }
            return _dateFormatter;
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

            if (btnSearch != null) {
                btnSearch.Click += new EventHandler(btnSearch_Click);
            }

            if (btnBack != null) {
                btnBack.Click += new EventHandler(btnBack_Click);
            }
        }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e) {
            RenderOrderList();
        }

        private void btnBack_Click(object sender, EventArgs e) {
            RenderOrderList();
        }
    }
}