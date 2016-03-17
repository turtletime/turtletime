using UnityEngine;
using CheloniiUnity;
using System;
using TurtleTime.Models;

namespace TurtleTime.Views
{
    class SeatView : View3D<SeatModel>
    {
        Material material;
        float time;

        protected override void Load()
        {
            this.name = "Seat";
            UnityUtils.CreateMesh(gameObject, "seat", "base", "Default-Diffuse");
            this.gameObject.transform.position = TurtleUtils.CafeSpaceToWorldCoordinates(Model.Position, 0.01f);
            this.gameObject.transform.Rotate(Vector3.forward, Mathf.Rad2Deg * Mathf.Atan2(-Model.Direction.y, Model.Direction.x));
            material = this.gameObject.GetComponent<MeshRenderer>().material;
        }

        public override void Update()
        {
            time += 0.1f;
            float t = (Mathf.Sin(time * 4) + 1) / 2;
            material.color = new Color(1, t, t, 1);
        }
    }
}
