using System;
using UnityEngine;
using UnityMVC;

namespace TurtleTime
{
    abstract class WorldObjectModel : Model, IPhysicalModel, ISpriteModel
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }

        public abstract string SpriteReferenceTag { get; }
    }
}
