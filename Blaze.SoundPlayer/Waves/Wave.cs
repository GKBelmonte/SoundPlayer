using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.Waves
{
    /// <summary>
    /// Represents the data of a wave, and functions to operate of this data.
    /// Inherited classes should override the Initialize function and fill 
    /// '<see cref="_data"/>' member with one period of the waveform with '<see cref="Resolution"/>' 
    /// number of samples.
    /// For optimal performance, use powers of two for resolution.
    /// </summary>
    public class Wave
    {
        protected short _resolution;
        protected short[] _data;
        protected bool _initialized;
        public short Resolution { get { return _resolution; } }

        public Wave(int resolution)
        {
            _data = new short[resolution];
            _resolution = (short) resolution;            
        }

        public Wave(Wave w1, Wave w2)
        {
            _resolution = w1._resolution;
            _data = new short[_resolution];
            for (var ii = 0; ii < _resolution; ++ii)
            {
                _data[ii] = (short)(w1._data[ii] + w2._data[ii]);
            }
            _initialized = true;
        }

        public virtual void Initialize()
        {
            if (_initialized)
                return;
            for (var ii = 0; ii < _resolution; ++ii)
                _data[ii] = 0; //base amplitude should be 100th of maximum (2^15 - 1)/100
        }

        public short this[int i]
        {
            get { return _data[i % _resolution]; }
        }

        public short this[int i, int freqMult]
        {
            get
            {
                return (short)(_data[(i * freqMult) % _resolution]);
            }
        }

        /// <summary>
        /// Gets the desired indexed value at the frequency given.
        /// If the frequency fractional part is non-zero, it will do a
        /// linear interpolation to get a data sample that is not zero
        /// </summary>
        /// <param name="i"></param>
        /// <param name="freq"></param>
        /// <returns></returns>
        public short this[int i, double freqMult]
        {
            get 
            {
                int exp;
                int mult;
                int closestIndex = (int)(i * freqMult);
                double delta = this[closestIndex + 1] - this[closestIndex];
                GetPowerAndMultiplier(freqMult, out exp, out mult);
                return  (short)( this[closestIndex] + (delta * mult)/(MathExtensions.PowerOfTwo(exp)) );
            }
        }

        public short this[int i, float amplitude, int freqMult]
        {
            get
            {
                return (short)(_data[(i * freqMult) % _resolution] * amplitude);
            }
        }

        public short this[int i, float amplitude, int freqMult, int phase]
        {
            get
            {
                return (short)(_data[(i * freqMult + phase) % _resolution] * amplitude);
            }
        }


        static int MaxNegativeExponent = 5;//resolution to the closest multiple of 2^(-5) = 0.03125
        static public void GetPowerAndMultiplier(double freq, out int negativeExponent, out int multiplier)
        {
            double fractional = freq - Math.Floor(freq);
            negativeExponent = 0;
            while (fractional != Math.Floor(fractional) && negativeExponent < MaxNegativeExponent)
            {
                negativeExponent++;
                fractional = fractional * 2;
            }
            multiplier = (int)Math.Floor(fractional);
        }

        public static Wave operator +(Wave w1, Wave w2)
        {
            return new Wave(w1, w2);
        }
    }
}
