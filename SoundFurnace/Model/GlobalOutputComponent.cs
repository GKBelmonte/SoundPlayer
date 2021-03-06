﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blaze;
using Blaze.SoundForge;

namespace Blaze.SoundForge.Model
{
    public sealed class GlobalOutputComponent : SoundComponent
    {
        static readonly public SoundComponentDefinition Definition 
            = SoundComponentDefinition.CreateDefinition("Global Output", "Out", "");

        SoundCircuit mParent;
        public GlobalOutputComponent(SoundCircuit parent)
            : base(Definition)
        {
            mParent = parent;
        }

        protected override void ComputeIntenal()
        {
            //Nothing to do here! ;)
        }

    }
}
