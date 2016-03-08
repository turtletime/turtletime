using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using UnityEngine;

namespace TurtleTime.Controllers
{
    class CameraController : Controller<CafeModule>
    {
        Vector2 mousePrev = new Vector2();

        public CameraController()
        {
            mousePrev = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        public override bool IsActive()
        {
            return true;
        }

        public override void Update(float deltaTime)
        {
            Vector2 dp = Vector2.zero;
            Vector2 mouseCurr = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (Input.GetMouseButton(0))
            {
                dp += mouseCurr - mousePrev;
            }
            mousePrev = mouseCurr;
            if (Input.touchCount > 0)
            {
                dp += Input.GetTouch(0).deltaPosition;
            }
            GameModule.CameraModel.Position += new Vector3(0.01f * dp.x, 0, 0);
        }
    }
}
