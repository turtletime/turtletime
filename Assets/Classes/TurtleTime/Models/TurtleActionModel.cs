﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;

namespace TurtleTime
{
    class TurtleActionModel : Model
    {
        public TurtleModel Subject { get; set; }
        public SeatModel Target { get; set; }

        public void Reset()
        {
            Subject = null;
            Target = null;
        }
    }
}
