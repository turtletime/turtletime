using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SimpleJSON;

namespace CheloniiUnity
{
    abstract class GameModule
    {
        private class Loadable<T>
        {
            public T Subject;
            public bool Loaded;
        }
        
        private List<IController> controllers = new List<IController>();
        private List<Loadable<IView>> views = new List<Loadable<IView>>();

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

        protected void AddView<T>(SingleModelView<T> view) where T : Model
        {
            if (view.GameObjectType == ViewType.WORLD)
            {
                view.SetParent(worldObject);
            }
            else
            {
                view.SetParent(uiObject);
            }
            Loadable<IView> l = new Loadable<IView>();
            l.Loaded = false;
            l.Subject = view;
            views.Add(l);
        }

        protected void AddView<T>(ModuleView<T> view) where T : GameModule
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
            Loadable<IView> l = new Loadable<IView>();
            l.Loaded = false;
            l.Subject = view;
            views.Add(l);
        }

        public void UpdateMain(float dt)
        {
            foreach (Loadable<IView> v in views)
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
            foreach (IController c in controllers)
            {
                if (c.IsActive())
                {
                    c.Update(dt);
                }
            }
        }

        public static T LoadFromJson<T>(JSONNode jsonNode) where T : IModel, new()
        {
            T result = new T();
            result.LoadFromJson(jsonNode);
            return result;
        }
    }
}
