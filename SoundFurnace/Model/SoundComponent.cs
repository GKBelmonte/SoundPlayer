using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundFurnace.Model
{
    public abstract class SoundComponent 
    {
        public double[] Outputs { get; protected set; }
        protected double[] Inputs { get; set; }
        private int[] mInputLinks;
        private SoundComponent[] InputSources { get; set; }
        
        public SoundComponent(int inputs, int outputs)
        {
            Inputs = new double[inputs];
            InputSources = new SoundComponent[inputs];
            mInputLinks = new int[inputs];
            Outputs = new double[outputs];
            mComputed = false;
        }

        private bool mComputed;
        public void Compute()
        {
            if (mComputed)
                return;
            for (var ii = 0; ii < Inputs.Length; ++ii)
            {
                if (InputSources[ii] != null)
                {
                    InputSources[ii].Compute();
                    Inputs[ii] = InputSources[ii].Outputs[mInputLinks[ii]];
                }
                else
                    Inputs[ii] = 0;
            }
            ComputeIntenal();
            mComputed = true;
        }

        abstract protected void ComputeIntenal();

        internal void NotifyCycleCompleted()
        {
            mComputed = false;
        }

        public void Link(int inputIndex, SoundComponent outputSource, int outputIndex)
        {
            InputSources[inputIndex] = outputSource;
            mInputLinks[inputIndex] = outputIndex;
        }
    }
}
