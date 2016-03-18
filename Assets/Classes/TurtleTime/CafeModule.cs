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
            /* Load JSON */

            JSONNode cafeJSON = Utils.LoadJSONConfig("cafe");
            JSONNode turtleJSON = Utils.LoadJSONConfig("turtles");
            JSONNode uiJSON = Utils.LoadJSONConfig("ui");

            /* Models */

            AddSingleModel("turtleDatabase", LoadFromJson<TurtleDatabaseModel>(turtleJSON["turtles"]));
            AddSingleModelWithView<CameraModel, CameraView>("camera", LoadFromJson<CameraModel>(cafeJSON["camera"]));
            AddSingleModelWithView<MouseInputModel, MouseInputView>("mouseRay", new MouseInputModel());
            AddSingleModelWithView<RoomModel, RoomView>("cafeRoom", new RoomModel("cafe_room"));
            // turtles
            AddEmptyModelList("turtles", new ModelWithViewCollection<TurtleModel, TurtleView>());
            // queues and tables
            AddEmptyModelList("tables", new ModelWithViewCollection<TableModel, TableView>());
            foreach (JSONNode node in cafeJSON["tables"].Childs)
            {
                GetModelCollection<TableModel>("tables").Add(LoadFromJson<TableModel>(node));
            }
            AddSingleModel("queue", LoadFromJson<QueueModel>(cafeJSON["queue"]));
            // seats
            AddEmptyModelList("seats", new ModelWithViewCollection<SeatModel, SeatView>());
            AddSingleModel("turtleAction", new TurtleActionModel());
            AddSingleModel("seatCollection", new SeatCollectionModel() { Seats = GetModelCollection<SeatModel>("seats") });
            GetModel<SeatCollectionModel>("seatCollection").Initialize(GetModel<QueueModel>("queue"), GetModelCollection<TableModel>("tables"));
            // ui stuff
            AddSingleModelWithView<UIModel, UIView>("ui", LoadFromJson<UIModel>(uiJSON["turtleProfile"]));

            /* Controllers */

            AddController(new CameraController() { CameraModel = GetModel<CameraModel>("camera") });
            AddController(new TurtleSpawnController()
            {
                TurtleModels = GetModelCollection<TurtleModel>("turtles"),
                SeatsModel = GetModel<SeatCollectionModel>("seatCollection"),
                TurtleDatabaseModel = GetModel<TurtleDatabaseModel>("turtleDatabase")
            });
            AddController(new InputController() { MouseRayModel = GetModel<MouseInputModel>("mouseRay") });
            AddController(new TurtleController()
            {
                TurtleModels = GetModelCollection<TurtleModel>("turtles"),
                MouseInputModel = GetModel<MouseInputModel>("mouseRay")
            });
            AddController(new SeatController()
            {
                SeatModels = GetModelCollection<SeatModel>("seats"),
                MouseInputModel = GetModel<MouseInputModel>("mouseRay")
            });
            AddController(new TurtleActionController()
            {
                ActionModel = GetModel<TurtleActionModel>("turtleAction"),
                SeatModels = GetModelCollection<SeatModel>("seats"),
                TurtleModels = GetModelCollection<TurtleModel>("turtles")
            });
            AddController(new UIController()
            {
                UIModel = GetModel<UIModel>("ui"),
                TurtleModels = GetModelCollection<TurtleModel>("turtles")
            });
        }

        public override void Unload()
        {
            // TODO
        }
    }
}
