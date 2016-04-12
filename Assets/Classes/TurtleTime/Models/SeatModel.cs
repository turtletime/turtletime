using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityMVC;

namespace TurtleTime
{
    class SeatModel : WorldObjectModel
    {
        public override string SpriteReferenceTag { get { return "chair"; } }

        public bool Taken { get; set; }
        public bool Selected { get; set; }

        public class View : BillboardSpriteView<SeatModel>
        {
            protected override string CurrentAnimation
            {
                get
                {
                    return "default";
                }
            }

            protected override void UpdateView()
            {
                spriteRenderer.color = Model.Selected ? Color.red : Color.white;
                base.UpdateView();
            }
        }
    }
}
