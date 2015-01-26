using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer.WaveProviders
{
    public interface IWaveProviderExposer //: NAudio.Wave.IWaveProvider 
    {
        int Resolution { get; }
        float Frequency { get; set; }
        float AmplitudeMultiplier { get; set; }
        /// <summary>
        /// Allows you to specify the sample rate and channels for this WaveProvider
        ///    (should be initialised before you pass it to a wave player)
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="channels"></param>
        void SetWaveFormat(int sampleRate, int channels);
        int Read(float[] buffer, int offset, int sampleCount);
    }
}
