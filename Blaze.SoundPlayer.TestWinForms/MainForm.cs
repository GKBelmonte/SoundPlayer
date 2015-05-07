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
        Dictionary<Keys, Note> mBaseKeyMapping;
        Dictionary<Keys, Note> mKeyMapping;
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
            
            /*Mapping the keys*/
            keyIsDown = new Dictionary<Keys, bool>();


            mKeyMapping = new Dictionary<Keys, Note>();
            mBaseKeyMapping = new Dictionary<Keys, Note>();
            
            mBaseKeyMapping.MapMany(
                Keys.D1, "C", 4,
                Keys.Q, "E", 4,
                Keys.A, "G", 4,
                Keys.Z, "B", 4,

                Keys.D2, "D", 4,
                Keys.W, "F", 4,
                Keys.S, "A", 4,
                Keys.X, "C", 5,

                Keys.D3, "E", 4,
                Keys.E, "G", 4,
                Keys.D, "B", 4,
                Keys.C, "D", 5,

                Keys.D4, "F", 4,
                Keys.R, "A", 4,
                Keys.F, "C", 5,
                Keys.V, "E", 5);

            foreach (KeyValuePair<Keys, Note> pair in mBaseKeyMapping)
                keyIsDown.Add(pair.Key, false);


            foreach (KeyValuePair<Keys, Note> pair in mBaseKeyMapping)
                mKeyMapping.Add(pair.Key, pair.Value);


        }

        static double mAttack = 10;
        static double mDecay = 350;

        static float Adsr1(int sampleRate, int sampleNumber)
        {
            var t = 1000 * (double)sampleNumber / (double)sampleRate;
            double period = 1000;
            //t = t %period
            var mults = Math.Floor(t / period);
            t = t - period * mults;

            const double attackTime = 300;
            if (t > attackTime)
                return (float)(Math.Exp(-(t - attackTime) / mDecay));
            else
                return (float)(1 - Math.Exp(-(t) / mAttack));

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

        Dictionary <Keys,bool> keyIsDown ;


        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

            bool isDown;
            bool valid = keyIsDown.TryGetValue(e.KeyCode, out isDown);
            if (!valid  ||  isDown)
                return;
            keyIsDown[e.KeyCode] = true;

            var note = mKeyMapping[e.KeyCode];
            mInstrument.NoteOn(note.mStep, note.mOctave);

            Console.WriteLine("Down {0}",note);

            //MessageBox.Show(string.Format("Value is  {0}:{1}", noteNumber, (byte)(100.0f * (155.0 * ((float)noteNumber - 60) / 12.0f))), "debug");
        }

        void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            bool isDown;
            bool valid = keyIsDown.TryGetValue(e.KeyCode, out isDown);
            if (!valid)
                return;
            keyIsDown[e.KeyCode] = false;
            Console.WriteLine("Up {0}", mKeyMapping[e.KeyCode]);
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

        private void trackBarKey_Scroll(object sender, EventArgs e)
        {
            mKeyMapping.Clear();
            foreach (KeyValuePair<Keys, Note> pair in mBaseKeyMapping)
                mKeyMapping.Add(pair.Key, pair.Value + trackBar1.Value);
            labelKeyValue.Text = mKeyMapping[Keys.Q].mStep;
        }

        private void trackBarAttack_Scroll(object sender, EventArgs e)
        {
            mAttack = (double) trackBarAttack.Value;
        }

        private void trackBarDecay_Scroll(object sender, EventArgs e)
        {
            mDecay = (double) trackBarDecay.Value * 10;
        }

    }

    static class Extensions
    { 
        static public void Map(this Dictionary<Keys,Note> self, Keys key, string note)
        {
            self.Add(key,new Note(note,4,0,0));   
        }

        static public void MapMany(this Dictionary<Keys, Note> self, params object[] stuff)
        {
            for (var ii = 0; ii < stuff.Length; ii += 3)
            {
                self.Add((Keys)stuff[ii], new Note((string)stuff[ii + 1], (int)stuff[ii + 2], 0, 0));
            }
        }

        static public void AddMany<K,T>(this Dictionary<K, T> self, params object[] stuff)
        {
            for (var ii = 0; ii < stuff.Length; ii += 2)
            {
                self.Add((K)stuff[ii], (T)stuff[ii + 1]);
            }
        }
    }
}
