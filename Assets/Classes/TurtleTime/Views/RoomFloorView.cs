using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;

namespace TurtleTime.Views
{
    class RoomFloorView : View<EnvironmentModule>
    {
        public override ViewType GameObjectType { get { return ViewType.WORLD; } }

        protected override string Name { get { return "Cafe Room"; } }

        public override bool IsActive()
        {
            return true;
        }

        public override void Load()
        {
            UnityUtils.CreateMesh(gameObject, "cafe_room", "cafe_floor", "cafe_floor_material");
        }

        public override void Unload()
        {
            
        }

        public override void Update(float dt)
        {
            
        }
    }
}
