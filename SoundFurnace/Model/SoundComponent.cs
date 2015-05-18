using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundForge.Model
{
    public abstract class SoundComponent 
    {
        public double[] Outputs { get; protected set; }
        public double[] Inputs { get; set; }
        private int[] mInputLinks;
        private SoundComponent[] InputSources { get; set; }

        protected SoundComponentDefinition mDefinition;

        public string[] OutputNames { get { return mDefinition.Outputs.ToArray(); } }
        public string[] InputNames { get { return mDefinition.Inputs.ToArray(); } }

        public string TypeName { get { return mDefinition.TypeName; } } 

        public SoundComponent(string name, int inputs, int outputs)
        {
            var outputNames = new string[outputs];
            var inputNames = new string[inputs];
            for (var ii = 0; ii < inputs; ++ii)
                inputNames[ii] = "x" + ii;
            for (var ii = 0; ii < outputs; ++ii)
                outputNames[ii] = "y" + ii;

            mDefinition = new SoundComponentDefinition(name, inputNames.ToList(), outputNames.ToList());
            Initialize();
        }

        public SoundComponent(SoundComponentDefinition def)
        {
            mDefinition = def;
            Initialize();
        }

        private void Initialize()
        {
            SoundComponentDefinition def = mDefinition;
            Inputs = new double[def.Inputs.Count];
            InputSources = new SoundComponent[def.Inputs.Count];
            mInputLinks = new int[def.Inputs.Count];
            Outputs = new double[def.Outputs.Count];

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

        /// <summary>
        /// This should be overriden (new) by any derived type that does not 
        /// have a default constructor, so that at drop-time, the user
        /// can enter the parameters for the constructor, through some method or another.
        /// </summary>
        /// <returns></returns>
        public static object[] GetParameters()
        {
            return null;
        }
    }
}
