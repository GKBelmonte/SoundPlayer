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
            mX = new float[xMem];
            mY = new float[yMem];
            for (var ii = 0; ii < xMem; ++ii)
                mX[0] = 0f;
            for (var ii = 0; ii < yMem; ++ii)
                mY[0] = 0f;
            //Initialize();
        }

        //protected abstract void Initialize();
        protected float[] mX;
        protected float[] mY;

        abstract public float Apply(float deltaTime, float freq, float value);        
        
        public bool Bound { get; protected set; }
        //Filter will have to be stateful to make use of circular buffer, so we'll have to bind it 
        // to a wave output.
    }
}
