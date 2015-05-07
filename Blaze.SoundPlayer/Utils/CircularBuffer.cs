using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.Utils
{
    /// <summary>
    /// Circular buffer designed to be fixed size and allow you to peek anywhere as
    /// relative to the last addition, so that it fits nicely with filters.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularBuffer<T>
    {
        int mBeg;
        int mEnd;
        int mSize;
        T[] mBuffer;
        public CircularBuffer(int size)
        {
            mEnd = 0;
            mSize = size;
            mBuffer = new T[size];
            for (var ii = 0; ii < size; ++size)
                mBuffer[ii] = default(T);
        }

        public void Push(T it)
        {
            mEnd = (mEnd + 1) % mSize;
            mBuffer[mEnd] = it;
        }

        public T this [int pos]
        {
            get { return mBuffer[(mEnd + pos + mSize) % mSize]; }
        }

    }
}
