using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System;

namespace Cuyahoga.Modules.ECommerce.Util {
	/// <summary>
	/// Summary description for StringUtils.
	/// </summary>
	public class StringUtils {

		private StringUtils() {
		}

		//Useful for random string text
		public const string CAR_PLATE_VALUES = "123456789ABCDEFGHJKLMNPRTVWXY";

		public const string CRLF = "\r\n";

		public const string REGEXP_VALID_INTERNET_CHAR = @"\w\-|_";
		public const string REGEXP_VALID_INTERNET_EMAIL_CHAR = REGEXP_VALID_INTERNET_CHAR + @"\.";
		public const string REGEXP_VALID_IP_ADDRESS = @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}";
		public const string REGEXP_VALID_DECIMAL = @"(\-)?(\d+\.)?\d+";
		public const string REGEXP_VALID_INTEGER = @"(\-)?\d+";
		public const string REGEXP_VALID_BOOLEAN = @"(True|False)";
		public const string REGEXP_VALID_SQL_DATE = @"\d{4}-\d{1,2}-\d{1,2}(\s\d{1,2}:\d{1,2}:\d{1,2}(\.\d+)?)?";
		public const string REGEXP_VALID_UK_POSTCODE = @"([a-zA-Z]\w{1,3})\s?(\d[a-zA-Z]{2})";
		public const string REGEXP_VALID_UK_POSTCODE_PARTIAL = @"([a-zA-Z]\w{1,3})\s?(\d[a-zA-Z]{2})?";
		
		public const string REGEXP_VALID_EMAIL_ADDRESS = @"[" + REGEXP_VALID_INTERNET_EMAIL_CHAR + @"]+"
			+ @"[" + REGEXP_VALID_INTERNET_CHAR + "]+"
			+ @"@[" + REGEXP_VALID_INTERNET_EMAIL_CHAR + @"]+\.\w{2,3}";

		//Kept for compatability
		public const string REGEXP_EMAIL_ADDRESS = REGEXP_VALID_EMAIL_ADDRESS;

		public const string REGEXP_VALID_TELEPHONE_NUMBER = @"(\+\d+\s*)?"  // +44
			+ @"(\(?\d+\)|\d+\s+)" //(0) or (01865) or 01865<space>
			+ @"\s*\d+" // lots of numbers
			+ @"(\s+\d+)?"; //plus some more optional numbers

		public const string REGEXP_VALID_URL = @"(http)?(s)?(:\/\/)?" //protocol
			+ "([" + REGEXP_VALID_INTERNET_CHAR + @"]+\.){2,}"  //www.igentics or www.store.wyko.co
			+ @"\w{2,4}" //.com or .uk or .info
			+ @"(:\d+)?" //Port Number
			+ @"(\/[" + REGEXP_VALID_INTERNET_CHAR + @"\/]*|\b)";

		/// <summary>
		/// Removes trailing carriage returns from a value.
		/// Useful when data has been cut and pasted
		/// </summary>
		/// <param name="itemValue">Value to be stripped</param>
		/// <returns></returns>
		public static string StripTrailingCrLf(string itemValue) {
			
			if (itemValue != null && itemValue.EndsWith(CRLF)) {
				
				int crlfIndex = itemValue.LastIndexOf(CRLF);

				if (crlfIndex < itemValue.Length - 1) {
					itemValue = itemValue.Substring(0, itemValue.Length - CRLF.Length);
				}
			}

			return itemValue;
		}

		/// <summary>
		/// Performs a standard VB Left, and allows strings
		/// shorter than the requested length
		/// </summary>
		/// <param name="input"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string Left(string input, int length) {
			if (input == null) {
				return "";
			}

			if (input.Length < length) {
				return input;
			}

			return input.Substring(0, length);
		}

		/// <summary>
		/// Performs a standard VB Right, and allows strings
		/// shorter than the requested length
		/// </summary>
		/// <param name="input"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string Right(string input, int length) {

			if (input == null) {
				return "";
			}

			if (input.Length < length) {
				return input;
			}

			return input.Substring(input.Length - length, length);
		}

		public static string ExtractSimpleEmail(string emailAddress) {

			string cleanEmail = emailAddress;
			Regex regexp = new Regex(REGEXP_VALID_EMAIL_ADDRESS);
			foreach (Match m in regexp.Matches(emailAddress)) {
				cleanEmail = m.ToString();
				break;
			}
			return cleanEmail;

		}

		public static bool IsEmailAddress(string emailAddress) {

			bool isEmail = false;

			Regex regexp = new Regex("^" + REGEXP_VALID_EMAIL_ADDRESS + "$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

			if (regexp.Matches(emailAddress).Count > 0) {
				isEmail = true;
			}
			return isEmail;

		}

		public static bool IsUkPostCode(string postcode) {

			if (postcode == null) {
				return false;
			}

			bool isPostcode = false;

			Regex regexp = new Regex("^" + REGEXP_VALID_UK_POSTCODE + "$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

			if (regexp.Matches(postcode).Count > 0) {
				isPostcode = true;
			}
			return isPostcode;

		}

		public static string NormalisePostcode(string postcode) {

			postcode = postcode.Trim().ToUpper();

			//Add a space if non is supplied
			if (postcode.IndexOf(" ") < 0 && postcode.Length > 3) {
				postcode = postcode.Substring(0, postcode.Length - 3) + " " + postcode.Substring(postcode.Length - 3);
			}

			//Now check the format
			if (IsUkPostCode(postcode)) {
				return postcode;
			} else {
				throw new ArgumentException("Invalid postcode");
			}
		}

		public static string RemoveEmailFromText(string text, string replacementText) {
			
			string newRE = REGEXP_VALID_EMAIL_ADDRESS.Replace("@", @"\s*(@|at)\s*");

			Regex re = new Regex(newRE, RegexOptions.Multiline | RegexOptions.IgnoreCase);
			return re.Replace(text, replacementText);
		}

		public static string RemoveHtmlFromText(string text, string replacementText) {

			Regex re = new Regex("<.*?>", RegexOptions.Multiline);
			return re.Replace(text, replacementText);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="replacementText"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string RemoveUrlFromText(string text, string replacementText) {

			Regex re = new Regex(REGEXP_VALID_URL, RegexOptions.Multiline | RegexOptions.IgnoreCase);

			return re.Replace(text, replacementText);

		}

		public static string RemoveTelephoneNumberFromText(string text, string replacementText) {

			Regex re = new Regex(REGEXP_VALID_TELEPHONE_NUMBER, RegexOptions.Multiline | RegexOptions.IgnoreCase);

			return re.Replace(text, replacementText);

		}

		public static string MakeItFit(string text, int maxlength) {
			if (text.Length > maxlength) {
				text = text.Substring(0, maxlength) + "...";
			}
			return text;
		}

		public static string ToCamelCase(string text) {

			if (text != null && text.Length > 1) {
				
				//All upper case is converted to lower case
				if (text.ToUpper() == text) {
					return text.ToLower();
				}

				return text.Substring(0, 1).ToLower() + text.Substring(1, text.Length - 1);

			} else {
				return text;
			}
		}

		public static string ToSqlString(DateTime date) {
			return date.ToString("yyyy-MM-dd");
		}

		public static string ToLongSqlString(DateTime date) {
			return date.ToString("yyyy-MM-dd hh:mm:ss");
		}

		public static string GetValidationExpression(System.Type type) {
			return GetValidationExpression(type.Name);
		}

		/// <summary>
		/// Gets a regular expression that may be used to validate string versions of the specified type
		/// </summary>
		/// <param name="objectType"></param>
		/// <returns></returns>
		public static string GetValidationExpression(string objectType) {

			string regexp;
			bool addBeginningEndChars = true;

			switch (objectType) {
				case "System.Decimal":
				case "System.Double":
				case "System.Single":
				case "float":
				case "double":
				case "decimal":
					regexp = REGEXP_VALID_DECIMAL;
					break;
				case "System.Int16":
				case "System.Int32":
				case "System.Int64":
				case "int":
					regexp = REGEXP_VALID_INTEGER;
					break;
				case "System.Date":
					regexp = REGEXP_VALID_SQL_DATE;
					break;
				case "System.String":
				case "string":
					regexp = ".+";
					addBeginningEndChars = false;
					break;
				case "System.Uri":
					regexp = REGEXP_VALID_URL;
					break;
				case "System.Boolean":
				case "bool":
					regexp = REGEXP_VALID_BOOLEAN;
					break;
				case "IpAddress":
					regexp = REGEXP_VALID_IP_ADDRESS;
					break;
				case "EmailAddress":
					regexp = REGEXP_VALID_EMAIL_ADDRESS;
					break;
				case "TelephoneNumber":
					regexp = REGEXP_VALID_TELEPHONE_NUMBER;
					break;
				default:
					regexp = ".+";
					addBeginningEndChars = false;
					break;
			}

			if (addBeginningEndChars) {
				return "^" + regexp + "$";
			} else {
				return regexp;
			}
		}

		public static string GenerateRandomText(int length) {
			return GenerateRandomText(length, CAR_PLATE_VALUES);
		}

		public static string GenerateRandomText(int length, string possibleValues) {
			return GenerateRandomText(length, possibleValues, new Random(DateTime.Now.Millisecond));
		}

		public static string GenerateRandomText(int length, string possibleValues, Random random) {

			//this ensures that the error code that is returned looks more important than it is
			string strString = "";

			for (int i = 0; i < length; i++) {
				int index =  random.Next(possibleValues.Length);
				strString += possibleValues[index];
			}

			return strString;
		}

		public static NameValueCollection GetNameValueCollection(string response, string pairSeparator, string valueSeparator) {
			
			NameValueCollection values = new NameValueCollection();

			string[] parameters = response.Split(pairSeparator.ToCharArray());

			for (int i = 0; i < parameters.Length; i++) {
				
				string nameValue = parameters[i];

				if (nameValue.IndexOf(valueSeparator) > 0) {
					string[] nameValuePair = nameValue.Split(valueSeparator.ToCharArray());
					values.Add(nameValuePair[0].Trim(), nameValuePair[1].Trim());
				}
			}

			return values;
		}
	}
}