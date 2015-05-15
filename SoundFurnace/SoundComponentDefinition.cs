using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundFurnace
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

        static SoundComponentDefinition()
        {
            StandardDefinitions = new Dictionary<string, SoundComponentDefinition>(10);
            StandardDefinitions.AddDefinition("Wave", "SampleRate,Sample,Frequency","Output");
            StandardDefinitions.AddDefinition("Constant", "", "Output");
        }

        static public Dictionary<string, SoundComponentDefinition> StandardDefinitions {get; private set;}
    }

    static class Extensions //because expressive power is good
    {
        public static void AddDefinition(this Dictionary<string, SoundComponentDefinition> self, string name, string inputs, string outputs)
        {
            var lInputs = inputs.Split(new [] {','},StringSplitOptions.RemoveEmptyEntries).ToList();
            var lOutputs = outputs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            lInputs.ForEach(a => a = a.Trim());
            lOutputs.ForEach(a => a = a.Trim());
            self.Add(name, new SoundComponentDefinition(name ,lInputs, lOutputs));
        }
    }
}
