using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer;
using Blaze.SoundPlayer.Sounds;
using Blaze.SoundPlayer.WaveProviders;
using Blaze.SoundPlayer.Waves;
namespace Blaze.SoundPlayer.TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISoundPlayer player4 = new NAudioSoundPlayer();
            Wave wav = new Sinusoid(4 * 1024);
            Wave wav1_5 = new Sinusoid(4 * 1024);
            Wave wav2 = new Sawtooth(4 * 1024);
            Console.WriteLine("fixed data sinusoid 440");
            Console.ReadKey(false);
            player4.PlaySync(wav, 440, 1000);

            Console.WriteLine("fixed data sawtooth 440");
            Console.ReadKey(false);
            player4.PlaySync(wav2, 440, 1000);

            Console.WriteLine("wave addition sin + saw");
            Console.ReadKey(false);
            player4.PlaySync(wav + wav2, 440, 1000);


            Console.WriteLine("multi wave");
            Console.ReadKey(false);
            player4.PlaySync(new Wave[] { wav, wav, wav }, new float[] { 440, 660, 880 }, 2000);

            Console.WriteLine("wave generator delegate");
            Console.ReadKey(false);
            player4.PlaySync(new WaveGenerator(SinWaveGen), 440, 1000);
            player4.PlaySync(wav, 440, 1000);

            Console.WriteLine("SimpleSound provider");
            Console.ReadKey(false);
            var sound = new SimpleSound(new WaveGenerator(SinWaveGen), new EnvelopeGenerator(Adsr1));
            IWaveProviderExposer thing = NAudioSoundPlayer.FactoryCreate(sound);
            player4.PlaySync(thing, 440, 3000);

            Console.WriteLine("Clipped sinusoid");
            Console.ReadKey(false);
            player4.PlaySync(new WaveGenerator(ClippedSinWaveGen), 440, 3000);

            Console.WriteLine("Freq mod sinusoid");
            Console.ReadKey(false);
            var sound2 = new SimpleSound(new WaveGenerator(SinWaveGen), null, new FrequencyModulator(FreqMod));
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound2), 440, 3000);

            Console.WriteLine("Fixed data sinusoid wave gen");
            Console.ReadKey(false);
            test.Initialize();
            var sound3 = new SimpleSound(new WaveGenerator(FixedSinWaveGen), new EnvelopeGenerator(Adsr1));
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound3), 440, 3000);

            Console.WriteLine("phase modulated sinusoid");
            Console.ReadKey(false);
            var sound4 = new SimpleSound(new WaveGenerator(SinWaveGen), new EnvelopeGenerator(Adsr1), null, new PhaseModulator(FreqMod));
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound4), 440, 3000);

            Console.WriteLine("Multiple sound provider");
            var sound5 = new SimpleSound(new WaveGenerator(SinWaveGen), null, new FrequencyModulator(FreqMod2));
            
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound2), 440, 3000);
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound5), 440, 3000);
            Console.ReadKey(false);
            var sounds = new List<SimpleSound>(2);
            sounds.Add(sound5);
            sounds.Add(sound2);
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sounds), 440, 120000);
            Console.ReadKey(false);
        }

        static float FreqMod(int sampleRate, int sampleNumber)
        {
            var t =  (float)sampleNumber / (float)sampleRate;
            double freq = 110;
            double amp = 1;
            //10 hz up and down at 10hz
            return (float)(amp * Math.Sin((2 * Math.PI * t * freq)));
        }

        static float FreqMod2(int sampleRate, int sampleNumber)
        {
            var t = (float)sampleNumber / (float)sampleRate;
            double freq = 220;
            double amp = 1;
            //10 hz up and down at 10hz
            return (float)(amp * Math.Sin((2 * Math.PI * t * freq)));
        }

        static Sinusoid test = new Sinusoid(1024);
        static short FixedSinWaveGen(int sampleRate, int sampleNumber, float freq)
        {
            return test[sampleNumber,(freq*test.Resolution/sampleRate)];
        }

        static short SinWaveGen(int sampleRate, int sampleNumber, float freq)
        {
            return (short)(300 * Math.Sin((2 * Math.PI * sampleNumber * freq) / sampleRate));
        }

        static short SquareWaveGen(int sampleRate, int sampleNumber, float freq)
        {
            //var t = sampleNumber
            return (short)(300 * Math.Sin((2 * Math.PI * sampleNumber * freq) / sampleRate));
        }

        static short ClippedSinWaveGen(int sampleRate, int sampleNumber, float freq)
        {
            var res = (short)(500 * Math.Sin((2 * Math.PI * sampleNumber * freq) / sampleRate));
            if (res > 300) return 300;
            else return res;
        }

        static float Adsr1(int sampleRate, int sampleNumber)
        {
            var t = 1000 *(double)sampleNumber / (double)sampleRate;
            double period = 3000;
            //t = t %period
            var mults = Math.Floor(t / period);
            t = t - period * mults;
            const double attackTau = 10;
            const double decayTau = 700;
            const double attackTime = 300;
            if (t > attackTime)
                return (float)(Math.Exp(-(t - attackTime) / decayTau));
            else
                return (float)(1 - Math.Exp(-(t) / attackTau));
            
        }
    }
}
