using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TurtleTime.Models;
using CheloniiUnity;

namespace TurtleTime.Controllers
{
    class InputController : Controller
    {
        public MouseRayModel MouseRayModel { get; set; }

        public override bool IsActive()
        {
            return true;
        }

        public override void Update(float deltaTime)
        {
            Camera currentCamera = Camera.current;
            if (currentCamera != null)
            {
                MouseRayModel.MouseRay = currentCamera.ViewportPointToRay(currentCamera.ScreenToViewportPoint(Input.mousePosition));
                MouseRayModel.MouseRay = new Ray(UnityUtils.GLOBAL_ROTATION * MouseRayModel.MouseRay.origin,
                    UnityUtils.GLOBAL_ROTATION * MouseRayModel.MouseRay.direction);
            }
        }
    }
}
