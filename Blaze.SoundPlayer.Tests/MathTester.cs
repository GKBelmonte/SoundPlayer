using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blaze.SoundPlayer.Tests
{
    [TestClass]
    public class MathTester
    {
        struct Tripple<T>
        {
            public T A;
            public T B;
            public T C;
            public Tripple(T a, T b, T c)
            {
                A = a;
                B = b;
                C = c;
            }
            public T this[int ii]
            {
                get
                {
                    switch (ii)
                    {
                        case 0: return A;
                        case 1: return B;
                        case 2: return C;
                        default: throw new IndexOutOfRangeException("Index must be less than 2, gt 0");
                    }
                }
            }
        }

        [TestMethod]
        public void TestIntegerPow()
        {
            var testVals = new Tripple<int>[] { 
                new Tripple<int>(2,4,16),
                new Tripple<int>(2,6,64),
                new Tripple<int>(2,8,256),
                new Tripple<int>(2,1,2),
                new Tripple<int>(2,0,1),
                new Tripple<int>(3,3,27),
                new Tripple<int>(8,5,32768)
            };

            foreach (var test in testVals)
            {
                Assert.AreEqual(test[2], MathExtensions.IntegerPow(test[0], test[1]));
            }
        }
    }
}
