using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Blaze.SoundPlayer.Waves;

namespace Blaze.SoundPlayer.WaveProviders
{
    internal class FixedDataWaveProvider : WaveProvider32, IWaveProviderExposer
    {
        int sample;
        readonly Wave _wave;
        public FixedDataWaveProvider(Wave wave )
        {
            Frequency = 1000;
            AmplitudeMultiplier = 1;// WaveProviderCommon.DefaultAmplitude;
            _wave = wave;
            _wave.Initialize();
        }

        public int Resolution { get; protected set; } 

        public float Frequency { get; set; }


        float _amplitude;
        /// <summary>
        /// 0 - 100 (%)
        /// </summary>
        public float AmplitudeMultiplier
        {
            get { return _amplitude; } 
            set { if (value < 0) _amplitude = 0; else if (value > 100) _amplitude = 100; else _amplitude = value; } 
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;//Hz
            double baseFreq = (double)sampleRate / (double)_wave.Resolution;
            double freqMultiplier = Math.Round(Frequency / baseFreq);
            for (int n = 0; n < sampleCount; n++)
            {
                //(short)(Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                buffer[n + offset] = (AmplitudeMultiplier*_wave[sample, freqMultiplier]);
                sample++;
            }
            return sampleCount;
        }

        public void AddToBuffer(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;//Hz
            double baseFreq = (double)sampleRate / (double)_wave.Resolution;
            int freqMultiplier = (int)Math.Round(Frequency / baseFreq);
            for (int n = 0; n < sampleCount; n++)
            {
                //(short)(Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                buffer[n + offset] += (AmplitudeMultiplier * _wave[sample, freqMultiplier]); 

                sample++;
            }
        }
    }
}
