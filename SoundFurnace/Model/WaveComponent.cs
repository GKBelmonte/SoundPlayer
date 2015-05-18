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
            var sampleRate = (int) Inputs[0];
            var sampleNumber = (int)Inputs[1];
            var freq = (float)Inputs[2];

            Outputs[0] = mWave[sampleNumber, freq * mWave.Resolution / sampleRate];
        }
    }
}
