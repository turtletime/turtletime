using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CheloniiUnity;
using TurtleTime.Models;

namespace TurtleTime.Views
{
    class TableView : View<TableModel>
    {
        protected override void Load()
        {
            this.name = "Table";
            UnityUtils.CreateMesh(gameObject, "table", "table", "Default-Diffuse");
            gameObject.GetComponent<Renderer>().material.color = Color.black;
        }

        public override void Update()
        {
            gameObject.transform.position = TurtleUtils.ToWorldCoordinates(Model.Position);
        }
    }
}
