using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;

namespace TurtleTime
{
    class TurtleSpawnController : Controller
    {
        public ModelCollection<TurtleModel> Turtles { get; set; }
        public QueueModel Queue { get; set; }

        public static int SPAWN_INTERVAL_SECONDS = 2;

        float timeSinceLastSpawn = 0;

        public override void Update(float deltaTime)
        {
            timeSinceLastSpawn += deltaTime;
            if (timeSinceLastSpawn > SPAWN_INTERVAL_SECONDS)
            {
                timeSinceLastSpawn -= SPAWN_INTERVAL_SECONDS;
                SeatModel assignedSeat = GetFreeQueueSeat();
                if (assignedSeat != null)
                {
                    // Spawn turtle
                    TurtleModel newTurtle = new TurtleModel() { StaticData = ObjectDatabaseModel.Instance["turtle_1_no_materials"] };
                    Turtles.Add(newTurtle);
                    TurtleModel.AssignTurtleToSeat(newTurtle, assignedSeat);
                }
            }
        }

        private SeatModel GetFreeQueueSeat()
        {
            foreach (var seat in Queue.Seats)
            {
                if (!seat.Taken)
                {
                    return seat;
                }
            }
            return null;
        }
    }
}
