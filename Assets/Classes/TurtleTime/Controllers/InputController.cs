using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityMVC;

namespace TurtleTime
{
    class InputController : Controller
    {
        public MouseInputModel MouseRayModel { get; set; }

        public override void Update(float deltaTime)
        {
            Camera currentCamera = Camera.current;
            if (currentCamera != null)
            {
                MouseRayModel.WorldSpaceRay = currentCamera.ViewportPointToRay(currentCamera.ScreenToViewportPoint(Input.mousePosition));
                MouseRayModel.WorldSpaceRay = new Ray(MouseRayModel.WorldSpaceRay.origin,
                    MouseRayModel.WorldSpaceRay.direction);
                MouseRayModel.ScreenSpacePoint = currentCamera.ScreenToViewportPoint(Input.mousePosition);
            }
            MouseRayModel.IsClicked = Input.GetMouseButton(0);
        }
    }
}
