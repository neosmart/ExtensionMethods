using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NeoSmart.ExtensionMethods
{
    public static class JsonExtensions
    {
        //Yes, JsonSerializer is thread safe
        static readonly JsonSerializer Serializer = JsonSerializer.Create();

        public static void WriteJson(this StreamWriter writer, object o)
        {
            Serializer.Serialize(writer, o);
            writer.Flush();
        }

        public static string ToJson(this object o, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(o, formatting);
        }

        public static bool IsJson(this string text)
        {
            if ((!text.StartsWith("{") || !text.EndsWith("}")) && (!text.StartsWith("[") || !text.EndsWith("]")))
            {
                return false;
            }

            try
            {
                JToken.Parse(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
