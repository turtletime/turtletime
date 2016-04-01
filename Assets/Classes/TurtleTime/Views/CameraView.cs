using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;
using TurtleTime.Models;
using TurtleTime.Utils;
using UnityEngine;

namespace TurtleTime.Views
{
    class CameraView : View3D<CameraModel>
    {
        public override string NodeName { get { return "Camera"; } }

        protected override void Load()
        {
            UnityUtils.CreateCamera(this.gameObject);
        }

        protected override void UpdateView()
        {
            transform.position = Model.Position;
            transform.rotation = Model.Rotation;
            transform.localScale = Model.Scale;
            GetComponent<Camera>().orthographic = Model.Projection == CameraModel.CameraProjection.ORTHOGRAPHIC;
            GetComponent<Camera>().fieldOfView = Model.FieldOfView;
        }
    }
}
