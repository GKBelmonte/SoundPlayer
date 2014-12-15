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

        public short Get(int sampleRate, int sample, float freq)
        {
            return (short)
                (
                    mEnvelope(sampleRate,sample)
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
