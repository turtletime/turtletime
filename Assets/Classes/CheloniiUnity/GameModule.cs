using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SimpleJSON;
using System.Collections;

namespace CheloniiUnity
{
    abstract class GameModule
    {
        private Dictionary<String, Model> models = new Dictionary<string, Model>();
        private Dictionary<String, object> modelLists = new Dictionary<string, object>();

        private HashSet<Controller> controllers = new HashSet<Controller>();

        public GameEngine Engine { get; set; }

        public abstract void Load();

        public abstract void Unload();

        public void UpdateMain(float dt)
        {
            foreach (Controller c in controllers)
            {
                c.Update(dt);
            }
        }

        protected void AddController(Controller controller)
        {
            controllers.Add(controller);
        }

        protected void AddSingleModel(String key, Model model)
        {
            models.Add(key, model);
        }
        
        protected void AddSingleModelWithView<M, V>(String key, M model) where M : Model where V : View<M>
        {
            GameObject view = new GameObject();
            view.AddComponent<V>().Model = model;
            models.Add(key, model);
        }

        protected void AddEmptyModelList<T>(String key, ModelCollection<T> modelCollection) where T : Model
        {
            modelLists.Add(key, modelCollection);
        }

        protected T GetModel<T>(String key) where T : Model
        {
            return (T)models[key];
        }

        protected ModelCollection<T> GetModelCollection<T>(String key) where T : Model
        {
            return (ModelCollection<T>)modelLists[key];
        }

        public static T LoadFromJson<T>(JSONNode jsonNode) where T : Model, new()
        {
            T result = new T();
            result.LoadFromJson(jsonNode);
            return result;
        }
    }
}
