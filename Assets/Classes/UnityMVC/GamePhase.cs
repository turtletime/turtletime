using System;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace UnityMVC
{
    /// <summary>
    /// A set of models that represent the state of the current phase of the game, and the controllers that manipulate them.
    /// All initialization logic occurs in Load().
    /// 
    /// Models must be "added" to the GameController with a key through one of the AddSingleModel* methods. If multiple instances of that model
    /// are anticipated, the AddModelList method should be used.
    /// 
    /// Controllers must also be "added" to the GameController, through the AddController method. Their Update methods will be called
    /// automatically in the order they are added. Any public-facing Model properties should be initialized by retrieving the model
    /// under a given key.
    /// 
    /// Having models and ModelCollection (see ModelCollection.cs) objects stored under keys allows automatic spawning and destroying of views,
    /// as models are added and removed from the game.
    /// 
    /// </summary>
    abstract class GamePhase
    {
        private Dictionary<String, Model> models = new Dictionary<string, Model>();
        private Dictionary<String, object> modelLists = new Dictionary<string, object>();

        private List<Controller> controllers = new List<Controller>();

        /// <summary>
        /// All initialization logic should go here. This includes all calls to Add* functions.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// All teardown logic should go here. TODO: No specific teardown policies yet.
        /// </summary>
        public abstract void Unload();

        /// <summary>
        /// Calls Update(dt) on all controllers in the order they were added.
        /// </summary>
        /// <param name="dt">The time that has elapsed since the last time UpdateMain(dt) was called.</param>
        public void UpdateMain(float dt)
        {
            foreach (Controller c in controllers)
            {
                c.Update(dt);
            }
        }

        /// <summary>
        /// Adds a controller to the GamePhase object. The controller's Update method will be called automatically.
        /// </summary>
        /// <param name="controller">A new controller object.</param>
        protected void AddController(Controller controller)
        {
            controllers.Add(controller);
        }

        protected void AddModel<M>(String key) where M : Model, new()
        {
            models.Add(key, new M());
        }

        protected void AddModel<M>(String key, GameObject gameObject) where M : Model, new()
        {
            M model = new M();
            model.LoadFromEditorPlacedGameObject(gameObject);
            models.Add(key, model);
        }

        protected void AddModel<M>(String key, JSONNode jsonNode) where M : Model, new()
        {
            M model = new M();
            model.LoadFromJson(jsonNode);
            models.Add(key, model);
        }

        protected void AddModel<M, V>(String key) where M : Model, new() where V : View<M>
        {
            AddModel<M, V>(key, new M());
        }

        private void AddModel<M, V>(String key, M model) where M : Model where V : View<M>
        {
            GameObject view = new GameObject();
            view.AddComponent<V>().Model = model;
            GameObject root = GameObject.Find("3D Node");
            bool parentFound = false;
            foreach (Transform t in root.GetComponentsInChildren<Transform>())
            {
                if (t.name.Equals(model.GetType().Name))
                {
                    view.transform.SetParent(t);
                    parentFound = true;
                    break;
                }
            }
            if (!parentFound)
            {
                GameObject t = new GameObject();
                t.name = model.GetType().Name;
                t.transform.SetParent(root.transform);
                view.transform.SetParent(t.transform);
            }
            models.Add(key, model);
        }

        protected void AddModel<M, V>(String key, GameObject gameObject) where M : Model, new() where V : View<M>
        {
            M model = new M();
            model.LoadFromEditorPlacedGameObject(gameObject);
            AddModel<M, V>(key, model);
        }

        protected void AddModel<M, V>(String key, JSONNode jsonNode) where M : Model, new() where V : View<M>
        {
            M model = new M();
            model.LoadFromJson(jsonNode);
            AddModel<M, V>(key, model);
        }

        protected void AddModelCollection<M>(String key) where M : Model
        {
            modelLists.Add(key, new ModelCollection<M>());
        }

        protected void AddModelCollection<M, V>(String key) where M : Model where V : View<M>, new()
        {
            modelLists.Add(key, new ModelWithViewCollection<M, V>());
        }

        protected M GetModel<M>(String key) where M : Model
        {
            return (M)models[key];
        }

        protected ModelCollection<M> GetModelCollection<M>(String key) where M : Model
        {
            return (ModelCollection<M>)modelLists[key];
        }

        protected M GetOneModelFromCollection<M>(String key, Predicate<M> predicate) where M : Model
        {
            return (modelLists[key] as ModelCollection<M>).GetOneModelSatisfyingPredicate(predicate);
        }
    }
}
