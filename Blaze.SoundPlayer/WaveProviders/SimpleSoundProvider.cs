using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Blaze.SoundPlayer.Sounds;

namespace Blaze.SoundPlayer.WaveProviders
{
    internal class SimpleSoundProvider : WaveProvider32, IWaveProviderExposer
    {
        int sample;
        SimpleSound mSound;
        public SimpleSoundProvider(SimpleSound sound)
        {
            mSound = sound;
            AmplitudeMultiplier = 1;// WaveProviderCommon.DefaultAmplitude;
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

        public float AmplitudeMultiplier
        {
            get;
            set;
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n++)
            {
                buffer[n + offset] = (AmplitudeMultiplier * mSound.Get(sampleRate, sample, Frequency));
                sample++;
            }
            return sampleCount;
        }
    }
}
