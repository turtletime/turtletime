using UnityEngine;
using UnityMVC;
using System;
using TurtleTime.Models;
using TurtleTime.Utils;

namespace TurtleTime.Views
{
    class SeatView : BillboardSpriteView<SeatModel>
    {
        Material material;
        float time;

        public override string NodeName { get { return "Seat"; } }

        protected override string SpriteName { get { return "test"; } }
    }
}
