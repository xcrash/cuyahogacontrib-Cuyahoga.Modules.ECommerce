namespace Cuyahoga.Modules.ECommerce.Util.Location {
	public interface IAddressFormatter {
		string FormatAddress(IAddress address);
		string FormatAddress(IAddress address, string lineDelimiter, string cultureCode);
        string FormatAddress(IAddress address, string linePrefix, string lineDelimiter, string cultureCode);
    }
}
