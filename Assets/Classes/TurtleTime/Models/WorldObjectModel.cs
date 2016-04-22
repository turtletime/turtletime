using System;
using UnityEngine;
using UnityMVC;

namespace TurtleTime
{
    abstract class WorldObjectModel : Model, IPhysicalModel, ISpriteModel
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public abstract string SpriteReferenceTag { get; }
    }
}
