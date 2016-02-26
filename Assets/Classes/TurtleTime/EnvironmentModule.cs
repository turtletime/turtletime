using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;

namespace TurtleTime
{
    /// <summary>
    /// Controls physical properties (decorations, turtles, tables, etc.)
    /// </summary>
    class EnvironmentModule : GameModule
    {
        public override GameModuleKey ModuleKey
        {
            get
            {
                return TurtleTimeModules.PHYSICAL;
            }
        }

        public override void Load()
        {
            
        }

        public override void ReceiveData(GameModule other)
        {
            
        }

        public override void Unload()
        {
            
        }
    }
}
