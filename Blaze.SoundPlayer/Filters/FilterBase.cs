using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.Filters
{
    public abstract class FilterBase
    {
        public FilterBase(int resolution)
        {
            mCircularBuffer = new float[resolution];
            Initialize();
        }

        protected abstract void Initialize();
        float[] mCircularBuffer;

        public bool Bound { get; protected set; }
        //Filter will have to be stateful to make use of circular buffer, so we'll have to bind it 
        // to a wave output.
    }
}
