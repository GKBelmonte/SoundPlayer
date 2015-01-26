using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer.Sounds;
using NAudio.Wave;

namespace Blaze.SoundPlayer.WaveProviders
{
    internal class AdditiveSynthesisWaveProvider : WaveProvider32, IWaveProviderExposer
    {
        protected int sample;
        protected List<SimpleSound> mWaves;
        protected List<float> mFreq;
        protected List<float> mAmps;
        public AdditiveSynthesisWaveProvider(IList<SimpleSound> waves, IList<float> freqs =null, IList<float> amplitudes=null)
        {
            mWaves = new List<SimpleSound>(waves.Count);
            mWaves.AddRange(waves);
            mFreq = new List<float>(waves.Count);
            mAmps = new List<float>(waves.Count);
            if (freqs != null)
                mFreq.AddRange(freqs);
            else
                mFreq = null;
            
            if (amplitudes != null)
                mAmps.AddRange(amplitudes);
            else
                mAmps = null;
            AmplitudeMultiplier = 1;//WaveProviderCommon.DefaultAmplitude;
            Frequency = 440;
        }



        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n++)
            {
                var res = 0f;
                for (var ii = 0; ii < mWaves.Count;++ii)
                    res +=
                        ( 
                            (mAmps !=null ? 
                                mAmps[ii]: 
                                AmplitudeMultiplier) *
                            mWaves[ii]
                            .Get(sampleRate, sample, mFreq!= null? mFreq[ii]:Frequency) 
                        );

                buffer[n + offset] = res;
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

        public float AmplitudeMultiplier
        {
            get;
            set;
        }
    }
}
