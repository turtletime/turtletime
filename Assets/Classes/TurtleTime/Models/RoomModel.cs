using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;

namespace TurtleTime
{
    class RoomModel : Model
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public List<WorldObjectModel> Models { get; private set; }

        public RoomModel()
        {
            Models = new List<WorldObjectModel>();
        }

        public override void LoadFromJson(IJsonObject jsonNode)
        {
            Width = jsonNode["width"].AsInt;
            Height = jsonNode["height"].AsInt;
            base.LoadFromJson(jsonNode);
        }

        public WorldObjectModel GetModelAtLocation(int x, int y)
        {
            // TODO: Highly inefficient
            foreach (WorldObjectModel model in Models)
            {
                int modelX = (int)Math.Round(model.Position.x);
                int modelY = (int)Math.Round(model.Position.y);
                bool result = true;
                result = result && x >= modelX - model.Width / 2;
                result = result && x < modelX + model.Width / 2;
                result = result && y >= modelY - model.Height / 2;
                result = result && y < modelY + model.Height / 2;
                if (result)
                {
                    return model;
                }
            }
            return null;
        }

        public bool CanBePlacedAt(WorldObjectModel model, int x, int y)
        {
            int modelX = (int)Math.Round(model.Position.x);
            int modelY = (int)Math.Round(model.Position.y);
            for (int i = modelX - model.Width / 2; i < modelX + model.Width / 2; i++)
            {
                for (int j = modelY - model.Height / 2; j < modelY + model.Height / 2; i++)
                {
                    if (GetModelAtLocation(i, j) != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
