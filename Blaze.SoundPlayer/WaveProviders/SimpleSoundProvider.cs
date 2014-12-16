using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Blaze.SoundPlayer.Sounds;

namespace Blaze.SoundPlayer.WaveProviders
{
    internal class SimpleSoundProvider : WaveProvider16, IWaveProviderExposer
    {
        int sample;
        SimpleSound mSound;
        public SimpleSoundProvider(SimpleSound sound)
        {
            mSound = sound;
            Amplitude = 15;
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

        public float Amplitude
        {
            get;
            set;
        }

        public override int Read(short[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n++)
            {
                buffer[n + offset] = (short)(Amplitude * mSound.Get(sampleRate, sample, Frequency));
                sample++;
            }
            return sampleCount;
        }
    }
}
