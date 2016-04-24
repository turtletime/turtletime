using System;
using System.Collections.Generic;
using UnityMVC;
using UnityEngine;

namespace TurtleTime
{
    abstract class BillboardSpriteView<M> : View3D<M> where M : WorldObjectModel
    {
        struct AnimationData
        {
            public String Name;
            public bool FlipX;
            public bool FlipY;
            public int Start;
            public int End;
            public int FrameDuration;
        }

        // TODO: Don't do this
        Camera mainCamera;

        protected SpriteRenderer spriteRenderer;
        Vector3 offset;
        int timer = 0;

        // Extracted from JSON sprite data
        String resourceName;
        int columns;
        int rows;
        Dictionary<String, AnimationData> animationDataDictionary;

        List<Sprite> sprites;
        int currentAnimationFrame;
        AnimationData currentAnimationRange;

        public override string NodeName { get { return Model.SpriteReferenceTag; } }
        
        /// <summary>
        /// Iteratively attempts to get a child node with the given name from a chain of jsonObjects.
        /// Returns the first one that succeeds.
        /// </summary>
        /// <param name="field">The name of the child.</param>
        /// <param name="fallbackChain">The sequence of json nodes to look through.</param>
        /// <returns></returns>
        private static IJsonObject GetDataFromNodeWithFallback(string field, params IJsonObject[] fallbackChain)
        {
            IJsonObject result = null;
            foreach (var node in fallbackChain)
            {
                if (node == null)
                {
                    continue;
                }
                result = node[field];
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }

        protected override void Load()
        {
            mainCamera = GameObject.Find("Camera").GetComponent<Camera>();
            offset = TurtleUtils.CafeSpaceToWorldCoordinates(new Vector2(-2, 0));
            // Load animation data
            IJsonObject globalSpriteData = ReadOnlyData.JsonData["sprites"];
            IJsonObject mySpriteData = globalSpriteData["sprites"][Model.SpriteReferenceTag];
            IJsonObject templateData = mySpriteData["template"] == null ? null : globalSpriteData["templates"][mySpriteData["template"].AsString];
            resourceName = GetDataFromNodeWithFallback("resourceName", mySpriteData, templateData).AsString;
            columns = GetDataFromNodeWithFallback("columns", mySpriteData, templateData).AsInt;
            rows = GetDataFromNodeWithFallback("rows", mySpriteData, templateData).AsInt;
            int numFrames = columns * rows;
            // Initialize animation data
            animationDataDictionary = new Dictionary<string, AnimationData>();
            IJsonObject animationData = GetDataFromNodeWithFallback("animations", mySpriteData, templateData);
            foreach (String jsonObjectName in animationData.AsDictionary.Keys)
            {
                IJsonObject jsonAnimationData = animationData[jsonObjectName];
                List<IJsonObject> startAndEnd = jsonAnimationData["range"].AsList;
                int start = startAndEnd[0].AsInt;
                int end = start;
                if (startAndEnd.Count > 1)
                {
                    end = startAndEnd[1].AsInt;
                }
                int frameDuration = GetDataFromNodeWithFallback("frameDuration", jsonAnimationData, mySpriteData, templateData).AsInt;
                bool flipX = false, flipY = false;
                if (jsonAnimationData["flip"] != null)
                {
                    flipX = jsonAnimationData["flip"].AsString.ToLower().Contains("x");
                    flipY = jsonAnimationData["flip"].AsString.ToLower().Contains("y");
                }
                animationDataDictionary.Add(jsonObjectName, new AnimationData()
                    { Name = jsonObjectName, Start = start, End = end, FlipX = flipX, FlipY = flipY, FrameDuration = frameDuration });
                if (start == 0)
                {
                    currentAnimationRange = animationDataDictionary[jsonObjectName];
                }
            }

            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            sprites = new List<Sprite>();
            sprites.AddRange(Resources.LoadAll<Sprite>("Sprites/" + resourceName));
            spriteRenderer.sprite = sprites[currentAnimationFrame];
        }

        public Sprite[] asdf(String s)
        {
            return Resources.LoadAll<Sprite>(s);
        }

        protected override void UpdateView()
        {
            transform.position = TurtleUtils.CafeSpaceToWorldCoordinates(Model.Position) + offset + new Vector3(0, 0, 0.001f * SortOrder);
            transform.rotation = mainCamera.transform.rotation;
            timer++;
            if (timer == currentAnimationRange.FrameDuration)
            {
                currentAnimationFrame++;
                if (currentAnimationFrame > currentAnimationRange.End)
                {
                    currentAnimationFrame = currentAnimationRange.Start;
                }
                spriteRenderer.sprite = sprites[currentAnimationFrame];
            }
            if (!CurrentAnimation.Equals(currentAnimationRange.Name))
            {
                timer = 0;
                currentAnimationRange = animationDataDictionary[CurrentAnimation];
                currentAnimationFrame = currentAnimationRange.Start;
                spriteRenderer.sprite = sprites[currentAnimationFrame];
            }
        }

        protected abstract String CurrentAnimation { get; }
        protected abstract int SortOrder { get; }
        // protected abstract bool FlipX { get; }
    }
}
