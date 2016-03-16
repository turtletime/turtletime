using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CheloniiUnity;

namespace TurtleTime.Models
{
    class SeatModel : Model
    {
        public bool Taken { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
    }
}
