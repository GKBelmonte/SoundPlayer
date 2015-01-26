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
            DefaultAmplitude = 0.10f;
            DefaultPhase = 0;
            DefaultFrequency = 440;
        }
        static public float DefaultFrequency
        {
            get;
            set;
        }

        static public float DefaultAmplitude
        { get; set; }

        static public float DefaultPhase
        { get; set; }
    }
}
