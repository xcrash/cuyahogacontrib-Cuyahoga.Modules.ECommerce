using System;
using System.Collections;
using System.Web;
using System.Web.SessionState;

using Cuyahoga.Core.Domain;
using log4net;
using IgCurrency = Cuyahoga.Modules.ECommerce.Util.Currency;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Shares context across modules
	/// </summary>
	public class WebStoreContext : StoreContext {

		public const string SESSION_INFO_CURRENT_BASKET_ID = "CURRENT_BASKET_ID";
		public const string SESSION_INFO_LAST_ORDER_ID = "LAST_ORDER_ID";
		public const string SESSION_INFO_IS_BASKET_EMPTY = "IS_BASKET_EMPTY";
        public const string SESSION_INFO_CURRENCY_CODE = "CURRENCY_CODE";

		public const string REQUEST_INFO_STORE_ID = "STORE_ID";

		private const string ITEM_NAME = "stateInfo";

		private HttpContext _context;
		private string _appPath = null;

		private WebStoreContext(HttpContext context) {
			_context = context;
		}

		//Make sure we only have one instance of this per request
		public static WebStoreContext Current {
			get {
				return GetState(HttpContext.Current);
			}
		}

		private static WebStoreContext GetState(HttpContext context) {
		
			WebStoreContext info = null;

			try {
				info = (WebStoreContext) context.Items[ITEM_NAME];
			} catch (Exception e) {
				LogManager.GetLogger(typeof(WebStoreContext)).Error(e);
			}

			if (info == null) {

				info = new WebStoreContext(context);

				try {

					context.Items.Add(ITEM_NAME, info);
	
					if (context.Session != null) {	
						LogManager.GetLogger(typeof(WebStoreContext)).Info("Added Context for SessionID " + context.Session.SessionID);
					} else {
						LogManager.GetLogger(typeof(WebStoreContext)).Info("Session is null");
					}

				} catch (System.Threading.ThreadAbortException) {
				} catch (Exception e) {
					LogManager.GetLogger(typeof(WebStoreContext)).Error(e);			
				}
			}
            
			return info;
		}

		private IDictionary RequestInfo {
			get {
				return _context.Items;
			}
		}

		private HttpSessionState SessionInfo {
			get {
				return _context.Session;
			}
        }

        public override string AppPath {
            get {
                if (_appPath == null) {
                    _appPath = _context.Request.ApplicationPath + "/";
                    _appPath = _appPath.Replace(@"//", @"/");
                }
                return _appPath;
            }
        }

        public override string CurrencyCode {
			get {
				try {
					return SessionInfo[SESSION_INFO_CURRENCY_CODE].ToString();
				} catch {
					string code = new IgCurrency("en-GB").CurrencyCode;
					CurrencyCode = code;
					return code;
				}
			}
			set {
				SessionInfo[SESSION_INFO_CURRENCY_CODE] = value;
			}
		}

		public override long BasketID {
			get {
				if (SessionInfo[SESSION_INFO_CURRENT_BASKET_ID] != null) {
					return (long) SessionInfo[SESSION_INFO_CURRENT_BASKET_ID];
				}
				return ID_NULL;
			}
			set {
				SessionInfo[SESSION_INFO_CURRENT_BASKET_ID] = value;
			}
		}

        public override long LastOrderID {
			get {
				if (SessionInfo[SESSION_INFO_LAST_ORDER_ID] != null) {
					return (long) SessionInfo[SESSION_INFO_LAST_ORDER_ID];
				}
				return ID_NULL;
			}
			set {
				SessionInfo[SESSION_INFO_LAST_ORDER_ID] = value;
			}
		}

        public override bool IsBasketEmpty {
			get {
				if (SessionInfo[SESSION_INFO_IS_BASKET_EMPTY] != null) {
					return (bool) SessionInfo[SESSION_INFO_IS_BASKET_EMPTY];
				}
				return true;
			}
			set {
				SessionInfo[SESSION_INFO_IS_BASKET_EMPTY] = value;
			}
		}

        public override int StoreID {
			get {
				try {
					return Int32.Parse(SessionInfo[REQUEST_INFO_STORE_ID].ToString());
				} catch {
					
					int storeID;

					try {
						storeID = Int32.Parse(_context.Request[REQUEST_INFO_STORE_ID]);
					} catch {
						storeID = 1;
					}

					StoreID = storeID;
					return storeID;
				}
			}
			set {
				SessionInfo[REQUEST_INFO_STORE_ID] = value;
			}
		}

        public override User CurrentUser {
            get {
                return HttpContext.Current.User as User;
            }
        }
	}
}
