using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer.Sounds;
using NAudio.Wave;

namespace Blaze.SoundPlayer.WaveProviders
{
    class AdditiveSynthesisWaveProvider : WaveProvider16, IWaveProviderExposer
    {
        int sample;
        List<SimpleSound> mWaves;
        public AdditiveSynthesisWaveProvider(IList<SimpleSound> waves)
        {
            mWaves = new List<SimpleSound>(waves.Count);
            mWaves.AddRange(waves);
            Amplitude = 5;
            Frequency = 440;
        }



        public override int Read(short[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n++)
            {
                int res = 0;
                foreach (SimpleSound w in mWaves)
                    res += Amplitude * w.Get(sampleRate, sample, Frequency);

                buffer[n + offset] = (short)res;
                sample++;
            }
            return sampleCount;
        }

        public int Resolution
        {
            get;
            private set;
        }

        public float Frequency
        {
            get;
            set;
        }

        public short Amplitude
        {
            get;
            set;
        }
    }
}
