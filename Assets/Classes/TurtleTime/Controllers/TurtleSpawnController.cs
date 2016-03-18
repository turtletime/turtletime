using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using TurtleTime.Models;
using TurtleTime.Views;

namespace TurtleTime.Controllers
{
    class TurtleSpawnController : Controller
    {
        public ModelCollection<TurtleModel> TurtleModels { get; set; }
        public SeatCollectionModel SeatsModel { get; set; }
        public TurtleDatabaseModel TurtleDatabaseModel { get; set; }

        public static int SPAWN_INTERVAL_SECONDS = 2;

        float timeSinceLastSpawn = 0;

        public override void Update(float deltaTime)
        {
            timeSinceLastSpawn += deltaTime;
            if (timeSinceLastSpawn > SPAWN_INTERVAL_SECONDS)
            {
                timeSinceLastSpawn -= SPAWN_INTERVAL_SECONDS;
                SeatModel assignedSeat = SeatsModel.GetFreeQueueSeat();
                if (assignedSeat != null)
                {
                    // Spawn turtle
                    TurtleModel newTurtle = new TurtleModel() { StaticData = TurtleDatabaseModel.TurtleData["turtle_1_no_materials"] };
                    TurtleModels.Add(newTurtle);
                    TurtleModel.AssignTurtleToSeat(newTurtle, assignedSeat);
                }
            }
        }
    }
}
