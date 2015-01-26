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
        public MainForm()
        {
            InitializeComponent();
            this.KeyDown += MainForm_KeyDown;

        }

        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

            int noteNumber = 0;
            switch (e.KeyCode)
            {
                case Keys.A:
                    noteNumber = 60;
                    break;
                case Keys.S:
                    noteNumber = 62;
                    break;
                case Keys.D:
                    noteNumber = 64;
                    break;

                case Keys.F:
                    noteNumber = 65;
                    break;
                case Keys.G:
                    noteNumber = 67;
                    break;
                case Keys.H:
                    noteNumber = 69;
                    break;
                case Keys.J:
                    noteNumber = 71;
                    break;
                case Keys.W:
                    noteNumber = 61;
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
    }
}
