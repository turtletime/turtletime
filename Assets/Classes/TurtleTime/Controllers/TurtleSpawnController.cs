using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using TurtleTime.Models;
using TurtleTime.Views;

namespace TurtleTime.Controllers
{
    class TurtleSpawnController : Controller<CafeModule>
    {
        public static int SPAWN_INTERVAL_SECONDS = 2;

        float timeSinceLastSpawn = 0;

        public override bool IsActive()
        {
            return true;
        }

        public override void Update(float deltaTime)
        {
            timeSinceLastSpawn += deltaTime;
            if (timeSinceLastSpawn > SPAWN_INTERVAL_SECONDS)
            {
                timeSinceLastSpawn -= SPAWN_INTERVAL_SECONDS;
                SeatModel assignedSeat = GameModule.SeatsModel.GetFreeQueueSeat();
                if (assignedSeat != null)
                {
                    // Spawn turtle
                    TurtleModel newTurtle = new TurtleModel() { StaticData = GameModule.DatabaseModule.TurtleData["turtle_1_no_materials"] };
                    GameModule.TurtleModels.Add(newTurtle);
                    GameModule.AddView(new TurtleView(newTurtle));
                    GameModule.AssignTurtleToSeat(newTurtle, assignedSeat);
                }
            }
        }
    }
}
