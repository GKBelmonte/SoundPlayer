using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer;
using Blaze.SoundPlayer.Sounds;
using Blaze.SoundPlayer.WaveProviders;
using Blaze.SoundPlayer.Waves;
using System.Threading;
namespace Blaze.SoundPlayer.TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISoundPlayer player4 = new NAudioSoundPlayer();
            player4.SampleFrequency = 22000;
            SimpleSound sound;
            Wave wav = new Sinusoid(4 * 1024);
            Wave wav2 = new Sawtooth(4 * 1024);;
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
            sound = new SimpleSound(new WaveGenerator(SinWaveGen), new EnvelopeGenerator(Adsr1));
            IWaveProviderExposer thing = NAudioSoundPlayer.FactoryCreate(sound);
            player4.PlaySync(thing, 440, 3000);

            Console.WriteLine("Clipped sinusoid");
            Console.ReadKey(false);
            player4.PlaySync(new WaveGenerator(ClippedSinWaveGen), 440, 3000);

            Console.WriteLine("Freq mod sinusoid");
            Console.ReadKey(false);
            var sound2 = new SimpleSound(new WaveGenerator(SinWaveGen), null, new FrequencyModulator(FreqMod110));
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound2), 440, 4000);

            Console.WriteLine("Fixed data sinusoid wave gen");
            Console.ReadKey(false);
            test.Initialize();
            var sound3 = new SimpleSound(test.WaveGenerator, new EnvelopeGenerator(Adsr1));
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound3), 440, 1000);

            Console.WriteLine("phase modulated sinusoid");
            Console.ReadKey(false);
            var sound4 = new SimpleSound(new WaveGenerator(SinWaveGen), new EnvelopeGenerator(Adsr1), null, new PhaseModulator(FreqMod));
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound4), 440, 1000);

            Console.WriteLine("Multiple sound provider");
            var sound5 = new SimpleSound(new WaveGenerator(SinWaveGen), null, new FrequencyModulator(FreqMod2));

            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound2), 440, 3000);
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound5), 440, 3000);
            Console.ReadKey(false);
            var sounds = new List<SimpleSound>(2);
            sounds.Add(sound5);
            sounds.Add(sound2);
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sounds), 440, 6000);

            Console.WriteLine("Square wave");
            Console.ReadKey(false);
            sound = new SimpleSound(test2.WaveGenerator);
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound), 440, 1000);

            Console.WriteLine("Sin wave 2nd degree mod");
            Console.ReadKey(false);
            sound = new SimpleSound(test2.WaveGenerator, freqMod: new FrequencyModulator(FreqMod2ndDegree));
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound), 440, 2000);

            Console.WriteLine("Square wave FM by sinusoid");
            Console.ReadKey(false);
            sound = new SimpleSound(test2.WaveGenerator, freqMod: new FrequencyModulator(FreqMod));
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound), 440, 3000);

            Console.WriteLine("Additive synthesis sinusoids");
            Console.ReadKey(false);
            sound = new SimpleSound(new WaveGenerator(SinWaveGen), new EnvelopeGenerator(Adsr1));
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(
                new SimpleSound[] { sound, sound, sound, sound },
                new float[] { 440, 880, 660, 1320 },
                new float[] { 10, 5.0f, 2.5f, 2.25f }
                ), 440, 3000);

            Console.WriteLine("Additive synthesis sawtooths");
            Console.ReadKey(false);
            sound = new SimpleSound(wav2.WaveGenerator, new EnvelopeGenerator(Adsr1));
            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(
                new SimpleSound[] { sound, sound, sound, sound },
                new float[] { 440, 880, 660, 1320 },
                new float[] { 1f, 0.5f, 0.25f, 0.125f }
                ), 440, 2000);

            Console.WriteLine("Sinusoid FM by square");
            Console.ReadKey(false);
            sound = new SimpleSound
                (
                    test.WaveGenerator,
                    new EnvelopeGenerator(Adsr1),
                    new FrequencyModulator((rate, sample) => (10 * test2.WaveGenerator(rate, sample, 4f)))
                );

            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound), 440, 2000);

            Console.WriteLine("Square FM by sinusoid");
            Console.ReadKey(false);
            sound = new SimpleSound
                (
                    test2.WaveGenerator,
                    null,
                    new FrequencyModulator((a, b) => (FreqMod(a, b, 220.0f)))
                   );

            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound), 440, 2000);

            Console.WriteLine("Sinusoid FM by deep harmonic");
            Console.ReadKey(false);
            sound = new SimpleSound
                (
                    test.WaveGenerator,
                    null,
                    new FrequencyModulator((a, b) => (FreqMod(a, b, 27.5f)))
                );

            player4.PlaySync(NAudioSoundPlayer.FactoryCreate(sound), 440, 2000);


            Console.WriteLine("Async sound");
            Console.ReadKey(false);
            sound = new SimpleSound
                (
                    test.WaveGenerator,
                    null,
                    null
                );
            player4.PlayAsync(sound, 440, 1000);
            Thread.Sleep(500);


            Console.WriteLine("Instrument triggable sound");
            Console.ReadKey(false);
            IInstrumentProvider inst = NAudioSoundPlayer.FactoryCreateInstrument(
                new SimpleSound[] { sound }
                //new float[] { 1f, 2f, 3f }, 
                //new float[] { 1f, 0.5f, 0.25f }
                );
            inst.Duration = 2000f;
            inst.AmplitudeMultiplier = 1.0f;
            player4.PlayAsync(inst, 440, 2000);
            inst.NoteOn("A",3, 1.0f);
            Thread.Sleep(500);
            inst.NoteOn("A",4, 1.0f);
            Thread.Sleep(500);
            inst.NoteOn("A",5, 1.0f);
            Thread.Sleep(1000);


            Console.WriteLine("Note to freq");
            Console.ReadKey(false);
            Console.WriteLine();
            var A0 = new Note("A", 0, 0, 0);
            for (var ii = 0; ii < 128; ++ii)
            {
                Console.WriteLine("{0} @ {1}", (A0 + ii).ToString().PadRight(4), (A0 + ii).GetFrequency().ToString().PadLeft(8));
            }
            Console.ReadKey(false);
        }

        static float FreqMod(int sampleRate, int sampleNumber, float freq)
        {
            var t = (float)sampleNumber / (float)sampleRate;
            double amp = 1;
            //10 hz up and down at 10hz
            return (float)(amp * Math.Sin((2 * Math.PI * t * freq)));
        }

        static float FreqMod(int sampleRate, int sampleNumber)
        {
            return FreqMod(sampleRate, sampleNumber, 440);
        }

        static float FreqMod2(int sampleRate, int sampleNumber)
        {
            return FreqMod(sampleRate, sampleNumber, 220);
        }

        static float FreqMod110(int sampleRate, int sampleNumber)
        {
            return FreqMod(sampleRate, sampleNumber, 110);
        }

        static float FreqMod2ndDegree(int sampleRate, int sampleNumber)
        {
            var t = (float)sampleNumber / (float)sampleRate;
            double freq = 1;
            double freq2 = 1;
            double amp = 1;
            //10 hz up and down at 10hz
            return (float)(amp * Math.Sin(2 * Math.PI * t * (freq + Math.Sin(2 * Math.PI * t * freq2))));
        }

        static Sinusoid test = new Sinusoid(1024);
        static float FixedSinWaveGen(int sampleRate, int sampleNumber, float freq)
        {
            return test[sampleNumber,(freq*test.Resolution/sampleRate)];
        }

        static float SinWaveGen(int sampleRate, int sampleNumber, float freq)
        {
            return (float)(AmpDef * Math.Sin((2 * Math.PI * sampleNumber * freq) / sampleRate));
        }

        static Square test2 = new Square(1024*8, 512*8);
        static float SquareWaveGen(int sampleRate, int sampleNumber, float freq)
        {
            //return test[sampleNumber, (freq * test.Resolution / sampleRate)];
            double t = (double)sampleNumber / (double)sampleRate;
            //var per = 1 / freq;
            //var numOfPer = Math.Floor(t/per);
            //var basicT = t - numOfPer*per;
            double basicT = t - Math.Floor(t * freq) / freq;
            var width = 0.5 / freq;
            var amp = AmpDef;
            return (float)((t < width) ? amp : -amp);
        }

        static float AmpDef = WaveProviderCommon.DefaultAmplitude;
        static float ClippedSinWaveGen(int sampleRate, int sampleNumber, float freq)
        {
            var res = (float)(AmpDef*4 * Math.Sin((2 * Math.PI * sampleNumber * freq) / sampleRate));
            if (res > AmpDef) return AmpDef;
            if (res < -AmpDef) return -AmpDef;
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
