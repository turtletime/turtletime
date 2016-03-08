using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using TurtleTime.Models;
using UnityEngine;

namespace TurtleTime.Views
{
    class CameraView : View<CafeModule>
    {
        private CameraModel cameraModel;
        public override ViewType GameObjectType { get { return ViewType.WORLD; } }

        protected override string Name { get { return "Camera"; } }

        public CameraView(CameraModel cameraModel)
        {
            this.cameraModel = cameraModel;
        }

        public override bool IsActive()
        {
            return true;
        }

        public override void Load()
        {
            UnityUtils.CreateCamera(this.gameObject);
        }

        public override void Unload()
        {
            
        }

        public override void Update(float dt)
        {
            gameObject.transform.position = cameraModel.Position;
            gameObject.transform.rotation = cameraModel.Rotation;
            gameObject.transform.localScale = cameraModel.Scale;
            gameObject.GetComponent<Camera>().orthographic = cameraModel.Projection == CameraModel.CameraProjection.ORTHOGRAPHIC;
            gameObject.GetComponent<Camera>().fieldOfView = cameraModel.FieldOfView;
        }
    }
}
