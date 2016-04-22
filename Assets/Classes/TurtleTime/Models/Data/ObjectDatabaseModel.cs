using System;
using UnityMVC;
using System.IO;
using System.Collections.Generic;

namespace TurtleTime
{
    class ObjectDatabaseModel : Model
    {
        public Dictionary<String, ObjectDataModel> ObjectData { get; private set; }

        public ObjectDatabaseModel()
        {
            ObjectData = new Dictionary<string, ObjectDataModel>();
        }

        public override void LoadFromJson(IJsonObject jsonNode)
        {
            foreach (IJsonObject child in jsonNode["tables"].AsList)
            {
                ObjectDataModel obj = new TableDataModel();
                obj.LoadFromJson(child);
                ObjectData.Add(obj.Name, obj);
            }
            foreach (IJsonObject child in jsonNode["seats"].AsList)
            {
                ObjectDataModel obj = new SeatDataModel();
                obj.LoadFromJson(child);
                ObjectData.Add(obj.Name, obj);
            }
            foreach (IJsonObject child in jsonNode["decorations"].AsList)
            {
                ObjectDataModel obj = new DecorationDataModel();
                obj.LoadFromJson(child);
                ObjectData.Add(obj.Name, obj);
            }
        }
    }
}
