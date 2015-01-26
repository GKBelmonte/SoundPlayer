using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.Waves
{
    public class Sawtooth : Wave
    {
        public Sawtooth(int resolution)
            : base(resolution)
        {
            Initialize();
        }

        public override void Initialize()
        {
            var maxVal = WaveProviders.WaveProviderCommon.DefaultAmplitude;
            float step = (maxVal) / ((float)(Resolution / 2));

            for (float ii = 0; ii < Resolution; ++ii)
            {
                _data[(int)ii] = (ii *step);
            }

            for (int ii = Resolution/2 + 1; ii < Resolution; ++ii)
            {
                _data[ii] -= (float)( 2*maxVal);
            }
        }
    }
}
