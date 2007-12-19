using System;
using System.Web;

using Cuyahoga.Core.Domain;
using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;

namespace Cuyahoga.Modules.ECommerce.Util {

    public class StoreContext : IStoreContext {

        public const long ID_NULL = 0;
        public const string DEFAULT_CULTURE_CODE = "en-US";

        private IBasket _basket = null;
        private IBasket _order = null;

        private string _appPath = "";
        private User _user;

        public StoreContext() {
            if (HttpContext.Current != null) {
                _user = HttpContext.Current.User as User;
            }
        }

        public StoreContext(User user) {
            _user = user;
        }

        #region IStoreContext Members

        public virtual IBasket CurrentBasket {
            get { return _basket; }
            set { _basket = value; }
        }

        public virtual IBasket LastOrder {
            get { return _order; }
            set { _order = value; }
        }

        public virtual string AppPath {
            get {
                return _appPath;
            }
        }

        private long _basketID;

        public virtual long BasketID {
            get {
                return _basketID;
            }
            set {
                _basketID = value;
            }
        }

        private string _currencyCode;

        public virtual string CurrencyCode {
            get {
                return _currencyCode;
            }
            set {
                _currencyCode = value;
            }
        }

        private bool _isBasketEmpty;

        public virtual bool IsBasketEmpty {
            get {
                return _isBasketEmpty;
            }
            set {
                _isBasketEmpty = value;
            }
        }

        private long _lastOrderID;

        public virtual long LastOrderID {
            get {
                return _lastOrderID;
            }
            set {
                _lastOrderID = value;
            }
        }

        private int _storeID;

        public virtual int StoreID {
            get {
                return _storeID;
            }
            set {
                _storeID = value;
            }
        }

        #endregion

        public virtual User CurrentUser {
            get {
                return _user;
            }
        }

        #region Basket refresh handling
        public event BasketChangedHandler OnBasketChanged;
        public event BasketChangedHandler OnHideBasketSummary;

        public delegate void BasketChangedHandler(object sender, IBasket basket);

        public void NotifyBasketChanged(IBasket basket) {
            if (OnBasketChanged != null) {
                OnBasketChanged(this, basket);
            }
        }

        public void NotifyHideBasketSummary(IBasket basket) {
            if (OnHideBasketSummary != null) {
                OnHideBasketSummary(this, basket);
            }
        }
        #endregion
    }
}
