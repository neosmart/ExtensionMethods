using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoSmart.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
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

        private static readonly char[] _whitespace = new[] { ' ', '\t', '\n', '\r' };
        public static string StripWhitespace(this string s)
        {
            return Strip(s, _whitespace);
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
#if NET20 || NET30 || NET35
            if (s is null || s.Length == 0)
            {
                return true;
            }

            foreach(var c in s)
            {
                if (c == ' ' || c == '\t' || c == '\v' || c == '\r' || c == '\n')
                {
                    return true;
                }
            }

            return false;
#else
            return string.IsNullOrWhiteSpace(s);
#endif
        }

        public static string Join(this string[] strings, string with = " ")
        {
#if NET20 || NET30 || NET35
            return string.Join(with, strings.Where(s => !s.IsNullOrWhiteSpace()).ToArray());
#else
            // Check if we must before using the IEnumerable<string> instead of string[] override
            if (strings.Any(IsNullOrWhiteSpace))
            {
                return string.Join(with, strings.Where(s => !s.IsNullOrWhiteSpace()));
            }
            else
            {
                return string.Join(with, strings);
            }
#endif
        }

        public static string Join(this IEnumerable<string> strings, string with = " ")
        {
#if NET20 || NET30 || NET35
            return string.Join(with, strings.Where(s => !s.IsNullOrWhiteSpace()).ToArray());
#else
            return string.Join(with, strings.Where(s => !s.IsNullOrWhiteSpace()));
#endif
        }

        private static Encoding UTF8 = new UTF8Encoding(false);
        public static byte[] ToUtf8Bytes(this string s)
        {
            return UTF8.GetBytes(s);
        }
    }
}
