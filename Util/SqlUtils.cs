using System;
using System.Data;
using System.Text.RegularExpressions;

using log4net;

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Summary description for SqlUtils.
	/// </summary>
	public class SqlUtils {

		public SqlUtils() {
		}

		/// <summary>
		/// Earliest date available for SQL smalldatetime - useful value to indicate NULL date
		/// </summary>
		public static DateTime MinSqlDateTime {
			get {
				return new DateTime(1900, 1, 1, 0, 0, 0, 0);
			}
		}
	}
}