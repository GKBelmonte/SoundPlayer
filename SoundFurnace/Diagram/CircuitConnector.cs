﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalssoft.DiagramNet;

namespace SoundFurnace.Diagram
{
    public class CircuitConnector : ConnectorElement 
    {

        public CircuitConnector(CircuitElement ele)
            : base(ele)
        { }
    }
}
