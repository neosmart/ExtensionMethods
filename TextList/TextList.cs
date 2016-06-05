using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSmart.ExtensionMethods
{
    public static class TextList
    {   
        [Flags] 
        public enum Formatting
        {
            Default = OxfordSeparator | Spaces,
            OxfordSeparator = 1,
            Spaces = 2,
            None = 0
        }

        public static string ToTextList<T>(this IEnumerable<T> entities, Formatting formatting = Formatting.Default, string conjunction = "and", string separator = ",")
        {
            bool oxfordComma = (formatting & Formatting.OxfordSeparator) == Formatting.OxfordSeparator;

            var sb = new StringBuilder();
            Queue<T> buffer = new Queue<T>(4);
            string space = ((formatting & Formatting.Spaces) == Formatting.Spaces) ? " " : string.Empty;

            foreach (var e in entities)
            {
                buffer.Enqueue(e);
                if (buffer.Count < 4)
                {
                    continue;
                }

                //three elements have been guaranteed put aside
                var t = buffer.Dequeue();
                sb.AppendFormat("{0}{1}{2}", t, separator, space);
            }

            if (buffer.Count >= 1)
            {
                sb.Append(buffer.Dequeue());
            }

            //remember, buffer count has dropped
            if (buffer.Count == 1) //means guaranteed only ever two entries
            {
                if (string.IsNullOrEmpty(conjunction))
                {
                    sb.AppendFormat("{0}{1}", separator, space, buffer.Dequeue());
                }
                else
                {
                    sb.AppendFormat("{0}{1}{2}{3}", space, conjunction, space, buffer.Dequeue());
                }
            }
            else if (buffer.Count == 2)
            {
                sb.AppendFormat("{0}{1}{2}{3}{4}", separator, space, buffer.Dequeue(), oxfordComma ? separator : string.Empty, space);
                if (string.IsNullOrEmpty(conjunction))
                {
                    sb.Append(buffer.Dequeue());
                }
                else
                {
                    sb.AppendFormat("{0}{1}{2}", conjunction, space, buffer.Dequeue());
                }       
            }
            return sb.ToString();
        }
    }    
}
