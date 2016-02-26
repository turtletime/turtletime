using System;
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
        private Dictionary<GameModuleKey, GameModule> moduleMap = new Dictionary<GameModuleKey, GameModule>();
        private List<GameModule> moduleList = new List<GameModule>();

        private GameObject worldObject;
        private GameObject uiObject;

        public void SetGameObjects(GameObject worldObject, GameObject uiObject)
        {
            this.worldObject = worldObject;
            this.uiObject = uiObject;
        }

        public abstract void Initialize();

        public void AddModule(GameModule module)
        {
            moduleList.Add(module);
            moduleMap.Add(module.ModuleKey, module);
            module.SetGameObject(worldObject, uiObject);
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
