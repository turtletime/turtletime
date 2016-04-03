using System;
using UnityEngine;
using UnityMVC;
using UnityMVC.UI;

namespace TurtleTime.Models
{
    class TurtleTimeUIModel : AbstractUIModel
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


        public override void LoadFromJson(IJsonObject jsonNode)
        {
            EdgePaddingPixels = jsonNode["edgePadding"].AsInt;
        }
    }
}
