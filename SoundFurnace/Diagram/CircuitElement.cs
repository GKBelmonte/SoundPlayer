using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Dalssoft.DiagramNet;
using Blaze.SoundForge.Model;
using System.Windows.Forms;

namespace Blaze.SoundForge.Diagram
{
    public class CircuitElement : RectangleNode
    {
        static Font CircuitElementInputLabel = new Font("Segoe UI", 9) ;
        static Font CircuitElementType = new Font("Segoe UI", 9, FontStyle.Bold);
        static Brush ForegroundBrush = new SolidBrush(Color.Black);
        static int sLabelSize = 16;
        static int sLabelPadding = 2;
        static int sCenterPadding = 5;
        static int sConnectorOffset = 5;
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

        protected ConnectorElement[] mAllConectors;
        protected CircuitConnector[] mInputConnectos;
        protected CircuitConnector[] mOutputConnectos;

        public override ConnectorElement[] Connectors
        {
            get
            {
                return mAllConectors;
            }
        }



        protected virtual void Layout()
        {
            int maxHeight = sLabelSize, maxWidth = 30;
            var inputs = mComponent.InputNames;
            foreach (var input in inputs)
            {
                var s = TextRenderer.MeasureText(input, CircuitElementInputLabel);
                maxHeight = Math.Max(s.Height, maxHeight);
                maxWidth = Math.Max(s.Width, maxWidth);
            }
            var outputs = mComponent.OutputNames;
            mOutputLabelSizes = new int[outputs.Length];
            int count = 0;
            foreach (var output in outputs)
            {
                var s = TextRenderer.MeasureText(output, CircuitElementInputLabel);
                maxHeight = Math.Max(s.Height, maxHeight);
                maxWidth = Math.Max(s.Width, maxWidth);
                mOutputLabelSizes[count++] = s.Width;
            }
            var lineCount = (Math.Max(mComponent.Inputs.Length, mComponent.Outputs.Length) + 1);
            Size = new Size(2 * maxWidth + sCenterPadding, ((maxHeight + sLabelPadding) * lineCount) + lineCount);

            //Add connectors
            connects = new ConnectorElement[0];
            mInputConnectos = new CircuitConnector[inputs.Length];
            mOutputConnectos = new CircuitConnector[outputs.Length];
            for (var ii = 0; ii < inputs.Length; ++ii)
                mInputConnectos[ii] = new CircuitConnector(this);
            for (var ii = 0; ii < outputs.Length; ++ii)
                mOutputConnectos[ii] = new CircuitConnector(this);
            var temp = new List<CircuitConnector>(inputs.Length + outputs.Length);
            temp.AddRange(mInputConnectos);
            temp.AddRange(mOutputConnectos);
            mAllConectors = temp.ToArray();
            UpdateConnectorsPosition();

            //off with the border
            //borderColor = Color.Transparent;//misleading, not actually used. 
            //A RectangleNode is a RectangleElement AND a NodeElement...

            rectangle.BorderColor = Color.Transparent;
        }

        protected override void UpdateConnectorsPosition()
        {
            if (mAllConectors == null)
                return;
            var inputs = mInputConnectos;
            
            for (var ii = 0; ii < inputs.Length; ++ii)
            {
                inputs[ii].Location = new Point(location.X - sConnectorOffset, location.Y + (ii + 1) * (sLabelSize + sLabelPadding) + 2*connectSize);
                inputs[ii].Size = new Size(connectSize * 2, connectSize * 2);
            }
            var outputs = mOutputConnectos;
            for (var ii = 0; ii < outputs.Length; ++ii)
            {
                outputs[ii].Location = new Point(location.X + size.Width + sConnectorOffset - 2*connectSize, location.Y + (ii + 1) * (sLabelSize + sLabelPadding) + 2 * connectSize);
                outputs[ii].Size = new Size(connectSize * 2, connectSize * 2);
            }
        }

        protected override void Draw(Graphics g)
        {
            base.Draw(g);
            g.DrawString("✗", CircuitElementInputLabel, ForegroundBrush, new Point(location.X + size.Width - 13, location.Y));
            var inputs = mComponent.InputNames;
            for (var ii = 0; ii < inputs.Length; ++ii)
                g.DrawString(inputs[ii], CircuitElementInputLabel, ForegroundBrush, new Point(location.X + sLabelPadding, location.Y + (ii + 1) * (sLabelSize + sLabelPadding)));
            var outputs = mComponent.OutputNames;
            for (var ii = 0; ii < outputs.Length; ++ii)
                g.DrawString(outputs[ii], CircuitElementInputLabel, ForegroundBrush, new Point(location.X + size.Width - mOutputLabelSizes[ii] - sLabelPadding, location.Y + (ii + 1) * (sLabelSize + sLabelPadding)));
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
