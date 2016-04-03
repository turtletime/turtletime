using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TurtleTime.Models
{
    interface IPhysicalModel
    {
        Vector2 Position { get; set; }
        Vector2 Direction { get; set; }
    }
}
