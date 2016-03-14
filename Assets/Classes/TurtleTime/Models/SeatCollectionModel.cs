using UnityEngine;
using CheloniiUnity;
using System.Collections.Generic;

namespace TurtleTime.Models
{
    class SeatCollectionModel : Model
    {
        public List<SeatModel> Seats { get; set; }
        Dictionary<TableModel, List<SeatModel>> tableSeats = new Dictionary<TableModel, List<SeatModel>>();
        List<SeatModel> queueSeats = new List<SeatModel>();

        public SeatCollectionModel(QueueModel queueModel, List<TableModel> tableModels)
        {
            Seats = new List<SeatModel>();
            for (int i = 0; i < queueModel.NumSeats; i++)
            {
                SeatModel seatModel = new SeatModel() { Position = queueModel.Position + i * queueModel.FacingDirection, Direction = queueModel.SeatExtensionDirection };
                queueSeats.Add(seatModel);
            }
            Seats.AddRange(queueSeats);
            foreach (TableModel tableModel in tableModels)
            {
                List<SeatModel> thisTableSeats = new List<SeatModel>();
                thisTableSeats.Add(new SeatModel() { Position = tableModel.Position + new Vector2(-1, 0), Direction = new Vector2(1, 0) });
                thisTableSeats.Add(new SeatModel() { Position = tableModel.Position + new Vector2(0, 1), Direction = new Vector2(0, -1) });
                thisTableSeats.Add(new SeatModel() { Position = tableModel.Position + new Vector2(1, 0), Direction = new Vector2(-1, 0) });
                thisTableSeats.Add(new SeatModel() { Position = tableModel.Position + new Vector2(0, -1), Direction = new Vector2(0, 1) });
                Seats.AddRange(thisTableSeats);
                tableSeats.Add(tableModel, thisTableSeats);
            }
        }

        public SeatModel GetFreeQueueSeat()
        {
            foreach (SeatModel s in queueSeats)
            {
                if (s.Turtle == null)
                {
                    return s;
                }
            }
            return null;
        }

        public SeatModel GetFreeTableSeat(TableModel tableModel)
        {
            foreach (SeatModel s in tableSeats[tableModel])
            {
                if (s.Turtle != null)
                {
                    return s;
                }
            }
            return null;
        }
    }
}
