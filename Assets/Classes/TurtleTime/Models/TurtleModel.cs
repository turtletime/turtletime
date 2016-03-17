using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using SimpleJSON;
using UnityEngine;

namespace TurtleTime.Models
{
    class TurtleModel : Model, IPhysicalModel
    {
        private SeatModel targetSeat;

        public TurtleDataModel StaticData { get; set; }
        public float ProgressToTargetSeat { get; set; }
        public bool Selected { get; set; }
        public Vector2 Position { get; set; }

        public SeatModel TargetSeat
        {
            get
            {
                return targetSeat;
            }
            set
            {
                ProgressToTargetSeat = 0;
                targetSeat = value;
            }
        }

        public static void AssignTurtleToSeat(TurtleModel turtleModel, SeatModel seatModel)
        {
            if (turtleModel != null && seatModel != null)
            {
                SeatModel oldSeat = turtleModel.TargetSeat;
                if (oldSeat != null)
                {
                    oldSeat.Taken = false;
                }
                turtleModel.TargetSeat = seatModel;
                seatModel.Taken = true;
            }
        }
    }
}
