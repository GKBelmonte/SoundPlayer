using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Blaze.SoundPlayer.WaveProviders
{
    public class SineWaveProvider : WaveProvider16 , IWaveProviderExposer
    {
        int sample;
 
        public SineWaveProvider()
        {
            Frequency = 1000;
            Amplitude = 3500;
            Resolution = -1;
        }

        public int Resolution { get; protected set; } 
        public float Frequency { get; set; }
        public short Amplitude { get; set; }
 
        public override int Read(short[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n++)
            {
                buffer[n+offset] = (short)(Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                sample++;
                if (sample >= sampleRate) sample = 0;
            }
            return sampleCount;
        }
    }
}
