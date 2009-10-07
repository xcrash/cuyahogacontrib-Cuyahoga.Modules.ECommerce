using System;
using Cuyahoga.Modules.ECommerce.Util.Location;

namespace Cuyahoga.Modules.ECommerce.Util.Interfaces {

	public sealed class UserDetailsHelper {

		public static void CopyUserDetails (IUserDetails source, IUserDetails destination) {
			destination.FirstName = source.FirstName;
			destination.LastName = source.LastName;
			destination.EmailAddress = source.EmailAddress;
            destination.TelephoneNumber = source.TelephoneNumber;
			destination.UserID = source.UserID;
            destination.CompanyName = source.CompanyName;
		}
	}

	/// <summary>
	/// General form of a user, registered or not
	/// </summary>
	public interface IUserDetails {

		string FirstName {get; set;}
		string LastName {get; set;}

		string EmailAddress {get; set;}
        string TelephoneNumber { get; set; }
        string FaxNumber { get; set; }
        string CompanyName { get; set; }
        long UserID { get; set;}
	}
}