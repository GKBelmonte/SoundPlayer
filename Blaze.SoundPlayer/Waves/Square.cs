using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.Waves
{
    public class Square : Wave
    {
        public int Width { get; private set; }
        public Square(int resolution, int width):base(resolution)
        {
            if(width > resolution)
                throw new NotSupportedException("width cannot be greater than resolution");
            Width = width;
            Initialize();
        }

        public override void Initialize()
        {
            var max = WaveProviders.WaveProviderCommon.DefaultAmplitude;
            var sum=0f;
            for (var ii = 0; ii < Resolution; ++ii)
            {
                _data[ii] = (ii < Width ? max : -max);
                sum += _data[ii];
            }
            sum = sum / Resolution;//remove dc component
            for (var ii = 0; ii < Resolution; ++ii)
            {
                _data[ii] -= sum;
            }
        }
    }
}
