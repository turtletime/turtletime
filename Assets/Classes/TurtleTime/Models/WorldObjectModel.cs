using System;
using UnityEngine;
using UnityMVC;

namespace TurtleTime
{
    abstract class WorldObjectModel : Model, IPhysicalModel, ISpriteModel
    {
        public static readonly Vector2 LEFT = new Vector2(-1, 0);
        public static readonly Vector2 RIGHT = new Vector2(1, 0);
        public static readonly Vector2 UP = new Vector2(0, -1);
        public static readonly Vector2 DOWN = new Vector2(0, 1);

        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public ObjectDataModel StaticData { get; set; }

        public int Width
        {
            get
            {
                if (Direction.Equals(LEFT) || Direction.Equals(RIGHT))
                {
                    return StaticData.Height;
                }
                return StaticData.Width;
            }
        }
        public int Height
        {
            get
            {
                if (Direction.Equals(LEFT) || Direction.Equals(RIGHT))
                {
                    return StaticData.Width;
                }
                return StaticData.Height;
            }
        }

        public abstract string SpriteReferenceTag { get; }
    }
}
