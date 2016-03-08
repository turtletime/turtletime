﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

namespace CheloniiUnity
{
    /// <summary>
    /// A class which represents the game's complete state.
    /// </summary>
    abstract class GameEngine
    {
        private Dictionary<Type, GameModule> moduleMap = new Dictionary<Type, GameModule>();
        private List<GameModule> moduleList = new List<GameModule>();

        private GameObject worldObject;
        private GameObject uiObject;

        public void SetGameObjects(GameObject worldObject, GameObject uiObject)
        {
            this.worldObject = worldObject;
            this.uiObject = uiObject;
        }

        public abstract void Initialize();

        protected void AddModule(GameModule module)
        {
            moduleList.Add(module);
            moduleMap.Add(module.GetType(), module);
            module.SetGameObject(worldObject, uiObject);
            module.Engine = this;
            module.Load();
        }

        public T GetModule<T>() where T : GameModule
        {
            return moduleMap[typeof(T)] as T;
        }

        public void Update(float dt)
        {
            foreach (GameModule module in moduleList)
            {
                module.UpdateMain(dt);
            }
        }
    }
}
