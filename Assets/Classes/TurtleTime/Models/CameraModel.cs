using CheloniiUnity;
using SimpleJSON;
using UnityEngine;

namespace TurtleTime.Models
{
    class CameraModel : Model
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public CameraProjection Projection { get; set; }
        public float FieldOfView { get; set; }

        public override void LoadFromJson(JSONNode jsonNode)
        {
            Position = ToVector3(jsonNode["position"]);
            Rotation = Quaternion.Euler(ToVector3(jsonNode["rotation"]));
            Scale = ToVector3(jsonNode["scale"]);
            Projection = jsonNode["projection"].Value.Equals("perspective") ? CameraProjection.PERSPECTIVE : CameraProjection.ORTHOGRAPHIC;
            FieldOfView = jsonNode["fieldOfView"] != null ? jsonNode["fieldOfView"].AsFloat : 0;
        }

        private static Vector3 ToVector3(JSONNode node)
        {
            return new Vector3(node[0].AsFloat, node[1].AsFloat, node[2].AsFloat);
        }

        public enum CameraProjection
        {
            PERSPECTIVE, ORTHOGRAPHIC
        }
    }
}
