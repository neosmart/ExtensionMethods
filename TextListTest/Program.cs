using System;
using System.Collections.Generic;
using System.Text;
using NeoSmart.ExtensionMethods.TextList;

namespace NeoSmart.ExtensionMethods.TextList
{
    [Flags] 
    public enum Formatting
    {
        Default = OxfordSeparator | Spaces,
        OxfordSeparator = 1,
        Spaces = 2,
        None = 0
    }

    public static class ExtensionMethods
    {   

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

                var t = buffer.Dequeue();
                //three elements have been guaranteed put aside
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

public class Test
{
    public static void Main()
    {
        var allNames = new [] {"Meg", "Jo", "Beth", "Amy"};
        var names = new List<string>(allNames.Length);

        bool oxford = true;
        bool spaces = true;

        Console.WriteLine("Empty list");
        Console.WriteLine(names.ToTextList());

        for (int i = 0; i < 2; ++i)
        {
            Console.WriteLine("With{0} oxford comma", oxford ? "" : "out");
            for (int j = 0; j < 2; ++j)
            {
                Console.WriteLine("With{0} spaces", spaces ? "" : "out");
                names.Clear();
                foreach (var name in allNames)
                {
                    names.Add(name);
                    Formatting flags = (spaces ? Formatting.Spaces : Formatting.None) | (oxford ? Formatting.OxfordSeparator : Formatting.None);
                    Console.WriteLine(" {0}", names.ToTextList(flags, (i + j) % 2 == 0? "and" : "", ","));
                }
                spaces = !spaces;
                Console.WriteLine();
            }
            oxford = !oxford;
        }
    }
}
