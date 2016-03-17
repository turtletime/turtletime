using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using TurtleTime.Models;
using UnityEngine;

namespace TurtleTime.Views
{
    class RoomView : View3D<RoomModel>
    {
        GameObject floor;
        GameObject walls;

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

        public override void Update()
        {
            
        }
    }
}
