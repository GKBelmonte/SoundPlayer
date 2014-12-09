using System;
using Blaze.SoundPlayer.Waves;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blaze.SoundPlayer.Tests
{
    [TestClass]
    public class WaveTester : Wave
    {
        public WaveTester() : base(1024) { }

        [TestMethod]
        public void TestGetPowAndMult()
        {
            double[] testVals =
            new double []{
                0,
                8,
                1.25,
                4.75,
                7.35,
                3.5,
                17.125,
                8.375
            };

            int[] powOutputs = new int[] {
                0,
                0,
                2,
                2,
                5,
                1,
                3,
                3
            };

            int[] multOutputs = new int[] {
                0,
                0,
                1,
                3,
                11,
                1,
                1,
                3
            };

            for (var ii = 0; ii < testVals.Length; ++ii)
            {
                int pow;
                int mult;
                GetPowerAndMultiplier(testVals[ii], out pow, out mult);
                Assert.AreEqual( powOutputs[ii], pow, string.Format("Power is wrong for test case {0}:{1}", ii, testVals[ii]));
                Assert.AreEqual(multOutputs[ii], mult, string.Format("Multiplier is wrong for test case {0}:{1}", ii, testVals[ii]));
            }
        }
    }
}
