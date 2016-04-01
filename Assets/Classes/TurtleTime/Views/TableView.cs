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
    class TableView : View3D<TableModel>
    {
        public override string NodeName { get { return "Table"; } }

        protected override void Load()
        {
            UnityUtils.CreateMesh(gameObject, "table", "table", "Default-Diffuse");
            gameObject.GetComponent<Renderer>().material.color = Color.black;
        }

        protected override void UpdateView()
        {
            gameObject.transform.position = TurtleUtils.CafeSpaceToWorldCoordinates(Model.Position);
        }
    }
}
