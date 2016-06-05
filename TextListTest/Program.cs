using System;
using System.Collections.Generic;
using System.Text;
using NeoSmart.ExtensionMethods;
using NeoSmart.ExtensionMethodTests;

public class ExtensionMethodTests
{
    public static void Main()
    {
        ITest[] tests = new [] { new TextListTest() };

        foreach (var test in tests)
        {
            int failcount;
            test.Test(out failcount);
        }
    }
}
