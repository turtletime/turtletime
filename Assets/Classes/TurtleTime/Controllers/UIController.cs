using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;
using TurtleTime.Models;

namespace TurtleTime.Controllers
{
    class UIController : Controller
    {
        public UIModel UIModel { get; set; }
        public ModelCollection<TurtleModel> TurtleModels { get; set; }

        public override void Update(float deltaTime)
        {
            UIModel.CurrentTurtleModel = null;
            foreach (TurtleModel turtle in TurtleModels)
            {
                if (turtle.Selected)
                {
                    UIModel.CurrentTurtleModel = turtle;
                    break;
                }
            }
        }
    }
}
