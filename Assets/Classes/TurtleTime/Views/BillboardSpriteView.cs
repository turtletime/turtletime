using System;
using System.Collections.Generic;
using UnityMVC;
using UnityEngine;

namespace TurtleTime
{
    class BillboardSpriteView<M> : View3D<M> where M : WorldObjectModel
    {
        struct AnimationData
        {
            public int Start;
            public int End;
        }

        // TODO: Don't do this
        Camera mainCamera;

        Vector3 offset;
        int timer = 0;

        // Extracted from JSON sprite data
        String resourceName;
        int columns;
        int rows;
        int frameDuration;
        Dictionary<String, AnimationData> animationData;

        public override string NodeName { get { return Model.SpriteReferenceTag; } }

        protected override void Load()
        {
            mainCamera = GameObject.Find("Camera").GetComponent<Camera>();
            offset = TurtleUtils.CafeSpaceToWorldCoordinates(new Vector2(-1, 0));
            // Load animation data
            IJsonObject globalSpriteData = ReadOnlyData.JsonData["sprites"];
            IJsonObject mySpriteData = globalSpriteData["sprites"][Model.SpriteReferenceTag];
            IJsonObject templateData = globalSpriteData["templates"][mySpriteData["template"].AsString];
            resourceName = mySpriteData["resourceName"].AsString;
            columns = templateData["columns"].AsInt;
            rows = templateData["rows"].AsInt;
            frameDuration = templateData["frameDuration"].AsInt;
            animationData = new Dictionary<string, AnimationData>();
            foreach (String jsonObjectName in templateData["animations"].AsDictionary.Keys)
            {
                List<IJsonObject> startAndEnd = templateData["animations"][jsonObjectName].AsList;
                int start = startAndEnd[0].AsInt;
                int end = start;
                if (startAndEnd.Count > 1)
                {
                    end = startAndEnd[1].AsInt;
                }
                animationData.Add(jsonObjectName, new AnimationData() { Start = start, End = end });
            }
            UnityUtils.CreateSprite(gameObject, resourceName);
        }

        protected override void UpdateView()
        {
            transform.position = TurtleUtils.CafeSpaceToWorldCoordinates(Model.Position) + offset;
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}
