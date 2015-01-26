using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer.Sounds;
using Blaze.SoundPlayer.WaveProviders;
using Blaze.SoundPlayer.Waves;

namespace Blaze.SoundPlayer
{
    
    public interface ISoundPlayer
    {
        int SampleFrequency { get; set; }

        void PlaySync(float[] data);
        void PlaySync(IList<float[]> datas);

        void PlaySync(Track track);
        void PlaySync(IList<Track> tracks);

        void PlaySync(WaveGenerator track, float freq, int fixedDuration);
        void PlaySync(IList<WaveGenerator> tracks, IList<float> freq, int fixedDuration);

        void PlaySync(Wave track, float freq, int fixedDuration);
        void PlaySync(IList<Wave> tracks, IList<float> freq, int fixedDuration);
        
        void PlaySync(SimpleSound track, float freq, int fixedDuration);
        void PlaySync(IList<SimpleSound> tracks, float freq, int fixedDuration);

        void PlaySync(IWaveProviderExposer tracks, float freq, int fixedDuration);

        bool Stop();
        bool Pause();

        bool SkipTo(int sample);
        bool SkipTo(TimeSpan moment);

        bool IsPlaying { get; }

        void PlayAsync(float[] data);
        void PlayAsync(IList<float[]> datas);

        void PlayAsync(Track track);
        void PlayAsync(IList<Track> tracks);

        void PlayAsync(WaveGenerator track, float freq, int fixedDuration);
        void PlayAsync(IList<WaveGenerator> tracks, IList<float> freq, int fixedDuration);

        void PlayAsync(Wave track, float freq, int fixedDuration);
        void PlayAsync(IList<Wave> tracks, IList<float> freq, int fixedDuration);

        void PlayAsync(SimpleSound track, float freq, int fixedDuration);
        void PlayAsync(IList<SimpleSound> tracks, float freq, int fixedDuration);

        void PlayAsync(IWaveProviderExposer tracks, float freq, int fixedDuration);
    }
}
