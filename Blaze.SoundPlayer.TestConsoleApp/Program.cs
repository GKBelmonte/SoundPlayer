using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer;
using Blaze.SoundPlayer.Waves;
namespace Blaze.SoundPlayer.TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //ISoundPlayerX player = new NAudioSoundPlayerX(0, 16000);
            //player.PlayOnceSync(1000,440);
            //Console.ReadKey(false);

            //ISoundPlayerX player2 = new NAudioSoundPlayerX(1, 16000);
            //player2.PlayOnceSync(1000,220);
            //Console.ReadKey(false);
            
            //ISoundPlayerX player3 = new NAudioSoundPlayerX(2, 16000);
            //player3.PlayOnceSync(1000, 1000);
            //Console.ReadKey(false);

            ISoundPlayer player4 = new NAudioSoundPlayer();
            Wave wav = new Sinusoid(4 * 1024);
            Wave wav1_5 = new Sinusoid(4 * 1024);
            Wave wav2 = new Sawtooth(4 * 1024);
            player4.PlaySync(wav, 440, 1000);
            player4.PlaySync(wav2, 660, 1000);
            Console.ReadKey(false);
            player4.PlaySync(wav + wav2, 440, 1000);
            Console.ReadKey(false);
            player4.PlaySync(new Wave[] {wav, wav, wav}, new float [] {440,660,880}, 2000);
            Console.ReadKey(false);
            player4.PlaySync(new WaveGenerator(SinWaveGen), 440, 1000);
            player4.PlaySync(wav, 440, 1000);
        }

        static short SinWaveGen(int sampleRate, int sampleNumber, float freq)
        {
            return (short)(100 * Math.Sin((2 * Math.PI * sampleNumber * freq) / sampleRate));
        }
    }
}
