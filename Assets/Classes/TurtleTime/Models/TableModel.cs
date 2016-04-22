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
        public override string SpriteReferenceTag { get { return "table"; } }

        public override void LoadFromJson(IJsonObject jsonNode)
        {
            Position = new Vector2(jsonNode["position"][0].AsInt, jsonNode["position"][1].AsInt);
            Width = 4;
            Height = 4;
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
        }
    }
}
