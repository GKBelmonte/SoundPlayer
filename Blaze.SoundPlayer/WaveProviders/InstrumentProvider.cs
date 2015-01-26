using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer.Sounds;

namespace Blaze.SoundPlayer.WaveProviders
{
    internal class InstrumentProvider : AdditiveSynthesisWaveProvider
    {

        protected List<Note> mNotes;
        protected List<bool> mNoteIsOn;
        int lastOnNote;

        public int Duration { get; set; }

        public InstrumentProvider(IList<SimpleSound> waves, IList<float> freqMultipliers = null, IList<float> amplitudes = null)
            : base(waves, freqMultipliers, amplitudes)
        {
            mNotes = new List<Note>();
            mNoteIsOn = new List<bool>();
            if (mFreq == null)
            {
                mFreq = new List<float>(mWaves.Count);
                foreach (var w in mWaves)
                {
                    mFreq.Add(1.0f);
                }
            }
            lastOnNote = 0;
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            var sampleNow = sample;
            for (var jj = lastOnNote; jj < mNotes.Count; ++jj)
            {
                sample = sampleNow;//reset sample number
                var note = mNotes[jj];
                var start = note.mStart;
                var freq = note.mFreq;
                var vel = note.mVelocity;
                if (sample - start > Duration)
                    mNoteIsOn[jj] = false;
                if (!mNoteIsOn[jj])
                {
                    //TODO: assuming that fifo for notes
                    lastOnNote++;
                    break;
                }
                for (int n = 0; n < sampleCount; n++)
                {
                    var res = 0f;
                    for (var ii = 0; ii < mWaves.Count; ++ii)
                        res +=
                            (
                                vel
                                    *
                                (mAmps != null ? mAmps[ii] : AmplitudeMultiplier)
                                    *
                                mWaves[ii]
                                .Get(sampleRate, sample - start, freq * mFreq[ii])
                            );

                    buffer[n + offset] = res;
                    sample++;
                }
            }
            return sampleCount;
        }

        int NoteOn(float freqBase, float velocity = 1, bool sustain = false)
        {
            mNotes.Add(new Note(freqBase,sample,velocity));
            mNoteIsOn.Add(true);
            return mNotes.Count - 1;
        }

        Note NoteOff(int id)
        {
            if (id >= mNotes.Count)
                return new Note();
            var note = mNotes[id];
            note.mEnd = sample;
            mNoteIsOn[id] = false;
            return note;
        }

    }
}
