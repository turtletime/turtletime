using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using UnityEngine;
using TurtleTime.Models;

namespace TurtleTime.Views
{
    class TurtleView : View<CafeModule>
    {
        private TurtleModel turtleModel;

        public TurtleView(TurtleModel turtleModel)
        {
            this.turtleModel = turtleModel;
        }

        public override ViewType GameObjectType { get { return ViewType.WORLD; } }

        protected override string Name { get { return "Turtle"; } }

        public override bool IsActive() { return true; }

        public override void Load()
        {
            UnityUtils.CreateMesh(gameObject, turtleModel.StaticData.Name, "turtle", "cafe_floor_material");
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }

        public override void Unload()
        {

        }

        public override void Update(float dt)
        {
            gameObject.transform.position = new Vector3(turtleModel.Position.x, 0, turtleModel.Position.y);
        }
    }
}
