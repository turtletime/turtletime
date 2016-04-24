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
            
            AddModel<CameraModel, CameraView>(ReadOnlyData.JsonData["cafe"]["camera"]);
            AddModel<MouseInputModel, MouseInputView>();
            AddModel<RoomModel, RoomView>(ReadOnlyData.JsonData["cafe"]["room"]);
            // turtles
            AddModelCollection<TurtleModel, TurtleModel.View>();
            // queues and tables
            AddModelCollection<TableModel, TableModel.View>(ReadOnlyData.JsonData["cafe"]["tables"]);
            AddModel<QueueModel>(ReadOnlyData.JsonData["cafe"]["queueSeats"]);
            // seats
            AddModelCollection<SeatModel, SeatModel.View>();
            AddModel<TurtleActionModel>();
            // ui stuff
            AddModel<TurtleTimeUIModel, UIView>(ReadOnlyData.JsonData["ui"]["turtleProfile"]);

            /* Controllers */

            AddController(new CafeObjectPopulationController()
            {
                Room = GetModel<RoomModel>(),
                Queue = GetModel<QueueModel>(),
                Tables = GetModelCollection<TableModel>(),
                Seats = GetModelCollection<SeatModel>()
            });
            AddController(new CameraController() { CameraModel = GetModel<CameraModel>() });
            AddController(new TurtleSpawnController()
            {
                Turtles = GetModelCollection<TurtleModel>(),
                Queue = GetModel<QueueModel>()
            });
            AddController(new InputController() { MouseRayModel = GetModel<MouseInputModel>() });
            AddController(new TurtleController()
            {
                RoomModel = GetModel<RoomModel>(),
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
