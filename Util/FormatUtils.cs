using System;
using System.Globalization;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Summary description for DateUtils.
	/// </summary>
	public sealed class FormatUtils {
	
		private FormatUtils() {
		}

		public static string FormatSqlDate(DateTime date) {
			return date.ToString("yyyy-MM-dd");
		}

		public static string FormatShortDate(DateTime date, string cultureCode) {
			return date.ToString("d", new CultureInfo(cultureCode).DateTimeFormat);
		}

		public static string FormatShortTime(DateTime date, string cultureCode) {
			return date.ToString("t", new CultureInfo(cultureCode).DateTimeFormat);
		}

		public static string FormatShortDateTime(DateTime date, string cultureCode) {
			return FormatShortDate(date, cultureCode) + " " + FormatShortTime(date, cultureCode);
		}
	}
}
