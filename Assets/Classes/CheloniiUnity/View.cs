using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheloniiUnity
{
    abstract class View<T> : MonoBehaviour where T : Model
    {
        public T Model { get; set; }

        protected abstract String ParentNodeName { get; }

        public void Start()
        {
            Load();
            // Attach this to a known node
            transform.SetParent(GameObject.Find("3D Node").transform, false);
        }

        protected abstract void Load();

        public abstract void Update();
    }

    abstract class View3D<T> : View<T> where T : Model
    {
        protected override String ParentNodeName { get { return "3D Node"; } }
    }

    abstract class ViewUI<T> : View<T> where T : Model
    {
        protected override String ParentNodeName { get { return "UI Node"; } }
    }
}
