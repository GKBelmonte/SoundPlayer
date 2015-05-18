using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundForge.Diagram;
using Dalssoft.DiagramNet;

namespace SoundFurnace.Diagram
{
    abstract class DiagramCommand //some command base or something
    {
        public DiagramCommand(Dalssoft.DiagramNet.Designer diagram)
        {
            mDiagram = diagram;
        }

        protected Dalssoft.DiagramNet.Designer mDiagram;

        public abstract void Do();

        public abstract void Undo();
    }

    class AddNodeCommand : DiagramCommand
    {
        CircuitElement mElement;
        public AddNodeCommand(Designer des, CircuitElement element) : base(des)
        {
            mElement = element;
        }

        public override void Do()
        {
            mDiagram.Document.AddElement(mElement);
            //do shit to resore links
        }

        public override void Undo()
        {
            //do shit to remove links
            mDiagram.Document.DeleteElement(mElement);
        }
    }
}
