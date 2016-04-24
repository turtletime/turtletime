using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;
using UnityEngine;

namespace TurtleTime
{
    class RoomView : View3D<RoomModel>
    {
        private class TileTuple
        {
            public int X;
            public int Y;
            public GameObject Tile;
        }

        GameObject floor;
        GameObject walls;
        List<TileTuple> tiles;

        public override string NodeName { get { return "Room"; } }

        protected override void Load()
        {
            this.name = "Cafe Room";
            floor = new GameObject("Floor");
            walls = new GameObject("Walls");
            floor.transform.SetParent(this.transform);
            walls.transform.SetParent(this.transform);
            //UnityUtils.CreateMesh(floor, "cafe_room", "cafe_floor", "cafe_floor_material");
            //UnityUtils.CreateMesh(walls, "cafe_room", "cafe_walls", "cafe_wall_material");
            tiles = new List<TileTuple>();
            for (int i = 0; i < Model.Width - 1; i++)
            {
                for (int j = 0; j < Model.Height - 1; j++)
                {
                    var tile = new GameObject("tile");
                    tile.transform.position = TurtleUtils.CafeSpaceToWorldCoordinates(new Vector2(i + 0.5f, j + 0.5f), 0.01f);
                    tile.transform.SetParent(this.transform);
                    UnityUtils.CreateMesh(tile, "quad", "quad", "default");
                    tile.transform.localScale = 0.1f * Vector3.one;
                    tile.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.red, Color.white, 0.5f);
                    tiles.Add(new TileTuple() { X = i, Y = j, Tile = tile });
                }
            }
        }

        protected override void UpdateView()
        {
            foreach (var tile in tiles)
            {
                tile.Tile.GetComponent<MeshRenderer>().material.color =
                    Color.Lerp(Model.GetModelAtLocation(new Vector2(tile.X, tile.Y)) != null ? Color.red : Color.blue, Color.white, 0.5f);
            }
        }
    }
}
