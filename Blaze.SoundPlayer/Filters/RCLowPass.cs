using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.Filters
{
    public class RCLowPass : Filter
    {
        public float RC { get; private set; }
        public RCLowPass(float cutOff) : base(0,1)
        {
            RC = (float)(1.0 / (2.0 * Math.PI * cutOff));
        }

        public override float Apply(float deltaTime, float freq, float value)
        {
            float alpha = deltaTime / (RC + deltaTime);
            float ret = mY[0] + alpha * (value - mY[0]);
            mY.Push(ret);
            return ret;
        }

    }
}
