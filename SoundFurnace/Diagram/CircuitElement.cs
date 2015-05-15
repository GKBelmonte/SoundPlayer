using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Dalssoft.DiagramNet;
using SoundFurnace.Model;
using System.Windows.Forms;

namespace SoundFurnace.Diagram
{
    class CircuitElement : RectangleNode
    {
        static Font CircuitElementInputLabel = new Font("Segoe UI", 9) ;
        static Font CircuitElementType = new Font("Segoe UI", 9, FontStyle.Bold);
        static Brush ForegroundBrush = new SolidBrush(Color.Black);
        static int sLabelSize = 16;
        static int sLabelPadding = 2;
        static int sCenterPadding = 5;
        protected int[] mOutputLabelSizes;

        SoundComponent mComponent;
        public CircuitElement(SoundComponent component)
        {
            mComponent = component;
            
            Label.Text = component.TypeName;
            controller.Resizeable = false;
            FillColor1 = FillColor2;
            Layout();
        }

        protected virtual void Layout()
        {
            int maxHeight = sLabelSize, maxWidth = 100;
            var inputs = mComponent.InputNames;
            foreach(var input in inputs)
            {
                var s = TextRenderer.MeasureText(input, CircuitElementInputLabel);
                maxHeight = Math.Max(s.Height,maxHeight);
                maxWidth = Math.Max(s.Width,maxWidth);
            }
            var outputs = mComponent.OutputNames;
            mOutputLabelSizes = new int[outputs.Length];
            int count = 0;
            foreach(var output in outputs)
            {
                var s = TextRenderer.MeasureText(output, CircuitElementInputLabel);
                maxHeight = Math.Max(s.Height,maxHeight);
                maxWidth = Math.Max(s.Width,maxWidth);
                mOutputLabelSizes[count++] = s.Width;
            }

            Size = new Size(2 * maxWidth + sCenterPadding, ((maxHeight + sLabelPadding) * (Math.Max(mComponent.Inputs.Length, mComponent.Outputs.Length) + 2)));
        }

        protected override void Draw(Graphics g)
        {
            base.Draw(g);
            g.DrawString("✗", CircuitElementInputLabel, ForegroundBrush, new Point(location.X + size.Width - 10, location.Y));
            var inputs = mComponent.InputNames;
            for (var ii = 0; ii < inputs.Length; ++ii)
                g.DrawString(inputs[ii], CircuitElementInputLabel, ForegroundBrush, new Point(location.X, location.Y + (ii+1)*(sLabelSize + sLabelPadding)));
            var outputs = mComponent.OutputNames;
            for (var ii = 0; ii < outputs.Length; ++ii)
                g.DrawString(outputs[ii], CircuitElementInputLabel, ForegroundBrush, new Point(location.X + size.Width - mOutputLabelSizes[ii], location.Y + (ii + 1) * (sLabelSize + sLabelPadding)));
        }

        public override Point Location
        {
            get { return base.Location; }
            set
            {
                base.Location = value;
                label.Location = new Point(Location.X, Location.Y - 10);
            }
        }

        public override Size Size
        {
            get { return base.Size; }
            set
            {
                base.Size = value;
                label.Size = new Size(Size.Width, 0);
            }
        }
    }
}
