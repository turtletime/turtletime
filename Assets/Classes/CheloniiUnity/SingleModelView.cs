using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheloniiUnity
{
    abstract class SingleModelView<GameModel> : IView where GameModel : Model
    {
        protected GameModel Model { get; set; }
        protected GameObject gameObject = new GameObject();

        public abstract ViewType GameObjectType { get; }

        protected abstract String Name { get; }

        public SingleModelView()
        {
            gameObject.name = Name;
        }

        public abstract void Load();

        public abstract void Unload();

        public abstract bool IsActive();

        public void SetParent(GameObject parent)
        {
            gameObject.transform.SetParent(parent.transform);
            if (parent.GetComponent<RectTransform>() != null)
            {
                RectTransform parentRect = parent.GetComponent<RectTransform>();
                RectTransform thisRect = gameObject.AddComponent<RectTransform>();
                thisRect.position = parentRect.position;
                thisRect.sizeDelta = parentRect.sizeDelta;
            }
        }

        public void SetParentOf(GameObject child)
        {
            child.transform.SetParent(gameObject.transform);
        }

        public abstract void Update(float dt);
    }
}
