using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using TurtleTime.Models;
using UnityEngine;

namespace TurtleTime.Controllers
{
    class TurtleController : Controller
    {
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
                    turtle.Position = Vector2.Lerp(turtle.Position, turtle.TargetSeat.Position, turtle.ProgressToTargetSeat);
                }
                turtle.ProgressToTargetSeat += 0.1f; // TODO
                if (turtle.ProgressToTargetSeat > 1)
                {
                    turtle.ProgressToTargetSeat = 1;
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
