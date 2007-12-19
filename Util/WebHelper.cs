using System;
using System.Configuration;
using System.Web;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Contains methods that should really be in the web project, but cannot be referenced
	/// </summary>
	public sealed class WebHelper {

		public const string CONFIG_KEY_DEFAULT_DOMAIN = "site.SiteDomain";
		public const string CONFIG_KEY_DEFAULT_APPLICATION_PATH = "site.ApplicationPath";
		public const string CONFIG_KEY_DEFAULT_PHYSICAL_APPLICATION_PATH = "site.PhysicalApplicationPath";
        public const string CONFIG_KEY_DEFAULT_IMAGE_PATH = "WebImageMakerWorkingDirectory";

		private WebHelper() {
		}

		public static string GetSchemeAndDomain() {
			if (HttpContext.Current != null) {
				return GetSchemeAndDomain(HttpContext.Current.Request.IsSecureConnection);
			} else {
				return GetSchemeAndDomain(false);
			}
		}

		public static string GetSchemeAndDomain(bool isSecure) {

			string scheme = (isSecure) ? "https" : "http";

			if (HttpContext.Current != null) {
				try {
					return scheme + "://" + HttpContext.Current.Request.Url.Authority;
				} catch (Exception e) {
					LogManager.GetLogger(typeof(WebHelper)).Error(e);
				}
			} else {
				return scheme + "://" + ConfigurationManager.AppSettings[CONFIG_KEY_DEFAULT_DOMAIN];
			}

			return "";
		}

		/// <summary>
		/// Application root path, always ending with a forward slash
		/// </summary>
		public static string GetAppPath() {
			if (HttpContext.Current != null) {
				
				string appPath = HttpContext.Current.Request.ApplicationPath;

				if (appPath.EndsWith("/")) {
					return appPath;
				} else {
					return appPath + "/";
				}

			} else {
				return ConfigurationManager.AppSettings[CONFIG_KEY_DEFAULT_APPLICATION_PATH];
			}
		}

		public static string GetPhysicalApplicationPath() {

			string path;

			if (HttpContext.Current != null) {
				path = HttpContext.Current.Request.PhysicalApplicationPath;
			} else {
				path = ConfigurationManager.AppSettings[CONFIG_KEY_DEFAULT_PHYSICAL_APPLICATION_PATH];
			}

			if (path.EndsWith("\\")) {
				return path;
			} else {
				return path + "\\";
			}
		}

        public static string GetImageWorkingDirectory() {

            string path = ConfigurationManager.AppSettings[CONFIG_KEY_DEFAULT_IMAGE_PATH];

            if (path.EndsWith("\\")) {
                return path;
            } else {
                return path + "\\";
            }
        }

        public static string GetImagePathWeb() {
            string mappedAppPath = HttpContext.Current.Request.MapPath(GetAppPath());
            return GetAppPath() + GetImageWorkingDirectory().Substring(mappedAppPath.Length).Replace("\\", "/") + "web/";
        }
    
        public static string GetImageUrl(string filePath) {
            if (filePath == null) return "";
            string mappedAppPath = GetImageWorkingDirectory() + "web\\";
            return filePath.Substring(mappedAppPath.Length).Replace("\\", "/");
        }
    }
}