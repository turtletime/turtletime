using UnityEngine;
using CheloniiUnity;
using TurtleTime.Models;
using System;

namespace TurtleTime.Views
{
    class UIView : ViewUI<UIModel>
    {
        GameObject child;

        protected override void Load()
        {
            name = "UI View";
            child = new GameObject("Rainbow");
            child.transform.SetParent(transform);
            UnityUtils.CreateImage(child, "test");
            child.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            AnchoredVector2 a = new AnchoredVector2(TextAnchor.UpperLeft, new Vector2(Model.EdgePaddingPixels, Model.EdgePaddingPixels));
            child.transform.localPosition = a.EvaluateWithRectangle(ViewportRect);
        }

        public override void Update()
        {
            child.SetActive(Model.Active);
        }
    }
}
