using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using TurtleTime.Models;

namespace TurtleTime.Controllers
{
    class TurtleActionController : Controller
    {
        public TurtleActionModel ActionModel { get; set; }
        public ModelCollection<TurtleModel> TurtleModels { get; set; }
        public ModelCollection<SeatModel> SeatModels { get; set; }

        public override void Update(float deltaTime)
        {
            ActionModel.Subject = null;
            foreach (TurtleModel turtle in TurtleModels)
            {
                if (turtle.Selected)
                {
                    ActionModel.Subject = turtle;
                    break;
                }
            }
            if (ActionModel.Subject != null)
            {
                ActionModel.Target = null;
                foreach (SeatModel seat in SeatModels)
                {
                    if (seat.Selected && !seat.Position.Equals(ActionModel.Subject.Position))
                    {
                        ActionModel.Target = seat;
                        break;
                    }
                }
                if (ActionModel.Target != null)
                {
                    // Conditions met for turtle move
                    ActionModel.Subject.Selected = false;
                    ActionModel.Target.Selected = false;
                    TurtleModel.AssignTurtleToSeat(ActionModel.Subject, ActionModel.Target);
                    ActionModel.Reset();
                }
            }
        }
    }
}
