using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;
using SimpleJSON;
using UnityEngine;

namespace TurtleTime
{
    class TurtleModel : WorldObjectModel
    {
        public override string SpriteReferenceTag { get { return "turtle"; } }

        private SeatModel targetSeat;

        public float ProgressToTargetSeat { get; set; }
        public bool Selected { get; set; }

        public SeatModel TargetSeat
        {
            get
            {
                return targetSeat;
            }
            protected set
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
                turtleModel.Direction = seatModel.Direction;
                seatModel.Taken = true;
            }
        }

        public class View : BillboardSpriteView<TurtleModel>
        {
            protected override void UpdateView()
            {
                spriteRenderer.color = Model.Selected ? Color.red : Color.white;
                base.UpdateView();
            }

            protected override string CurrentAnimation
            {
                get
                {
                    if (Model.Direction.x > 0)
                    {
                        return "idle_l";
                    }
                    else if (Model.Direction.x < 0)
                    {
                        return "idle_r";
                    }
                    else if (Model.Direction.y < 0)
                    {
                        return "idle_b";
                    }
                    else
                    {
                        return "idle_f";
                    }
                }
            }
        }
    }
}
