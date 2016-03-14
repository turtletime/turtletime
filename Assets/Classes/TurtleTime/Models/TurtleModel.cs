using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using SimpleJSON;
using UnityEngine;

namespace TurtleTime.Models
{
    class TurtleModel : Model
    {
        private SeatModel targetSeat;

        public TurtleDataModel StaticData { get; set; }
        public SeatModel PreviousSeat { get; private set; }
        public float ProgressToTargetSeat { get; set; }

        public SeatModel TargetSeat
        {
            get
            {
                return targetSeat;
            }
            set
            {
                PreviousSeat = targetSeat;
                targetSeat = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                if (TargetSeat == null)
                {
                    return Vector2.zero;
                }
                else if (PreviousSeat == null)
                {
                    return TargetSeat.Position;
                }
                else
                {
                    return Vector2.Lerp(PreviousSeat.Position, TargetSeat.Position, ProgressToTargetSeat);
                }
            }
        }
    }
}
