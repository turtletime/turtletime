using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;
using TurtleTime.Models;
using TurtleTime.Utils;
using UnityEngine;

namespace TurtleTime.Views
{
    class RoomView : View3D<RoomModel>
    {
        GameObject floor;
        GameObject walls;

        public override string NodeName { get { return "Room"; } }

        protected override void Load()
        {
            this.name = "Cafe Room";
            floor = new GameObject("Floor");
            walls = new GameObject("Walls");
            floor.transform.SetParent(this.transform);
            walls.transform.SetParent(this.transform);
            UnityUtils.CreateMesh(floor, "cafe_room", "cafe_floor", "cafe_floor_material");
            UnityUtils.CreateMesh(walls, "cafe_room", "cafe_walls", "cafe_wall_material");
        }

        protected override void UpdateView()
        {
            throw new NotImplementedException();
        }
    }
}
