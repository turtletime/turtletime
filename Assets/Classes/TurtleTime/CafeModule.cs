using UnityMVC;
using UnityEngine;

namespace TurtleTime
{
    /// <summary>
    /// Controls physical properties (decorations, turtles, tables, etc.)
    /// </summary>
    class CafeModule : GameModule
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

            AddModel<TurtleDatabaseModel>(ReadOnlyData.JsonData["turtles"]["turtles"]);
            AddModel<CameraModel, CameraView>(ReadOnlyData.JsonData["cafe"]["camera"]);
            AddModel<MouseInputModel, MouseInputView>();
            AddModel<RoomModel, RoomView>();
            // turtles
            AddModelCollection<TurtleModel, TurtleModel.View>();
            // queues and tables
            AddModelCollection<TableModel, TableModel.View>(ReadOnlyData.JsonData["cafe"]["tables"]);
            AddModel<QueueModel>(ReadOnlyData.JsonData["cafe"]["queue"]);
            // seats
            AddModelCollection<SeatModel, SeatModel.View>();
            AddModel<TurtleActionModel>();
            AddModel<SeatCollectionModel>();
            GetModel<SeatCollectionModel>().Seats = GetModelCollection<SeatModel>();
            GetModel<SeatCollectionModel>().Initialize(GetModel<QueueModel>(), GetModelCollection<TableModel>());
            // ui stuff
            AddModel<TurtleTimeUIModel, UIView>(ReadOnlyData.JsonData["ui"]["turtleProfile"]);

            /* Controllers */

            AddController(new InitializationController()
            {
                Room = GetModel<RoomModel>(),
                SeatCollection = GetModel<SeatCollectionModel>(),
                Queue = GetModel<QueueModel>(),
                Turtles = GetModelCollection<TurtleModel>(),
                Tables = GetModelCollection<TableModel>(),
                Seats = GetModelCollection<SeatModel>()
            });
            AddController(new CameraController() { CameraModel = GetModel<CameraModel>() });
            AddController(new TurtleSpawnController()
            {
                TurtleModels = GetModelCollection<TurtleModel>(),
                SeatsModel = GetModel<SeatCollectionModel>(),
                TurtleDatabaseModel = GetModel<TurtleDatabaseModel>()
            });
            AddController(new InputController() { MouseRayModel = GetModel<MouseInputModel>() });
            AddController(new TurtleController()
            {
                TurtleModels = GetModelCollection<TurtleModel>(),
                MouseInputModel = GetModel<MouseInputModel>()
            });
            AddController(new SeatController()
            {
                SeatModels = GetModelCollection<SeatModel>(),
                MouseInputModel = GetModel<MouseInputModel>()
            });
            AddController(new TurtleActionController()
            {
                ActionModel = GetModel<TurtleActionModel>(),
                SeatModels = GetModelCollection<SeatModel>(),
                TurtleModels = GetModelCollection<TurtleModel>()
            });
            AddController(new UIController()
            {
                UIModel = GetModel<TurtleTimeUIModel>(),
                TurtleModels = GetModelCollection<TurtleModel>()
            });
        }

        public override void Unload()
        {
            // TODO
        }
    }
}
