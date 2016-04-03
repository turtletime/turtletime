using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;

namespace TurtleTime
{
    class TurtleDataModel : Model
    {
        public String Name { get; private set; }
        public String FriendlyName { get; private set; }

        public override void LoadFromJson(IJsonObject jsonNode)
        {
            Name = jsonNode["name"].AsString;
            FriendlyName = jsonNode["friendlyName"].AsString;
        }
    }
}
