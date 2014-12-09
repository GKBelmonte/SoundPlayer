using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using Blaze.SoundPlayer.Waves;
using Blaze.SoundPlayer.WaveProviders;

namespace Blaze.SoundPlayer
{
    public class NAudioSoundPlayer : ISoundPlayer
    {

        protected WaveProvider16 wave;
        protected WaveOut waveOut;
        protected object waveOutLock = new object();
        public NAudioSoundPlayer(int type, int sampleRate)
        {
            if (type == 0)
                wave = new SineWaveProvider();
            else
                wave = new FixedDataWaveProvider(new Waves.Sinusoid(1024 * 4));
            wave.SetWaveFormat(sampleRate, 1);            
        }
        public void SetSampleRate()
        {
            throw new NotImplementedException();
        }

        public int[] Data
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public void PlayOnce(int duration)
        {

        }

        public void PlayOnceSync(int duration)
        {
            lock (waveOutLock)
            {
                waveOut = new WaveOut();
                waveOut.Init(wave);
                waveOut.Play();
            }
            Thread.Sleep(duration);
            waveOut.Stop();
        }
    }
}
