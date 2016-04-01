using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;
using SimpleJSON;

namespace TurtleTime.Models
{
    class TurtleDataModel : Model
    {
        public String Name { get; private set; }
        public String FriendlyName { get; private set; }

        public override void LoadFromJson(JSONNode jsonNode)
        {
            Name = jsonNode["name"];
            FriendlyName = jsonNode["friendlyName"];
        }
    }
}
