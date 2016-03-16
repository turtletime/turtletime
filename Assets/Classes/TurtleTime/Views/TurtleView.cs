﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using UnityEngine;
using TurtleTime.Models;

namespace TurtleTime.Views
{
    class TurtleView : View<TurtleModel>
    {
        protected override void Load()
        {
            this.name = "Turtle";
            UnityUtils.CreateMesh(gameObject, Model.StaticData.Name, "turtle", "cafe_floor_material");
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }

        public override void Update()
        {
            gameObject.transform.position = TurtleUtils.ToWorldCoordinates(Model.Position);
        }
    }
}
