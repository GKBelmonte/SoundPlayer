using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer.Waves;

namespace SoundFurnace.Model
{
    class WaveComponent : SoundComponent
    {
        protected Wave mWave;
        public WaveComponent(Wave wave) : base(SoundComponentDefinition.StandardDefinitions["Wave"])
        {
            mWave = wave;
        }

        protected override void ComputeIntenal()
        {
            var sampleRate = (int) Inputs[0];
            var sampleNumber = (int)Inputs[1];
            var freq = (float)Inputs[2];

            Outputs[0] = mWave[sampleNumber, freq * mWave.Resolution / sampleRate];
        }
    }
}
