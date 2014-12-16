using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.Waves
{
    /// <summary>
    /// Represents the data of a wave, a single perdiod since that is all that is required to represent it. <br/>
    /// Contains functions to operate on the data, including grabbing the wave at different multiple frequencies and
    /// amplitudes. <br/>
    /// The base frequency of a wave (played at 1 multiplier) will be given by fs/Resolution.<br/>
    /// The amplitude of the wave is recommended to be 1% of the max value of integral data type to allow
    /// supporting classes to assume an amplitude percentage of the max volume.<br/>
    /// Getting a fractional multiple of the base frequency of the wave is avalaible via linear interpolation, 
    /// up to the degree of BinaryPrecision number of places.<br/>
    /// Inherited classes should override the Initialize function and fill 
    /// '<see cref="_data"/>' member with one period of the waveform with '<see cref="Resolution"/>' 
    /// number of samples.<br/>
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
                _data[ii] = 0; //base amplitude should be 100th of maximum (2^15 - 1)/128
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

        static int BinaryPrecision
        {
            get { return maxNegativeExponent; }
            set 
            {
                if (value <= 0) maxNegativeExponent = 0;
                else if (value >= 16) maxNegativeExponent = 16;
                else maxNegativeExponent = value;
            }
        }
        static int maxNegativeExponent = 5;//resolution to the closest multiple of 2^(-5) = 0.03125
        static public void GetPowerAndMultiplier(double freq, out int negativeExponent, out int multiplier)
        {
            double fractional = freq - Math.Floor(freq);
            negativeExponent = 0;
            while (fractional != Math.Floor(fractional) && negativeExponent < maxNegativeExponent)
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

        protected WaveGenerator mWaveGenerator;

        public WaveGenerator WaveGenerator
        {
            get
            {
                if (mWaveGenerator == null)
                    mWaveGenerator 
                        = new WaveGenerator((sampleRate, sampleNumber, freq) => 
                            (this[sampleNumber, freq * Resolution / sampleRate]));
                return mWaveGenerator;
                
            }
        }
    }
}
