using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blaze.SoundPlayer
{
    public struct Note
    {
        public string mStep;
        public int mOctave;
        public int mStart;
        public int mEnd;
        public float mFreq;
        public float mVelocity;

        public Note(string step, int octave, int start, int end)
        {
            mStep = step; 
            mOctave = octave; 
            mStart = start; 
            mEnd = end; 
            mFreq = 0;
            mVelocity = 1;
        }

        public Note(float freq, int start, float velocity, int end = -1)
        {
            var temp = Helpers.FrequencyToNote(freq);
            mStart = start;
            mStep = temp.mStep;
            mOctave = temp.mOctave;
            mEnd = end;
            mFreq = freq;
            mVelocity = velocity;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", mStep, mOctave);
        }

        static public Note operator + (Note me, int what)
        {
            var number = Helpers.NoteLetterToKeyNumber(me.mStep)+what;
            var octaveIncrease = number / 12;
            var newStep = Helpers.NoteLetterGetter(number % 12);
            return new Note(newStep, me.mOctave+octaveIncrease, me.mStart, me.mEnd);
        }

    }
}
