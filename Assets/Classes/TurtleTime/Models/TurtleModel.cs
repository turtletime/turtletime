using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using SimpleJSON;
using UnityEngine;

namespace TurtleTime.Models
{
    class TurtleModel : Model
    {
        public TurtleDataModel StaticData { get; set; }
        public Vector2 Position { get; set; }
    }
}
