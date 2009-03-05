using System;
using System.Xml.Serialization;
using System.Globalization;

namespace Cuyahoga.Modules.ECommerce.Util {

    /// <summary>
    /// Encapsulates locale specific currency and formatting options.
    /// </summary>
    /// <remarks>
	/// Currently, there is no way of creating a currency object purely from a
	/// currency code as this does not uniquely define the numeric currency code,
	/// formatting options or other locale-specific options such as currency symbols,
	/// symbol placement etc.
	/// 
	/// Incorrect setting of culture information will break the ToString() method
	/// of Money
	/// </remarks>
    public class Currency : IFormatProvider, ICustomFormatter {

        public const int CURRENCY_CODE_LENGTH = 3;

		private const int CULTURE_CODE_LENGTH = 2;
        private const int FRACTION_DIGITS_DEFAULT = 2;
        public const string FORMAT_PLAIN = "P";

        private string _currencyCode = "";
        private int _currencyCodeNumeric;
        private int _fractionDigits = FRACTION_DIGITS_DEFAULT;
        private CultureInfo _cultureInfo;
        private bool _isCultureCurrency = false;

        public static Currency CreateDefaultCurrency(string cultureCode) {
            return new Currency(new CultureInfo(cultureCode));
        }

        public static Currency CreateDefaultCurrency(CultureInfo cultureInfo) {
            return new Currency(cultureInfo);
        }

        public Currency(CultureInfo cultureInfo) {
			SetCultureInfo(cultureInfo);
        }

        public Currency (string cultureCode) {
			SetCultureInfo(cultureCode);
        }

        public Currency() {
			SetCultureInfo(CultureInfo.CurrentCulture);
        }

		/// <summary>
		/// Used ONLY to deserialize this object. Should not be used for any other purpose.
		/// </summary>
		[XmlAttribute]
		public string CultureCode {
			get {
				return _cultureInfo.Name;
			}
			set {
				SetCultureInfo(value);
			}
		}

		private void SetCultureInfo(string cultureCode) {
			SetCultureInfo(GetCultureInfo(cultureCode));
		}

		private void SetCultureInfo(CultureInfo culture) {
			_cultureInfo = culture;
            _fractionDigits = culture.NumberFormat.CurrencyDecimalDigits;
            _isCultureCurrency = true;
			GetCountryData();
		}

		private CultureInfo GetCultureInfo(string cultureCode) {

			if (cultureCode == null || cultureCode.Length < CULTURE_CODE_LENGTH) {
				return null;
			}

			//Might be a partial culture code, or country code
			//Two character locales are a bit suspect as they don't have a country component
			if (cultureCode.Length == CULTURE_CODE_LENGTH) {
				try {
					return new CultureInfo(cultureCode.ToLower() + "-" + cultureCode.ToUpper());
				} catch {}
			}

            //This might be a valid two character locale (still won't know the country, though)
			try {
				return new CultureInfo(cultureCode);
			} catch {}

			//OK, assume this is a country code
			//now have to find something that ends with this code
			CultureInfo[] cultureList = CultureInfo.GetCultures(CultureTypes.AllCultures);
			string countryCode = cultureCode.ToUpper();

			foreach (CultureInfo cinfo in cultureList) {
				if (cinfo.Name.EndsWith(countryCode)) {
					return cinfo;
				}
			}

			return null;
		}

		[XmlIgnore]
        public CultureInfo CultureInfo {
            get {
                return _cultureInfo;
            }
        }

		[XmlIgnore]
		public int FractionDigits {
            get {
                return _fractionDigits;
            }
        }

        /// <summary>
        /// Indicates whether the currency code is the default used for the supplied culture
        /// </summary>
        [XmlIgnore]
        public bool IsCultureCurrency {
            get {
                return _isCultureCurrency;
            }
        }

        /// <summary>
        /// Gets the 3-Char ISO 4217 currency code of this currency
        /// </summary>
        /// <remarks>Setter exists purely for serialization purposes and should not be used</remarks>
        [XmlAttribute]
        public string CurrencyCode {
            get {
                return _currencyCode;
            }
			set {
                if (value == null || value.Length != CURRENCY_CODE_LENGTH) {
                    throw new ArgumentException("Invalid currency code: " + ((value != null) ? value : "NULL"));
                }

                if (value != _currencyCode) {
                    _currencyCode = value;
                    _currencyCodeNumeric = 0;
                    _fractionDigits = FRACTION_DIGITS_DEFAULT;
                    _isCultureCurrency = false;
                }
			}
        }    

        /// <summary>
        /// Gets the 3-Digit ISO 4217 Country code of this currency
        /// </summary>
		[XmlAttribute]
		public int CurrencyCodeNumeric {
            get {
                return _currencyCodeNumeric;
            }
            set {
                _currencyCodeNumeric = value;
            }
        }  
 
        /// <summary>
        /// Gets the 2-Char ISO 3166 country code for this currency
        /// </summary>
        private string CountryCode {
            get {
                if (_cultureInfo != null && _cultureInfo.Name.Length == 5) {
                    return _cultureInfo.Name.Substring(3, 2);
                } else {
                    throw new ArgumentException("Invalid Culture");
                }
            }
        }
 
        private void GetCountryData() {

            switch (CountryCode.ToUpper()) {
                    
                case "AD":
                    _currencyCodeNumeric = 20;
                    _currencyCode = "EUR";
                    break;

                case "AE":
                    _currencyCodeNumeric = 784;
                    _currencyCode = "AED";
                    break;

                case "AF":
                    _currencyCodeNumeric = 4;
                    _currencyCode = "AFA";
                    break;

                case "AG":
                    _currencyCodeNumeric = 28;
                    _currencyCode = "XCD";
                    break;

                case "AI":
                    _currencyCodeNumeric = 660;
                    _currencyCode = "XCD";
                    break;

                case "AL":
                    _currencyCodeNumeric = 8;
                    _currencyCode = "ALL";
                    break;

                case "AN":
                    _currencyCodeNumeric = 530;
                    _currencyCode = "ANG";
                    break;

                case "AO":
                    _currencyCodeNumeric = 24;
                    _currencyCode = "AOK";
                    break;

                case "AR":
                    _currencyCodeNumeric = 32;
                    _currencyCode = "ARS";
                    break;

                case "AS":
                    _currencyCodeNumeric = 16;
                    _currencyCode = "USD";
                    break;

                case "AT":
                    _currencyCodeNumeric = 40;
                    _currencyCode = "EUR";
                    break;

                case "AU":
                    _currencyCodeNumeric = 36;
                    _currencyCode = "AUD";
                    break;

                case "AW":
                    _currencyCodeNumeric = 533;
                    _currencyCode = "AWG";
                    break;

                case "AZ":
                    _currencyCodeNumeric = 31;
                    _currencyCode = "AJM";
                    break;

                case "BA":
                    _currencyCodeNumeric = 70;
                    _currencyCode = "BAD";
                    break;

                case "BB":
                    _currencyCodeNumeric = 52;
                    _currencyCode = "BBD";
                    break;

                case "BD":
                    _currencyCodeNumeric = 50;
                    _currencyCode = "BDT";
                    break;

                case "BE":
                    _currencyCodeNumeric = 56;
                    _currencyCode = "EUR";
                    break;

                case "BF":
                    _currencyCodeNumeric = 854;
                    _currencyCode = "XOF";
                    break;

                case "BG":
                    _currencyCodeNumeric = 100;
                    _currencyCode = "BGL";
                    break;

                case "BH":
                    _currencyCodeNumeric = 48;
                    _currencyCode = "BHD";
                    break;

                case "BI":
                    _currencyCodeNumeric = 108;
                    _currencyCode = "BIF";
                    break;

                case "BJ":
                    _currencyCodeNumeric = 204;
                    _currencyCode = "XOF";
                    break;

                case "BM":
                    _currencyCodeNumeric = 60;
                    _currencyCode = "BMD";
                    break;

                case "BN":
                    _currencyCodeNumeric = 96;
                    _currencyCode = "BND";
                    break;

                case "BO":
                    _currencyCodeNumeric = 68;
                    _currencyCode = "BOB";
                    break;

                case "BR":
                    _currencyCodeNumeric = 76;
                    _currencyCode = "BRE";
                    break;

                case "BS":
                    _currencyCodeNumeric = 44;
                    _currencyCode = "BSD";
                    break;

                case "BT":
                    _currencyCodeNumeric = 64;
                    _currencyCode = "BTN";
                    break;

                case "BV":
                    _currencyCodeNumeric = 74;
                    _currencyCode = "NOK";
                    break;

                case "BW":
                    _currencyCodeNumeric = 72;
                    _currencyCode = "BWP";
                    break;

                case "BY":
                    _currencyCodeNumeric = 112;
                    _currencyCode = "BYB";
                    break;

                case "BZ":
                    _currencyCodeNumeric = 84;
                    _currencyCode = "BZD";
                    break;

                case "CA":
                    _currencyCodeNumeric = 124;
                    _currencyCode = "CAD";
                    break;

                case "CC":
                    _currencyCodeNumeric = 166;
                    _currencyCode = "AUD";
                    break;

                case "CF":
                    _currencyCodeNumeric = 140;
                    _currencyCode = "XAF";
                    break;

                case "CG":
                    _currencyCodeNumeric = 178;
                    _currencyCode = "XAF";
                    break;

                case "CH":
                    _currencyCodeNumeric = 756;
                    _currencyCode = "CHF";
                    break;

                case "CI":
                    _currencyCodeNumeric = 384;
                    _currencyCode = "XOF";
                    break;

                case "CK":
                    _currencyCodeNumeric = 184;
                    _currencyCode = "NZD";
                    break;

                case "CL":
                    _currencyCodeNumeric = 152;
                    _currencyCode = "CLP";
                    break;

                case "CM":
                    _currencyCodeNumeric = 120;
                    _currencyCode = "XAF";
                    break;

                case "CN":
                    _currencyCodeNumeric = 156;
                    _currencyCode = "CNY";
                    break;

                case "CO":
                    _currencyCodeNumeric = 170;
                    _currencyCode = "COP";
                    break;

                case "CR":
                    _currencyCodeNumeric = 188;
                    _currencyCode = "CRC";
                    break;

                case "CU":
                    _currencyCodeNumeric = 192;
                    _currencyCode = "CUP";
                    break;

                case "CV":
                    _currencyCodeNumeric = 132;
                    _currencyCode = "CVE";
                    break;

                case "CX":
                    _currencyCodeNumeric = 162;
                    _currencyCode = "AUD";
                    break;

                case "CY":
                    _currencyCodeNumeric = 196;
                    _currencyCode = "CYP";
                    break;

                case "CZ":
                    _currencyCodeNumeric = 203;
                    _currencyCode = "CZK";
                    break;

                case "DE":
                    _currencyCodeNumeric = 276;
                    _currencyCode = "EUR";
                    break;

                case "DJ":
                    _currencyCodeNumeric = 262;
                    _currencyCode = "DJF";
                    break;

                case "DK":
                    _currencyCodeNumeric = 208;
                    _currencyCode = "DKK";
                    break;

                case "DM":
                    _currencyCodeNumeric = 212;
                    _currencyCode = "XCD";
                    break;

                case "DO":
                    _currencyCodeNumeric = 214;
                    _currencyCode = "DOP";
                    break;

                case "DZ":
                    _currencyCodeNumeric = 12;
                    _currencyCode = "DZD";
                    break;

                case "EC":
                    _currencyCodeNumeric = 218;
                    _currencyCode = "ECS";
                    break;

                case "EE":
                    _currencyCodeNumeric = 233;
                    _currencyCode = "EEK";
                    break;

                case "EG":
                    _currencyCodeNumeric = 818;
                    _currencyCode = "EGP";
                    break;

                case "EH":
                    _currencyCodeNumeric = 732;
                    _currencyCode = "MAD";
                    break;

                case "ES":
                    _currencyCodeNumeric = 724;
                    _currencyCode = "EUR";
                    break;

                case "ET":
                    _currencyCodeNumeric = 231;
                    _currencyCode = "ETB";
                    break;

                case "FI":
                    _currencyCodeNumeric = 246;
                    _currencyCode = "EUR";
                    break;

                case "FJ":
                    _currencyCodeNumeric = 242;
                    _currencyCode = "FJD";
                    break;

                case "FK":
                    _currencyCodeNumeric = 238;
                    _currencyCode = "FKP";
                    break;

                case "FM":
                    _currencyCodeNumeric = 583;
                    _currencyCode = "USD";
                    break;

                case "FO":
                    _currencyCodeNumeric = 234;
                    _currencyCode = "DKK";
                    break;

                case "FR":
                    _currencyCodeNumeric = 250;
                    _currencyCode = "EUR";
                    break;

                case "GA":
                    _currencyCodeNumeric = 266;
                    _currencyCode = "XAF";
                    break;

                case "GB":
                    _currencyCodeNumeric = 826;
                    _currencyCode = "GBP";
                    break;

                case "GD":
                    _currencyCodeNumeric = 308;
                    _currencyCode = "XCD";
                    break;

                case "GF":
                    _currencyCodeNumeric = 254;
                    _currencyCode = "EUR";
                    break;

                case "GH":
                    _currencyCodeNumeric = 288;
                    _currencyCode = "GHC";
                    break;

                case "GI":
                    _currencyCodeNumeric = 292;
                    _currencyCode = "GIP";
                    break;

                case "GL":
                    _currencyCodeNumeric = 304;
                    _currencyCode = "DKK";
                    break;

                case "GM":
                    _currencyCodeNumeric = 270;
                    _currencyCode = "GMD";
                    break;

                case "GN":
                    _currencyCodeNumeric = 324;
                    _currencyCode = "GNF";
                    break;

                case "GP":
                    _currencyCodeNumeric = 312;
                    _currencyCode = "EUR";
                    break;

                case "GQ":
                    _currencyCodeNumeric = 226;
                    _currencyCode = "XAF";
                    break;

                case "GR":
                    _currencyCodeNumeric = 300;
                    _currencyCode = "EUR";
                    break;

                case "GT":
                    _currencyCodeNumeric = 320;
                    _currencyCode = "GTQ";
                    break;

                case "GU":
                    _currencyCodeNumeric = 316;
                    _currencyCode = "USD";
                    break;

                case "GW":
                    _currencyCodeNumeric = 624;
                    _currencyCode = "GWP";
                    break;

                case "GY":
                    _currencyCodeNumeric = 328;
                    _currencyCode = "GYD";
                    break;

                case "HK":
                    _currencyCodeNumeric = 344;
                    _currencyCode = "HKD";
                    break;

                case "HN":
                    _currencyCodeNumeric = 340;
                    _currencyCode = "HNL";
                    break;

                case "HR":
                    _currencyCodeNumeric = 191;
                    _currencyCode = "HRK";
                    break;

                case "HT":
                    _currencyCodeNumeric = 332;
                    _currencyCode = "HTG";
                    break;

                case "HU":
                    _currencyCodeNumeric = 348;
                    _currencyCode = "HUF";
                    break;

                case "ID":
                    _currencyCodeNumeric = 360;
                    _currencyCode = "IDR";
                    break;

                case "IE":
                    _currencyCodeNumeric = 372;
                    _currencyCode = "EUR";
                    break;

                case "IL":
                    _currencyCodeNumeric = 376;
                    _currencyCode = "ILS";
                    break;

                case "IN":
                    _currencyCodeNumeric = 356;
                    _currencyCode = "INR";
                    break;

                case "IO":
                    _currencyCodeNumeric = 86;
                    _currencyCode = "USD";
                    break;

                case "IQ":
                    _currencyCodeNumeric = 368;
                    _currencyCode = "IQD";
                    break;

                case "IR":
                    _currencyCodeNumeric = 364;
                    _currencyCode = "IRR";
                    break;

                case "IS":
                    _currencyCodeNumeric = 352;
                    _currencyCode = "ISK";
                    break;

                case "IT":
                    _currencyCodeNumeric = 380;
                    _currencyCode = "EUR";
                    break;

                case "JM":
                    _currencyCodeNumeric = 388;
                    _currencyCode = "JMD";
                    break;

                case "JO":
                    _currencyCodeNumeric = 400;
                    _currencyCode = "JOD";
                    break;

                case "JP":
                    _currencyCodeNumeric = 392;
                    _currencyCode = "JPY";
                    break;

                case "KE":
                    _currencyCodeNumeric = 404;
                    _currencyCode = "KES";
                    break;

                case "KG":
                    _currencyCodeNumeric = 417;
                    _currencyCode = "KGS";
                    break;

                case "KH":
                    _currencyCodeNumeric = 116;
                    _currencyCode = "KHR";
                    break;

                case "KI":
                    _currencyCodeNumeric = 296;
                    _currencyCode = "AUD";
                    break;

                case "KM":
                    _currencyCodeNumeric = 174;
                    _currencyCode = "KMF";
                    break;

                case "KN":
                    _currencyCodeNumeric = 659;
                    _currencyCode = "XCD";
                    break;

                case "KP":
                    _currencyCodeNumeric = 408;
                    _currencyCode = "KPW";
                    break;

                case "KR":
                    _currencyCodeNumeric = 410;
                    _currencyCode = "KRW";
                    break;

                case "KW":
                    _currencyCodeNumeric = 414;
                    _currencyCode = "KWD";
                    break;

                case "KY":
                    _currencyCodeNumeric = 136;
                    _currencyCode = "KYD";
                    break;

                case "KZ":
                    _currencyCodeNumeric = 398;
                    _currencyCode = "KZT";
                    break;

                case "LA":
                    _currencyCodeNumeric = 418;
                    _currencyCode = "LAK";
                    break;

                case "LB":
                    _currencyCodeNumeric = 422;
                    _currencyCode = "LBP";
                    break;

                case "LC":
                    _currencyCodeNumeric = 662;
                    _currencyCode = "XCD";
                    break;

                case "LI":
                    _currencyCodeNumeric = 438;
                    _currencyCode = "CHF";
                    break;

                case "LK":
                    _currencyCodeNumeric = 144;
                    _currencyCode = "LKR";
                    break;

                case "LR":
                    _currencyCodeNumeric = 430;
                    _currencyCode = "LRD";
                    break;

                case "LS":
                    _currencyCodeNumeric = 426;
                    _currencyCode = "LSL";
                    break;

                case "LT":
                    _currencyCodeNumeric = 440;
                    _currencyCode = "LTL";
                    break;

                case "LU":
                    _currencyCodeNumeric = 442;
                    _currencyCode = "EUR";
                    break;

                case "LV":
                    _currencyCodeNumeric = 428;
                    _currencyCode = "LVL";
                    break;

                case "LY":
                    _currencyCodeNumeric = 434;
                    _currencyCode = "LYD";
                    break;

                case "MA":
                    _currencyCodeNumeric = 504;
                    _currencyCode = "MAD";
                    break;

                case "MC":
                    _currencyCodeNumeric = 492;
                    _currencyCode = "EUR";
                    break;

                case "MD":
                    _currencyCodeNumeric = 498;
                    _currencyCode = "MDL";
                    break;

                case "MG":
                    _currencyCodeNumeric = 450;
                    _currencyCode = "MGF";
                    break;

                case "MK":
                    _currencyCodeNumeric = 807;
                    _currencyCode = "MKD";
                    break;

                case "ML":
                    _currencyCodeNumeric = 466;
                    _currencyCode = "XOF";
                    break;

                case "MM":
                    _currencyCodeNumeric = 104;
                    _currencyCode = "MMK";
                    break;

                case "MN":
                    _currencyCodeNumeric = 496;
                    _currencyCode = "MNT";
                    break;

                case "MO":
                    _currencyCodeNumeric = 446;
                    _currencyCode = "MOP";
                    break;

                case "MP":
                    _currencyCodeNumeric = 580;
                    _currencyCode = "USD";
                    break;

                case "MQ":
                    _currencyCodeNumeric = 474;
                    _currencyCode = "EUR";
                    break;

                case "MR":
                    _currencyCodeNumeric = 478;
                    _currencyCode = "MRO";
                    break;

                case "MS":
                    _currencyCodeNumeric = 500;
                    _currencyCode = "XCD";
                    break;

                case "MT":
                    _currencyCodeNumeric = 470;
                    _currencyCode = "MTL";
                    break;

                case "MU":
                    _currencyCodeNumeric = 480;
                    _currencyCode = "MUR";
                    break;

                case "MV":
                    _currencyCodeNumeric = 462;
                    _currencyCode = "MVR";
                    break;

                case "MW":
                    _currencyCodeNumeric = 454;
                    _currencyCode = "MWK";
                    break;

                case "MX":
                    _currencyCodeNumeric = 484;
                    _currencyCode = "MXP";
                    break;

                case "MY":
                    _currencyCodeNumeric = 458;
                    _currencyCode = "MYR";
                    break;

                case "MZ":
                    _currencyCodeNumeric = 508;
                    _currencyCode = "MZM";
                    break;

                case "NA":
                    _currencyCodeNumeric = 516;
                    _currencyCode = "ZAR";
                    break;

                case "NC":
                    _currencyCodeNumeric = 540;
                    _currencyCode = "XPF";
                    break;

                case "NE":
                    _currencyCodeNumeric = 562;
                    _currencyCode = "XOF";
                    break;

                case "NF":
                    _currencyCodeNumeric = 574;
                    _currencyCode = "AUD";
                    break;

                case "NG":
                    _currencyCodeNumeric = 566;
                    _currencyCode = "NGN";
                    break;

                case "NI":
                    _currencyCodeNumeric = 558;
                    _currencyCode = "NIO";
                    break;

                case "NL":
                    _currencyCodeNumeric = 528;
                    _currencyCode = "EUR";
                    break;

                case "NO":
                    _currencyCodeNumeric = 578;
                    _currencyCode = "NOK";
                    break;

                case "NP":
                    _currencyCodeNumeric = 524;
                    _currencyCode = "NPR";
                    break;

                case "NR":
                    _currencyCodeNumeric = 520;
                    _currencyCode = "AUD";
                    break;

                case "NU":
                    _currencyCodeNumeric = 570;
                    _currencyCode = "NZD";
                    break;

                case "NZ":
                    _currencyCodeNumeric = 554;
                    _currencyCode = "NZD";
                    break;

                case "OM":
                    _currencyCodeNumeric = 512;
                    _currencyCode = "OMR";
                    break;

                case "PA":
                    _currencyCodeNumeric = 591;
                    _currencyCode = "PAB";
                    break;


                case "PE":
                    _currencyCodeNumeric = 604;
                    _currencyCode = "PEN";
                    break;

                case "PF":
                    _currencyCodeNumeric = 258;
                    _currencyCode = "XPF";
                    break;

                case "PG":
                    _currencyCodeNumeric = 598;
                    _currencyCode = "PGK";
                    break;

                case "PH":
                    _currencyCodeNumeric = 608;
                    _currencyCode = "PHP";
                    break;

                case "PK":
                    _currencyCodeNumeric = 586;
                    _currencyCode = "PKR";
                    break;

                case "PL":
                    _currencyCodeNumeric = 616;
                    _currencyCode = "PLZ";
                    break;

                case "PM":
                    _currencyCodeNumeric = 666;
                    _currencyCode = "EUR";
                    break;

                case "PN":
                    _currencyCodeNumeric = 612;
                    _currencyCode = "NZD";
                    break;

                case "PR":
                    _currencyCodeNumeric = 630;
                    _currencyCode = "USD";
                    break;

                case "PT":
                    _currencyCodeNumeric = 620;
                    _currencyCode = "EUR";
                    break;

                case "PW":
                    _currencyCodeNumeric = 585;
                    _currencyCode = "USD";
                    break;

                case "PY":
                    _currencyCodeNumeric = 600;
                    _currencyCode = "PYG";
                    break;

                case "QA":
                    _currencyCodeNumeric = 634;
                    _currencyCode = "QAR";
                    break;

                case "RE":
                    _currencyCodeNumeric = 638;
                    _currencyCode = "EUR";
                    break;

                case "RO":
                    _currencyCodeNumeric = 642;
                    _currencyCode = "ROL";
                    break;

                case "RU":
                    _currencyCodeNumeric = 643;
                    _currencyCode = "RUR";
                    break;

                case "RW":
                    _currencyCodeNumeric = 646;
                    _currencyCode = "RWF";
                    break;

                case "SA":
                    _currencyCodeNumeric = 682;
                    _currencyCode = "SAR";
                    break;

                case "SB":
                    _currencyCodeNumeric = 90;
                    _currencyCode = "SBD";
                    break;

                case "SC":
                    _currencyCodeNumeric = 690;
                    _currencyCode = "SCR";
                    break;

                case "SD":
                    _currencyCodeNumeric = 736;
                    _currencyCode = "SDP";
                    break;

                case "SE":
                    _currencyCodeNumeric = 752;
                    _currencyCode = "SEK";
                    break;

                case "SG":
                    _currencyCodeNumeric = 702;
                    _currencyCode = "SGD";
                    break;

                case "SH":
                    _currencyCodeNumeric = 654;
                    _currencyCode = "SHP";
                    break;

                case "SI":
                    _currencyCodeNumeric = 705;
                    _currencyCode = "SIT";
                    break;

                case "SJ":
                    _currencyCodeNumeric = 744;
                    _currencyCode = "NOK";
                    break;

                case "SK":
                    _currencyCodeNumeric = 703;
                    _currencyCode = "SKK";
                    break;

                case "SL":
                    _currencyCodeNumeric = 694;
                    _currencyCode = "SLL";
                    break;

                case "SM":
                    _currencyCodeNumeric = 674;
                    _currencyCode = "EUR";
                    break;

                case "SN":
                    _currencyCodeNumeric = 686;
                    _currencyCode = "XOF";
                    break;

                case "SO":
                    _currencyCodeNumeric = 706;
                    _currencyCode = "SOS";
                    break;

                case "SR":
                    _currencyCodeNumeric = 740;
                    _currencyCode = "SRG";
                    break;

                case "ST":
                    _currencyCodeNumeric = 678;
                    _currencyCode = "STD";
                    break;

                case "SV":
                    _currencyCodeNumeric = 222;
                    _currencyCode = "SVC";
                    break;

                case "SY":
                    _currencyCodeNumeric = 760;
                    _currencyCode = "SYP";
                    break;

                case "SZ":
                    _currencyCodeNumeric = 748;
                    _currencyCode = "SZL";
                    break;

                case "TC":
                    _currencyCodeNumeric = 796;
                    _currencyCode = "USD";
                    break;

                case "TD":
                    _currencyCodeNumeric = 148;
                    _currencyCode = "XAF";
                    break;

                case "TG":
                    _currencyCodeNumeric = 768;
                    _currencyCode = "XOF";
                    break;

                case "TH":
                    _currencyCodeNumeric = 764;
                    _currencyCode = "THB";
                    break;

                case "TJ":
                    _currencyCodeNumeric = 762;
                    _currencyCode = "TJS";
                    break;

                case "TK":
                    _currencyCodeNumeric = 772;
                    _currencyCode = "NZD";
                    break;

                case "TM":
                    _currencyCodeNumeric = 795;
                    _currencyCode = "TMM";
                    break;

                case "TN":
                    _currencyCodeNumeric = 788;
                    _currencyCode = "TND";
                    break;

                case "TO":
                    _currencyCodeNumeric = 776;
                    _currencyCode = "TOP";
                    break;

                case "TP":
                    _currencyCodeNumeric = 626;
                    _currencyCode = "TPE";
                    break;

                case "TR":
                    _currencyCodeNumeric = 792;
                    _currencyCode = "TRL";
                    break;

                case "TT":
                    _currencyCodeNumeric = 780;
                    _currencyCode = "TTD";
                    break;

                case "TV":
                    _currencyCodeNumeric = 798;
                    _currencyCode = "AUD";
                    break;

                case "TW":
                    _currencyCodeNumeric = 158;
                    _currencyCode = "TWD";
                    break;

                case "TZ":
                    _currencyCodeNumeric = 834;
                    _currencyCode = "TZS";
                    break;

                case "UA":
                    _currencyCodeNumeric = 804;
                    _currencyCode = "UAG";
                    break;

                case "UG":
                    _currencyCodeNumeric = 800;
                    _currencyCode = "UGX";
                    break;

                case "UM":
                    _currencyCodeNumeric = 581;
                    _currencyCode = "USD";
                    break;

                case "US":
                    _currencyCodeNumeric = 840;
                    _currencyCode = "USD";
                    break;

                case "UY":
                    _currencyCodeNumeric = 858;
                    _currencyCode = "UYP";
                    break;

                case "UZ":
                    _currencyCodeNumeric = 860;
                    _currencyCode = "UZS";
                    break;

                case "VA":
                    _currencyCodeNumeric = 336;
                    _currencyCode = "EUR";
                    break;

                case "VC":
                    _currencyCodeNumeric = 670;
                    _currencyCode = "XCD";
                    break;

                case "VE":
                    _currencyCodeNumeric = 862;
                    _currencyCode = "VEB";
                    break;

                case "VG":
                    _currencyCodeNumeric = 92;
                    _currencyCode = "USD";
                    break;

                case "VI":
                    _currencyCodeNumeric = 850;
                    _currencyCode = "USD";
                    break;

                case "VN":
                    _currencyCodeNumeric = 704;
                    _currencyCode = "VND";
                    break;

                case "VU":
                    _currencyCodeNumeric = 548;
                    _currencyCode = "VUV";
                    break;

                case "WF":
                    _currencyCodeNumeric = 876;
                    _currencyCode = "XPF";
                    break;

                case "WS":
                    _currencyCodeNumeric = 882;
                    _currencyCode = "WST";
                    break;

                case "YE":
                    _currencyCodeNumeric = 887;
                    _currencyCode = "YER";
                    break;

                case "YU":
                    _currencyCodeNumeric = 891;
                    _currencyCode = "YUN";
                    break;

                case "ZA":
                    _currencyCodeNumeric = 710;
                    _currencyCode = "ZAR";
                    break;

                case "ZM":
                    _currencyCodeNumeric = 894;
                    _currencyCode = "ZMK";
                    break;

                default:
                    throw new ArgumentException("Unknown country code");
            }
        }

        #region IFormatProvider Members
        public object GetFormat(Type formatType) {
            return this;
        }
        #endregion

        #region ICustomFormatter Members

        public string Format(string format, object arg, IFormatProvider formatProvider) {

            decimal amount;

            try {
                amount = Convert.ToDecimal(arg);

                if (IsCultureCurrency && format != FORMAT_PLAIN) {
                    return amount.ToString("C", CultureInfo);
                } else {
                    return amount.ToString("f", CultureInfo) + " " + CurrencyCode;
                }
            } catch {
                return arg.ToString() + " " + CurrencyCode;
            }
        }
        #endregion
    }
}