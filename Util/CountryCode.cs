using System;
using System.Globalization;
using System.Resources;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Util.Location {

	/// <summary>
	/// Summary description for Country.
	/// </summary>
	public sealed class CountryCode {

		public const int COUNTRY_CODE_LENGTH = 2;
		public const string COUNTRY_NAME_TRANSLATION_TAG_PREFIX = "country_name_";
		private static string[] _countrycodeList;

		#region ISO 3166-1-alpha-2 code elements
		public const string COUNTRY_CODE_ANDORRA = "AD";
		public const string COUNTRY_CODE_UNITED_ARAB_EMIRATES = "AE";
		public const string COUNTRY_CODE_AFGHANISTAN = "AF";
		public const string COUNTRY_CODE_ANTIGUA_AND_BARBUDA = "AG";
		public const string COUNTRY_CODE_ANGUILLA = "AI";
		public const string COUNTRY_CODE_ALBANIA = "AL";
		public const string COUNTRY_CODE_ARMENIA = "AM";
		public const string COUNTRY_CODE_NETHERLANDS_ANTILLES = "AN";
		public const string COUNTRY_CODE_ANGOLA = "AO";
		public const string COUNTRY_CODE_ANTARCTICA = "AQ";
		public const string COUNTRY_CODE_ARGENTINA = "AR";
		public const string COUNTRY_CODE_AMERICAN_SAMOA = "AS";
		public const string COUNTRY_CODE_AUSTRIA = "AT";
		public const string COUNTRY_CODE_AUSTRALIA = "AU";
		public const string COUNTRY_CODE_ARUBA = "AW";
		public const string COUNTRY_CODE_ÅLAND_ISLANDS = "AX";
		public const string COUNTRY_CODE_AZERBAIJAN = "AZ";
		public const string COUNTRY_CODE_BOSNIA_AND_HERZEGOWINA = "BA";
		public const string COUNTRY_CODE_BARBADOS = "BB";
		public const string COUNTRY_CODE_BANGLADESH = "BD";
		public const string COUNTRY_CODE_BELGIUM = "BE";
		public const string COUNTRY_CODE_BURKINA_FASO = "BF";
		public const string COUNTRY_CODE_BULGARIA = "BG";
		public const string COUNTRY_CODE_BAHRAIN = "BH";
		public const string COUNTRY_CODE_BURUNDI = "BI";
		public const string COUNTRY_CODE_BENIN = "BJ";
		public const string COUNTRY_CODE_BERMUDA = "BM";
		public const string COUNTRY_CODE_BRUNEI_DARUSSALAM = "BN";
		public const string COUNTRY_CODE_BOLIVIA = "BO";
		public const string COUNTRY_CODE_BRAZIL = "BR";
		public const string COUNTRY_CODE_BAHAMAS = "BS";
		public const string COUNTRY_CODE_BHUTAN = "BT";
		public const string COUNTRY_CODE_BOUVET_ISLAND = "BV";
		public const string COUNTRY_CODE_BOTSWANA = "BW";
		public const string COUNTRY_CODE_BELARUS = "BY";
		public const string COUNTRY_CODE_BELIZE = "BZ";
		public const string COUNTRY_CODE_CANADA = "CA";
		public const string COUNTRY_CODE_COCOS_KEELING_ISLANDS = "CC";
		public const string COUNTRY_CODE_CONGO_THE_DEMOCRATIC_REPUBLIC_OF_THE = "CD";
		public const string COUNTRY_CODE_CENTRAL_AFRICAN_REPUBLIC = "CF";
		public const string COUNTRY_CODE_CONGO = "CG";
		public const string COUNTRY_CODE_SWITZERLAND = "CH";
		public const string COUNTRY_CODE_COTE_DIVOIRE = "CI";
		public const string COUNTRY_CODE_COOK_ISLANDS = "CK";
		public const string COUNTRY_CODE_CHILE = "CL";
		public const string COUNTRY_CODE_CAMEROON = "CM";
		public const string COUNTRY_CODE_CHINA = "CN";
		public const string COUNTRY_CODE_COLOMBIA = "CO";
		public const string COUNTRY_CODE_COSTA_RICA = "CR";
		public const string COUNTRY_CODE_CUBA = "CU";
		public const string COUNTRY_CODE_CAPE_VERDE = "CV";
		public const string COUNTRY_CODE_CHRISTMAS_ISLAND = "CX";
		public const string COUNTRY_CODE_CYPRUS = "CY";
		public const string COUNTRY_CODE_CZECH_REPUBLIC = "CZ";
		public const string COUNTRY_CODE_GERMANY = "DE";
		public const string COUNTRY_CODE_DJIBOUTI = "DJ";
		public const string COUNTRY_CODE_DENMARK = "DK";
		public const string COUNTRY_CODE_DOMINICA = "DM";
		public const string COUNTRY_CODE_DOMINICAN_REPUBLIC = "DO";
		public const string COUNTRY_CODE_ALGERIA = "DZ";
		public const string COUNTRY_CODE_ECUADOR = "EC";
		public const string COUNTRY_CODE_ESTONIA = "EE";
		public const string COUNTRY_CODE_EGYPT = "EG";
		public const string COUNTRY_CODE_WESTERN_SAHARA = "EH";
		public const string COUNTRY_CODE_ERITREA = "ER";
		public const string COUNTRY_CODE_SPAIN = "ES";
		public const string COUNTRY_CODE_ETHIOPIA = "ET";
		public const string COUNTRY_CODE_FINLAND = "FI";
		public const string COUNTRY_CODE_FIJI = "FJ";
		public const string COUNTRY_CODE_FALKLAND_ISLANDS_MALVINAS = "FK";
		public const string COUNTRY_CODE_MICRONESIA_FEDERATED_STATES_OF = "FM";
		public const string COUNTRY_CODE_FAROE_ISLANDS = "FO";
		public const string COUNTRY_CODE_FRANCE = "FR";
		public const string COUNTRY_CODE_FRANCE_METROPOLITAN = "FX";
		public const string COUNTRY_CODE_GABON = "GA";
		public const string COUNTRY_CODE_UNITED_KINGDOM = "GB";
		public const string COUNTRY_CODE_GRENADA = "GD";
		public const string COUNTRY_CODE_GEORGIA = "GE";
		public const string COUNTRY_CODE_FRENCH_GUIANA = "GF";
		public const string COUNTRY_CODE_GUERNSEY = "GG";
		public const string COUNTRY_CODE_GHANA = "GH";
		public const string COUNTRY_CODE_GIBRALTAR = "GI";
		public const string COUNTRY_CODE_GREENLAND = "GL";
		public const string COUNTRY_CODE_GAMBIA = "GM";
		public const string COUNTRY_CODE_GUINEA = "GN";
		public const string COUNTRY_CODE_GUADELOUPE = "GP";
		public const string COUNTRY_CODE_EQUATORIAL_GUINEA = "GQ";
		public const string COUNTRY_CODE_GREECE = "GR";
		public const string COUNTRY_CODE_SOUTH_GEORGIA_AND_THE_SOUTH_SANDWICH_ISLANDS = "GS";
		public const string COUNTRY_CODE_GUATEMALA = "GT";
		public const string COUNTRY_CODE_GUAM = "GU";
		public const string COUNTRY_CODE_GUINEA_BISSAU = "GW";
		public const string COUNTRY_CODE_GUYANA = "GY";
		public const string COUNTRY_CODE_HONG_KONG = "HK";
		public const string COUNTRY_CODE_HEARD_AND_MC_DONALD_ISLANDS = "HM";
		public const string COUNTRY_CODE_HONDURAS = "HN";
		public const string COUNTRY_CODE_CROATIA_LOCAL_NAME__HRVATSKA = "HR";
		public const string COUNTRY_CODE_HAITI = "HT";
		public const string COUNTRY_CODE_HUNGARY = "HU";
		public const string COUNTRY_CODE_INDONESIA = "ID";
		public const string COUNTRY_CODE_IRELAND = "IE";
		public const string COUNTRY_CODE_ISRAEL = "IL";
		public const string COUNTRY_CODE_ISLE_OF_MAN = "IM";
		public const string COUNTRY_CODE_INDIA = "IN";
		public const string COUNTRY_CODE_BRITISH_INDIAN_OCEAN_TERRITORY = "IO";
		public const string COUNTRY_CODE_IRAQ = "IQ";
		public const string COUNTRY_CODE_IRAN_ISLAMIC_REPUBLIC_OF = "IR";
		public const string COUNTRY_CODE_ICELAND = "IS";
		public const string COUNTRY_CODE_ITALY = "IT";
		public const string COUNTRY_CODE_JERSEY = "JE";
		public const string COUNTRY_CODE_JAMAICA = "JM";
		public const string COUNTRY_CODE_JORDAN = "JO";
		public const string COUNTRY_CODE_JAPAN = "JP";
		public const string COUNTRY_CODE_KENYA = "KE";
		public const string COUNTRY_CODE_KYRGYZSTAN = "KG";
		public const string COUNTRY_CODE_CAMBODIA = "KH";
		public const string COUNTRY_CODE_KIRIBATI = "KI";
		public const string COUNTRY_CODE_COMOROS = "KM";
		public const string COUNTRY_CODE_SAINT_KITTS_AND_NEVIS = "KN";
		public const string COUNTRY_CODE_KOREA_DEMOCRATIC_PEOPLES_REPUBLIC_OF = "KP";
		public const string COUNTRY_CODE_KOREA_REPUBLIC_OF = "KR";
		public const string COUNTRY_CODE_KUWAIT = "KW";
		public const string COUNTRY_CODE_CAYMAN_ISLANDS = "KY";
		public const string COUNTRY_CODE_KAZAKHSTAN = "KZ";
		public const string COUNTRY_CODE_LAO_PEOPLES_DEMOCRATIC_REPUBLIC = "LA";
		public const string COUNTRY_CODE_LEBANON = "LB";
		public const string COUNTRY_CODE_SAINT_LUCIA = "LC";
		public const string COUNTRY_CODE_LIECHTENSTEIN = "LI";
		public const string COUNTRY_CODE_SRI_LANKA = "LK";
		public const string COUNTRY_CODE_LIBERIA = "LR";
		public const string COUNTRY_CODE_LESOTHO = "LS";
		public const string COUNTRY_CODE_LITHUANIA = "LT";
		public const string COUNTRY_CODE_LUXEMBOURG = "LU";
		public const string COUNTRY_CODE_LATVIA = "LV";
		public const string COUNTRY_CODE_LIBYAN_ARAB_JAMAHIRIYA = "LY";
		public const string COUNTRY_CODE_MOROCCO = "MA";
		public const string COUNTRY_CODE_MONACO = "MC";
		public const string COUNTRY_CODE_MOLDOVA_REPUBLIC_OF = "MD";
		public const string COUNTRY_CODE_MONTENEGRO = "ME";
		public const string COUNTRY_CODE_MADAGASCAR = "MG";
		public const string COUNTRY_CODE_MARSHALL_ISLANDS = "MH";
		public const string COUNTRY_CODE_MACEDONIA_THE_FORMER_YUGOSLAV_REPUBLIC_OF = "MK";
		public const string COUNTRY_CODE_MALI = "ML";
		public const string COUNTRY_CODE_MYANMAR = "MM";
		public const string COUNTRY_CODE_MONGOLIA = "MN";
		public const string COUNTRY_CODE_MACAU = "MO";
		public const string COUNTRY_CODE_NORTHERN_MARIANA_ISLANDS = "MP";
		public const string COUNTRY_CODE_MARTINIQUE = "MQ";
		public const string COUNTRY_CODE_MAURITANIA = "MR";
		public const string COUNTRY_CODE_MONTSERRAT = "MS";
		public const string COUNTRY_CODE_MALTA = "MT";
		public const string COUNTRY_CODE_MAURITIUS = "MU";
		public const string COUNTRY_CODE_MALDIVES = "MV";
		public const string COUNTRY_CODE_MALAWI = "MW";
		public const string COUNTRY_CODE_MEXICO = "MX";
		public const string COUNTRY_CODE_MALAYSIA = "MY";
		public const string COUNTRY_CODE_MOZAMBIQUE = "MZ";
		public const string COUNTRY_CODE_NAMIBIA = "NA";
		public const string COUNTRY_CODE_NEW_CALEDONIA = "NC";
		public const string COUNTRY_CODE_NIGER = "NE";
		public const string COUNTRY_CODE_NORFOLK_ISLAND = "NF";
		public const string COUNTRY_CODE_NIGERIA = "NG";
		public const string COUNTRY_CODE_NICARAGUA = "NI";
		public const string COUNTRY_CODE_NETHERLANDS = "NL";
		public const string COUNTRY_CODE_NORWAY = "NO";
		public const string COUNTRY_CODE_NEPAL = "NP";
		public const string COUNTRY_CODE_NAURU = "NR";
		public const string COUNTRY_CODE_NIUE = "NU";
		public const string COUNTRY_CODE_NEW_ZEALAND = "NZ";
		public const string COUNTRY_CODE_OMAN = "OM";
		public const string COUNTRY_CODE_PANAMA = "PA";
		public const string COUNTRY_CODE_PERU = "PE";
		public const string COUNTRY_CODE_FRENCH_POLYNESIA = "PF";
		public const string COUNTRY_CODE_PAPUA_NEW_GUINEA = "PG";
		public const string COUNTRY_CODE_PHILIPPINES = "PH";
		public const string COUNTRY_CODE_PAKISTAN = "PK";
		public const string COUNTRY_CODE_POLAND = "PL";
		public const string COUNTRY_CODE_ST_PIERRE_AND_MIQUELON = "PM";
		public const string COUNTRY_CODE_PITCAIRN = "PN";
		public const string COUNTRY_CODE_PUERTO_RICO = "PR";
		public const string COUNTRY_CODE_PALESTINIAN_TERRITORY_OCCUPIED = "PS";
		public const string COUNTRY_CODE_PORTUGAL = "PT";
		public const string COUNTRY_CODE_PALAU = "PW";
		public const string COUNTRY_CODE_PARAGUAY = "PY";
		public const string COUNTRY_CODE_QATAR = "QA";
		public const string COUNTRY_CODE_REUNION = "RE";
		public const string COUNTRY_CODE_ROMANIA = "RO";
		public const string COUNTRY_CODE_SERBIA = "RS";
		public const string COUNTRY_CODE_RUSSIAN_FEDERATION = "RU";
		public const string COUNTRY_CODE_RWANDA = "RW";
		public const string COUNTRY_CODE_SAUDI_ARABIA = "SA";
		public const string COUNTRY_CODE_SOLOMON_ISLANDS = "SB";
		public const string COUNTRY_CODE_SEYCHELLES = "SC";
		public const string COUNTRY_CODE_SUDAN = "SD";
		public const string COUNTRY_CODE_SWEDEN = "SE";
		public const string COUNTRY_CODE_SINGAPORE = "SG";
		public const string COUNTRY_CODE_ST_HELENA = "SH";
		public const string COUNTRY_CODE_SLOVENIA = "SI";
		public const string COUNTRY_CODE_SVALBARD_AND_JAN_MAYEN_ISLANDS = "SJ";
		public const string COUNTRY_CODE_SLOVAKIA_SLOVAK_REPUBLIC = "SK";
		public const string COUNTRY_CODE_SIERRA_LEONE = "SL";
		public const string COUNTRY_CODE_SAN_MARINO = "SM";
		public const string COUNTRY_CODE_SENEGAL = "SN";
		public const string COUNTRY_CODE_SOMALIA = "SO";
		public const string COUNTRY_CODE_SURINAME = "SR";
		public const string COUNTRY_CODE_SAO_TOME_AND_PRINCIPE = "ST";
		public const string COUNTRY_CODE_EL_SALVADOR = "SV";
		public const string COUNTRY_CODE_SYRIAN_ARAB_REPUBLIC = "SY";
		public const string COUNTRY_CODE_SWAZILAND = "SZ";
		public const string COUNTRY_CODE_TURKS_AND_CAICOS_ISLANDS = "TC";
		public const string COUNTRY_CODE_CHAD = "TD";
		public const string COUNTRY_CODE_FRENCH_SOUTHERN_TERRITORIES = "TF";
		public const string COUNTRY_CODE_TOGO = "TG";
		public const string COUNTRY_CODE_THAILAND = "TH";
		public const string COUNTRY_CODE_TAJIKISTAN = "TJ";
		public const string COUNTRY_CODE_TOKELAU = "TK";
		public const string COUNTRY_CODE_TIMOR_LESTE = "TL";
		public const string COUNTRY_CODE_TURKMENISTAN = "TM";
		public const string COUNTRY_CODE_TUNISIA = "TN";
		public const string COUNTRY_CODE_TONGA = "TO";
		public const string COUNTRY_CODE_EAST_TIMOR = "TP";
		public const string COUNTRY_CODE_TURKEY = "TR";
		public const string COUNTRY_CODE_TRINIDAD_AND_TOBAGO = "TT";
		public const string COUNTRY_CODE_TUVALU = "TV";
		public const string COUNTRY_CODE_TAIWAN_PROVINCE_OF_CHINA = "TW";
		public const string COUNTRY_CODE_TANZANIA_UNITED_REPUBLIC_OF = "TZ";
		public const string COUNTRY_CODE_UKRAINE = "UA";
		public const string COUNTRY_CODE_UGANDA = "UG";
		public const string COUNTRY_CODE_UNITED_STATES_MINOR_OUTLYING_ISLANDS = "UM";
		public const string COUNTRY_CODE_UNITED_STATES = "US";
		public const string COUNTRY_CODE_URUGUAY = "UY";
		public const string COUNTRY_CODE_UZBEKISTAN = "UZ";
		public const string COUNTRY_CODE_HOLY_SEE_VATICAN_CITY_STATE = "VA";
		public const string COUNTRY_CODE_SAINT_VINCENT_AND_THE_GRENADINES = "VC";
		public const string COUNTRY_CODE_VENEZUELA = "VE";
		public const string COUNTRY_CODE_VIRGIN_ISLANDS_BRITISH = "VG";
		public const string COUNTRY_CODE_VIRGIN_ISLANDS_US = "VI";
		public const string COUNTRY_CODE_VIETNAM = "VN";
		public const string COUNTRY_CODE_VANUATU = "VU";
		public const string COUNTRY_CODE_WALLIS_AND_FUTUNA_ISLANDS = "WF";
		public const string COUNTRY_CODE_SAMOA = "WS";
		public const string COUNTRY_CODE_YEMEN = "YE";
		public const string COUNTRY_CODE_MAYOTTE = "YT";
		public const string COUNTRY_CODE_SERBIA_AND_MONTENEGRO = "YU";
		public const string COUNTRY_CODE_SOUTH_AFRICA = "ZA";
		public const string COUNTRY_CODE_ZAMBIA = "ZM";
		public const string COUNTRY_CODE_ZIMBABWE = "ZW";
		#endregion

		static CountryCode() {
			_countrycodeList = new string[] {
												COUNTRY_CODE_AFGHANISTAN,
												COUNTRY_CODE_ÅLAND_ISLANDS,
												COUNTRY_CODE_ALBANIA,
												COUNTRY_CODE_ALGERIA,
												COUNTRY_CODE_AMERICAN_SAMOA,
												COUNTRY_CODE_ANDORRA,
												COUNTRY_CODE_ANGOLA,
												COUNTRY_CODE_ANGUILLA,
												COUNTRY_CODE_ANTARCTICA,
												COUNTRY_CODE_ANTIGUA_AND_BARBUDA,
												COUNTRY_CODE_ARGENTINA,
												COUNTRY_CODE_ARMENIA,
												COUNTRY_CODE_ARUBA,
												COUNTRY_CODE_AUSTRALIA,
												COUNTRY_CODE_AUSTRIA,
												COUNTRY_CODE_AZERBAIJAN,
												COUNTRY_CODE_BAHAMAS,
												COUNTRY_CODE_BAHRAIN,
												COUNTRY_CODE_BANGLADESH,
												COUNTRY_CODE_BARBADOS,
												COUNTRY_CODE_BELARUS,
												COUNTRY_CODE_BELGIUM,
												COUNTRY_CODE_BELIZE,
												COUNTRY_CODE_BENIN,
												COUNTRY_CODE_BERMUDA,
												COUNTRY_CODE_BHUTAN,
												COUNTRY_CODE_BOLIVIA,
												COUNTRY_CODE_BOSNIA_AND_HERZEGOWINA,
												COUNTRY_CODE_BOTSWANA,
												COUNTRY_CODE_BOUVET_ISLAND,
												COUNTRY_CODE_BRAZIL,
												COUNTRY_CODE_BRITISH_INDIAN_OCEAN_TERRITORY,
												COUNTRY_CODE_BRUNEI_DARUSSALAM,
												COUNTRY_CODE_BULGARIA,
												COUNTRY_CODE_BURKINA_FASO,
												COUNTRY_CODE_BURUNDI,
												COUNTRY_CODE_CAMBODIA,
												COUNTRY_CODE_CAMEROON,
												COUNTRY_CODE_CANADA,
												COUNTRY_CODE_CAPE_VERDE,
												COUNTRY_CODE_CAYMAN_ISLANDS,
												COUNTRY_CODE_CENTRAL_AFRICAN_REPUBLIC,
												COUNTRY_CODE_CHAD,
												COUNTRY_CODE_CHILE,
												COUNTRY_CODE_CHINA,
												COUNTRY_CODE_CHRISTMAS_ISLAND,
												COUNTRY_CODE_COCOS_KEELING_ISLANDS,
												COUNTRY_CODE_COLOMBIA,
												COUNTRY_CODE_COMOROS,
												COUNTRY_CODE_CONGO,
												COUNTRY_CODE_CONGO_THE_DEMOCRATIC_REPUBLIC_OF_THE,
												COUNTRY_CODE_COOK_ISLANDS,
												COUNTRY_CODE_COSTA_RICA,
												COUNTRY_CODE_COTE_DIVOIRE,
												COUNTRY_CODE_CROATIA_LOCAL_NAME__HRVATSKA,
												COUNTRY_CODE_CUBA,
												COUNTRY_CODE_CYPRUS,
												COUNTRY_CODE_CZECH_REPUBLIC,
												COUNTRY_CODE_DENMARK,
												COUNTRY_CODE_DJIBOUTI,
												COUNTRY_CODE_DOMINICA,
												COUNTRY_CODE_DOMINICAN_REPUBLIC,
												COUNTRY_CODE_EAST_TIMOR,
												COUNTRY_CODE_ECUADOR,
												COUNTRY_CODE_EGYPT,
												COUNTRY_CODE_EL_SALVADOR,
												COUNTRY_CODE_EQUATORIAL_GUINEA,
												COUNTRY_CODE_ERITREA,
												COUNTRY_CODE_ESTONIA,
												COUNTRY_CODE_ETHIOPIA,
												COUNTRY_CODE_FALKLAND_ISLANDS_MALVINAS,
												COUNTRY_CODE_FAROE_ISLANDS,
												COUNTRY_CODE_FIJI,
												COUNTRY_CODE_FINLAND,
												COUNTRY_CODE_FRANCE,
												COUNTRY_CODE_FRANCE_METROPOLITAN,
												COUNTRY_CODE_FRENCH_GUIANA,
												COUNTRY_CODE_FRENCH_POLYNESIA,
												COUNTRY_CODE_FRENCH_SOUTHERN_TERRITORIES,
												COUNTRY_CODE_GABON,
												COUNTRY_CODE_GAMBIA,
												COUNTRY_CODE_GEORGIA,
												COUNTRY_CODE_GERMANY,
												COUNTRY_CODE_GHANA,
												COUNTRY_CODE_GIBRALTAR,
												COUNTRY_CODE_GREECE,
												COUNTRY_CODE_GREENLAND,
												COUNTRY_CODE_GRENADA,
												COUNTRY_CODE_GUADELOUPE,
												COUNTRY_CODE_GUAM,
												COUNTRY_CODE_GUATEMALA,
												COUNTRY_CODE_GUERNSEY,
												COUNTRY_CODE_GUINEA,
												COUNTRY_CODE_GUINEA_BISSAU,
												COUNTRY_CODE_GUYANA,
												COUNTRY_CODE_HAITI,
												COUNTRY_CODE_HEARD_AND_MC_DONALD_ISLANDS,
												COUNTRY_CODE_HOLY_SEE_VATICAN_CITY_STATE,
												COUNTRY_CODE_HONDURAS,
												COUNTRY_CODE_HONG_KONG,
												COUNTRY_CODE_HUNGARY,
												COUNTRY_CODE_ICELAND,
												COUNTRY_CODE_INDIA,
												COUNTRY_CODE_INDONESIA,
												COUNTRY_CODE_IRAN_ISLAMIC_REPUBLIC_OF,
												COUNTRY_CODE_IRAQ,
												COUNTRY_CODE_IRELAND,
												COUNTRY_CODE_ISLE_OF_MAN,
												COUNTRY_CODE_ISRAEL,
												COUNTRY_CODE_ITALY,
												COUNTRY_CODE_JAMAICA,
												COUNTRY_CODE_JAPAN,
												COUNTRY_CODE_JERSEY,
												COUNTRY_CODE_JORDAN,
												COUNTRY_CODE_KAZAKHSTAN,
												COUNTRY_CODE_KENYA,
												COUNTRY_CODE_KIRIBATI,
												COUNTRY_CODE_KOREA_DEMOCRATIC_PEOPLES_REPUBLIC_OF,
												COUNTRY_CODE_KOREA_REPUBLIC_OF,
												COUNTRY_CODE_KUWAIT,
												COUNTRY_CODE_KYRGYZSTAN,
												COUNTRY_CODE_LAO_PEOPLES_DEMOCRATIC_REPUBLIC,
												COUNTRY_CODE_LATVIA,
												COUNTRY_CODE_LEBANON,
												COUNTRY_CODE_LESOTHO,
												COUNTRY_CODE_LIBERIA,
												COUNTRY_CODE_LIBYAN_ARAB_JAMAHIRIYA,
												COUNTRY_CODE_LIECHTENSTEIN,
												COUNTRY_CODE_LITHUANIA,
												COUNTRY_CODE_LUXEMBOURG,
												COUNTRY_CODE_MACAU,
												COUNTRY_CODE_MACEDONIA_THE_FORMER_YUGOSLAV_REPUBLIC_OF,
												COUNTRY_CODE_MADAGASCAR,
												COUNTRY_CODE_MALAWI,
												COUNTRY_CODE_MALAYSIA,
												COUNTRY_CODE_MALDIVES,
												COUNTRY_CODE_MALI,
												COUNTRY_CODE_MALTA,
												COUNTRY_CODE_MARSHALL_ISLANDS,
												COUNTRY_CODE_MARTINIQUE,
												COUNTRY_CODE_MAURITANIA,
												COUNTRY_CODE_MAURITIUS,
												COUNTRY_CODE_MAYOTTE,
												COUNTRY_CODE_MEXICO,
												COUNTRY_CODE_MICRONESIA_FEDERATED_STATES_OF,
												COUNTRY_CODE_MOLDOVA_REPUBLIC_OF,
												COUNTRY_CODE_MONACO,
												COUNTRY_CODE_MONGOLIA,
												COUNTRY_CODE_MONTENEGRO,
												COUNTRY_CODE_MONTSERRAT,
												COUNTRY_CODE_MOROCCO,
												COUNTRY_CODE_MOZAMBIQUE,
												COUNTRY_CODE_MYANMAR,
												COUNTRY_CODE_NAMIBIA,
												COUNTRY_CODE_NAURU,
												COUNTRY_CODE_NEPAL,
												COUNTRY_CODE_NETHERLANDS,
												COUNTRY_CODE_NETHERLANDS_ANTILLES,
												COUNTRY_CODE_NEW_CALEDONIA,
												COUNTRY_CODE_NEW_ZEALAND,
												COUNTRY_CODE_NICARAGUA,
												COUNTRY_CODE_NIGER,
												COUNTRY_CODE_NIGERIA,
												COUNTRY_CODE_NIUE,
												COUNTRY_CODE_NORFOLK_ISLAND,
												COUNTRY_CODE_NORTHERN_MARIANA_ISLANDS,
												COUNTRY_CODE_NORWAY,
												COUNTRY_CODE_OMAN,
												COUNTRY_CODE_PAKISTAN,
												COUNTRY_CODE_PALAU,
												COUNTRY_CODE_PALESTINIAN_TERRITORY_OCCUPIED,
												COUNTRY_CODE_PANAMA,
												COUNTRY_CODE_PAPUA_NEW_GUINEA,
												COUNTRY_CODE_PARAGUAY,
												COUNTRY_CODE_PERU,
												COUNTRY_CODE_PHILIPPINES,
												COUNTRY_CODE_PITCAIRN,
												COUNTRY_CODE_POLAND,
												COUNTRY_CODE_PORTUGAL,
												COUNTRY_CODE_PUERTO_RICO,
												COUNTRY_CODE_QATAR,
												COUNTRY_CODE_REUNION,
												COUNTRY_CODE_ROMANIA,
												COUNTRY_CODE_RUSSIAN_FEDERATION,
												COUNTRY_CODE_RWANDA,
												COUNTRY_CODE_SAINT_KITTS_AND_NEVIS,
												COUNTRY_CODE_SAINT_LUCIA,
												COUNTRY_CODE_SAINT_VINCENT_AND_THE_GRENADINES,
												COUNTRY_CODE_SAMOA,
												COUNTRY_CODE_SAN_MARINO,
												COUNTRY_CODE_SAO_TOME_AND_PRINCIPE,
												COUNTRY_CODE_SAUDI_ARABIA,
												COUNTRY_CODE_SENEGAL,
												COUNTRY_CODE_SERBIA,
												COUNTRY_CODE_SERBIA_AND_MONTENEGRO,
												COUNTRY_CODE_SEYCHELLES,
												COUNTRY_CODE_SIERRA_LEONE,
												COUNTRY_CODE_SINGAPORE,
												COUNTRY_CODE_SLOVAKIA_SLOVAK_REPUBLIC,
												COUNTRY_CODE_SLOVENIA,
												COUNTRY_CODE_SOLOMON_ISLANDS,
												COUNTRY_CODE_SOMALIA,
												COUNTRY_CODE_SOUTH_AFRICA,
												COUNTRY_CODE_SOUTH_GEORGIA_AND_THE_SOUTH_SANDWICH_ISLANDS,
												COUNTRY_CODE_SPAIN,
												COUNTRY_CODE_SRI_LANKA,
												COUNTRY_CODE_ST_HELENA,
												COUNTRY_CODE_ST_PIERRE_AND_MIQUELON,
												COUNTRY_CODE_SUDAN,
												COUNTRY_CODE_SURINAME,
												COUNTRY_CODE_SVALBARD_AND_JAN_MAYEN_ISLANDS,
												COUNTRY_CODE_SWAZILAND,
												COUNTRY_CODE_SWEDEN,
												COUNTRY_CODE_SWITZERLAND,
												COUNTRY_CODE_SYRIAN_ARAB_REPUBLIC,
												COUNTRY_CODE_TAIWAN_PROVINCE_OF_CHINA,
												COUNTRY_CODE_TAJIKISTAN,
												COUNTRY_CODE_TANZANIA_UNITED_REPUBLIC_OF,
												COUNTRY_CODE_THAILAND,
												COUNTRY_CODE_TIMOR_LESTE,
												COUNTRY_CODE_TOGO,
												COUNTRY_CODE_TOKELAU,
												COUNTRY_CODE_TONGA,
												COUNTRY_CODE_TRINIDAD_AND_TOBAGO,
												COUNTRY_CODE_TUNISIA,
												COUNTRY_CODE_TURKEY,
												COUNTRY_CODE_TURKMENISTAN,
												COUNTRY_CODE_TURKS_AND_CAICOS_ISLANDS,
												COUNTRY_CODE_TUVALU,
												COUNTRY_CODE_UGANDA,
												COUNTRY_CODE_UKRAINE,
												COUNTRY_CODE_UNITED_ARAB_EMIRATES,
												COUNTRY_CODE_UNITED_KINGDOM,
												COUNTRY_CODE_UNITED_STATES,
												COUNTRY_CODE_UNITED_STATES_MINOR_OUTLYING_ISLANDS,
												COUNTRY_CODE_URUGUAY,
												COUNTRY_CODE_UZBEKISTAN,
												COUNTRY_CODE_VANUATU,
												COUNTRY_CODE_VENEZUELA,
												COUNTRY_CODE_VIETNAM,
												COUNTRY_CODE_VIRGIN_ISLANDS_BRITISH,
												COUNTRY_CODE_VIRGIN_ISLANDS_US,
												COUNTRY_CODE_WALLIS_AND_FUTUNA_ISLANDS,
												COUNTRY_CODE_WESTERN_SAHARA,
												COUNTRY_CODE_YEMEN,
												COUNTRY_CODE_ZAMBIA,
												COUNTRY_CODE_ZIMBABWE
											};
		}

		private CountryCode() {
		}

		public static string[] CountryList {
			get {
				return _countrycodeList;
			}
		}

		public static string GetResourceKey(string countryCode) {
			return COUNTRY_NAME_TRANSLATION_TAG_PREFIX + countryCode.ToLower();
		}

		public static string GetCountryName(string countryCode, string cultureCode) {
			ResourceManager resMan = GetResourceManager(cultureCode);
			return resMan.GetString(COUNTRY_NAME_TRANSLATION_TAG_PREFIX + countryCode.ToLower(), new CultureInfo(cultureCode));
		}

		private static ResourceManager GetResourceManager(string cultureCode) {
			try {
				return new ResourceManager("Cuyahoga.Modules.ECommerce.Util.Location", typeof(CountryCode).Assembly);
			} catch (Exception e) {
				LogManager.GetLogger(typeof(CountryCode)).Error(e);
				return null;
			}
		}
	}
}