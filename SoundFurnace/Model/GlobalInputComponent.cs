using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundForge.Model
{
    public sealed class GlobalInputComponent : SoundComponent
    {
        static readonly public SoundComponentDefinition Definition
            = SoundComponentDefinition.CreateDefinition("Global Input", "", 
            "Sample Rate, Absolute Sample, Relative Sample, Absolute Time, Relative Time, Frequency");

        CircuitInstrument mParent;
        public GlobalInputComponent(CircuitInstrument parent)
            : base(Definition)
        {
            mParent = parent;
        }

        protected override void ComputeIntenal()
        {
            //The parent sets this up
        }
    }
}
