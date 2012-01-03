using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestFastScript
{
    static class Extentions
    {
        public static bool areArraysEqual(this object[] actual, object[] expected)
        {
            //check length
            if (expected.Length != actual.Length)
            {
                return false;
            }

            //check whole array
            for (int i = 0; i < expected.Length; i++)
            {
                if (expected[i].ToString() != actual[i].ToString())
                {
                    return false;
                }
            }

            return true;    // must be the same
        }
    }
}
