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
            //Reset all the elements
            for (var ii = 0; ii < Components.Count; ++ii)
                Components[ii].NotifyCycleCompleted();
            InputComponent.Outputs[0] = sampleRate;
            InputComponent.Outputs[1] = sample;
            InputComponent.Outputs[2] = relativeSample;
            InputComponent.Outputs[3] = (double)sample / (double)sampleRate;
            InputComponent.Outputs[4] = (double)relativeSample / (double)sampleRate;
            InputComponent.Outputs[5] = note.mFreq;
        }
    }
}
