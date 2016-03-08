﻿using System;
using CheloniiUnity;
using TurtleTime.Models;
using System.IO;
using System.Collections.Generic;
using SimpleJSON;

namespace TurtleTime
{
    class TurtleDatabaseModule : GameModule
    {
        public Dictionary<String, TurtleDataModel> TurtleData { get; private set; }

        public override void Load()
        {
            if (TurtleData == null)
            {
                TurtleData = new Dictionary<string, TurtleDataModel>();
            }
            using (TextReader tr = new StreamReader("Assets/Resources/Text/turtles.json"))
            {
                String input = tr.ReadToEnd();
                foreach (JSONNode child in JSON.Parse(input)["turtles"].Childs)
                {
                    TurtleDataModel turtle = new TurtleDataModel();
                    turtle.LoadFromJson(child);
                    TurtleData.Add(turtle.Name, turtle);
                }
            }
        }

        public override void Unload()
        {
            TurtleData.Clear();
        }
    }
}