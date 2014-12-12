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

        public NAudioSoundPlayer()
        {
            mWaitForPlayBackStopped = new AutoResetEvent(false);
            mSampleFrequency = 1024 * 16;
        }

        public bool IsPlaying
        {
            get;
            protected set;
        }

        int mSampleFrequency;
        public int SampleFrequency
        {
            get
            {
                return mSampleFrequency;
            }
            set
            {
                if (!IsPlaying)
                    mSampleFrequency = value;
                else
                    throw new InvalidOperationException("Attempting to change sample frequency mid-play");
            }
        }

        readonly protected object mWaveLock = new Object();
        protected WaveOut mWaveOut;

        AutoResetEvent mWaitForPlayBackStopped;
 
        void mWaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            mWaitForPlayBackStopped.Set();
        }

        public void PlaySync(Wave track, float freq = 440, int fixedDuration = -1)
        {
            var wave = new FixedDataWaveProvider(track);
            wave.SetWaveFormat(mSampleFrequency, 1);
            wave.Frequency = freq;
            lock (mWaveLock)
            {
                mWaveOut = new WaveOut();
                if(fixedDuration == -1)
                    mWaveOut.PlaybackStopped += mWaveOut_PlaybackStopped;
                mWaveOut.Init(wave);
                mWaveOut.Play();
            }

            if (fixedDuration == -1)
                mWaitForPlayBackStopped.WaitOne();
            else
            {
                Thread.Sleep(fixedDuration);
                mWaveOut.Stop();
            }
            
            lock (mWaveOut)
            {
                if (fixedDuration == -1)
                    mWaveOut.PlaybackStopped -= mWaveOut_PlaybackStopped;
                mWaveOut.Dispose();
            }

        }


        public void PlaySync(IList<Wave> tracks, IList<float> freq, int fixedDuration)
        {
            var wave = new CompositeFixedDataWaveProvider(tracks);
            wave.SetWaveFormat(mSampleFrequency, 1);
            wave.Frequencies = freq;
            lock (mWaveLock)
            {
                mWaveOut = new WaveOut();
                if (fixedDuration == -1)
                    mWaveOut.PlaybackStopped += mWaveOut_PlaybackStopped;
                mWaveOut.Init(wave);
                mWaveOut.Play();
            }

            if (fixedDuration == -1)
                mWaitForPlayBackStopped.WaitOne();
            else
            {
                Thread.Sleep(fixedDuration);
                mWaveOut.Stop();
            }

            lock (mWaveOut)
            {
                if (fixedDuration == -1)
                    mWaveOut.PlaybackStopped -= mWaveOut_PlaybackStopped;
                mWaveOut.Dispose();
            }
        }


        public void PlaySync(short[] data)
        {
            throw new NotImplementedException();
        }

        public void PlaySync(IList<short[]> datas)
        {
            throw new NotImplementedException();
        }

        public void PlaySync(Track track)
        {
            throw new NotImplementedException();
        }

        public void PlaySync(IList<Track> tracks)
        {
            throw new NotImplementedException();
        }

        public void PlaySync(WaveGenerator track, float freq, int fixedDuration)
        {
            throw new NotImplementedException();
        }

        public void PlaySync(IList<WaveGenerator> tracks, IList<float> freq, int fixedDuration)
        {
            throw new NotImplementedException();
        }



        public bool Stop()
        {
            throw new NotImplementedException();
        }

        public bool Pause()
        {
            throw new NotImplementedException();
        }

        public bool SkipTo(int sample)
        {
            throw new NotImplementedException();
        }

        public bool SkipTo(TimeSpan moment)
        {
            throw new NotImplementedException();
        }

        public void PlayAsync(short[] data)
        {
            throw new NotImplementedException();
        }

        public void PlayAsync(IList<short[]> datas)
        {
            throw new NotImplementedException();
        }

        public void PlayAsync(Track track)
        {
            throw new NotImplementedException();
        }

        public void PlayAsync(IList<Track> tracks)
        {
            throw new NotImplementedException();
        }

        public void PlayAsync(WaveGenerator track, float freq, int fixedDuration)
        {
            throw new NotImplementedException();
        }

        public void PlayAsync(IList<WaveGenerator> tracks, IList<float> freq, int fixedDuration)
        {
            throw new NotImplementedException();
        }

        public void PlayAsync(Wave track, float freq, int fixedDuration)
        {
            throw new NotImplementedException();
        }

        public void PlayAsync(IList<Wave> tracks, IList<float> freq, int fixedDuration)
        {
            throw new NotImplementedException();
        }
    }





    public class NAudioSoundPlayerX : ISoundPlayerX
    {

        protected IWaveProviderExposer _wave;
        //protected WaveProvider16 wave;
        protected WaveOut _waveOut;
        protected object _waveOutLock = new object();
        protected int _sampleRate;
        public NAudioSoundPlayerX(int type, int sampleRate)
        {
            if (type == 0)
                _wave = (new SineWaveProvider());
            else if (type == 1)
                _wave = (new FixedDataWaveProvider(new Waves.Sinusoid(1024 * 4)));
            else
            {
                _wave = new CompositeFixedDataWaveProvider();
                var temp = new FixedDataWaveProvider(new Waves.Sinusoid(1024 * 4));
                temp.Frequency = 500;
                (_wave as CompositeFixedDataWaveProvider).AddWave(temp);
                temp = new FixedDataWaveProvider(new Waves.Sinusoid(1024 * 4));
                temp.Frequency = 1000;
                (_wave as CompositeFixedDataWaveProvider).AddWave(temp);
            }
            _wave.SetWaveFormat(sampleRate, 1);
            _sampleRate = sampleRate;
        }

        public int SampleRate
        {
            get{ return _sampleRate;}
            set
            {
                _wave.SetWaveFormat(value, 1);
                _sampleRate = value;
            }
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
        public void PlayOnce(int duration, float freq)
        {
            var wave = _wave;

            wave.Frequency = freq;
            lock (_waveOutLock)
            {
                _waveOut = new WaveOut();
                _waveOut.Init(wave);
                _waveOut.Play();
            }
        }

        public void PlayOnceSync(int duration, float freq)
        {
            var wave = _wave;
            wave.Frequency = freq;
            lock (_waveOutLock)
            {
                _waveOut = new WaveOut();
                _waveOut.Init(wave);
                _waveOut.Play();
            }
            Thread.Sleep(duration);
            _waveOut.Stop();
        }
    }
}
