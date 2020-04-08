using System;
using System.Collections.Generic;
using System.Linq;

namespace NeoSmart.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

#if !NET20 && !NET30 && !NET35
        public static bool IsNullOrWhitespace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
#endif

        public static string Strip(this string s, params char[] matches)
        {
            //We will binary search
            Array.Sort(matches);

            var sb = new System.Text.StringBuilder(s);
            foreach (var match in matches)
            {
                if (Array.BinarySearch(matches, match) < 0)
                {
                    sb.Append(match);
                }
            }

            return sb.ToString();
        }

        private static readonly char[] _whitespace = new[] { ' ', '\t', '\n', '\r' };
        public static string StripWhitespace(this string s)
        {
            return Strip(s, _whitespace);
        }

        public static string Join(this IEnumerable<string> strings, string with = " ")
        {
            return string.Join(with, strings.Where(s => !string.IsNullOrWhiteSpace(s)));
        }

        private static Encoding UTF8 = new UTF8Encoding(false);
        public static byte[] ToUtf8Bytes(this string s)
        {
            return UTF8.GetBytes(s);
        }
    }
}
