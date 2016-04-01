using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;

namespace TurtleTime
{
    class TurtleTimeEngine : GameEngine
    {
        protected override void Initialize()
        {
            AddModule<CafeModule>();
        }
    }
}
