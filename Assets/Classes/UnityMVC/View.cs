﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityMVC
{
    abstract class View<T> : MonoBehaviour where T : Model
    {
        public T Model { get; set; }

        public abstract String NodeName { get; }

        public void Start()
        {
            Load();
            this.name = NodeName;
        }

        public void Update()
        {
            UpdateView();
        }

        public void LateUpdate()
        {
            UpdateModel();
        }

        protected abstract void Load();

        protected virtual void UpdateModel() { }

        protected abstract void UpdateView();
    }

    abstract class View3D<T> : View<T> where T : Model
    {
        
    }

    abstract class ViewUI<T> : View<T> where T : Model
    {
        protected Rect ViewportRect { get { return transform.GetComponentInParent<RectTransform>().rect; } }
    }
}