using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Blaze.SoundPlayer.WaveProviders
{
    public abstract class InstrumentBase : WaveProvider32, IInstrumentProvider
    {
        protected int sample;
        protected List<Note> mNotes;
        protected List<bool> mNoteIsOn;
        const int NUMBER_OF_POSS_NOTES = 200;
        protected List<Filters.Filter> mFilters;
        public float Duration { get; set; }
        public float AmplitudeMultiplier { get; set; }

        public InstrumentBase()
        {
            mNoteIsOn = new List<bool>(NUMBER_OF_POSS_NOTES);
            mNoteIsOn.AddRange(new bool[NUMBER_OF_POSS_NOTES]);
            var c0 = new Note("C", 0, 0, 0);
            mNotes = new List<Note>(NUMBER_OF_POSS_NOTES);
            mNotes.Add(c0);
            for (var ii = 1; ii < NUMBER_OF_POSS_NOTES; ++ii)
                mNotes.Add(mNotes[ii - 1] + 1);

            mFilters = new List<Filters.Filter>();
        }

        public Note NoteOn(string step, int octave, float velocity = 1, bool sustain = false)
        {
            var index = StepAndOctaveToNumber(step, octave);
            var outNote = mNotes[index];
            outNote.mEnd = sample;
            var newNote = outNote;
            newNote.mStart = sample;
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
            note.mEnd = sample;
            return note;
        }

        public void AddFilter(Filters.Filter filter)
        {
            mFilters.Add(filter);
        }

        //abstract public int Read(float[] buffer, int offset, int sampleCount);
    }
}
