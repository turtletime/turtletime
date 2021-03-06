﻿using UnityEngine;
using UnityMVC;
using System;

namespace TurtleTime
{
    class UIView : ViewUI<TurtleTimeUIModel>
    {
        GameObject child;

        public override string NodeName { get { return "UI View"; } }

        protected override void Load()
        {
            child = new GameObject("Rainbow");
            child.transform.SetParent(transform);
            UnityUtils.CreateImage(child, "test");
            child.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            AnchoredVector2 a = new AnchoredVector2(TextAnchor.UpperLeft, new Vector2(Model.EdgePaddingPixels, Model.EdgePaddingPixels));
            child.transform.localPosition = a.EvaluateWithRectangle(ViewportRect);
        }

        protected override void UpdateView()
        {
            child.SetActive(Model.Active);
        }
    }
}
