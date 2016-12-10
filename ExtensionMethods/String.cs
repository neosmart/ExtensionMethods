using System;

namespace NeoSmart.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhitespace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static string StripWhitespace(this string s)
        {
            return s.Trim(' ', '\t', '\r', '\n');
        }
    }
}
