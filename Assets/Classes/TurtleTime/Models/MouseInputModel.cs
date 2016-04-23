using System;
using UnityMVC;
using UnityMVC.UI;
using UnityEngine;

namespace TurtleTime
{
    class MouseInputModel : Model
    {
        public Ray WorldSpaceRay { get; set; }
        public Vector2 ScreenSpacePoint { get; set; }

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

        public bool Intersects(WorldObjectModel physicalModel)
        {
            Plane p = new Plane(Vector3.up, 0);
            float enter;
            bool intersects = p.Raycast(WorldSpaceRay, out enter);
            if (intersects)
            {
                Vector3 entryPoint = TurtleUtils.WorldSpacePointToCafeCoordinates(WorldSpaceRay.GetPoint(enter));
                intersects = intersects && Math.Abs(entryPoint.x - physicalModel.Position.x) < physicalModel.Width / 2f;
                intersects = intersects && Math.Abs(entryPoint.y - physicalModel.Position.y) < physicalModel.Height / 2f;
            }
            return intersects;
        }

        public bool Intersects(AbstractUIModel uiModel)
        {
            return uiModel.Rectangle.Contains(ScreenSpacePoint);
        }
    }
}
