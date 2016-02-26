using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheloniiUnity
{
    interface IViewable : IControllable
    {
        void Load();

        void Unload();
    }
}
