using CheloniiUnity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

namespace CheloniiUnity
{
    class Model : IModel
    {
        public virtual void LoadFromJson(JSONNode jsonNode) { }

        public virtual IEnumerable<IView> InstantiateSingleModelViews() { yield break; }
    }
}
