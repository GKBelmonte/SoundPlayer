using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.WaveProviders
{
    public class WaveProviderCommon
    {
        static WaveProviderCommon()
        {
            DefaultAmplitude = 5;
            DefaultPhase = 0;
            DefaultFrequency = 440;
        }
        static public float DefaultFrequency
        {
            get;
            set;
        }

        static public short DefaultAmplitude
        { get; set; }

        static public int DefaultPhase
        { get; set; }
    }
}
