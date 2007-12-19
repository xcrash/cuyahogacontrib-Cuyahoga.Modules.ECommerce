using System;

namespace Cuyahoga.Modules.ECommerce.Core {

    public sealed class ParameterEncoder {

        public const string FORWARD_SLASH = @"/";
        public const string REPLACEMENT_CHAR = "_";

        public static string EncodeParameterValue(string escapedValue) {
            if (escapedValue != null && escapedValue.Length > 0) {
                return escapedValue.Replace(REPLACEMENT_CHAR, REPLACEMENT_CHAR + REPLACEMENT_CHAR).Replace(FORWARD_SLASH, REPLACEMENT_CHAR);
            }
            return escapedValue;
        }

        public static string DecodeParameterValue(string escapedValue) {
            if (escapedValue != null && escapedValue.Length > 0) {
                return escapedValue.Replace(REPLACEMENT_CHAR + REPLACEMENT_CHAR, REPLACEMENT_CHAR).Replace(REPLACEMENT_CHAR, FORWARD_SLASH);
            }
            return escapedValue;
        }
    }
}
