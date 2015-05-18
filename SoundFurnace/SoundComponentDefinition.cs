using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundForge
{
    public class SoundComponentDefinition
    {
        public string TypeName { get; protected set; }
        public List<string> Inputs {get; protected set;}
        public List<string> Outputs { get; protected set; }
        public SoundComponentDefinition(string name, List<string> inputs, List<string> outputs)
        {
            TypeName = name;
            Inputs = new List<string>(inputs);
            Outputs = new List<string>(outputs);
        }

        public override string ToString()
        {
            return TypeName;
        }

        static SoundComponentDefinition()
        {
            StandardDefinitions = new Dictionary<string, SoundComponentDefinition>(10);
            StandardDefinitions.AddDefinition("Wave", "SampleRate,Sample,Frequency","Output");
            //StandardDefinitions.AddDefinition("Sinusoid", "SampleRate,Sample,Frequency", "Output");
            //StandardDefinitions.AddDefinition("Square", "SampleRate,Sample,Frequency", "Output");
            //StandardDefinitions.AddDefinition("Triangle", "SampleRate,Sample,Frequency", "Output");
            //StandardDefinitions.AddDefinition("Constant", "", "Output");
            var soundComponentType = typeof(Model.SoundComponent);
            Assembly asm = Assembly.GetAssembly(soundComponentType);
            Type[] types = asm.GetTypes();
            ComponentTypes = new List<Type>();
            for (var ii = 0; ii < types.Length; ++ii)
            {
                if (types[ii].BaseType == soundComponentType)
                    ComponentTypes.Add(types[ii]);
            }



        }
        static public List<Type> ComponentTypes;
        static public Dictionary<string, SoundComponentDefinition> StandardDefinitions { get; private set; }

        public static SoundComponentDefinition CreateDefinition(string name, string inputs, string outputs)
        {
            var lInputs = inputs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var lOutputs = outputs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            lInputs.ForEach(a => a = a.Trim());
            lOutputs.ForEach(a => a = a.Trim());
            return new SoundComponentDefinition(name, lInputs, lOutputs);
        }
    }

    static class Extensions //because expressive power is good
    {
        public static void AddDefinition(this Dictionary<string, SoundComponentDefinition> self, string name, string inputs, string outputs)
        {
            self.Add(name, SoundComponentDefinition.CreateDefinition(name,inputs, outputs));
        }


    }
}
