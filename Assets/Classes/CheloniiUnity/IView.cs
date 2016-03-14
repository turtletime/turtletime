using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheloniiUnity
{
    interface IView : IController
    {
        void Dispose();

        void Load();

        void Unload();
    }
}
