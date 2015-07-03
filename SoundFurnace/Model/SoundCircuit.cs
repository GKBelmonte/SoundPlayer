using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer;

namespace Blaze.SoundForge.Model
{
    public class SoundCircuit
    {
        #region Fields/Properties
        //Circuit components
        public GlobalInputComponent InputComponent { get; private set; }
        public GlobalOutputComponent OutputComponent { get; private set; }
        public List<SoundComponent> Components { get; private set; }

        public List<SoundComponent> ComponentComputeOrder { get; private set; }
        #endregion
        public SoundCircuit()
        {
            //Circuit i/o
            InputComponent = new GlobalInputComponent(this);
            OutputComponent = new GlobalOutputComponent(this);
            Components = new List<SoundComponent>();
        }

        public void CycleSetup(int sampleRate, Note note, int sample, int relativeSample)
        {
            //Sample Rate, Absolute Sample, Relative Sample, Absolute Time, Relative Time, Frequency

            for (var ii = 0; ii < samplesPerComputation; ++ii)
            {
                InputComponent.Outputs[0][ii] = sampleRate;
                InputComponent.Outputs[1][ii] = sample + ii;
                InputComponent.Outputs[2][ii] = relativeSample + ii;
                InputComponent.Outputs[3][ii] = (double)(sample + ii) / (double)sampleRate;
                InputComponent.Outputs[4][ii] = (double)(relativeSample + ii) / (double)sampleRate;
                InputComponent.Outputs[5][ii] = note.mFreq;
            }
        }

        public void Compute()
        {
            for (var ii = 0; ii < ComponentComputeOrder.Count; ++ii)
            {
                ComponentComputeOrder[ii].Compute();
            }
        }

        public void Compile()
        {
            foreach (var c in Components)
                c.ResetCompiledFlag();
            InputComponent.ResetCompiledFlag();
            OutputComponent.ResetCompiledFlag();

            List<SoundComponent> order = new List<SoundComponent>(Components.Count + 2);
            OutputComponent.Compile(order);
            order.Reverse();

            ComponentComputeOrder = order; 
        }
    }
}
