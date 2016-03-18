using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using SimpleJSON;
using UnityEngine;

namespace TurtleTime.Models
{
    class UIModel : Model
    {
        public TurtleModel CurrentTurtleModel { get; set; }
        public int EdgePaddingPixels { get; set; }

        public bool Active
        {
            get
            {
                return CurrentTurtleModel != null;
            }
        }

        public override void LoadFromJson(JSONNode jsonNode)
        {
            EdgePaddingPixels = jsonNode["edgePadding"].AsInt;
        }
    }
}
