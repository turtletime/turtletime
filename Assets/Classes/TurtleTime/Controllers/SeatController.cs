﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;
using TurtleTime.Models;
using UnityEngine;

namespace TurtleTime.Controllers
{
    class SeatController : Controller
    {
        public ModelCollection<SeatModel> SeatModels { get; set; }
        public MouseInputModel MouseInputModel { get; set; }

        public override void Update(float deltaTime)
        {
            SeatModel clickedSeat = null;
            foreach (SeatModel seat in SeatModels)
            {
                // Clicked?
                if (MouseInputModel.JustClicked && MouseInputModel.Intersects(seat))
                {
                    clickedSeat = seat;
                }
            }
            if (clickedSeat != null)
            {
                foreach (SeatModel seat in SeatModels)
                {
                    if (!clickedSeat.Equals(seat))
                    {
                        seat.Selected = false;
                    }
                }
                clickedSeat.Selected = !clickedSeat.Selected;
            }
        }
    }
}