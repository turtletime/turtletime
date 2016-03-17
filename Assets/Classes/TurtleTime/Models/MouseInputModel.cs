using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using UnityEngine;
using TurtleTime.Views;

namespace TurtleTime.Models
{
    class MouseInputModel : Model
    {
        public Ray WorldSpaceRay { get; set; }

        public bool JustClicked
        {
            get
            {
                return clickedThisFrame && !clickedLastFrame;
            }
        }

        public bool IsClicked
        {
            get
            {
                return clickedThisFrame;
            }
            set
            {
                clickedLastFrame = clickedThisFrame;
                clickedThisFrame = value;
            }
        }
        
        private bool clickedThisFrame;
        private bool clickedLastFrame;

        public bool Intersects(IPhysicalModel physicalModel)
        {
            Plane p = new Plane(Vector3.up, 0);
            float enter;
            bool intersects = p.Raycast(WorldSpaceRay, out enter);
            if (intersects)
            {
                Vector3 entryPoint = TurtleUtils.WorldSpacePointToCafeCoordinates(WorldSpaceRay.GetPoint(enter));
                intersects = intersects && Math.Abs(entryPoint.x - physicalModel.Position.x) < 0.5f;
                intersects = intersects && Math.Abs(entryPoint.y - physicalModel.Position.y) < 0.5f;
            }
            return intersects;
        }
    }
}
