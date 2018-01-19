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

        public static bool IsNullOrWhitespace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

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

        private static char[] _whitespace = new[] { ' ', '\t', '\n', '\r' };
        public static string StripWhitespace(this string s, params char[] matches)
        {
            return Strip(s, _whitespace);
        }

        public static string Join(this IEnumerable<string> strings, string with = " ")
        {
            return string.Join(with, strings.Where(s => !string.IsNullOrWhiteSpace(s)));
        }
    }
}
