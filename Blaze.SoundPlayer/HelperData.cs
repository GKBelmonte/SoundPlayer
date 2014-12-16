using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer
{

    public delegate short WaveGenerator(int sampleRate, int sampleNumber, float freq);
    public delegate float EnvelopeGenerator(int sampleRate, int sampleNumber);
    public delegate float FrequencyModulator(int sampleRate, int sampleNumber);
    public delegate float PhaseModulator(int sampleRate, int sampleNumber);

    public enum SampleRates { At16kHz = 16000, At44_1kHz = 44100, At32kHz = 32000, CDSampleAt44_1kHz = 44100, At32_768kHz = 32768 }
    public enum WaveResolution { };
    public static class HelperData
    {
    }
}
