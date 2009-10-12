using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

using Cuyahoga.Core.Domain;
using Cuyahoga.Web.Util;

namespace Cuyahoga.Modules.ECommerce.Core {

    /// <summary>
    /// Simple module with parameters parsed as name value pairs
    /// </summary>
    public class GenericModule : ModuleBase {

        public const string PARAM_VIEW_NAME = "view";

        private NameValueCollection _requestParameters;
        private string _viewName = "";

        public GenericModule()
            : base() {
        }

        protected override void ParsePathInfo() {

            base.ParsePathInfo();

            RequestParameters = new NameValueCollection();

            string[] parameters = ModuleParams;
           // if (HttpContext.Current != null && parameters != null) {
             //   HttpContext.Current.Response.Redirect(parameters[parameters.Length - 1]);
          //  }
            if (parameters != null) {
                for (int i = 0; i < parameters.Length - 1; i += 2) {
                    RequestParameters.Add(parameters[i], ParameterEncoder.DecodeParameterValue(parameters[i + 1]));
                }
            }

            if (RequestParameters != null && RequestParameters.Count > 0) {
                try {
                    ViewName = RequestParameters[PARAM_VIEW_NAME];
                } catch {
                }
            }
        }

        protected string GetViewBase() {
            return DefaultViewControlPath.Substring(0, DefaultViewControlPath.LastIndexOf("/") + 1);
        }

        public virtual string ViewName {
            get {
                return _viewName;
            }
            set {
                _viewName = value;
            }
        }

        public override string CurrentViewControlPath {
            get {
                if (ViewName != null && ViewName.Length > 0) {
                    return GetViewBase() + ViewName + ".ascx";
                } else {
                    return DefaultViewControlPath;
                }
            }
        }

        public NameValueCollection RequestParameters {
            get {
                return _requestParameters;
            }
            set {
                _requestParameters = value;
            }
        }

        public virtual string GetUrl() {
            return UrlHelper.GetUrlFromSection(Section);
        }

        public virtual string GetUrl(NameValueCollection parameterList) {

            StringBuilder url = new StringBuilder();

            url.Append(GetUrl());

            foreach (string key in parameterList) {
                url.Append(ParameterEncoder.FORWARD_SLASH);
                url.Append(key);
                url.Append(ParameterEncoder.FORWARD_SLASH);
                url.Append(HttpUtility.UrlEncode(ParameterEncoder.EncodeParameterValue(parameterList[key])));
            }

            return url.ToString();
        }

        public virtual string GetMenuUrl(NameValueCollection parameterList) {

            StringBuilder url = new StringBuilder();

          //  url.Append(GetUrl());

            foreach (string key in parameterList) {
                url.Append(ParameterEncoder.FORWARD_SLASH);
                url.Append(key);
                url.Append(ParameterEncoder.FORWARD_SLASH);
                url.Append(HttpUtility.UrlEncode(ParameterEncoder.EncodeParameterValue(parameterList[key])));
            }

            return url.ToString();
        }

        /*
        public override string CacheKey {
            get {

                //Assume all parameters are part of the key
                if (RequestParameters != null) {

                    StringBuilder cacheKey = new StringBuilder();

                    for (int i = 0; i < RequestParameters.Count; i++) {
                        if (i > 0) cacheKey.Append("__");
                        cacheKey.Append(RequestParameters[i]);
                    }

                    return cacheKey.ToString();
                }

                return "";
            }
        }
        */
    }
}
