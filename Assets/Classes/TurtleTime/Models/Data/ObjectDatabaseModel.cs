using System;
using UnityMVC;
using System.IO;
using System.Collections.Generic;

namespace TurtleTime
{
    /// <summary>
    /// A persistent model that stores all static information about objects (i.e. information that
    /// never changes).
    /// </summary>
    class ObjectDatabaseModel : Model
    {
        private static ObjectDatabaseModel _instance;
        public static ObjectDatabaseModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ObjectDatabaseModel();
                    _instance.LoadFromJson(ReadOnlyData.JsonData["objects"]);
                }
                return _instance;
            }
        }

        public ObjectDataModel this[String s] { get { return ObjectData[s]; } }

        public Dictionary<String, ObjectDataModel> ObjectData { get; private set; }

        public ObjectDatabaseModel()
        {
            ObjectData = new Dictionary<string, ObjectDataModel>();
        }

        public override void LoadFromJson(IJsonObject jsonNode)
        {
            foreach (IJsonObject child in jsonNode["turtles"].AsList)
            {
                var obj = new TurtleDataModel();
                obj.LoadFromJson(child);
                ObjectData.Add(obj.ID, obj);
            }
            foreach (IJsonObject child in jsonNode["tables"].AsList)
            {
                var obj = new TableDataModel();
                obj.LoadFromJson(child);
                ObjectData.Add(obj.ID, obj);
            }
            foreach (IJsonObject child in jsonNode["seats"].AsList)
            {
                var obj = new SeatDataModel();
                obj.LoadFromJson(child);
                ObjectData.Add(obj.ID, obj);
            }
            foreach (IJsonObject child in jsonNode["decorations"].AsList)
            {
                var obj = new DecorationDataModel();
                obj.LoadFromJson(child);
                ObjectData.Add(obj.ID, obj);
            }
        }
    }
}
