using System;
using System.Globalization;

using Cuyahoga.Modules.ECommerce.Util.Interfaces;
using Cuyahoga.Modules.ECommerce.Util;
using Cuyahoga.Modules.ECommerce.Service.Translation;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Commonly used formatting for HTML.
	/// </summary>
	public sealed class HtmlFormatUtils {

		public HtmlFormatUtils() {
		}

		public static string FormatMoney(Money money) {
			return FormatMoney(money, "en-GB");
		}

		public static string FormatMoney(Money money, string cultureCode) {
			return money.ToString().Replace(" ", "&nbsp;");
		}

		public static string FormatLinePrice(IBasketLine line) {
			return FormatMoney(line.LinePrice);
		}

		public static string FormatUnitLinePrice(IBasketLine line) {
			return FormatMoney(line.UnitLinePrice);
		}

        public static string FormatLinePrice(IBasketLine line, IBasketRules rules, ITextTranslator translator) {
			return FormatMoney(line.LinePrice, rules, translator);
		}

        public static string FormatUnitLinePrice(IBasketLine line, IBasketRules rules, ITextTranslator translator) {
			return FormatMoney(line.UnitLinePrice, rules, translator);
		}

        public static string FormatMoney(Money money, IBasketRules rules, ITextTranslator translator) {
			return FormatMoney(money, "en-GB", rules, translator);
		}

		public static string FormatMoney(Money money, string cultureCode, IBasketRules rules, ITextTranslator translator) {

			if (rules.ShowPrices(WebStoreContext.Current.CurrentUser)) {
				return FormatMoney(money, cultureCode);
			}

			return translator.GetText("no_price_shown");
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