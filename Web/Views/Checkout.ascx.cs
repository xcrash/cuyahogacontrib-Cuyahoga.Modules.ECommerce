namespace Cuyahoga.Modules.ECommerce.Web.Views {

    using System;
    using System.Collections;
    using System.Web;

    using Cuyahoga.Modules.ECommerce.Util.Enums;
    using Cuyahoga.Modules.ECommerce.Util.Interfaces;
    using Cuyahoga.Modules.ECommerce.Service.PaymentProvider;
    using Cuyahoga.Modules.ECommerce.Util;
    using log4net;
    using Cuyahoga.Modules.ECommerce.Service.Translation;
    using Cuyahoga.Modules.ECommerce.Util.Location;
    using Cuyahoga.Modules.ECommerce.Web.Controls;
    using Cuyahoga.Modules.ECommerce.Service;
    using Cuyahoga.Modules.ECommerce.Domain;


    public class Checkout : Cuyahoga.Modules.ECommerce.Core.GenericModuleControl {

        public enum CheckoutStep {
            SelectPaymentMethod,
            EnterAddressDetails,
            ConfirmDetails,
            ProceedToPayment,
            EnterAccountDetails,
            ShowSubmittedOrder,
            ShowCompletedOrder,
            Login
        }

        public enum BreadCrumbImage {
            CreditCardDetails,
            CreditOrderConfirmation,
            CreditPaymentType,
            CreditUserDetails,
            TradePaymentType,
            TradeAccountDetails,
            TradeOrderConfirmation
        }

        protected AddressEdit ctlInvoiceAddress;
        protected AddressEdit ctlDeliveryAddress;

        protected OrderViewComposite ctlOrderViewComposite;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.UserDetailsEdit ctlUser;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.UserDetailsEdit ctlUserAcct;
        protected Cuyahoga.Modules.ECommerce.Web.Controls.Login ctlLogin;

        protected System.Web.UI.WebControls.TextBox txtCompanyName;
        protected System.Web.UI.WebControls.TextBox txtAccountNumber;

        protected System.Web.UI.WebControls.RadioButtonList rblPaymentMethod;

        protected System.Web.UI.WebControls.PlaceHolder phStep1;
        protected System.Web.UI.WebControls.PlaceHolder phStep1a;
        protected System.Web.UI.WebControls.PlaceHolder phStep2a;
        protected System.Web.UI.WebControls.PlaceHolder phStep2b;
        protected System.Web.UI.WebControls.PlaceHolder phStep3a;
        protected System.Web.UI.WebControls.PlaceHolder phStep3b;

        protected System.Web.UI.WebControls.Image imgBreadCrumb;
        protected System.Web.UI.WebControls.Image imgTradeBreadCrumb;

        protected System.Web.UI.WebControls.Button btnPrevious;
        protected System.Web.UI.WebControls.Button btnNext;
        protected System.Web.UI.WebControls.CheckBox chkSameAddress;

        private ECommerceModule _emod = null;

        protected ECommerceModule EModule {
            get {
                if (_emod == null) {
                    _emod = Module as ECommerceModule;
                }
                return _emod;
            }
        }

        private void Page_Load(object sender, System.EventArgs e) {

            try {

                BasketDecorator basketOrder = GetCurrentBasket();
                if (basketOrder == null) {
                    basketOrder = GetLastOrder();
                }

                //Should never happen
                if (basketOrder == null) {
                    Response.Redirect(Page.ResolveUrl("~/Default.aspx"), true);
                    return;
                }

                if (!IsPostBack) {

                    IStoreContext context = WebStoreContext.Current;

                    if (basketOrder == null || !EModule.Rules.AllowPlaceOrder(context.CurrentUser)) {
                        Response.Redirect(Page.ResolveUrl("~/Default.aspx"), true);
                    }

                    PopulateAddressDetails(basketOrder, context);
                    PopulateUserDetails(basketOrder, context);

                    //Handle for extra details
                    PopulateExtraDetails(basketOrder);
                    if (Context.User.Identity.IsAuthenticated) {
                        if (basketOrder.IsPurchased) {
                            SetAndActionNewStep(CheckoutStep.ShowCompletedOrder, basketOrder);
                        } else {
                            SetAndActionNewStep(CheckoutStep.EnterAddressDetails, basketOrder);
                        }
                    } else {
                        SetAndActionNewStep(CheckoutStep.Login, basketOrder);
                    }
                }
            } catch (System.Threading.ThreadAbortException) {
            } catch (Exception f) {
                LogManager.GetLogger(GetType()).Error(f);
            }
        }

        private void PopulateAddressDetails(BasketDecorator basketOrder, IStoreContext context) {

            //Sort out the country list
            IList countryList = EModule.AccountService.GetCountries();

            if (ctlInvoiceAddress != null) {
                ctlInvoiceAddress.SetAvailableCountries(countryList);
            }

            if (ctlDeliveryAddress != null) {
                ctlDeliveryAddress.SetAvailableCountries(countryList);
            }

            //Populate any details already in the order
            if (basketOrder.OrderHeader != null) {
                if (ctlInvoiceAddress != null && basketOrder.OrderHeader.InvoiceAddress != null) {
                    ctlInvoiceAddress.BindAddress(basketOrder.OrderHeader.InvoiceAddress);
                }

                if (ctlDeliveryAddress != null && basketOrder.OrderHeader.DeliveryAddress != null) {
                    ctlDeliveryAddress.BindAddress(basketOrder.OrderHeader.DeliveryAddress);
                }
            } else {
                if (context != null && context.WebStoreUser != null) {
                    if (ctlDeliveryAddress != null && context.WebStoreUser.UserDetails != null) {
                        ctlInvoiceAddress.BindAddress(context.WebStoreUser.UserAddress);
                        ctlDeliveryAddress.BindAddress(context.WebStoreUser.UserAddress);
                    }
                }  
            }
        }

        private void PopulateUserDetails(BasketDecorator basketOrder, IStoreContext context) {
           
            if (basketOrder.UserDetails != null || basketOrder.AltUserDetails != null) {

                UserDetail userDetails = ((basketOrder.UserDetails != null) ? new UserDecorator(basketOrder.UserDetails) : basketOrder.AltUserDetails) as UserDetail;

                if (ctlUser != null && userDetails != null) {
                    ctlUser.BindUserDetails(userDetails);
                }

                if (ctlUserAcct != null && basketOrder.UserDetails != null) {
                    ctlUserAcct.BindUserDetails(userDetails);
                    txtAccountNumber.Text = userDetails.AccountNumber;
                    txtCompanyName.Text = userDetails.CompanyName;
                }
            } else {

                if (context != null && context.WebStoreUser != null) {
                    if (ctlUser != null && context.WebStoreUser.UserDetails != null) {
                        ctlUser.BindUserDetails(context.WebStoreUser.UserDetails);
                    }
                }   
            }
        }

        protected virtual void PopulateExtraDetails(BasketDecorator basketOrder) {

            if (basketOrder.OrderHeader == null || basketOrder.OrderHeader.InvoiceAddress == null) {

                //See if the user has any address details
                IAddress defaultAddress = basketOrder.UserDetails as IAddress;

                if (defaultAddress != null) {
                    ctlInvoiceAddress.BindAddress(defaultAddress);
                    ctlDeliveryAddress.CountryCode = ctlInvoiceAddress.CountryCode;
                }
            }
        }

        protected CheckoutStep Step {
            get {
                try {
                    return (CheckoutStep)ViewState["Step"];
                } catch {
                    return CheckoutStep.SelectPaymentMethod;
                }
            }
            set {
                CheckoutStep oldStep = Step;
                ViewState["Step"] = value;
            }
        }

        private void SetAndActionNewStep(CheckoutStep newStep, BasketDecorator basketOrder) {
            CheckoutStep oldStep = Step;
            Step = newStep;
            PerformStepActions(oldStep, Step, basketOrder);
        }

        protected bool IsAccountPayment {
            get {
                try {
                    return Convert.ToBoolean(ViewState["isAccountPayment"]);
                } catch {
                    return false;
                }
            }
            set {
                ViewState["isAccountPayment"] = value;
            }
        }

        protected virtual void PerformStepActions(CheckoutStep oldStep, CheckoutStep newStep, BasketDecorator basketOrder) {

            //Hide all of the steps
            phStep1.Visible
                = phStep1a.Visible
                = phStep2a.Visible
                = phStep2b.Visible
                = phStep3a.Visible
                = phStep3b.Visible
                = false;

            btnPrevious.Visible = (newStep != CheckoutStep.SelectPaymentMethod);

            switch (newStep) {

                case CheckoutStep.ShowCompletedOrder:

                    if (IsAccountPayment) {
                        phStep3a.Visible = true;
                    } else {
                        phStep3b.Visible = true;
                    }

                    CloseOrder(basketOrder);
                    ShowCompletedOrder();

                    btnNext.Visible = false;
                    btnPrevious.Visible = false;

                    break;

                case CheckoutStep.ConfirmDetails:

                    if (IsAccountPayment) {
                        phStep3a.Visible = true;
                        btnNext.Text = GetText("submit order request");
                    } else {
                        phStep3b.Visible = true;
                        btnNext.Text = GetText("proceed to payment");
                    }

                    PopulateOrderDetailsFromForm(basketOrder);
                    break;

                case CheckoutStep.ProceedToPayment:

                    if (IsAccountPayment) {
                        phStep2a.Visible = true;
                    } else {
                        phStep2b.Visible = true;
                    }

                    CollectPayment(basketOrder);
                    break;

                case CheckoutStep.EnterAddressDetails:

                    if (IsAccountPayment) {
                        phStep2a.Visible = true;
                    } else {
                        phStep2b.Visible = true;
                    }

                    btnNext.Text = GetText("proceed to step 3");
                    break;

                case CheckoutStep.SelectPaymentMethod:
                    phStep1.Visible = true;
                    btnNext.Text = GetText("proceed to step 2");
                    break;

                case CheckoutStep.EnterAccountDetails:
                    phStep2a.Visible = true;
                    btnNext.Text = GetText("submit order request");
                    break;

                case CheckoutStep.ShowSubmittedOrder:
                    phStep3a.Visible = true;
                    SubmitAccountOrder(basketOrder);
                    btnNext.Visible = false;
                    btnPrevious.Visible = false;
                    break;

                case CheckoutStep.Login:
                    phStep1a.Visible = true;
                    btnNext.Visible = false;
                    btnPrevious.Visible = false;
                    ctlLogin.RedirectTo = this.Page.Request.RawUrl;
                    break;
            }
        }

        private bool isCredit() {
            return true; //wtf is this?
        }

        /// <summary>
        /// Action performed before the order is closed and confirmation is shown
        /// </summary>
        protected virtual void CloseOrder(BasketDecorator basketOrder) {
            if (!basketOrder.IsPurchased) {
                EModule.CommerceService.CloseCurrentOrder(WebStoreContext.Current, this);
            }
        }

        protected virtual void CollectPayment(BasketDecorator basketOrder) {

            IElectronicPayment payment = new PaymentHelper(EModule.CommerceDao).CreatePayment(basketOrder, this);
            IPaymentProvider provider = EModule.PaymentProvider;//HACK to get it to compile MUST FIX

            IWebFormPaymentProvider webProvider = provider as IWebFormPaymentProvider;

            if (webProvider != null) {
                webProvider.TransferClientToPaymentPage(payment, PaymentRequestTypes.ImmediatePayment);
            } else {
                if (provider != null) {
                    provider.RequestAuthPayment(payment);
                    IBasket order = new PaymentHelper(EModule.CommerceDao).ProcessReceivedPayment(payment, this);
                    if (order != null) {
                        EModule.CommerceService.CloseOrder(order, TranslatorUtils.GetTextTranslator(GetType(), order.CultureCode));
                    }
                } else {
                    //What??
                    PopulateOrderDetailsFromForm(basketOrder);
                }
            }
        }

        protected virtual void ShowCompletedOrder() {
            ctlOrderViewComposite.BindOrder(EModule.CommerceService.GetLastOrder(WebStoreContext.Current));
        }

        protected virtual void SubmitAccountOrder(BasketDecorator basketOrder) {

            //probably redundant, but must ensure it is true
            IsAccountPayment = true;

            if (basketOrder.OrderHeader == null) {
                new BasketDecorator(basketOrder).ConvertToOrder(EModule.CommerceDao, EModule.CommonDao);
                basketOrder.OrderHeader.PaymentMethod = PaymentMethodType.PurchaseOrderInvoice;
            }

            if (ctlUserAcct != null && basketOrder.AltUserDetails != null) {
                UserDetail userDetails = basketOrder.AltUserDetails as UserDetail;
                CopyUserDetailsInfo(ctlUserAcct, userDetails);
            }

            //See if any of these details have changed anything
            EModule.CommerceService.RefreshBasket(WebStoreContext.Current);

            if (ctlUserAcct != null && basketOrder.AltUserDetails == null) {

                UserDetail userDetail = new UserDetail();
                basketOrder.AltUserDetails = userDetail;
                CopyUserDetailsInfo(ctlUserAcct, userDetail);

                EModule.CommonDao.SaveOrUpdateObject(userDetail);
                EModule.CommerceService.RefreshBasket(WebStoreContext.Current);
            }


            if (EModule.CommerceService.SubmitCurrentOrder(WebStoreContext.Current, this)) {
                WebStoreContext.Current.NotifyBasketChanged(null);
            }
        }

        private void CopyUserDetailsInfo(IUserDetails source, UserDetail userDetails) {

            UserDetailsHelper.CopyUserDetails(source, userDetails);

            userDetails.AccountType = AccountType.StandardAccount;
            userDetails.AccountNumber = txtAccountNumber.Text;
            userDetails.CompanyName = txtCompanyName.Text;
        }

        protected virtual void PopulateOrderDetailsFromForm(BasketDecorator basketOrder) {

            if (basketOrder.OrderHeader == null) {
                new BasketDecorator(basketOrder).ConvertToOrder(EModule.CommerceDao, EModule.CommonDao);
                basketOrder.OrderHeader.InvoiceAddress = EModule.CommerceDao.CreateAddress();
                basketOrder.OrderHeader.DeliveryAddress = EModule.CommerceDao.CreateAddress();
                basketOrder.OrderHeader.PaymentMethod = (IsAccountPayment) ? PaymentMethodType.PurchaseOrderInvoice : PaymentMethodType.CreditCard;
            }

            if (basketOrder.UserDetails == null) {
                basketOrder.UserDetails = WebStoreContext.Current.CurrentUser;
            }

            if (ctlInvoiceAddress != null) {
                AddressHelper.CopyAddress(ctlInvoiceAddress, basketOrder.OrderHeader.InvoiceAddress);
            }

            if (ctlDeliveryAddress != null && ctlDeliveryAddress.Enabled) {
                if (basketOrder.OrderHeader.DeliveryAddress == null) {
                    basketOrder.OrderHeader.DeliveryAddress = EModule.CommerceDao.CreateAddress();
                }
                AddressHelper.CopyAddress(ctlDeliveryAddress, basketOrder.OrderHeader.DeliveryAddress);
            } else {
                basketOrder.OrderHeader.DeliveryAddress = null;
            }

            if (ctlUser != null && basketOrder.AltUserDetails != null) {
                UserDetailsHelper.CopyUserDetails(ctlUser, basketOrder.AltUserDetails);
            }

            //Might need to set other things from the entered details,
            //or force a final price/availability for all items
            UpdateExtraDetails();

            //See if any of these details have changed anything
            EModule.CommerceService.RefreshBasket(WebStoreContext.Current);

            if (ctlUser != null && basketOrder.AltUserDetails == null) {
                UserDetail userDetail = new UserDetail();
                userDetail.AccountType = (IsAccountPayment) ? AccountType.StandardAccount : AccountType.CreditCard;
                basketOrder.AltUserDetails = userDetail;
                UserDetailsHelper.CopyUserDetails(ctlUser, basketOrder.AltUserDetails);
                EModule.CommonDao.SaveOrUpdateObject(userDetail);
                EModule.CommerceService.RefreshBasket(WebStoreContext.Current);
            }

            //Show saved details
            ctlOrderViewComposite.BindOrder(basketOrder);
        }

        /// <summary>
        /// Reads extra information from the form and sets appropriate properties in the order
        /// or performs additional actions before the order is saved
        /// </summary>
        protected virtual void UpdateExtraDetails() {
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

            this.btnNext.Click += new EventHandler(btnNext_Click);
            this.btnPrevious.Click += new EventHandler(btnPrevious_Click);

            IStoreContext context = WebStoreContext.Current;
            context.NotifyHideBasketSummary(null);

            if (!IsPostBack) {
                btnPrevious.Text = GetText("Previous");
            }

            if (ctlInvoiceAddress != null) {
                ctlInvoiceAddress.CountryList.SelectedIndexChanged += new EventHandler(CountryList_SelectedIndexChanged);
            }

            if (chkSameAddress != null) {
                chkSameAddress.CheckedChanged += new EventHandler(chkSameAddress_CheckedChanged);
            }

            //Make sure the child controls know about the base module
            if (ctlDeliveryAddress != null) ctlDeliveryAddress.Module = Module;
            if (ctlInvoiceAddress != null) ctlInvoiceAddress.Module = Module;
            if (ctlOrderViewComposite != null) ctlOrderViewComposite.Module = Module;
            if (ctlUser != null) ctlUser.Module = Module;
            if (ctlUserAcct != null) ctlUserAcct.Module = Module;
            if (ctlLogin != null) ctlLogin.Module = Module;
        }

        private void SetChildTranslation(ITranslatable control) {
            if (control != null) {
                control.Translator = this;
            }
        }
        #endregion

        protected BasketDecorator GetCurrentBasket() {

            IBasket basket = EModule.CommerceService.GetCurrentBasket(WebStoreContext.Current);
            if (basket == null) {
                return null;
            }

            BasketDecorator decorator = basket as BasketDecorator;
            if (decorator == null) {
                decorator = new BasketDecorator(basket);
            }

            return decorator;
        }

        protected BasketDecorator GetLastOrder() {

            //Gets the last order placed in this session, usually the result of having just completed
            //a credit card order
            IBasket order = EModule.CommerceService.GetLastOrder(WebStoreContext.Current);
            if (order == null) {
                return null;
            }

            BasketDecorator decorator = order as BasketDecorator;
            if (decorator == null) {
                decorator = new BasketDecorator(order);
            }

            return decorator;
        }

        private void btnNext_Click(object sender, EventArgs e) {

            if (Step == CheckoutStep.SelectPaymentMethod) {
                IsAccountPayment = (rblPaymentMethod.SelectedValue != null && rblPaymentMethod.SelectedValue == "account");
            }

            try {
                BasketDecorator basketOrder = GetCurrentBasket();
                SetAndActionNewStep(GetNextStep(Step, basketOrder), basketOrder);
            } catch (System.Threading.ThreadAbortException) {
                //Damn those exceptions! They just won't go away! -- er..what?
            }
        }

        protected virtual CheckoutStep GetNextStep(CheckoutStep currentStep, BasketDecorator basketOrder) {

            //Can't move on unless the current page is valid
            if (!Page.IsValid) {
                return currentStep;
            }

            switch (currentStep) {
                case CheckoutStep.SelectPaymentMethod:
                    return (!IsAccountPayment) ? CheckoutStep.EnterAddressDetails : CheckoutStep.EnterAccountDetails;
                case CheckoutStep.EnterAccountDetails:
                    return CheckoutStep.ShowSubmittedOrder;
                case CheckoutStep.EnterAddressDetails:
                    return CheckoutStep.ConfirmDetails;
                default:
                    if (basketOrder.OrderHeader != null && basketOrder.OrderHeader.PaymentMethod == PaymentMethodType.PurchaseOrderInvoice) {
                        return CheckoutStep.ShowCompletedOrder;
                    } else {
                        return CheckoutStep.ProceedToPayment;
                    }
            }
        }

        protected virtual CheckoutStep GetPreviousStep(CheckoutStep currentStep) {
            switch (currentStep) {
                case CheckoutStep.ConfirmDetails:
                    return CheckoutStep.EnterAddressDetails;
                default:
                    return CheckoutStep.SelectPaymentMethod;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e) {
            BasketDecorator basketOrder = GetCurrentBasket();
            SetAndActionNewStep(GetPreviousStep(Step), basketOrder);
        }

        private void CountryList_SelectedIndexChanged(object sender, EventArgs e) {
            ctlDeliveryAddress.CountryCode = ctlInvoiceAddress.CountryCode;
        }

        private void chkSameAddress_CheckedChanged(object sender, EventArgs e) {
            if (ctlDeliveryAddress != null) {

                if (chkSameAddress != null) {
                    ctlDeliveryAddress.Enabled = !chkSameAddress.Checked;
                }

                if (ctlInvoiceAddress != null) {
                    ctlDeliveryAddress.CountryCode = ctlInvoiceAddress.CountryCode;
                }
            }
        }
    }
}