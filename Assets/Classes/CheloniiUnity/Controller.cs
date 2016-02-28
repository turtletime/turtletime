using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheloniiUnity
{
    /// <summary>
    /// A class that contains logic for updating the game state.
    /// This class MUST NOT contain extra state.
    /// </summary>
    /// <typeparam name="Model">The type of state class on which this Controller should operate.</typeparam>
    abstract class Controller<Model> : IControllable where Model : GameModule
    {
        /// <summary>
        /// Gets or sets a pointer to game state.
        /// </summary>
        public Model Module { get; set; }
        
        public abstract bool IsActive();

        public abstract void Update(float deltaTime);
    }
}
