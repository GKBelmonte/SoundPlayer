using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer
{
    public static class MathExtensions
    {
        public static int IntegerPow(int b, int exponent)
        {
            var ii = 1;
            if (exponent == 0)
                return 1;
            var res = b;
            while (ii < exponent && 2* ii <= exponent )
            {
                res = res * res;
                ii = 2 * ii;
            }
            if (ii == exponent)
                return res;
            else
                return res * IntegerPow(b, exponent - ii);
        }

        public static int PowerOfTwo(int pow)
        {
            return 1 << pow;
        }
    }
}
