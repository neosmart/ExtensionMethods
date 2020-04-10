using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoSmart.ExtensionMethods
{
    public static class DictionaryExtensions
    {
        public static string SafeLookup<TKey>(this IDictionary<TKey, string> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : "";
        }

        public static TValue SafeLookup<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue ifNotFound)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : ifNotFound;
        }
    }
}
