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
        public TurtleDatabaseModule DatabaseModule;
        public QuickOptionsModule OptionsModule;

        public CameraModel CameraModel;
        public RoomModel RoomModel;
        public List<TableModel> TableModels = new List<TableModel>();
        public QueueModel QueueModel;
        public List<TurtleModel> TurtleModels = new List<TurtleModel>();
        public SeatCollectionModel SeatsModel;

        public override void Load()
        {
            // Load JSON
            JSONNode jsonNode = Utils.LoadJSONConfig("cafe");
            // Data links
            DatabaseModule = Engine.GetModule<TurtleDatabaseModule>();
            OptionsModule = Engine.GetModule<QuickOptionsModule>();
            // Models
            CameraModel = LoadFromJson<CameraModel>(jsonNode["camera"]);
            RoomModel = new RoomModel("cafe_room");
            foreach (JSONNode node in jsonNode["tables"].Childs)
            {
                TableModels.Add(LoadFromJson<TableModel>(node));
            }
            QueueModel = LoadFromJson<QueueModel>(jsonNode["queue"]);
            SeatsModel = new SeatCollectionModel(QueueModel, TableModels);
            // Controllers
            AddController(new CameraController());
            AddController(new TurtleSpawnController());
            // Views
            AddView(new RoomFloorView());
            AddView(new RoomWallsView());
            AddView(new CameraView(CameraModel));
            foreach (TableModel tableModel in TableModels) { AddView(new TableView(tableModel)); }
            foreach (TurtleModel turtleModel in TurtleModels) { AddView(new TurtleView(turtleModel)); }
            foreach (SeatModel seat in SeatsModel.Seats) { AddView(new SeatView(seat)); }
        }

        public override void Unload()
        {
            // TODO
        }

        public void AssignTurtleToSeat(TurtleModel turtleModel, SeatModel seatModel)
        {
            if (turtleModel != null && seatModel != null)
            {
                SeatModel oldSeat = turtleModel.TargetSeat;
                if (oldSeat != null)
                {
                    oldSeat.Turtle = null;
                }
                turtleModel.TargetSeat = seatModel;
                seatModel.Turtle = turtleModel;
            }
        }
    }
}
