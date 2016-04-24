using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;
using UnityEngine;

namespace TurtleTime
{
    class TurtleController : Controller
    {
        public RoomModel RoomModel { get; set; }
        public ModelCollection<TurtleModel> TurtleModels { get; set; }
        public MouseInputModel MouseInputModel { get; set; }

        public override void Update(float deltaTime)
        {
            TurtleModel clickedTurtle = null;
            foreach (TurtleModel turtle in TurtleModels)
            {
                if (turtle.TargetSeat == null)
                {
                    turtle.Position = Vector2.zero;
                }
                else
                {
                    turtle.Position = Vector2.MoveTowards(turtle.Position, turtle.TargetPosition, (turtle.StaticData as TurtleDataModel).Speed);
                    if (turtle.TargetPosition != turtle.TargetSeat.Position && turtle.Position == turtle.TargetPosition)
                    {
                        turtle.TargetPosition = RoomModel.Pathfind(turtle, turtle.TargetSeat);
                        turtle.Direction = (turtle.TargetPosition - turtle.Position).AxisAlign();
                    }
                    else if (turtle.TargetPosition == turtle.TargetSeat.Position)
                    {
                        turtle.Direction = turtle.TargetSeat.Direction;
                    }
                }
                // Clicked?
                if (MouseInputModel.JustClicked && MouseInputModel.Intersects(turtle))
                {
                    clickedTurtle = turtle;
                }
            }
            if (clickedTurtle != null)
            {
                foreach (TurtleModel model in TurtleModels)
                {
                    if (!clickedTurtle.Equals(model))
                    {
                        model.Selected = false;
                    }
                }
                clickedTurtle.Selected = !clickedTurtle.Selected;
            }
        }
    }
}
