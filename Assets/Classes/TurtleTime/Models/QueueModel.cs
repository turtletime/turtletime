using UnityMVC;
using UnityEngine;

namespace TurtleTime
{
    class QueueModel : Model
    {
        public Vector2 Position { get; set; }
        public Vector2 SeatExtensionDirection { get; set; }
        public Vector2 FacingDirection { get; set; }
        public int NumSeats { get; set; }

        public override void LoadFromJson(IJsonObject jsonNode)
        {
            Position = new Vector2(jsonNode["position"][0].AsInt, jsonNode["position"][1].AsInt);
            SeatExtensionDirection = new Vector2(jsonNode["direction"][0].AsInt, jsonNode["direction"][1].AsInt);
            FacingDirection = new Vector2(jsonNode["facing"][0].AsInt, jsonNode["facing"][1].AsInt);
            NumSeats = jsonNode["numSeats"].AsInt;
        }
    }
}
