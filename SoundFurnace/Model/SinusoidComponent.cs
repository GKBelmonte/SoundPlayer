using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundForge.Model
{
    public class SinusoidComponent : SoundComponent
    {
        public SinusoidComponent() : base(SoundComponentDefinition.StandardDefinitions["Wave"])
        {
            
        }

        protected override void ComputeIntenal()
        {
            for (var ii = 0; ii < SamplesPerComputation; ++ii)
            {
                var sampleRate = (int)Inputs[0][ii];
                var sampleNumber = (int)Inputs[1][ii];
                var freq = (float)Inputs[2][ii];

                Outputs[0][ii] = (float)(Math.Sin((2 * Math.PI * sampleNumber * freq) / sampleRate));
            }
        }
    }
}
