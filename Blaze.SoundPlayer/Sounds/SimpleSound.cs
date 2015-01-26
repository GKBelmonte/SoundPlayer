using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.Sounds
{
    public class SimpleSound
    {
        WaveGenerator mWave;
        EnvelopeGenerator mEnvelope;
        FrequencyModulator mFreqMod;
        PhaseModulator mPhaseMod;
        public SimpleSound(WaveGenerator wave, EnvelopeGenerator envelope = null, FrequencyModulator freqMod = null, PhaseModulator phaseMod = null)
        {
            mEnvelope = envelope ?? IdentityEnvelopeGenerator;
            mFreqMod = freqMod ?? IdentityFrequencyModulator;
            mPhaseMod = phaseMod ?? IdentityPhaseGenerator;
            if (wave == null)
                throw new ArgumentNullException("wave");
            mWave = wave;
        }

        public float Get(int sampleRate, int sample, float freq)
        {
            bool dither = false;
            if (Length != -1 && 1000 * sample / sampleRate > Length)
            {
                DonePlaying = true;
                return 0;
            }

            if(dither)
                return 
                (
                    mEnvelope(sampleRate,sample)
                    *
                     ((float)((short) (100 * mWave
                    (
                        sampleRate,
                        (int)(sample + mPhaseMod(sampleRate, sample)),
                        (freq + mFreqMod(sampleRate, sample))
                    ))))/100
                );
            else
                return
                (
                    mEnvelope(sampleRate, sample)
                    *
                      mWave
                    (
                        sampleRate,
                        (int)(sample + mPhaseMod(sampleRate, sample)),
                        (freq + mFreqMod(sampleRate, sample))
                    )
                );
        }

        public static float identityEnvelopeGenerator(int a, int b)
        {
            return 1;
        }

        public static float identityModulator(int a, int b)
        {
            return 0;
        }

        /// <summary>
        /// True if the sound is done playing and it can be removed from the queue
        /// </summary>
        public bool DonePlaying { get; protected set; }

        /// <summary>
        /// The length in milliseconds the sound should play for.
        /// -1 for continious sounds. 
        /// </summary>
        public int Length { get; set; }

        private static EnvelopeGenerator _identityEnvelope = new EnvelopeGenerator(identityEnvelopeGenerator);
        private static FrequencyModulator _identityFreq=new FrequencyModulator(identityModulator); 
        private static PhaseModulator _identityPhase = new PhaseModulator(identityModulator);

        private static EnvelopeGenerator IdentityEnvelopeGenerator 
        {
            get { return _identityEnvelope; }
        }
        private static FrequencyModulator IdentityFrequencyModulator
        {
            get { return _identityFreq; }
        }
        private static PhaseModulator IdentityPhaseGenerator
        {
            get { return _identityPhase; }
        }
    }

}
