using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using Blaze.SoundPlayer.Waves;
using System.Collections;

namespace Blaze.SoundPlayer.WaveProviders
{
    public class CompositeFixedDataWaveProvider : WaveProvider16 , IWaveProviderExposer
    {
        int sample;
        readonly protected List<FixedDataWaveProvider> _waves;
        public CompositeFixedDataWaveProvider()
        {
            Frequency = 1000;
            Amplitude = 5;
            _waves = new List<FixedDataWaveProvider>();
            Resolution = 1024 * 4;
        }

        public CompositeFixedDataWaveProvider(IList<Wave> waves) : this()
        {
            foreach(var wav in waves)
                _waves.Add(new FixedDataWaveProvider(wav));
        }

        public IList<float> Frequencies 
        {
            get 
            {
                List<float> res = new List<float>(_waves.Count);
                foreach(var wav in _waves)
                    res.Add(wav.Frequency);
                return res;
            }
            set
            {
                for (var ii = 0; ii < value.Count; ++ii)
                {
                    _waves[ii].Frequency = value[ii];
                }
            }
        }

        public int Resolution { get; protected set; } 

        public float Frequency { get; set; }

        public void AddWave(FixedDataWaveProvider wave)
        {
            _waves.Add(wave);
        }


        short _amplitude;
        /// <summary>
        /// 0 - 100 (%)
        /// </summary>
        public short Amplitude
        {
            get { return _amplitude; } 
            set { if (value < 0) _amplitude = 0; else if (value > 100) _amplitude = 100; else _amplitude = value; } 
        }

        public override int Read(short[] buffer, int offset, int sampleCount)
        {
            sample += sampleCount;
            foreach (FixedDataWaveProvider wave in _waves)
                wave.AddToBuffer(buffer, offset, sampleCount);
            return sampleCount;
        }
    }
}
