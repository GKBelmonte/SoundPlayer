using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer.Waves;

namespace Blaze.SoundForge.Model
{
    class WaveComponent : SoundComponent
    {
        static public readonly SoundComponentDefinition WaveComponentDefinition =
            SoundComponentDefinition.CreateDefinition("Wave", "SampleRate,Sample,Frequency", "Output");
        protected Wave mWave;
        public WaveComponent(Wave wave)
            : base(WaveComponentDefinition)
        {
            mWave = wave;
        }

        protected override void ComputeIntenal()
        {
            for (var ii = 0; ii < SamplesPerComputation; ++ii)
            {
                var sampleRate = (int)Inputs[0][ii];
                var sampleNumber = (int)Inputs[1][ii];
                var freq = (float)Inputs[2][ii];

                Outputs[0][ii] = mWave[sampleNumber, freq * mWave.Resolution / sampleRate];
            }
        }
    }
}
