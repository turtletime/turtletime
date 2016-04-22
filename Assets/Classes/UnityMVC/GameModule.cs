using System;
using System.Collections.Generic;
using UnityEngine;

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
    abstract class GameModule
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

        private static String GenerateDefaultKey<M>()
        {
            return "__" + typeof(M).Name + "__";
        }

        private void AddModel<M>(M model, String key) where M : Model
        {
            if (key == null)
            {
                key = GenerateDefaultKey<M>();
            }
            models.Add(key, model);
        }

        private void AddModel<M, V>(M model, String key) where M : Model where V : View<M>
        {
            if (key == null)
            {
                key = GenerateDefaultKey<M>();
            }
            GameObject view = new GameObject();
            view.AddComponent<V>().Model = model;
            GameObject root = GameObject.Find(view.GetComponent<V>().NodeParent);
            view.transform.SetParent(root.transform);
            models.Add(key, model);
        }

        private void AddModelCollection<M>(ModelCollection<M> collection, String key) where M : Model
        {
            if (key == null)
            {
                key = GenerateDefaultKey<M>();
            }
            modelLists.Add(key, collection);
        }

        protected void AddModel<M>(String key = null) where M : Model, new()
        {
            AddModel(new M(), key);
        }

        protected void AddModel<M>(GameObject gameObject, String key = null) where M : Model, new()
        {
            M model = new M();
            model.LoadFromEditorPlacedGameObject(gameObject);
            AddModel(model, key);
        }

        protected void AddModel<M>(IJsonObject jsonNode, String key = null) where M : Model, new()
        {
            M model = new M();
            model.LoadFromJson(jsonNode);
            AddModel(model, key);
        }

        protected void AddModel<M, V>(String key = null) where M : Model, new() where V : View<M>
        {
            AddModel<M, V>(new M(), key);
        }

        protected void AddModel<M, V>(GameObject gameObject, String key = null) where M : Model, new() where V : View<M>
        {
            M model = new M();
            model.LoadFromEditorPlacedGameObject(gameObject);
            AddModel<M, V>(model, key);
        }

        protected void AddModel<M, V>(IJsonObject jsonNode, String key = null) where M : Model, new() where V : View<M>
        {
            M model = new M();
            model.LoadFromJson(jsonNode);
            AddModel<M, V>(model, key);
        }

        protected void AddModelCollection<M>(String key = null) where M : Model
        {
            AddModelCollection(new ModelCollection<M>(), key);
        }

        protected void AddModelCollection<M>(IJsonObject jsonNode, String key) where M : Model, new()
        {
            var collection = new ModelCollection<M>();
            jsonNode.AsList.ForEach(jsonObject =>
            {
                var model = new M();
                model.LoadFromJson(jsonObject);
                collection.Add(model);
            });
            AddModelCollection(collection, key);
        }

        protected void AddModelCollection<M, V>(String key = null) where M : Model where V : View<M>, new()
        {
            AddModelCollection(new ModelWithViewCollection<M, V>(), key);
        }

        protected void AddModelCollection<M, V>(IJsonObject jsonNode, String key = null) where M : Model, new() where V : View<M>, new()
        {
            var collection = new ModelWithViewCollection<M, V>();
            jsonNode.AsList.ForEach(jsonObject =>
            {
                var model = new M();
                model.LoadFromJson(jsonObject);
                collection.Add(model);
            });
            AddModelCollection(collection, key);
        }

        protected M GetModel<M>(String key = null) where M : Model
        {
            if (key == null)
            {
                key = GenerateDefaultKey<M>();
            }
            return (M)models[key];
        }

        protected ModelCollection<M> GetModelCollection<M>(String key = null) where M : Model
        {
            if (key == null)
            {
                key = GenerateDefaultKey<M>();
            }
            return (ModelCollection<M>)modelLists[key];
        }

        protected M GetOneModelFromCollection<M>(Predicate<M> predicate, String key = null) where M : Model
        {
            if (key == null)
            {
                key = GenerateDefaultKey<M>();
            }
            return (modelLists[key] as ModelCollection<M>).GetOneModelSatisfyingPredicate(predicate);
        }
    }
}
