using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer.WaveProviders;
using Blaze.SoundPlayer.Waves;

namespace Blaze.SoundPlayer
{
    public delegate short WaveGenerator(int sampleRate, int sampleNumber, float freq);
    public delegate float EnvelopeGenerator(int sampleRate, int sampleNumber);
    public delegate float FrequencyModulator(int sampleRate, int sampleNumber);
    public delegate float PhaseModulator(int sampleRate, int sampleNumber);
    public interface ISoundPlayer
    {
        int SampleFrequency { get; set; }

        void PlaySync(short[] data);
        void PlaySync(IList<short[]> datas);

        void PlaySync(Track track);
        void PlaySync(IList<Track> tracks);

        void PlaySync(WaveGenerator track, float freq, int fixedDuration);
        void PlaySync(IList<WaveGenerator> tracks, IList<float> freq, int fixedDuration);

        void PlaySync(Wave track, float freq, int fixedDuration);
        void PlaySync(IList<Wave> tracks, IList<float> freq, int fixedDuration);
        
        void PlaySync(IWaveProviderExposer tracks, float freq, int fixedDuration);

        bool Stop();
        bool Pause();

        bool SkipTo(int sample);
        bool SkipTo(TimeSpan moment);

        bool IsPlaying { get; }

        void PlayAsync(short[] data);
        void PlayAsync(IList<short[]> datas);

        void PlayAsync(Track track);
        void PlayAsync(IList<Track> tracks);

        void PlayAsync(WaveGenerator track, float freq, int fixedDuration);
        void PlayAsync(IList<WaveGenerator> tracks, IList<float> freq, int fixedDuration);

        void PlayAsync(Wave track, float freq, int fixedDuration);
        void PlayAsync(IList<Wave> tracks, IList<float> freq, int fixedDuration);

    }

    public interface ISoundPlayerX
    {
        int SampleRate { get; set; }
        void PlayOnce(int duration, float freq);
        void PlayOnceSync(int duration, float freq);
        int[] Data { get; set; }
    }
}
