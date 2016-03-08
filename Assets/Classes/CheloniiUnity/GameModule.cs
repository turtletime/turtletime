using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheloniiUnity
{
    abstract class GameModule : Model
    {
        private class Loadable<T>
        {
            public T Subject;
            public bool Loaded;
        }
        
        private List<IControllable> controllers = new List<IControllable>();
        private List<Loadable<IViewable>> views = new List<Loadable<IViewable>>();

        private GameObject worldObject;
        private GameObject uiObject;

        public GameEngine Engine { get; set; }

        public void SetGameObject(GameObject worldObject, GameObject uiObject)
        {
            this.worldObject = worldObject;
            this.uiObject = uiObject;
        }

        public abstract void Load();

        public abstract void Unload();

        protected void AddController<T>(Controller<T> controller) where T : GameModule
        {
            controller.GameModule = (T)this;
            controllers.Add(controller);
        }

        protected void AddView<T>(View<T> view) where T : GameModule
        {
            view.GameModule = (T)this;
            if (view.GameObjectType == ViewType.WORLD)
            {
                view.SetParent(worldObject);
            }
            else
            {
                view.SetParent(uiObject);
            }
            Loadable<IViewable> l = new Loadable<IViewable>();
            l.Loaded = false;
            l.Subject = view;
            views.Add(l);
        }

        public void UpdateMain(float dt)
        {
            foreach (IControllable c in controllers)
            {
                if (c.IsActive())
                {
                    c.Update(dt);
                }
            }
            foreach (Loadable<IViewable> v in views)
            {
                if (v.Subject.IsActive())
                {
                    if (!v.Loaded)
                    {
                        v.Subject.Load();
                        v.Loaded = true;
                    }
                    v.Subject.Update(dt);
                }
                else
                {
                    if (v.Loaded)
                    {
                        v.Subject.Unload();
                        v.Loaded = false;
                    }
                }
            }
        }
    }
}
