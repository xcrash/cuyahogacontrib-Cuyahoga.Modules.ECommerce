using System;

namespace Cuyahoga.Modules.ECommerce.Util.Enums {

	public enum TaxBreakdownType {

		TotalTax = 0,

		//Most of Europe
		GeneralTax1 = 1,
		GeneralTax2 = 2,
		GeneralTax3 = 3,
		GeneralTax4 = 4,

		//US Regional
		UsCityTax = 5,
		UsCountyTax = 6,
		UsStateTax = 7,
		UsOtherTax = 8,

		//Broad US categories
		UsSalesTax = 9,
		UsUseTax = 10
	}
}