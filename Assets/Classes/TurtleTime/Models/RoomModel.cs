using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;

namespace TurtleTime
{
    class RoomModel : Model
    {
        String layoutName;

        public RoomModel()
        {
            this.layoutName = "cafe_room";
        }
    }
}
