using System.Collections.Generic;
using UnityMVC;
using UnityEngine;

namespace TurtleTime
{
    class QueueModel : Model
    {
        public List<SeatModel> Seats { get; private set; }

        public QueueModel()
        {
            Seats = new List<SeatModel>();
        }

        public override void LoadFromJson(IJsonObject jsonNode)
        {
            foreach (var seatJSON in jsonNode.AsList)
            {
                Vector2 position = new Vector2(seatJSON["position"][0].AsInt, seatJSON["position"][1].AsInt);
                Vector2 direction = new Vector2(seatJSON["direction"][0].AsInt, seatJSON["direction"][1].AsInt);
                Seats.Add(new SeatModel()
                {
                    Position = position,
                    Direction = direction,
                    StaticData = ObjectDatabaseModel.Instance[seatJSON["seatID"].AsString]
                });
            }
        }
    }
}
