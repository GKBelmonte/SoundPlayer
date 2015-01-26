using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Blaze.SoundPlayer.WaveProviders
{
    internal class SineWaveProvider : WaveProvider32, IWaveProviderExposer
    {
        int sample;
 
        public SineWaveProvider()
        {
            Frequency = 1000;
            AmplitudeMultiplier =1;// WaveProviderCommon.DefaultAmplitude;
            Resolution = -1;
        }

        public int Resolution { get; protected set; } 
        public float Frequency { get; set; }
        public float AmplitudeMultiplier { get; set; }
 
        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n++)
            {
                buffer[n+offset] = (float) (AmplitudeMultiplier * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                sample++;
                if (sample >= sampleRate) sample = 0;
            }
            return sampleCount;
        }
    }
}
