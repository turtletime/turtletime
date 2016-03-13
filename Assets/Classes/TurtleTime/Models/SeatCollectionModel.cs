using UnityEngine;
using CheloniiUnity;
using System.Collections.Generic;

namespace TurtleTime.Models
{
    class SeatCollectionModel : Model
    {
        public List<SeatModel> Seats { get; set; }
        Dictionary<TableModel, List<SeatModel>> tableSeats = new Dictionary<TableModel, List<SeatModel>>();

        public SeatCollectionModel(List<TableModel> tableModels)
        {
            Seats = new List<SeatModel>();
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
    }
}
