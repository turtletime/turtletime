using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CheloniiUnity;
using TurtleTime.Models;

namespace TurtleTime.Views
{
    class MouseRayView : View<MouseRayModel>
    {
        GameObject a;
        GameObject b;

        protected override void Load()
        {
            this.name = "Mouse Ray";
            a = new GameObject();
            b = new GameObject();
            a.transform.SetParent(gameObject.transform);
            b.transform.SetParent(gameObject.transform);
        }

        public override void Update()
        {
            a.transform.position = Model.MouseRay.origin;
            b.transform.position = Model.MouseRay.origin + Model.MouseRay.direction;
        }
    }
}
