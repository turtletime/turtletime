using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheloniiUnity
{
    interface IController
    {
        /// <summary>
        /// Returns whether this controller is active.
        /// </summary>
        /// <returns>Whether this controller is active.</returns>
        bool IsActive();

        /// <summary>
        /// Executes per-frame, state-updating logic.
        /// </summary>
        /// <param name="dt">The duration of a frame.</param>
        void Update(float deltaTime);
    }
}
