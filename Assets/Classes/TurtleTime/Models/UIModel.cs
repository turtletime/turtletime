using UnityMVC;

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

        public override void LoadFromJson(IJsonObject jsonNode)
        {
            EdgePaddingPixels = jsonNode["edgePadding"].AsInt;
        }
    }
}
