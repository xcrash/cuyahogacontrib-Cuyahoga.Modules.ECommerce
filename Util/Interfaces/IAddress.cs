using System;

namespace Cuyahoga.Modules.ECommerce.Util.Location {

	public sealed class AddressHelper {

		public static void CopyAddress (IAddress source, IAddress destination) {
            destination.ContactName = source.ContactName;
			destination.AddressLine1 = source.AddressLine1;
			destination.AddressLine2 = source.AddressLine2;
			destination.AddressLine3 = source.AddressLine3;
			destination.City = source.City;
			destination.Region = source.Region;
			destination.Postcode = source.Postcode;
			destination.CountryCode = source.CountryCode;
		}

		public static bool AreSame(IAddress address1, IAddress address2) {

			//Remove obvious cases
			if (address1 == null && address2 != null || address2 == null && address1 != null) {
				return false;
			}

			return (address1.AddressLine1 != address2.AddressLine1
				|| address1.AddressLine2 != address2.AddressLine2
				|| address1.AddressLine3 != address2.AddressLine3
				|| address1.City != address2.City
                || address1.ContactName != address2.ContactName
				|| address1.CountryCode != address2.CountryCode
				|| address1.Postcode != address2.CountryCode
				|| address1.Region != address2.Region);
		}
	}

	/// <summary>
	/// Summary description for IAddress.
	/// </summary>
	public interface IAddress {
        string ContactName { get; set;} //Or recipient name
		string AddressLine1 {get; set;}
		string AddressLine2 {get; set;}
		string AddressLine3 {get; set;}
		string City {get; set;}
		string Region {get; set;}
		string Postcode {get; set;}
		string CountryCode {get; set;}
	}
}