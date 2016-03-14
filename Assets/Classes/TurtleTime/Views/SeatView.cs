using UnityEngine;
using CheloniiUnity;
using System;
using TurtleTime.Models;

namespace TurtleTime.Views
{
    class SeatView : View<CafeModule>
    {
        public override ViewType GameObjectType { get { return ViewType.WORLD; } }

        protected override string Name { get { return "Seat"; } }

        SeatModel model;
        Material material;
        float time;

        public SeatView(SeatModel model)
        {
            this.model = model;
        }

        public override bool IsActive()
        {
            return GameModule.OptionsModule.Options.debugMode;
        }

        public override void Load()
        {
            UnityUtils.CreateMesh(gameObject, "seat", "base", "Default-Diffuse");
            this.gameObject.transform.position = TurtleUtils.ToWorldCoordinates(model.Position, 0.01f);
            this.gameObject.transform.Rotate(Vector3.forward, Mathf.Rad2Deg * Mathf.Atan2(-model.Direction.y, model.Direction.x));
            material = this.gameObject.GetComponent<MeshRenderer>().material;
        }

        public override void Unload()
        {
            
        }

        public override void Update(float dt)
        {
            time += dt;
            float t = (Mathf.Sin(time * 4) + 1) / 2;
            material.color = new Color(1, t, t, 1);
        }
    }
}
