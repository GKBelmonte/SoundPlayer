using Blaze.SoundPlayer.Sounds;
using Blaze.SoundPlayer.WaveProviders;
using Blaze.SoundPlayer.Waves;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blaze.SoundPlayer.TestWinForms
{
    public partial class MainForm : Form
    {
        ISoundPlayer mSound;
        IInstrumentProvider mInstrument;
        public MainForm()
        {
            InitializeComponent();
            this.KeyDown += MainForm_KeyDown;
            this.KeyUp += MainForm_KeyUp;
            mSound = new NAudioSoundPlayer();
            mSound.SampleFrequency = 10000;
            var sinWave = new Sinusoid(1024);
            var sawWave = new Sawtooth(1024);
            var sqrWave = new Square(1024, 512);
            //1.9 ms/note w/ new WaveGenerator(SinWaveGen)
            //2.9 ms/note w/ sinWave.Generator
            var sine = new SimpleSound(sinWave.WaveGenerator, new EnvelopeGenerator(Adsr1));
            
            var sounds = new SimpleSound[] {
                     new SimpleSound(
                        sinWave.WaveGenerator,
                        new EnvelopeGenerator(Adsr1)
                    ),
                    new SimpleSound(
                        sawWave.WaveGenerator,
                        new EnvelopeGenerator(Adsr1)
                    ),
                    new SimpleSound(
                        sqrWave.WaveGenerator,
                        new EnvelopeGenerator(Adsr1)
                    )
                };
            
            mInstrument = NAudioSoundPlayer.FactoryCreateInstrument(  sounds );
            var rc = new Filters.RCLowPass(1000.0f);
            mInstrument.AddFilter(rc);
            //mInstrument = NAudioSoundPlayer.FactoryCreateInstrument(new SimpleSound[] { sine, sine, sine, sine},
            //    new float[] { 1.0f, 2.0f, 1.5f, 3.0f },
            //    new float[] { 1f, 0.5f, 0.25f, 0.125f }
            //    );
            mInstrument.Duration = 1000;
            mInstrument.AmplitudeMultiplier = 1.0f;
            mSound.PlayAsync(mInstrument,220,-1);
        }

        

        static float Adsr1(int sampleRate, int sampleNumber)
        {
            var t = 1000 * (double)sampleNumber / (double)sampleRate;
            double period = 1000;
            //t = t %period
            var mults = Math.Floor(t / period);
            t = t - period * mults;
            const double attackTau = 10;
            const double decayTau = 350;
            const double attackTime = 300;
            if (t > attackTime)
                return (float)(Math.Exp(-(t - attackTime) / decayTau));
            else
                return (float)(1 - Math.Exp(-(t) / attackTau));

        }

        static float FreqMod(int sampleRate, int sampleNumber, float freq)
        {
            var t = (float)sampleNumber / (float)sampleRate;
            double amp = 1;
            //10 hz up and down at 10hz
            return (float)(amp * Math.Sin((2 * Math.PI * t * freq)));
        }
        
        
        static float AmpDef = WaveProviderCommon.DefaultAmplitude;
        static float SinWaveGen(int sampleRate, int sampleNumber, float freq)
        {
            return (float)(AmpDef * Math.Sin((2 * Math.PI * sampleNumber * freq) / sampleRate));
        }

        bool [] keyIsDown = new bool[13];

        int keyCodeToIndex(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                    return 0;

                case Keys.S:
                    return 2;

                case Keys.D:
                    return 4;

                case Keys.F:
                    return 5;

                case Keys.G:
                    return 7;

                case Keys.H:
                    return 9;

                case Keys.J:
                    return 11;

                case Keys.K:
                    return 12;


                case Keys.W:
                    return 1;
                case Keys.E:
                    return 3;

                case Keys.T:
                    return 6;
                case Keys.Y:
                    return 8;
                case Keys.U:
                    return 10;
                default:
                    return-1;
            }
        }

        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            int noteNumber = keyCodeToIndex(e.KeyCode);
            if (noteNumber == -1  ||  keyIsDown[noteNumber])
                return;
            keyIsDown[noteNumber] = true;
            switch (e.KeyCode)
            {
                case Keys.A:
                    mInstrument.NoteOn("C",4);
                    break;
                case Keys.S:
                    mInstrument.NoteOn("D", 4);
                    break;
                case Keys.D:
                    mInstrument.NoteOn("E", 4);
                    break;

                case Keys.F:
                    mInstrument.NoteOn("F", 4);
                    break;
                case Keys.G:
                    mInstrument.NoteOn("G", 4);
                    break;
                case Keys.H:
                    mInstrument.NoteOn("A", 4);
                    break;
                case Keys.J:
                    mInstrument.NoteOn("B", 4);
                    break;
                case Keys.K:
                    mInstrument.NoteOn("C", 5);
                    break;

                case Keys.W:
                    mInstrument.NoteOn("C#", 4);
                    break;
                case Keys.E:
                    mInstrument.NoteOn("D#", 4);
                    break;
                case Keys.T:
                    mInstrument.NoteOn("F#", 4);
                    break;

                case Keys.Y:
                    mInstrument.NoteOn("G#", 4);
                    break;
                case Keys.U:
                    mInstrument.NoteOn("A#", 4);
                    break;
                default:
                    return;
            } Console.WriteLine("Down {0}",noteNumber);
            //MessageBox.Show(string.Format("Value is  {0}:{1}", noteNumber, (byte)(100.0f * (155.0 * ((float)noteNumber - 60) / 12.0f))), "debug");
        }

        void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            int noteNumber = keyCodeToIndex(e.KeyCode);
            if (noteNumber == -1)
                return;
            keyIsDown[noteNumber] = false;
            Console.WriteLine("Up {0}", noteNumber);
        }

        private void trackBarSin_Scroll(object sender, EventArgs e)
        {
            mInstrument.AmplitudeMultipliers[0] = trackBarSin.Value / 100f;
        }

        private void trackBarSaw_Scroll(object sender, EventArgs e)
        {
            mInstrument.AmplitudeMultipliers[1] = trackBarSaw.Value / 100f;
        }

        private void trackBarSqr_Scroll(object sender, EventArgs e)
        {
            mInstrument.AmplitudeMultipliers[2] = trackBarSqr.Value / 100f;
        }
    }
}
