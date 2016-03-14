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

        //private HashSet<IModel> models = new HashSet<IModel>();
        //private Dictionary<IModel, List<Loadable<IView>>> singleModelViews = new Dictionary<IModel, List<Loadable<IView>>>();
        private HashSet<IController> controllers = new HashSet<IController>();
        private HashSet<Loadable<IView>> views = new HashSet<Loadable<IView>>();

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

        //protected void AddModel<T>(Model model) where T : GameModule
        //{
        //    models.Add(model);
        //    singleModelViews.Add(model, new List<Loadable<IView>>());
        //    foreach (View<T> view in )
        //    {
        //        view.GameModule = (T)this;
        //        if (view.GameObjectType == ViewType.WORLD)
        //        {
        //            view.SetParent(worldObject);
        //        }
        //        else
        //        {
        //            view.SetParent(uiObject);
        //        }
        //        Loadable<IView> l = new Loadable<IView>();
        //        l.Loaded = false;
        //        l.Subject = view;
        //        views.Add(l);
        //        singleModelViews[model].Add(l);
        //    }
        //}

        //protected void RemoveModel(Model model)
        //{
        //    models.Remove(model);
        //    foreach (Loadable<IView> v in singleModelViews[model])
        //    {
        //        v.Subject.Dispose();
        //        views.Remove(v);
        //    }
        //    singleModelViews.Remove(model);
        //}

        protected void AddController<T>(Controller<T> controller) where T : GameModule
        {
            controller.GameModule = (T)this;
            controllers.Add(controller);
        }

        public void AddView<T>(View<T> view) where T : GameModule
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
