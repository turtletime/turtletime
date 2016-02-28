using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheloniiUnity
{
    /// <summary>
    /// A class which represents part of the game's visual or aural output.
    /// Any models referenced in this class MUST be treated as read-only.
    /// </summary>
    /// <typeparam name="Model">The type of state class from which this View should read state.</typeparam>
    abstract class View<Module> : IViewable where Module : GameModule
    {
        public Module GameModule { get; set; }
        protected GameObject gameObject = new GameObject();

        public abstract ViewType GameObjectType { get; }

        protected abstract String Name { get; }

        public View()
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
