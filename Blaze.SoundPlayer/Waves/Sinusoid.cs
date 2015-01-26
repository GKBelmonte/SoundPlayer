using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.Waves
{
    public class Sinusoid : Wave
    {
        public Sinusoid(int resolution)
            : base(resolution)
        {
            Initialize();
        }

        public override void Initialize()
        {
            double step = Math.PI * 2.0 / ((double)Resolution);
            for (var ii = 0; ii < Resolution; ++ii)
            {
                _data[ii] = (float)((WaveProviders.WaveProviderCommon.DefaultAmplitude) * Math.Cos(step * ii));
            }
        }
    }
}
