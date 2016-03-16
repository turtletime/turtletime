using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheloniiUnity
{
    /// <summary>
    /// A class that contains logic for updating the game state.
    /// This class MUST NOT contain extra state beyond pointers to models.
    /// </summary>
    abstract class Controller : IController
    {        
        public abstract bool IsActive();

        public abstract void Update(float deltaTime);
    }
}
