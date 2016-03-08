using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CheloniiUnity;
using TurtleTime.Models;

namespace TurtleTime.Views
{
    class TableView : View<CafeModule>
    {
        private TableModel tableModel;

        public TableView(TableModel tableModel)
        {
            this.tableModel = tableModel;
        }

        public override ViewType GameObjectType { get { return ViewType.WORLD; } }

        protected override string Name { get { return "Table"; } }

        public override bool IsActive() { return true; }

        public override void Load()
        {
            UnityUtils.CreateMesh(gameObject, "table", "table", "Default-Diffuse");
            gameObject.GetComponent<Renderer>().material.color = Color.black;
        }

        public override void Unload()
        {
            
        }

        public override void Update(float dt)
        {
            gameObject.transform.position = new Vector3(tableModel.Position.x, 0, tableModel.Position.y);
        }
    }
}
