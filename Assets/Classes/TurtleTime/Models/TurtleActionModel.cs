using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;

namespace TurtleTime.Models
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
