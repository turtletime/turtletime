using System;
using UnityMVC;

namespace TurtleTime
{
    class InitializationController : Controller
    {
        public RoomModel Room { get; set; }
        public SeatCollectionModel SeatCollection { get; set; }
        public QueueModel Queue { get; set; }
        public ModelCollection<SeatModel> Seats { get; set; }
        public ModelCollection<TableModel> Tables { get; set; }
        public ModelCollection<TurtleModel> Turtles { get; set; }

        bool initialized = false;

        public override void Update(float deltaTime)
        {
            if (!initialized)
            {

                initialized = true;
            }
        }
    }
}
