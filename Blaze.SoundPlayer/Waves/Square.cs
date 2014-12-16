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
            var sum=0;
            for (var ii = 0; ii < Resolution; ++ii)
            {
                _data[ii] = (short) (ii < Width ? short.MaxValue / 128 : -short.MaxValue / 128);
                sum += _data[ii];
            }
            sum = sum / Resolution;//remove dc component
            for (var ii = 0; ii < Resolution; ++ii)
            {
                _data[ii] -= (short)sum;
            }
        }
    }
}
