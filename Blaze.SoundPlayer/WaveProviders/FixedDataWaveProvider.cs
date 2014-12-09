using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Blaze.SoundPlayer.Waves;

namespace Blaze.SoundPlayer.WaveProviders
{
    public class FixedDataWaveProvider : WaveProvider16, IWaveProviderExposer
    {
        int sample;
        readonly Wave _wave;
        public FixedDataWaveProvider(Wave wave )
        {
            Frequency = 1000;
            Amplitude = 100;
            _wave = wave;
            _wave.Initialize();
        }

        public float Frequency { get; set; }

        float _amplitude;
        /// <summary>
        /// 0 - 500 (%)
        /// </summary>
        public float Amplitude
        {
            get { return _amplitude; } 
            set { if (value < 0) _amplitude = 0; else if (value > 500) _amplitude = 500; else _amplitude = value; } 
        }

        public override int Read(short[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;//Hz
            double baseFreq = (double)sampleRate / (double)_wave.Resolution;
            int freqMultiplier = (int) Math.Round(Frequency / baseFreq);
            for (int n = 0; n < sampleCount; n++)
            {
                //(short)(Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                buffer[n + offset] = _wave[sample, Amplitude, freqMultiplier];
                sample++;
            }
            return sampleCount;
        }
    }
}
