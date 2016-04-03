using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityMVC;

namespace TurtleTime
{
    class SeatModel : PhysicalModel
    {
        public bool Taken { get; set; }
        public bool Selected { get; set; }

        public class View : BillboardSpriteView<SeatModel>
        {
            public override string NodeName { get { return "Seat"; } }
            protected override string SpriteName { get { return "test"; } }
        }
    }
}
