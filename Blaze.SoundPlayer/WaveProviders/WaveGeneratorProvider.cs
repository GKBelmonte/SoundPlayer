using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Blaze.SoundPlayer.WaveProviders
{
    internal class WaveGeneratorProvider : WaveProvider32, IWaveProviderExposer
    {
        readonly WaveGenerator _wave;
        public WaveGeneratorProvider (WaveGenerator waveGen)
        {
            _wave = waveGen;
            Frequency = WaveProviderCommon.DefaultFrequency;
            AmplitudeMultiplier = 1;//WaveProviderCommon.DefaultAmplitude;
        }

        public int Resolution
        {
            get;
            protected set;
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
        int sample;
        public override int Read(float[] buffer, int offset, int count)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < count; n++)
            {
                buffer[n + offset] = (AmplitudeMultiplier * _wave(sampleRate,sample,Frequency));
                sample++;
            }
            return count;
        }

    }
}
