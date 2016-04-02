﻿using System;
using UnityMVC;
using TurtleTime.Models;
using System.IO;
using System.Collections.Generic;

namespace TurtleTime.Models
{
    class TurtleDatabaseModel : Model
    {
        public Dictionary<String, TurtleDataModel> TurtleData { get; private set; }

        public TurtleDatabaseModel()
        {
            TurtleData = new Dictionary<string, TurtleDataModel>();
        }

        public override void LoadFromJson(IJsonObject jsonNode)
        {
            foreach (IJsonObject child in jsonNode.Children)
            {
                TurtleDataModel turtle = new TurtleDataModel();
                turtle.LoadFromJson(child);
                TurtleData.Add(turtle.Name, turtle);
            }
        }
    }
}
