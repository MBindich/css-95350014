using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public class SomeClass
    {
        public string someString;
        public int someInt;
        public double someDouble;

        public SomeClass(string sString, int sInt, double sDouble)
        {
            someString = sString;
            someInt = sInt;
            someDouble = sDouble;
        }

        public int getSum(int a, int b)
        {
            return a + b;
        }
    }
}
