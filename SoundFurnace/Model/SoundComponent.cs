﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundForge.Model
{
    public abstract class SoundComponent 
    {
        public double[][] Outputs { get; protected set; }
        public double[][] Inputs { get; set; }
        private int[] mInputLinks;
        private SoundComponent[] InputSources { get; set; }
        public int SamplesPerComputation { get; private set; }

        protected SoundComponentDefinition mDefinition;

        public string[] OutputNames { get { return mDefinition.Outputs.ToArray(); } }
        public string[] InputNames { get { return mDefinition.Inputs.ToArray(); } }

        public string TypeName { get { return mDefinition.TypeName; } } 

        public System.Drawing.Point Location {get; set;}

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
            
            InputSources = new SoundComponent[def.Inputs.Count];
            mInputLinks = new int[def.Inputs.Count];

            Inputs = new double[def.Inputs.Count];

            mComputed = false;
        }

        public void SetSamplesPerComputation(int samplesPerComputation)
        {
            for (var ii = 0; ii < Inputs.Length; ++ii)
                Inputs[ii] = new double[samplesPerComputation];
            for (var ii = 0; ii < Outputs.Length; ++ii)
                Outputs[ii] = new double[samplesPerComputation];
            SamplesPerComputation = samplesPerComputation;
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
                //else
                    //Inputs[ii] = new double[samplePerCall] 
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
