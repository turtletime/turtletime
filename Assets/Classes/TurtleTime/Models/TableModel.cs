using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityMVC;

namespace TurtleTime
{
    class TableModel : WorldObjectModel
    {
        public List<SeatModel> Seats { get; private set; }

        public override string SpriteReferenceTag { get { return "table"; } }

        public TableModel()
        {
            Seats = new List<SeatModel>();
        }

        public override void LoadFromJson(IJsonObject jsonNode)
        {
            base.LoadFromJson(jsonNode);
            Position = new Vector2(jsonNode["position"][0].AsInt, jsonNode["position"][1].AsInt);
            StaticData = ObjectDatabaseModel.Instance[jsonNode["id"].AsString];
            foreach (var seatJSON in jsonNode["seats"].AsList)
            {
                Vector2 position = new Vector2(seatJSON["position"][0].AsInt, seatJSON["position"][1].AsInt);
                Vector2 direction = new Vector2(seatJSON["direction"][0].AsInt, seatJSON["direction"][1].AsInt);
                Seats.Add(new SeatModel()
                {
                    Position = this.Position + position,
                    Direction = direction,
                    StaticData = ObjectDatabaseModel.Instance[seatJSON["seatID"].AsString]
                });
            }
        }

        public class View : BillboardSpriteView<TableModel>
        {
            protected override string CurrentAnimation
            {
                get
                {
                    return "default";
                }
            }

            protected override int SortOrder { get { return Constants.SORT_LAYER_TABLE; } }
        }
    }
}
