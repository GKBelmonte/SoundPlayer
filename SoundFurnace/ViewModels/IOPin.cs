using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundFurnace.ViewModels
{
    class IOPin
    {
        public Type PinType {get; private set;}
        public string Name { get; private set; }
        
        SoundComponentViewModel mParent;
        public IOPin(SoundComponentViewModel parent, Type type, string name)
        {
            mParent = parent;
            PinType = type;
            Name = name;
        }

        public enum Type { Input, Output } 
        
    }
}
