using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CheloniiUnity;
using TurtleTime.Models;
using TurtleTime.Controllers;
using TurtleTime.Views;
using System.IO;
using SimpleJSON;

namespace TurtleTime
{
    /// <summary>
    /// Controls physical properties (decorations, turtles, tables, etc.)
    /// </summary>
    class CafeModule : GameModule
    {
        TurtleDatabaseModule databaseModule;

        public CameraModel CameraModel;
        public RoomModel RoomModel;
        public List<TableModel> TableModels = new List<TableModel>();
        public List<TurtleModel> TurtleModels = new List<TurtleModel>();

        public override void Load()
        {
            // Load JSON
            JSONNode jsonNode = Utils.LoadJSONConfig("cafe");
            // Data links
            databaseModule = Engine.GetModule<TurtleDatabaseModule>();
            // Models
            CameraModel = LoadFromJson<CameraModel>(jsonNode["camera"]);
            RoomModel = new RoomModel("cafe_room");
            TableModels.Add(new TableModel() { Position = new Vector2(2, 2) });
            TurtleModels.Add(new TurtleModel() { StaticData = databaseModule.TurtleData["turtle_1_no_materials"], Position = new Vector2(3, 2) });
            // Controllers
            AddController(new CameraController());
            // Views
            AddView(new RoomFloorView());
            AddView(new RoomWallsView());
            AddView(new CameraView(CameraModel));
            foreach (TableModel tableModel in TableModels) { AddView(new TableView(tableModel)); }
            foreach (TurtleModel turtleModel in TurtleModels) { AddView(new TurtleView(turtleModel)); }
        }

        public override void Unload()
        {
            // TODO
        }
    }
}
