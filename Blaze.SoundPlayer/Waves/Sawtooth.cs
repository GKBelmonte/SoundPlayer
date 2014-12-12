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
        }

        public override void Initialize()
        {
            var maxVal = short.MaxValue / 100;
            double step = ((double)maxVal)/((double)(Resolution/2));
            
            for (var ii = 0; ii < Resolution; ++ii)
            {
                _data[ii] = (short)(((double)ii) *step);
            }

            for (int ii = Resolution/2 + 1; ii < Resolution; ++ii)
            {
                _data[ii] -= (short)( 2*maxVal);
            }
        }
    }
}
