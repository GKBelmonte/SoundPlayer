using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer.Filters;
using NAudio.Wave;

namespace Blaze.SoundPlayer.WaveProviders
{
    public abstract class InstrumentBase : WaveProvider32, IInstrumentProvider
    {

        public int SampleRate { get; protected set; }
        protected int mSample;

        //Sound note implementation
        protected List<Note> mNotes;
        protected List<bool> mNoteIsOn;
        const int NUMBER_OF_POSS_NOTES = 200;
        
        //Sound basic modifiers
        public float Duration { get; set; }
        public float AmplitudeMultiplier { get; set; }

        //Sound extended modifiers (which are not awesome to have here but wtv)
        protected List<Filter> mFilters;

        public InstrumentBase()
        {
            //Sampling stuff
            mSample = 0;
            SampleRate = (int)SampleRates.At16kHz;

            //Initialize notes
            mNoteIsOn = new List<bool>(NUMBER_OF_POSS_NOTES);
            mNoteIsOn.AddRange(new bool[NUMBER_OF_POSS_NOTES]);
            var c0 = new Note("C", 0, 0, 0);
            mNotes = new List<Note>(NUMBER_OF_POSS_NOTES);
            mNotes.Add(c0);
            for (var ii = 1; ii < NUMBER_OF_POSS_NOTES; ++ii)
                mNotes.Add(mNotes[ii - 1] + 1);
            //Filters
            mFilters = new List<Filter>();
        }

        public Note NoteOn(string step, int octave, float velocity = 1, bool sustain = false)
        {
            var index = StepAndOctaveToNumber(step, octave);
            var outNote = mNotes[index];
            outNote.mEnd = mSample;
            var newNote = outNote;
            newNote.mStart = mSample;
            newNote.mVelocity = velocity;
            mNotes[index] = newNote;
            mNoteIsOn[index] = true;
            return outNote;
        }

        static public int StepAndOctaveToNumber(string step, int octave)
        {
            return octave * 12 + Note.NoteLetterToKeyNumber(step);
        }

        public Note NoteOff(string step, int octave)
        {
            var index = StepAndOctaveToNumber(step, octave);
            var note = mNotes[index];
            note.mEnd = mSample;
            return note;
        }

        public void AddFilter(Filters.Filter filter)
        {
            mFilters.Add(filter);
        }

        //abstract public int Read(float[] buffer, int offset, int sampleCount);
    }
}
