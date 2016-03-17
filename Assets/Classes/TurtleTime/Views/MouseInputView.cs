using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CheloniiUnity;
using TurtleTime.Models;

namespace TurtleTime.Views
{
    class MouseInputView : View3D<MouseInputModel>
    {
        protected override void Load()
        {
            this.name = "Mouse Ray";
            UnityUtils.CreateMesh(gameObject, "mouseRay", "mouseRay", "Default-Diffuse");
        }

        public override void Update()
        {
            transform.position = Model.WorldSpaceRay.origin;
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, Model.WorldSpaceRay.direction);
            Plane p = new Plane(Vector3.up, 0);
            float enter;
            p.Raycast(Model.WorldSpaceRay, out enter);
            transform.localScale = new Vector3(1, 1, enter);
        }
    }
}
