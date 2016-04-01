using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityMVC;
using TurtleTime.Models;
using TurtleTime.Utils;

namespace TurtleTime.Views
{
    class MouseInputView : View3D<MouseInputModel>
    {
        public override string NodeName { get { return "Mouse Input (Debug)"; } }

        protected override void Load()
        {
            UnityUtils.CreateMesh(gameObject, "mouseRay", "mouseRay", "Default-Diffuse");
        }

        protected override void UpdateView()
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
