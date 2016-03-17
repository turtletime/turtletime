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
        public MouseInputModel MouseRayModel { get; set; }

        public override bool IsActive()
        {
            return true;
        }

        public override void Update(float deltaTime)
        {
            TurtleModel selectedModel = null;
            foreach (TurtleModel model in TurtleModels)
            {
                if (model.TargetSeat == null)
                {
                    model.Position = Vector2.zero;
                }
                else
                {
                    model.Position = Vector2.Lerp(model.Position, model.TargetSeat.Position, model.ProgressToTargetSeat);
                }
                model.ProgressToTargetSeat += 0.1f; // TODO
                if (model.ProgressToTargetSeat > 1)
                {
                    model.ProgressToTargetSeat = 1;
                }
                // Selected?
                if (MouseRayModel.JustClicked && MouseRayModel.Intersects(model))
                {
                    selectedModel = model;
                }
            }
            if (selectedModel != null)
            {
                foreach (TurtleModel model in TurtleModels)
                {
                    if (!selectedModel.Equals(model))
                    {
                        model.Selected = false;
                    }
                }
                selectedModel.Selected = !selectedModel.Selected;
            }
        }
    }
}
