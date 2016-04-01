using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityMVC;
using SimpleJSON;

namespace TurtleTime.Models
{
    class TableModel : Model
    {
        public Vector2 Position { get; set; }

        public override void LoadFromJson(JSONNode jsonNode)
        {
            Position = new Vector2(jsonNode["position"][0].AsInt, jsonNode["position"][1].AsInt);
        }
    }
}
