﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace TurtleTime
{
    /// <summary>
    /// A collection of methods which help the game interface with Unity's GameObject objects.
    /// </summary>
    static class UnityUtils
    {
        public static readonly Quaternion GLOBAL_ROTATION = Quaternion.Euler(new Vector3(270, 180, 0));

        public static void CreateCamera(GameObject gameObject)
        {
            gameObject.AddComponent<Camera>();
        }

        public static void CreateMesh(GameObject gameObject, string fileName, string meshName, string matName)
        {
            MeshFilter filter = gameObject.AddComponent<MeshFilter>();
            MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
            Mesh[] meshes = Resources.LoadAll<Mesh>("Models/" + fileName);
            foreach (Mesh m in meshes)
            {
                if (m.name.Equals(meshName))
                {
                    filter.mesh = m;
                    break;
                }
            }
            filter.transform.localRotation = GLOBAL_ROTATION;
            Material material = Resources.Load<Material>("Models/Materials/" + matName);
            if (material != null)
            {
                renderer.material = material;
            }
            else
            {
                renderer.material = Resources.Load<Material>("Models/Materials/default");
            }
        }

        public static void CreateImage(GameObject gameObject, string spriteName)
        {
            Image image = gameObject.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Images/" + spriteName);
        }

        public static void CreateText(GameObject gameObject, string font, int fontSize, TextAnchor alignment = TextAnchor.MiddleCenter)
        {
            Text text = gameObject.AddComponent<Text>();
            text.font = Font.CreateDynamicFontFromOSFont(font, fontSize);
            text.alignment = alignment;
        }

        public static void SetTextField(GameObject gameObject, string expr)
        {
            Text text = gameObject.GetComponent<Text>();
            text.text = expr;
        }

        public static void SetRectTransform(GameObject gameObject, AnchoredVector2 center, Vector2 size)
        {
            RectTransform transform = gameObject.GetComponent<RectTransform>();
            if (transform == null)
            {
                transform = gameObject.AddComponent<RectTransform>();
            }
            transform.pivot = new Vector2(0.5f, 0.5f);
            transform.localPosition = center.EvaluateWithRectangle(gameObject.transform.parent.GetComponent<RectTransform>().rect);
            transform.sizeDelta = size;
        }

        public static void SetRectTransform(GameObject gameObject, params AnchoredVector2[] pointsInRectangle)
        {
            RectTransform transform = gameObject.GetComponent<RectTransform>();
            if (transform == null)
            {
                transform = gameObject.AddComponent<RectTransform>();
            }
            transform.pivot = new Vector2(0.5f, 0.5f);
            Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
            Vector2 max = new Vector2(float.MinValue, float.MinValue);
            foreach (AnchoredVector2 point in pointsInRectangle)
            {
                Vector2 v = point.EvaluateWithRectangle(gameObject.transform.parent.GetComponent<RectTransform>().rect);
                if (v.x < min.x) { min.x = v.x; }
                if (v.y < min.y) { min.y = v.y; }
                if (v.x > max.x) { max.x = v.x; }
                if (v.y > max.y) { max.y = v.y; }
            }
            transform.pivot = new Vector2(0.5f, 0.5f);
            transform.localPosition = min + (max - min) / 2;
            transform.sizeDelta = max - min;
        }

        public static IEnumerable<Transform> GetChildren(Transform parent)
        {
            int numChildren = parent.childCount;
            for (int index = 0; index < numChildren; index++)
            {
                yield return parent.GetChild(index);
            }
        }
    }
}
