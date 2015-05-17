using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.Core.Wpf;

namespace Blaze.SoundForge.ViewModels
{
    class SoundComponentViewModel : ViewModel
    {
        public string Name { get; private set;}

        List<IOPin> mInputPins;
        IList<IOPin> InputPins { get { return mInputPins; } }

        List<IOPin> mOutputPins;
        IList<IOPin> OutputPins { get { return mOutputPins; } }

        public SoundComponentViewModel(string name, SoundComponentDefinition def)
        {
            Name = name;
            foreach (var input in def.Inputs)
                mInputPins.Add(new IOPin(this, IOPin.Type.Input, input));
            foreach (var output in def.Outputs)
                mOutputPins.Add(new IOPin(this, IOPin.Type.Output, output));
        }

    }
}
