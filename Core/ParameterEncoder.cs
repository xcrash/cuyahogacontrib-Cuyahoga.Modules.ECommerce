using System;

namespace Cuyahoga.Modules.ECommerce.Core {

    public sealed class ParameterEncoder {

        public const string FORWARD_SLASH = @"/";
        public const string HYPHEN = @"-";
        public const string REPLACEMENT_CHAR = "_";
        public const string HYPHEN_REPLACEMENT = "0x2D";
        

        public static string EncodeParameterValue(string escapedValue) {
            if (escapedValue != null && escapedValue.Length > 0) {
                return escapedValue.Replace(REPLACEMENT_CHAR, REPLACEMENT_CHAR + REPLACEMENT_CHAR).Replace(FORWARD_SLASH, REPLACEMENT_CHAR).Replace(HYPHEN, HYPHEN_REPLACEMENT);
            }
            return escapedValue;
        }

        public static string DecodeParameterValue(string escapedValue) {
            if (escapedValue != null && escapedValue.Length > 0) {
                return escapedValue.Replace(REPLACEMENT_CHAR + REPLACEMENT_CHAR, REPLACEMENT_CHAR).Replace(REPLACEMENT_CHAR, FORWARD_SLASH).Replace(HYPHEN_REPLACEMENT, HYPHEN); 
            }
            return escapedValue;
        }
    }
}
