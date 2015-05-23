//#define BENCH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer.Sounds;
using NAudio.Wave;

namespace Blaze.SoundPlayer.WaveProviders
{
    internal class InstrumentProvider : InstrumentBase, IAdditiveSynthInstrument
    {
        protected List<SimpleSound> mWaves;
        protected List<float> mFreq;
        protected List<float> mAmps;

        public InstrumentProvider(IList<SimpleSound> waves, IList<float> freqMultipliers = null, IList<float> amplitudes = null)
        {
            mWaves = new List<SimpleSound>(waves.Count);
            mWaves.AddRange(waves);
            mFreq = new List<float>(waves.Count);
            mAmps = new List<float>(waves.Count);

            if (freqMultipliers == null)
                foreach (var w in mWaves)
                    mFreq.Add(1.0f);
            else
                mFreq.AddRange(freqMultipliers);

            if (amplitudes == null)
                foreach (SimpleSound s in mWaves)
                    mAmps.Add(1f);
            else
                mAmps.AddRange(amplitudes);

            AmplitudeMultiplier = 1;
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
#if BENCH
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
#endif
            int sampleRate = WaveFormat.SampleRate;
            var sampleNow = mSample;
            var notesThatAreOn = mNoteIsOn.IndexOfAll(x => (x));

            for (int n = 0; n < sampleCount; n++)
            {
                var res = 0f;

                for (var jj = 0; jj < notesThatAreOn.Count; ++jj)
                {
                    var note = mNotes[notesThatAreOn[jj]];
                    var start = note.mStart;
                    var freq = note.mFreq;
                    var vel = note.mVelocity;
                    //Note should stop ringing if it has rang long enough
                    if ((1000f * (float)(mSample - start)) / ((float)sampleRate) > Duration)
                        mNoteIsOn[notesThatAreOn[jj]] = false;

                    for (var ii = 0; ii < mWaves.Count; ++ii)
                        res +=
                            (
                                vel
                                    *
                                (mAmps[ii] * AmplitudeMultiplier)
                                    *
                                mWaves[ii]
                                .Get(sampleRate, mSample - start, freq * mFreq[ii])
                            );
                }

                buffer[n + offset] = res;
                mSample++;
            }

            //Apply filters if any
            float deltaTime = 1 / (float)sampleRate;
            for (var ii = 0; ii < mFilters.Count; ++ii)
                for (int n = 0; n < sampleCount; n++)
                    mFilters[ii].Apply(deltaTime, 0 , buffer[n + offset]);

            #if BENCH
            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
            #endif //BENCH

            return  sampleCount ;
        }

        public IList<float> AmplitudeMultipliers
        {
            get { return mAmps; }
        }

    }
}
