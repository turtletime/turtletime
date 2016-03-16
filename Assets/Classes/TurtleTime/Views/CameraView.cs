using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using TurtleTime.Models;
using UnityEngine;

namespace TurtleTime.Views
{
    class CameraView : View<CameraModel>
    {
        protected override void Load()
        {
            this.name = "Camera";
            UnityUtils.CreateCamera(this.gameObject);
        }

        public override void Update()
        {
            transform.position = Model.Position;
            transform.rotation = Model.Rotation;
            transform.localScale = Model.Scale;
            GetComponent<Camera>().orthographic = Model.Projection == CameraModel.CameraProjection.ORTHOGRAPHIC;
            GetComponent<Camera>().fieldOfView = Model.FieldOfView;
        }
    }
}
