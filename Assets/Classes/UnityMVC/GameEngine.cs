using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMVC
{
    /// <summary>
    /// A class which represents the game's complete state.
    /// </summary>
    abstract class GameEngine : MonoBehaviour
    {
        private Dictionary<Type, GamePhase> gameControllerMap = new Dictionary<Type, GamePhase>();
        private List<GamePhase> gameControllerList = new List<GamePhase>();

        public float DeltaTime = 1/60f;

        protected abstract void Initialize();

        protected void AddModule<Module>() where Module : GamePhase, new()
        {
            Module module = new Module();
            gameControllerList.Add(module);
            gameControllerMap.Add(module.GetType(), module);
            module.Load();
        }

        public T GetModule<T>() where T : GamePhase
        {
            return gameControllerMap[typeof(T)] as T;
        }

        public void Start()
        {
            GameObject child3D = new GameObject("3D Node");
            child3D.transform.SetParent(this.transform);
            GameObject childUI = new GameObject("UI Node");
            childUI.AddComponent<Canvas>();
            childUI.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            childUI.transform.SetParent(this.transform);
            Initialize();
        }

        public void Update()
        {
            foreach (GamePhase module in gameControllerList)
            {
                module.UpdateMain(DeltaTime);
            }
        }
    }
}
