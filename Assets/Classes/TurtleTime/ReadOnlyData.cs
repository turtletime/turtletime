using UnityMVC;
using UnityEngine;

namespace TurtleTime
{
    static class ReadOnlyData
    {
        public static IJsonObject JsonData { get; private set; }

        static ReadOnlyData()
        {
            JsonData = new UnityMVC.JsonObject.ConglomorateJsonObject();
            foreach (TextAsset t in Resources.LoadAll<TextAsset>("Config"))
            {
                // Json Data can be found in Config folder
                string path = "Config/" + t.name;
                try
                {
                    (JsonData as UnityMVC.JsonObject.ConglomorateJsonObject).AddSource(t.name, t.text);
                }
                catch
                {
                    Debug.LogWarning(t.name + " could not be loaded.");
                }
            }
        }
    }
}
