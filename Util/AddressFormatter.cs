using System.Globalization;
using System.Text;

using log4net;
using Cuyahoga.Modules.ECommerce.Service.Translation;
using Cuyahoga.Modules.ECommerce.Util.Location;

namespace Cuyahoga.Modules.ECommerce.Util.Location {

	/// <summary>
	/// Summary description for AddressFormatter.
	/// </summary>
	public class AddressFormatter : IAddressFormatter {

		public const string TRANSLATION_TAG_PREFIX_COUNTRY_NAME = "country_name_";

		private string _lineDelimiter;
        private string _linePrefix;
        private bool _isDelimiterToAdd = false;

		public AddressFormatter() {
			_lineDelimiter = "\r\n";
            _linePrefix = "";
		}

		public AddressFormatter(string lineDelimiter) : this() {
			_lineDelimiter = lineDelimiter;
		}

        public AddressFormatter(string linePrefix, string lineDelimiter)
            : this(lineDelimiter) {
            _linePrefix = linePrefix;
        }
        
        private string GetPostcodePrefix(string countryCode) {
			return countryCode.ToUpper() + "-";
		}

		private string RemovePostcodePrefix(string postcode, string countryCode) {

			string prefix = GetPostcodePrefix(countryCode);
			
			if (postcode.StartsWith(countryCode) && postcode.Length > prefix.Length) {
				return countryCode.Substring(prefix.Length);
			}

			return postcode;
		}

		private bool AppendLineDelimiter(StringBuilder text, bool isFirstLine, bool isLastLine) {
			return AppendLineDelimiter(text, isFirstLine, isLastLine, _linePrefix, _lineDelimiter);
		}

        private bool AppendLineDelimiter(StringBuilder text, bool isFirstLine, string linePrefix, string lineDelimiter) {
            return AppendLineDelimiter(text, isFirstLine, false, linePrefix, lineDelimiter);
        }

		private bool AppendLineDelimiter(StringBuilder text, bool isFirstLine, bool isLastLine, string linePrefix, string lineDelimiter) {
            
			if (!isFirstLine && _isDelimiterToAdd) {
                //Add the previous delimiter
				text.Append(lineDelimiter);
			}

            if (!isLastLine) {
                //record that we have a delimiter to add next time
                _isDelimiterToAdd = true;
            }

            if (!isLastLine && !string.IsNullOrEmpty(linePrefix)) {
                text.Append(linePrefix);
            }

			return false;
		}

		public string FormatAddress(IAddress address) {
			return FormatAddress(address, _linePrefix, _lineDelimiter, CultureInfo.CurrentCulture.Name);
		}
        
        public string FormatAddress(IAddress address, string lineDelimiter, string cultureCode) {
            return FormatAddress(address, null, lineDelimiter, cultureCode);
        }

		public string FormatAddress(IAddress address, string linePrefix, string lineDelimiter, string cultureCode) {

			AddressFormattingRules rules = AddressFormattingRules.GetRules(address.CountryCode);

			StringBuilder addressText = new StringBuilder();
			bool isFirstLine = true;

			if (address.ContactName != null && address.ContactName.Length > 0) {
				
				isFirstLine = AppendLineDelimiter(addressText, isFirstLine, linePrefix, lineDelimiter);

				if (rules.IsAddressUpperCase) {
					addressText.Append(address.ContactName.ToUpper());
				} else {
					addressText.Append(address.ContactName);
				}
			}

			if (address.AddressLine1 != null && address.AddressLine1.Length > 0) {

                isFirstLine = AppendLineDelimiter(addressText, isFirstLine, linePrefix, lineDelimiter);

				if (rules.IsAddressUpperCase) {
					addressText.Append(address.AddressLine1.ToUpper());
				} else {
					addressText.Append(address.AddressLine1);
				}
			}

			if (address.AddressLine2  != null && address.AddressLine2.Length > 0) {

                isFirstLine = AppendLineDelimiter(addressText, isFirstLine, linePrefix, lineDelimiter);

				if (rules.IsAddressUpperCase) {
					addressText.Append(address.AddressLine2.ToUpper());
				} else {
					addressText.Append(address.AddressLine2);
				}
			}

			if (address.AddressLine3  != null && address.AddressLine3.Length > 0) {

                isFirstLine = AppendLineDelimiter(addressText, isFirstLine, linePrefix, lineDelimiter);

				if (rules.IsAddressUpperCase) {
					addressText.Append(address.AddressLine3.ToUpper());
				} else {
					addressText.Append(address.AddressLine3);
				}
			}

			if (address.City != null && address.City.Length > 0) {

                isFirstLine = AppendLineDelimiter(addressText, isFirstLine, linePrefix, lineDelimiter);

				if (!rules.IsPostcodeOnOwnLine && rules.IsPostcodeBeforeCity && address.Postcode != null && address.Postcode.Length > 0) {
					addressText.Append(address.Postcode.ToUpper());
					addressText.Append(" ");
				}

				if (rules.IsAddressUpperCase || rules.IsCityUpperCase) {
					addressText.Append(address.City.ToUpper());
				} else {
					addressText.Append(address.City);
				}

				if (!rules.IsRegionOnOwnLine && address.Region != null && address.Region.Length > 0) {
					
					addressText.Append(" ");

					if (rules.IsAddressUpperCase || rules.IsRegionUpperCase) {
						addressText.Append(address.Region.ToUpper());
					} else {
						addressText.Append(address.Region);
					}
				}

				if (!rules.IsPostcodeOnOwnLine && !rules.IsPostcodeBeforeCity && address.Postcode != null && address.Postcode.Length > 0) {
					addressText.Append(" ");
					addressText.Append(address.Postcode.ToUpper());
				}

			} else {
				if (!rules.IsPostcodeOnOwnLine && address.Postcode != null && address.Postcode.Length > 0) {
                    isFirstLine = AppendLineDelimiter(addressText, isFirstLine, linePrefix, lineDelimiter);
					addressText.Append(address.Postcode.ToUpper());
				}
			}

			//Stand-alone region
			if ((rules.IsRegionOnOwnLine || address.City == null || address.City.Length == 0) && address.Region != null && address.Region.Length > 0) {

                isFirstLine = AppendLineDelimiter(addressText, isFirstLine, linePrefix, lineDelimiter);

				if (rules.IsAddressUpperCase || rules.IsRegionUpperCase) {
					addressText.Append(address.Region.ToUpper());
				} else {
					addressText.Append(address.Region);
				}
			}

			//Stand-alone postcode
			if (rules.IsPostcodeOnOwnLine && address.Postcode != null && address.Postcode.Length > 0) {
                isFirstLine = AppendLineDelimiter(addressText, isFirstLine, linePrefix, lineDelimiter);
				addressText.Append(address.Postcode.ToUpper());
			}

			if (address.CountryCode != null && address.CountryCode.Length > 0) {

                isFirstLine = AppendLineDelimiter(addressText, isFirstLine, linePrefix, lineDelimiter);

				string countryName;
				if (address.CountryCode.Length == CountryCode.COUNTRY_CODE_LENGTH) {
					countryName = GetCountryFullName(address.CountryCode, cultureCode);
				} else {
					countryName = address.CountryCode;
				}

				if (rules.IsAddressUpperCase || rules.IsCountryUpperCase) {
					addressText.Append(countryName.ToUpper());
				} else {
					addressText.Append(countryName);
				}
			}

            //Add any line delimiters that need to be added
            AppendLineDelimiter(addressText, isFirstLine, true, linePrefix, lineDelimiter);

			return addressText.ToString();
		}

		private ITextTranslator GetDefaultTranslator(string cultureCode) {
			return TranslatorUtils.GetTextTranslator(typeof(IAddressFormatter), cultureCode);
		}

		//There may be loads of ways to get this information
		protected virtual string GetCountryFullName(string countryCode, string cultureCode) {

			ITextTranslator translator = GetDefaultTranslator(cultureCode);
			
			if (translator != null) {
				
				string name = translator.GetText(TRANSLATION_TAG_PREFIX_COUNTRY_NAME + countryCode);
				
				if (name != null && name.Length > 0) {
					return name;
				}

				//This should have been found
				LogManager.GetLogger(GetType()).Warn("Cannot find translated name for country code [" + countryCode 
					+ "], culture [" + translator.CultureCode + "]");
			}

			return countryCode.ToUpper();
		}
	}

	public class AddressFormattingRules {
		/*

AT
R. Fellner         [recipient]
Pazmaniteng 24-9   [street address]
A-1020 Vienna      [postal code + city/town/locality]  The A- is an optional country code for mailing within the EU.
AUSTRIA            [country name]

BE
M. André Dupont
Rue du Cornet 6
B-4800 VERVIERS
BELGIUM

CH
Philatelie der Post
Maria Reichenbach
Fraumünsterpost
Kappelergasse 1                    [Street name + building number]
8022 Zürich                        [Postal code + city/town/village]
Switzerland                        [Country name]

DE
Herrn                         ["Mr." (form of address)]
Eberhard Wellhausen           [name]
Wittekindshof                 
Schulstrasse 4                [street address]
32547 Bad Oyenhausen          [postal code + city/town]
GERMANY                       [country]

DK
Hr. Niels Henriksen             [recipient]
Kastanievej 15                  [street address]
DK-8660 SKANDERBORG             [country code + postal code + city/town/locality.  The "DK-" is an optional country code.]
DENMARK                         [country]

ES
Organismo Autónomio Correos y Telégraphos
Area de Asuntos Internacionales
Calle Aduana, 29                       [street name, house/building number]
28070 MADRID                           [postal code + city/town/locality]
SPAIN

FI
Ms. Aulikki Laasko   [recipient]
Vesakkotic 1399      [street address]
FIN-00630 HELSINKI   [country code + postal code + postal district.  The "FIN-" is an optional country code.]
FINLAND              [country]
      
FR
Madame Duval             Some people prefer to write the recipient's last name in ALL CAPITAL LETTERS, but the UPU does not deem this necessary.
27, rue Pasteur          [street address (house/building number, street name)]
14390 CABOURG            [postal code + city]  You may precede the postal code with France's country code and a dash, e.g. FR-14390
FRANCE

M. Bernard               this is a variant "Small locality" format
Impasse Vivaldi
VAUCE
53300 COUESNES VAUCE
FRANCE

GB
Nildram Ltd         [recipient]
Ardenham Court      [probably the building name: see Format Information.  Not all addresses have this part.]
Oxford Road         [street name]
AYLESBURY           [postal town (town/city)]
BUCKINGHAMSHIRE     [county (not needed)]
HP19 3EQ            [postal code]
GREAT BRITAIN       [country name]

IE
The Avalon Hotel
223 BURLINGTON ROAD
IE DUBLIN 4

IT
Sig. Mario Rossi
Viale Europa, 22      [street, house/building number]
00144-ROMA RM         [postal code-city province]  Province may be optional when city is a provincial capital.
ITALY

LU
Luxembourg address format seems mostly similar to France's format, except that:

* The postal code is a four-digit number.
* Of course, the letter that you can optionally write before the postal code is L- for Luxembourg (not F- for France.)
  for example, L-1316. 

A possible source of confusion is that Luxembourg is the name of both the country and its capital city, so you could write an address such as:

M. Andrée TROMMER   [(M. = Monsieur) recipient name]
BP 5019             [P.O. Box + number]
L-1050 Luxembourg   [postal code + city/locality]
LUXEMBOURG          [country name]
   
NL
Geschillencommissie				[recipient name]
Surinamestraat 24               [street address (street name + house number / building number)]
2585 GJ Den Haag                [postal code + town/locality]
Netherlands                     [country name]

NO
Norwegian Post and Telecommunications Authority [Recipient]
Revierstredet 2                                 [street name + building number]
NO-0104 OSLO                                    [Postal code + city/town.  The N- is optional, at least when mailing from the U.S.]
Norway                                          [Country name]

SE
Ms. Hypothetical                Name of person/company/whatever
c/o Jon Wätte                   Extra name/info (optional)
Hagagatan 1, vi                 Street, number, apartment floor
SE-113 49 Stockholm             Postal code and city
SWEDEN                          Country

US
Many street addresses include a direction (e.g. E = EAST):

CHRIS NISWANDEE
BITBOOST SYSTEMS
421 E DRACHMAN
TUCSON AZ 85705
USA

An example with the optional latter 4 digits of the zip code:

JOHN DOE
BITBOOST SYSTEMS
421 E DRACHMAN
TUCSON AZ 85705-7445
USA
  
*/

		private string _countryCode;

		private AddressFormattingRules(string countryCode) {
			if (countryCode != null && countryCode.Length == CountryCode.COUNTRY_CODE_LENGTH) {
				_countryCode = countryCode;
			} else {
				_countryCode = CountryCode.COUNTRY_CODE_UNITED_STATES;
			}
		}

		public static AddressFormattingRules GetRules(string countryCode) {
			//Should retrieve this from a static hashmap
			return new AddressFormattingRules(countryCode);
		}

		public bool IsPostcodeOnOwnLine {
			get {
				return (_countryCode == CountryCode.COUNTRY_CODE_UNITED_KINGDOM);
			}
		}

		public bool IsPostcodeBeforeCity {
			get {
				return (_countryCode != CountryCode.COUNTRY_CODE_IRELAND && _countryCode != CountryCode.COUNTRY_CODE_UNITED_STATES);
			}
		}

		public bool IsRegionOnOwnLine {
			get {
				return (_countryCode != CountryCode.COUNTRY_CODE_UNITED_STATES);
			}
		}

		public bool IsCityUpperCase {
			get {
				return true;
			}
		}

		public bool IsRegionUpperCase {
			get {
				return true;
			}
		}

		public bool IsCountryUpperCase {
			get {
				return true;
			}
		}

		public bool IsAddressUpperCase {
			get {
				return (_countryCode == CountryCode.COUNTRY_CODE_IRELAND || _countryCode == CountryCode.COUNTRY_CODE_UNITED_STATES);
			}
		}
	}
}