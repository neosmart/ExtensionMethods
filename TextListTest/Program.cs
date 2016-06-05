using System;
using System.Collections.Generic;
using System.Text;
using NeoSmart.ExtensionMethods;

public class Test
{
    public static void Main()
    {
        var allNames = new [] { "Meg", "Jo", "Beth", "Amy" };
        var names = new List<string>(allNames.Length);

        bool oxford = true;
        bool spaces = true;
        bool conjunction = true;

        Func<bool, string> with = x => x ? "" : "out";

        Console.WriteLine("***Empty list***");
        Console.WriteLine(names.ToTextList());

        for (int i = 0; i < 2; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                for (int k = 0; k < 2; ++k)
                {
                    Console.WriteLine("***With{0} oxford comma, with{1} spaces, with{2} conjunction***", with(oxford), with(spaces), with(conjunction));

                    names.Clear();
                    foreach (var name in allNames)
                    {
                        names.Add(name);
                        var flags = (spaces ? TextList.Formatting.Spaces : TextList.Formatting.None) |
                            (oxford ? TextList.Formatting.OxfordSeparator : TextList.Formatting.None);
                        Console.WriteLine(" {0}", names.ToTextList(flags, conjunction ? "and" : string.Empty));
                    }
                    Console.WriteLine();
                    conjunction = !conjunction;
                }
                spaces = !spaces;
            }
            oxford = !oxford;
        }
    }
}
