using System;
using UnityMVC;

namespace TurtleTime
{
    /// <summary>
    /// Populates the cafe with chairs and tables.
    /// Runs once.
    /// </summary>
    class CafeObjectPopulationController : Controller
    {
        public RoomModel Room { get; set; }
        public QueueModel Queue { get; set; }
        public ModelCollection<SeatModel> Seats { get; set; }
        public ModelCollection<TableModel> Tables { get; set; }

        bool initialized = false;

        public override void Update(float deltaTime)
        {
            if (!initialized)
            {
                foreach (var table in Tables)
                {
                    Seats.AddRange(table.Seats);
                }
                Seats.AddRange(Queue.Seats);
                initialized = true;
            }
        }
    }
}
