using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.Core.Wpf;

namespace Blaze.SoundForge.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<SoundComponentDefinition> Components{get; private set;}
        public MainWindowViewModel()
        {
            Components = new ObservableCollection<SoundComponentDefinition>();
            foreach (var ele in SoundComponentDefinition.StandardDefinitions.Values)
                Components.Add(ele);
        }

    }
}
