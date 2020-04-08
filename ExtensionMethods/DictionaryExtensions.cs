using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoSmart.ExtensionMethods
{
    public static class DictionaryExtensions
    {
        public static TValue SafeLookup<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue ifNotFound)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : ifNotFound;
        }
    }
}
