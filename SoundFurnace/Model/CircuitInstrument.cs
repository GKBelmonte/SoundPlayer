using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer;
using Blaze.SoundPlayer.WaveProviders;
using Blaze.SoundPlayer.Filters;
namespace Blaze.SoundForge.Model
{
    public class CircuitInstrument : IInstrumentProvider
    {
        #region Fields/Properties
        //Sampling parameters
        public int SampleRate { get; protected set; }
        protected int mSample;

        //Note behaviour
        protected List<Note> mNotes;
        protected List<bool> mNoteIsOn;
        const int NUMBER_OF_POSS_NOTES = 200;

        //Sound basic modifiers
        public float Duration { get; set; }
        public float AmplitudeMultiplier {get; set;}        
        
        //Sound extended modifiers (which are not awesome to have here but wtv)
        protected List<Filter> mFilters;

        //Circuit components
        public GlobalInputComponent InputComponent { get; private set; }
        public GlobalOutputComponent OutputComponent { get; private set; }
        public List<SoundComponent> mComponents { get; private set; }
        #endregion
        public CircuitInstrument()
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

            //Circuit i/o
            InputComponent = new GlobalInputComponent(this);
            OutputComponent = new GlobalOutputComponent(this);
            mComponents = new List<SoundComponent>();
        }

        public SoundPlayer.Note NoteOn(string step, int octave, float velocity = 1, bool sustain = false)
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
            note.mEnd = mSample;
            return note;
        }

        public int Read(float[] buffer, int offset, int sampleCount)
        {
            float sampleRate = SampleRate;
            
            var notesThatAreOn = mNoteIsOn.IndexOfAll(x => (x));

            for (var jj = 0; jj < notesThatAreOn.Count; ++jj)
            {
                //reset sample
                var sampleNow = mSample;
                //get info
                var note = mNotes[notesThatAreOn[jj]];
                var start = note.mStart;
                var freq = note.mFreq;
                var vel = note.mVelocity;
                
                //Note should stop ringing if it has rang long enough
                var effectiveSampleCount = sampleCount;
                if ((1000f * (float)((sampleNow + sampleCount) - start)) / sampleRate > Duration)
                {
                    mNoteIsOn[notesThatAreOn[jj]] = false;
                    effectiveSampleCount = (int)(Duration * (float)sampleRate / 1000f + (float)start - (float)sampleNow);
                }

                for (int n = 0; n < effectiveSampleCount; n++)
                {
                    //Compute the sum of all notes currently playing at sample mSample
                    CycleSetup(note, sampleNow, sampleNow - start);
                    OutputComponent.Compute();
                    
                    buffer[n + offset] += (float) OutputComponent.Inputs[0];
                    sampleNow++;
                }
            }
            mSample += sampleCount;

            //Apply filters if any
            float deltaTime = 1 / (float)sampleRate;
            for (var ii = 0; ii < mFilters.Count; ++ii)
                for (int n = 0; n < sampleCount; n++)
                    mFilters[ii].Apply(deltaTime, 0, buffer[n + offset]);

            return sampleCount;
        }

        private void CycleSetup(Note note, int sample, int relativeSample)
        {
            //Sample Rate, Absolute Sample, Relative Sample, Absolute Time, Relative Time, Frequency
            //Reset all the elements
            for (var ii = 0; ii < mComponents.Count; ++ii)
                mComponents[ii].NotifyCycleCompleted();
            InputComponent.Outputs[0] = SampleRate;
            InputComponent.Outputs[1] = sample;
            InputComponent.Outputs[2] = relativeSample;
            InputComponent.Outputs[3] = (double)sample / (double)SampleRate;
            InputComponent.Outputs[4] = (double)relativeSample / (double)SampleRate;
            InputComponent.Outputs[5] = note.mFreq;
        }

        public void AddFilter(SoundPlayer.Filters.Filter filter)
        {
            throw new NotImplementedException();
        }

        public void SetWaveFormat(int sampleRate, int channels) //TODO: might need to check implementation of channels
        {
            SampleRate = sampleRate;
        }

        public int Resolution //why is this even here?
        {
            get { throw new NotImplementedException(); }
        }

        public float Frequency //TODO: Dont need
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<float> AmplitudeMultipliers
        {
            get { throw new NotImplementedException(); }
        }

        
    }
}
