using UnityMVC;
using UnityEngine;

namespace TurtleTime
{
    /// <summary>
    /// Controls physical properties (decorations, turtles, tables, etc.)
    /// </summary>
    class CafeModule : GamePhase
    {
        private static T LoadFromJson<T>(IJsonObject node) where T : Model, new()
        {
            T result = new T();
            result.LoadFromJson(node);
            return result;
        }

        public override void Load()
        {
            GameObject.Find("UI").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

            /* Models */

            AddModel<TurtleDatabaseModel>("turtleDatabase", ReadOnlyData.JsonData["turtles"]["turtles"]);
            AddModel<CameraModel, CameraView>("camera", ReadOnlyData.JsonData["cafe"]["camera"]);
            AddModel<MouseInputModel, MouseInputView>("mouseRay");
            AddModel<RoomModel, RoomView>("cafeRoom");
            // turtles
            AddModelCollection<TurtleModel, TurtleModel.View>("turtles");
            // queues and tables
            AddModelCollection<TableModel, TableModel.View>("tables");
            foreach (IJsonObject node in ReadOnlyData.JsonData["cafe"]["tables"].AsList)
            {
                GetModelCollection<TableModel>("tables").Add(LoadFromJson<TableModel>(node));
            }
            AddModel<QueueModel>("queue", ReadOnlyData.JsonData["cafe"]["queue"]);
            // seats
            AddModelCollection<SeatModel, SeatModel.View>("seats");
            AddModel<TurtleActionModel>("turtleAction");
            AddModel<SeatCollectionModel>("seatCollection");
            GetModel<SeatCollectionModel>("seatCollection").Seats = GetModelCollection<SeatModel>("seats");
            GetModel<SeatCollectionModel>("seatCollection").Initialize(GetModel<QueueModel>("queue"), GetModelCollection<TableModel>("tables"));
            // ui stuff
            AddModel<TurtleTimeUIModel, UIView>("ui", ReadOnlyData.JsonData["ui"]["turtleProfile"]);

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
                UIModel = GetModel<TurtleTimeUIModel>("ui"),
                TurtleModels = GetModelCollection<TurtleModel>("turtles")
            });
        }

        public override void Unload()
        {
            // TODO
        }
    }
}
