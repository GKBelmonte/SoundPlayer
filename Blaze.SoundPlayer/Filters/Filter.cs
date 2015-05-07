using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.Filters
{
    public abstract class Filter
    {
        public Filter(int xMem, int yMem)
        {
            mX = new Utils.CircularBuffer<float>(xMem);
            mY = new Utils.CircularBuffer<float>(yMem);
        }

        //protected abstract void Initialize();
        protected Utils.CircularBuffer<float> mX;
        protected Utils.CircularBuffer<float> mY;

        abstract public float Apply(float deltaTime, float freq, float value);        
        
        public bool Bound { get; protected set; }
        //Filter will have to be stateful to make use of circular buffer, so we'll have to bind it 
        // to a wave output.
    }
}
