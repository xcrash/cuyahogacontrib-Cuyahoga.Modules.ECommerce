using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Util {
	/// <summary>
	/// Summary description for HttpUtils.
	/// </summary>
	public class HttpUtils {

		public const string CONFIG_KEY_DEFAULT_DOMAIN_AND_APPLIACTION_PATH = "site.DefaultDomainAndAppPath";
		public const int PORT_NUMBER_DEFAULT_HTTP = 80;
		public const int PORT_NUMBER_DEFAULT_HTTPS = 443;

		public const string SCHEMA_HTTP = "http";
		public const string SCHEMA_HTTPS = "https";

		private HttpUtils() {
		}

		public static string ReadHtmlPage(string url) {
			return ReadHtmlPage(url, "GET", "");
		}
         
		public static string ReadHtmlPage(string url, string method, string parameters) {
			return ReadHtmlPage(url, method, parameters, 0);
		}

		/// <summary>
		/// Turns a URL fragment into a valid absolute URL. Does not automatically include the domain
		/// </summary>
		/// <param name="url">URL fragment</param>
		/// <returns></returns>
		public static string GetAbsoluteUrl(string url, HttpRequest request) {
			if (url.StartsWith("/") || url.StartsWith(SCHEMA_HTTP) || url.StartsWith(SCHEMA_HTTPS)) {
				return url;
			} else {
				if (request.ApplicationPath != "/") {
					return request.ApplicationPath + "/" + url;
				} else {
					return "/" + url;
				}
			}
		}

		public static string GetUrlWithoutQueryString(string url) {
			int queryIndex = url.IndexOf("?");
			if (queryIndex > 0) {
				return url.Substring(0, queryIndex);
			} else {
				return url;
			}
		}

		/// <summary>
		/// Sorts out the problems when POSTing xml fields over HTTP - the ampersand confuses the field delimiters
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public static string EncodeQueryStringAmpersands(string xml) {
			return xml.Replace("&", "%26");
		}

		public static string DecodeQueryStringAmpersands(string xml) {
			return xml.Replace("%26", "&");
		}

		public static string HtmlEncode(string text) {
			if (HttpContext.Current != null && HttpContext.Current.Server != null) {
				return HttpContext.Current.Server.HtmlEncode(text);
			} else {
                return System.Web.HttpUtility.HtmlEncode(text);
			}
		}

		public static string UrlEncode(string text) {
			if (HttpContext.Current != null && HttpContext.Current.Server != null) {
				return HttpContext.Current.Server.UrlEncode(text);
			} else {
                return System.Web.HttpUtility.UrlEncode(text);
			}
		}

		public static string ReadHtmlPage(string url, string method, string parameters, int timeoutMilliseconds) {
         
			string result = "";
			StreamWriter myWriter = null;

			HttpWebRequest objRequest = (HttpWebRequest) WebRequest.Create(url);
			objRequest.Method = method;

			if (timeoutMilliseconds > 0) {
				objRequest.Timeout = timeoutMilliseconds;
			}

			if (parameters.Length > 0) {
				try {
					objRequest.ContentType = "application/x-www-form-urlencoded";
					objRequest.KeepAlive = false;

					myWriter = new StreamWriter(objRequest.GetRequestStream());
					myWriter.Write(parameters);
				} catch (Exception e) {
					LogManager.GetLogger(typeof(HttpUtils)).Error(e);
					return e.Message;
				} finally {
					myWriter.Close();
					myWriter = null;
				}
			}

			try {
				HttpWebResponse objResponse = (HttpWebResponse) objRequest.GetResponse();
				StreamReader sr;
				sr = new StreamReader(objResponse.GetResponseStream());
				result = sr.ReadToEnd();
				sr.Close();
				sr = null;

				//Should always get something of interest...
				if (result == null || result.Length == 0) {
					LogManager.GetLogger(typeof(HttpUtils)).Error("Returned empty string reading URL [" + url + "]");
				}

			} catch (Exception e) {
				LogManager.GetLogger(typeof(HttpUtils)).Error(
					"Error requesting URL [" + url + "], "
					+ "method [" + method + "], "
					+ "parameters [" + parameters + "]"
					);
				LogManager.GetLogger(typeof(HttpUtils)).Error(e);
			}

			return result;
		}

		public static string GetHttpRpcResponse(string url, string method, string requestParameters) {
			return GetHttpRpcResponse(url, method, requestParameters, 0);
		}

		public static string GetHttpRpcResponse(string url, string method, string requestParameters, int timeoutMilliseconds) {
            
			try {
				string rawResponse =  HttpUtils.ReadHtmlPage(url, method, requestParameters, timeoutMilliseconds);
				string headerlessResponse = XMLUtils.StripXmlHeader(rawResponse);

				XmlDocument responseXml = new XmlDataDocument();
				responseXml.LoadXml(headerlessResponse);
				return responseXml.FirstChild.InnerText;

			} catch (Exception e) {
				LogManager.GetLogger(typeof(HttpUtils)).Error(
					"Error requesting URL [" + url + "], "
					+ "method [" + method + "], "
					+ "parameters [" + requestParameters + "]"
					);

				LogManager.GetLogger(typeof(HttpUtils)).Error(e);
				return null;
			}

		}

		/// <summary>
		/// Takes the supplied URL and strips the page, leaving the absolute path
		/// </summary>
		/// <param name="url">Original URL</param>
		/// <returns></returns>
		public static string GetPath(Uri url) {

			int segmentLength = url.Segments.Length - 1;
			if (segmentLength == 0) segmentLength = 1;
			string path = "";

			for (int i = 0; i < segmentLength; i++) {
				path += url.Segments[i];
			}

			return path;
		}

		public static string GetPath(string url) {
			return GetPath(new Uri(url));
		}

		public static string GetFullPath(Uri url) {
			return url.Scheme + "://" + url.Host + GetPath(url);
		}

		/// <summary>
		/// Like GetPath, but includes the domain name as well
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string GetFullPath(string url) {
			return GetFullPath(new Uri(url));
		}

		/// <summary>
		/// Takes all of the values submitted to this page and creates a new form
		/// which automatically submits itself to the supplied URL
		/// </summary>
		/// <param name="request">Current HttpRequest object</param>
		/// <param name="newUrl">The new URL which should receive this data</param>
		/// <returns></returns>
		public static string CreateHtmlFormFromRequest(HttpRequest request, string newUrl) {

			StringBuilder html = new StringBuilder();
			html.Append("<html>\n<body onLoad=\"document.forms[0].submit();\">\n");
			html.Append("<form method=\"POST\" action=\"" + newUrl + "\">\n");

			foreach (string key in request.Form.AllKeys) {
				if (key.ToUpper() != "__VIEWSTATE") {
					html.Append("<input type=\"hidden\" name=\"" + key + "\" value=\"" + request.Form[key] + "\"/>\n");
				}
			}

			html.Append("</form>\n");
			html.Append("</body>\n</html>");

			return html.ToString();

		}

		public static void TransferToExternalUrl(string newUrl) {

			HttpRequest request = HttpContext.Current.Request;
			HttpResponse response = HttpContext.Current.Response;
            
			string html = CreateHtmlFormFromRequest(request, newUrl);
			response.Write(html);
			try {
				response.End();
			} catch {}

		}

		/// <summary>
		/// Takes a Regular Expression and gets matches from the supplied URL
		/// </summary>
		public static MatchCollection ScrapePage(string url, string pattern) {
			string html = HttpUtils.ReadHtmlPage(url, "GET", "");
			Regex re = new Regex(pattern);
			return re.Matches(html);
		}

		public static MatchCollection ScrapePage(string url, string pattern, string method, RegexOptions options) {
			string html = HttpUtils.ReadHtmlPage(url, method, "");
			Regex re = new Regex(pattern, options);
			return re.Matches(html);
		}
	
		/// <summary>
		/// Gets the full path of the application, including domain name and scheme
		/// </summary>
		/// <returns></returns>
		public static string GetApplicationPath() {
			if (HttpContext.Current != null) {
				return GetApplicationPath(HttpContext.Current.Request.IsSecureConnection);
			} else {
				return GetApplicationPath(false);
			}
		}

		public static string GetApplicationPath(bool isSecure) {

			StringBuilder newUrl = new StringBuilder();
			HttpContext context = HttpContext.Current;

			if (context != null) {

				Uri url = context.Request.Url;

				//Force secure / non-secure
				if (string.Compare("http", url.Scheme, true) == 0 || string.Compare("http", url.Scheme, true) == 0) {
					newUrl.Append((isSecure) ? "https" : "http");
				} else {
					newUrl.Append(url.Scheme);
				}

				newUrl.Append("://");
				newUrl.Append(url.Host);

				//None-standard ports?
				if (url.Port != PORT_NUMBER_DEFAULT_HTTP && url.Port != PORT_NUMBER_DEFAULT_HTTPS) {
					newUrl.Append(":");
					newUrl.Append(url.Port);
				}

				newUrl.Append(context.Request.ApplicationPath);

			} else {
				//No context. See if a value is stored in the config file
				newUrl.Append((isSecure) ? "https" : "http");
                newUrl.Append(ConfigurationManager.AppSettings[CONFIG_KEY_DEFAULT_DOMAIN_AND_APPLIACTION_PATH]);
			}

			string urlString = newUrl.ToString(); 

			if (!urlString.EndsWith("/")) {
				return urlString + "/";
			}

			return urlString;
		}

		#region SSL/Non-SSL redirection
		public static string CreateSecureUrlFromNonSecureUrl(string urlString) {
			return CreateSecureUrlFromNonSecureUrl(urlString, PORT_NUMBER_DEFAULT_HTTPS);
		}

		public static string CreateSecureUrlFromNonSecureUrl(string urlString, int newPortNumber) {
			return CreateRedirectUrl(urlString, SCHEMA_HTTPS, newPortNumber);
		}

		public static string CreateSecureUrlFromNonSecureUrl(Uri url) {
			return CreateRedirectUrl(url, SCHEMA_HTTPS, PORT_NUMBER_DEFAULT_HTTPS);
		}

		public static string CreateSecureUrlFromNonSecureUrl(Uri url, int newPortNumber) {
			return CreateRedirectUrl(url, SCHEMA_HTTPS, newPortNumber);
		}

		public static string CreateNonSecureUrlFromSecureUrl(string urlString) {
			return CreateNonSecureUrlFromSecureUrl(urlString, PORT_NUMBER_DEFAULT_HTTP);
		}

		public static string CreateNonSecureUrlFromSecureUrl(string urlString, int newPortNumber) {
			return CreateRedirectUrl(urlString, SCHEMA_HTTP, newPortNumber);
		}
		
		public static string CreateNonSecureUrlFromSecureUrl(Uri url, int newPortNumber) {
			return CreateRedirectUrl(url, SCHEMA_HTTP, newPortNumber);
		}

		private static string CreateRedirectUrl(string urlString, string newScheme, int newPortNumber) {
			try {
				Uri url = new Uri(urlString);
				return CreateRedirectUrl(url, newScheme, newPortNumber);
			} catch {
				return urlString;
			}
		}

		private static string CreateRedirectUrl(Uri url, string newScheme, int newPortNumber) {

			string newUrl = newScheme + "://" + url.Host;

			if (newPortNumber != PORT_NUMBER_DEFAULT_HTTP && newPortNumber != PORT_NUMBER_DEFAULT_HTTPS) {
				newUrl += ":" + newPortNumber;
			}

			foreach (string segment in url.Segments) {
				newUrl += segment;
			}

			if (url.Query != null && url.Query.Length > 0) {
				newUrl += url.Query;
			}

			return newUrl;
		}
		#endregion
	}
}