using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheloniiUnity;
using SimpleJSON;
using UnityEngine;

namespace TurtleTime
{
    class QuickOptionsModule : GameModule
    {
        public class QuickOptionsModel : MonoBehaviour, IModel
        {
            public bool debugMode = false;

            public void LoadFromJson(JSONNode jsonNode)
            {
                debugMode = jsonNode["debugMode"].AsBool;
            }
        }

        public class QuickOptionsView : ModuleView<QuickOptionsModule>
        {
            public override ViewType GameObjectType { get { return ViewType.UI; } }

            protected override string Name { get { return "Options"; } }

            public QuickOptionsModel QuickOptionsObject { get { return gameObject.GetComponent<QuickOptionsModel>(); } }

            public override bool IsActive()
            {
                return true;
            }

            public QuickOptionsView()
            {
                gameObject.AddComponent<QuickOptionsModel>();
            }

            public override void Load() { }

            public override void Unload() { }

            public override void Update(float dt) { }
        }

        public QuickOptionsModel Options;

        public override void Load()
        {
            QuickOptionsView view = new QuickOptionsView();
            Options = view.QuickOptionsObject;
            Options.LoadFromJson(Utils.LoadJSONConfig("game"));
            AddView(view);
        }

        public override void Unload() { }
    }
}
