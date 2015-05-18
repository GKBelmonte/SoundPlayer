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
            var sampleRate = (int)Inputs[0];
            var sampleNumber = (int)Inputs[1];
            var freq = (float)Inputs[2];

            Outputs[0] = (float)(Math.Sin((2 * Math.PI * sampleNumber * freq) / sampleRate));
        }
    }
}
