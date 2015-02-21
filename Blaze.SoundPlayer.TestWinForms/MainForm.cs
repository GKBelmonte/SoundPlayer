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
            mSound = new NAudioSoundPlayer();
            mSound.SampleFrequency = 10000;
            var sinWave = new Sinusoid(1024);
            var sawWave = new Sawtooth(1024);
            var sqrWave = new Square(1024, 512);

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
            mInstrument.Duration = 1000;
            mInstrument.AmplitudeMultiplier = 1.0f;
            mSound.PlayAsync(mInstrument,220,-1);
        }

        static float Adsr1(int sampleRate, int sampleNumber)
        {
            var t = 1000 * (double)sampleNumber / (double)sampleRate;
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

        static float FreqMod(int sampleRate, int sampleNumber, float freq)
        {
            var t = (float)sampleNumber / (float)sampleRate;
            double amp = 1;
            //10 hz up and down at 10hz
            return (float)(amp * Math.Sin((2 * Math.PI * t * freq)));
        }

        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

            int noteNumber = 0;
            switch (e.KeyCode)
            {
                case Keys.A:
                    mInstrument.NoteOn(261.6256f);
                    break;
                case Keys.S:
                    mInstrument.NoteOn(293.6648f);
                    break;
                case Keys.D:
                    mInstrument.NoteOn(329.6276f);
                    break;

                case Keys.F:
                    mInstrument.NoteOn(349.2282f);
                    break;
                case Keys.G:
                    mInstrument.NoteOn(391.9954f);
                    break;
                case Keys.H:
                    mInstrument.NoteOn(440);
                    break;
                case Keys.J:
                    mInstrument.NoteOn(493.8833f);
                    break;
                case Keys.W:
                    mInstrument.NoteOn(523.2512f);
                    break;
                case Keys.E:
                    noteNumber = 63;
                    break;
                case Keys.T:
                    noteNumber = 66;
                    break;

                case Keys.Y:
                    noteNumber = 68;
                    break;
                case Keys.U:
                    noteNumber = 70;
                    break;
                default:
                    return;
            }

            //MessageBox.Show(string.Format("Value is  {0}:{1}", noteNumber, (byte)(100.0f * (155.0 * ((float)noteNumber - 60) / 12.0f))), "debug");
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
