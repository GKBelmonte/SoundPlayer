using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.WaveProviders
{
    abstract class InstrumentBase : IInstrumentProvider
    {

        public Note NoteOn(string step, int octave, float velocity = 1, bool sustain = false)
        {
            throw new NotImplementedException();
        }

        public Note NoteOff(string step, int octave)
        {
            throw new NotImplementedException();
        }

        public float Duration{get;  set;}

        public IList<float> AmplitudeMultipliers
        {
            get { throw new NotImplementedException(); }
        }

        public void AddFilter(Filters.Filter filter)
        {
            throw new NotImplementedException();
        }

        public float AmplitudeMultiplier
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void SetWaveFormat(int sampleRate, int channels)
        {
            throw new NotImplementedException();
        }

        public int Read(float[] buffer, int offset, int sampleCount)
        {
            throw new NotImplementedException();
        }
    }
}
