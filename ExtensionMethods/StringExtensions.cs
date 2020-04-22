using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace NeoSmart.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        private static string StripSorted(this string s, params char[] matches)
        {
            var sb = new StringBuilder(s.Length);
            foreach (var c in s)
            {
                if (Array.BinarySearch(matches, c) < 0)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public static string Strip(this string s, params char[] matches)
        {
            // We will binary search
            Array.Sort(matches);
            return StripSorted(s, matches);
        }

        private static readonly char[] _whitespace = new[] { ' ', '\t', '\n', '\r' };
        public static string StripWhitespace(this string s)
        {
            return Strip(s, _whitespace);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use the alternate spelling IsNullOrWhitespace with a lowercase s in Whitespace")]
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return IsNullOrWhitespace(s);
        }

        public static bool IsNullOrWhitespace(
#if NETSTANDARD2_1 || NETCOREAPP3_0 || NETCOREAPP3_1
            [NotNullWhen(false)]
#endif
        this string s)
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
            return string.Join(with, strings.Where(s => !s.IsNullOrWhitespace()).ToArray());
#else
            // Check if we must before using the IEnumerable<string> instead of string[] override
            if (strings.Any(IsNullOrWhitespace))
            {
                return string.Join(with, strings.Where(s => !s.IsNullOrWhitespace()));
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
            return string.Join(with, strings.Where(s => !s.IsNullOrWhitespace()).ToArray());
#else
            return string.Join(with, strings.Where(s => !s.IsNullOrWhitespace()));
#endif
        }

        private static Encoding UTF8 = new UTF8Encoding(false);
        public static byte[] ToUtf8Bytes(this string s)
        {
            return UTF8.GetBytes(s);
        }
    }
}
