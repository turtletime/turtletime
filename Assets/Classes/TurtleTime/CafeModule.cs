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
        public override void Load()
        {
            // Load JSON
            JSONNode cafeJSON = Utils.LoadJSONConfig("cafe");
            JSONNode turtleJSON = Utils.LoadJSONConfig("turtles");

            // Models

            AddSingleModel("turtleDatabase", LoadFromJson<TurtleDatabaseModel>(turtleJSON["turtles"]));
            AddSingleModelWithView<CameraModel, CameraView>("camera", LoadFromJson<CameraModel>(cafeJSON["camera"]));
            AddSingleModelWithView<MouseInputModel, MouseInputView>("mouseRayModel", new MouseInputModel());
            AddSingleModelWithView<RoomModel, RoomView>("cafeRoom", new RoomModel("cafe_room"));
            AddEmptyModelList("turtles", new ModelWithViewCollection<TurtleModel, TurtleView>());
            AddEmptyModelList("tables", new ModelWithViewCollection<TableModel, TableView>());
            foreach (JSONNode node in cafeJSON["tables"].Childs)
            {
                GetModelCollection<TableModel>("tables").Add(LoadFromJson<TableModel>(node));
            }
            AddSingleModel("queue", LoadFromJson<QueueModel>(cafeJSON["queue"]));
            AddSingleModel("seats", new SeatCollectionModel(GetModel<QueueModel>("queue"), GetModelCollection<TableModel>("tables")));

            // Controllers

            AddController(new CameraController() { CameraModel = GetModel<CameraModel>("camera") });
            AddController(new TurtleSpawnController() {
                TurtleModels = GetModelCollection<TurtleModel>("turtles"),
                SeatsModel = GetModel<SeatCollectionModel>("seats"),
                TurtleDatabaseModel = GetModel<TurtleDatabaseModel>("turtleDatabase") });
            AddController(new InputController() { MouseRayModel = GetModel<MouseInputModel>("mouseRayModel") });
            AddController(new TurtleController() {
                TurtleModels = GetModelCollection<TurtleModel>("turtles"),
                MouseRayModel = GetModel<MouseInputModel>("mouseRayModel") });
        }

        public override void Unload()
        {
            // TODO
        }
    }
}
