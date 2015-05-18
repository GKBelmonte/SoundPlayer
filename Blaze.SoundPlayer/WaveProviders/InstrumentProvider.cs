//#define BENCH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer.Sounds;
using NAudio.Wave;

namespace Blaze.SoundPlayer.WaveProviders
{
    internal class InstrumentProvider : WaveProvider32, IInstrumentProvider
    {
        protected List<Note> mNotes;
        protected List<bool> mNoteIsOn;
        const int NUMBER_OF_POSS_NOTES = 200;
        protected List<Filters.Filter> mFilters;
        public float Duration { get; set; }


        protected List<SimpleSound> mWaves;
        protected List<float> mFreq;
        protected List<float> mAmps;

        public InstrumentProvider(IList<SimpleSound> waves, IList<float> freqMultipliers = null, IList<float> amplitudes = null)
        {
            mWaves = new List<SimpleSound>(waves.Count);
            mWaves.AddRange(waves);
            mFreq = new List<float>(waves.Count);
            mAmps = new List<float>(waves.Count);

            if (freqMultipliers == null)
                foreach (var w in mWaves)
                    mFreq.Add(1.0f);
            else
                mFreq.AddRange(freqs)

            if (amplitudes == null)
                foreach (SimpleSound s in mWaves)
                    mAmps.Add(1f);
            else
                mAmps.AddRange(amplitudes);

            AmplitudeMultiplier = 1;



            mNoteIsOn = new List<bool>(NUMBER_OF_POSS_NOTES);
            mNoteIsOn.AddRange(new bool[NUMBER_OF_POSS_NOTES]);
            var c0 = new Note("C", 0, 0, 0);
            mNotes = new List<Note>(NUMBER_OF_POSS_NOTES);
            mNotes.Add(c0);
            for (var ii = 1; ii < NUMBER_OF_POSS_NOTES; ++ii)
                mNotes.Add(mNotes[ii - 1] + 1);

            mFilters = new List<Filters.Filter>();
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
#if BENCH
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
#endif
            int sampleRate = WaveFormat.SampleRate;
            var sampleNow = sample;
            var notesThatAreOn = mNoteIsOn.IndexOfAll(x => (x));

            for (int n = 0; n < sampleCount; n++)
            {
                var res = 0f;

                for (var jj = 0; jj < notesThatAreOn.Count; ++jj)
                {
                    var note = mNotes[notesThatAreOn[jj]];
                    var start = note.mStart;
                    var freq = note.mFreq;
                    var vel = note.mVelocity;
                    //Note should stop ringing if it has rang long enough
                    if ((1000f * (float)(sample - start)) / ((float)sampleRate) > Duration)
                    {
                        mNoteIsOn[notesThatAreOn[jj]] = false;
                        continue;
                    }
                    for (var ii = 0; ii < mWaves.Count; ++ii)
                        res +=
                            (
                                vel
                                    *
                                (mAmps[ii] * AmplitudeMultiplier)
                                    *
                                mWaves[ii]
                                .Get(sampleRate, sample - start, freq * mFreq[ii])
                            );
                }

                buffer[n + offset] = res;
                sample++;
            }

            //Apply filters if any
            float deltaTime = 1 / (float)sampleRate;
            for (var ii = 0; ii < mFilters.Count; ++ii)
                for (int n = 0; n < sampleCount; n++)
                    mFilters[ii].Apply(deltaTime, 0 , buffer[n + offset]);

            #if BENCH
            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
            #endif //BENCH

            return  sampleCount ;
        }

        public Note NoteOn(string step, int octave, float velocity = 1, bool sustain = false)
        {
            var index = StepAndOctaveToNumber(step,octave);
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
            var index = StepAndOctaveToNumber(step,octave);
            var note = mNotes[index];
            note.mEnd = sample;
            return note;
        }

        public IList<float> AmplitudeMultipliers
        {
            get { return mAmps; }
        }

        public void AddFilter(Filters.Filter filter) 
        {
            mFilters.Add(filter);
        }

    }
}
