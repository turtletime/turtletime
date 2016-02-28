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
        Models.RoomModel roomModel;

        public override GameModuleKey ModuleKey
        {
            get
            {
                return TurtleTimeModules.PHYSICAL;
            }
        }

        public override void Load()
        {
            // Models
            roomModel = new Models.RoomModel("cafe_room");
            // Views
            AddView(new Views.RoomFloorView());
            AddView(new Views.RoomWallsView());
        }

        public override void ReceiveData(GameModule other)
        {
            
        }

        public override void Unload()
        {
            
        }
    }
}
