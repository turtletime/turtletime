using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityMVC;
using TurtleTime.Models;
using TurtleTime.Controllers;
using TurtleTime.Views;
using TurtleTime.Utils;
using SimpleJSON;

namespace TurtleTime
{
    /// <summary>
    /// Controls physical properties (decorations, turtles, tables, etc.)
    /// </summary>
    class CafeModule : GamePhase
    {
        private static T LoadFromJson<T>(JSONNode node) where T : Model, new()
        {
            T result = new T();
            result.LoadFromJson(node);
            return result;
        }

        public override void Load()
        {
            /* Load JSON */

            JSONNode cafeJSON = TurtleUtils.LoadJSONConfig("cafe");
            JSONNode turtleJSON = TurtleUtils.LoadJSONConfig("turtles");
            JSONNode uiJSON = TurtleUtils.LoadJSONConfig("ui");

            /* Models */

            AddModel<TurtleDatabaseModel>("turtleDatabase", turtleJSON["turtles"]);
            AddModel<CameraModel, CameraView>("camera", cafeJSON["camera"]);
            AddModel<MouseInputModel, MouseInputView>("mouseRay");
            AddModel<RoomModel, RoomView>("cafeRoom");
            // turtles
            AddModelCollection<TurtleModel, TurtleView>("turtles");
            // queues and tables
            AddModelCollection<TableModel, TableView>("tables");
            foreach (JSONNode node in cafeJSON["tables"].Childs)
            {
                GetModelCollection<TableModel>("tables").Add(LoadFromJson<TableModel>(node));
            }
            AddModel<QueueModel>("queue", cafeJSON["queue"]);
            // seats
            AddModelCollection<SeatModel, SeatView>("seats");
            AddModel<TurtleActionModel>("turtleAction");
            AddModel<SeatCollectionModel>("seatCollection");
            GetModel<SeatCollectionModel>("seatCollection").Seats = GetModelCollection<SeatModel>("seats");
            GetModel<SeatCollectionModel>("seatCollection").Initialize(GetModel<QueueModel>("queue"), GetModelCollection<TableModel>("tables"));
            // ui stuff
            AddModel<UIModel, UIView>("ui", uiJSON["turtleProfile"]);

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
