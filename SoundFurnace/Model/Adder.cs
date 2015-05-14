using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundFurnace.Model
{
    class Adder : SoundComponent
    {
        public Adder(int inputs) : base(inputs,1)
        {
        }

        protected override void ComputeIntenal()
        {
            double sum = 0;
            for (var ii = 0; ii < Inputs.Length; ++ii)
                sum += Inputs[ii];
            Outputs[0] = sum;
        }
    }
}
