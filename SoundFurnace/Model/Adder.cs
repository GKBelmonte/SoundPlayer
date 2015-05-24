using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundForge.Model
{
    class Adder : SoundComponent
    {
        public Adder(int inputs) : base("Adder",inputs,1)
        {
        }

        protected override void ComputeIntenal()
        {
            for (var jj = 0; jj < SamplesPerComputation; ++jj )
            {
                double sum = 0;
                for (var ii = 0; ii < Inputs.Length; ++ii)
                    sum += Inputs[ii][jj];
                Outputs[0][jj] = sum;
            }
        }
    }
}
