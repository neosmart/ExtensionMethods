using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NeoSmart.ExtensionMethods
{
    public static class NameValueCollectionExtensions
    {
        public static bool TryGetValue(this NameValueCollection dictionary, string key, out string value)
        {
            if (dictionary.AllKeys.Contains(key))
            {
                value = dictionary.Get(key);
                return true;
            }

            value = null;
            return false;
        }

        public static string SafeLookup(this NameValueCollection dictionary, string key, string ifNotFound = "")
        {
            string value;
            return dictionary.TryGetValue(key, out value) ? value : ifNotFound;
        }

        public static bool Contains(this NameObjectCollectionBase.KeysCollection keys, string key)
        {
            for (int i = 0; i < keys.Count; ++i)
            {
                var item = keys.Get(i);
                if (item == key)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
