using System;
using NeoSmart.ExtensionMethods;

namespace NeoSmart.ExtensionMethodTests
{
    public class StringTests : ITest
    {
        public bool Test(out int failCount)
        {
            failCount = 2;
            try
            {
                string s = null;
                Console.WriteLine("(null string).IsNullOrEmpty(): {0}", s.IsNullOrEmpty());
                --failCount;
                Console.WriteLine("(null string).IsNullOrWhitespace(): {0}", s.IsNullOrWhitespace());
                --failCount;
            }
            catch
            {
            }

            return failCount == 0;
        }
    }
}
