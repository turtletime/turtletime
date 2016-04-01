using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;
using UnityEngine;
using TurtleTime.Models;
using TurtleTime.Utils;

namespace TurtleTime.Views
{
    class TurtleView : View3D<TurtleModel>
    {
        public override string NodeName { get { return "Camera"; } }

        protected override void Load()
        {
            UnityUtils.CreateMesh(gameObject, Model.StaticData.Name, "turtle", "cafe_floor_material");
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }

        protected override void UpdateView()
        {
            gameObject.transform.position = TurtleUtils.CafeSpaceToWorldCoordinates(Model.Position);
            gameObject.transform.localRotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(-Model.Direction.y, Model.Direction.x) - 90, Vector3.forward);
            if (Model.Selected)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}
