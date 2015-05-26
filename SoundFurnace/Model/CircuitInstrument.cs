using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer;
using Blaze.SoundPlayer.WaveProviders;
using Blaze.SoundPlayer.Filters;
namespace Blaze.SoundForge.Model
{
    public class CircuitInstrument : InstrumentBase
    {
        #region Fields/Properties
        //Circuit components
        public SoundCircuit Circuit { get; private set; }
        #endregion
        public CircuitInstrument()
        {
            //Circuit i/o
            Circuit = new SoundCircuit();

            mComponents.Add(InputComponent);
            mComponents.Add(OutputComponent);
        }

        public void Initialize()
        {
            foreach (var comp in mComponents)
            {
                comp.SetSamplesPerComputation(64);
            }
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            float sampleRate = SampleRate;
            
            var notesThatAreOn = mNoteIsOn.IndexOfAll(x => (x));

            for (var jj = 0; jj < notesThatAreOn.Count; ++jj)
            {
                //reset sample
                var sampleNow = mSample;
                //get info
                var note = mNotes[notesThatAreOn[jj]];
                var start = note.mStart;
                var freq = note.mFreq;
                var vel = note.mVelocity;
                
                //Note should stop ringing if it has rang long enough
                var effectiveSampleCount = sampleCount;
                if ((1000f * (float)((sampleNow + sampleCount) - start)) / sampleRate > Duration)
                {
                    mNoteIsOn[notesThatAreOn[jj]] = false;
                    effectiveSampleCount = (int)(Duration * (float)sampleRate / 1000f + (float)start - (float)sampleNow);
                }
                //Compute the sum of all notes currently playing at sample mSample
                CycleSetup(note, sampleNow, sampleNow - start);
                OutputComponent.Compute();

                for (int n = 0; n < sampleCount; n++)
                {
                    buffer[n + offset] += (float) OutputComponent.Inputs[0][n];
                    sampleNow++;
                }
            }
            mSample += sampleCount;

            //Apply filters if any
            float deltaTime = 1 / (float)sampleRate;
            for (var ii = 0; ii < mFilters.Count; ++ii)
                for (int n = 0; n < sampleCount; n++)
                    mFilters[ii].Apply(deltaTime, 0, buffer[n + offset]);

            return sampleCount;
        }

        private void CycleSetup(Note note, int sample, int relativeSample)
        {
            Circuit.CycleSetup(SampleRate, note, sample, relativeSample);
            var samplesPerComputation = InputComponent.SamplesPerComputation;
            for (var ii = 0; ii < samplesPerComputation; ++ii)
            {
                InputComponent.Outputs[0][ii] = SampleRate;
                InputComponent.Outputs[1][ii] = sample + ii;
                InputComponent.Outputs[2][ii] = relativeSample + ii;
                InputComponent.Outputs[3][ii] = (double)(sample + ii) / (double)SampleRate;
                InputComponent.Outputs[4][ii] = (double)(relativeSample + ii) / (double)SampleRate;
                InputComponent.Outputs[5][ii] = note.mFreq;
            }
        }

    }
}
