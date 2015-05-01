using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.WaveProviders
{
    public interface IInstrumentProvider : IWaveProviderExposer
    {
        /// <summary>
        /// Triggers the note on.
        /// </summary>
        /// <param name="freqBase">The base frequency (main harmonic). The rest of the harmonics will be multiples of this.</param>
        /// <param name="velocity">A [0,1] amplitude modifier.</param>
        /// <param name="sustain">Whether the note should sustain and wait for a note-off call or just decay normally</param>
        /// <returns>The note info of the last played note in that position</returns>
        Note NoteOn(string step, int octave, float velocity = 1, bool sustain = false);

        /// <summary>
        /// Release a note with the given ID, that was told to sustain
        /// </summary>
        /// <param name="id">The ID of the note</param>
        /// <returns>The Note information (start,end, etc)</returns>
        Note NoteOff(string step, int octave);


        /// <summary>
        /// The default duration in ms that a triggered sound should play for
        /// </summary>
        float Duration { get; set; }

        IList<float> AmplitudeMultipliers { get; }

        void AddFilter(Filters.Filter filter);
    }
}
