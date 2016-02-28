using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;

namespace TurtleTime.Models
{
    class RoomModel : Model
    {
        String layoutName;

        public RoomModel(String layoutName)
        {
            this.layoutName = layoutName;
        }
    }
}
