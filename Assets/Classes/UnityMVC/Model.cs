using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SimpleJSON;

namespace UnityMVC
{
    /// <summary>
    /// A class which represents part of the game's state.
    /// Any logic in this class MUST be limited to enforcing data constraints.
    /// </summary>
    abstract class Model
    {
        /// <summary>
        /// Loads this model from data stored in an existing GameObject object.
        /// </summary>
        /// <param name="gameObject">The existing GameObject object from which Model data should be loaded.</param>
        public virtual void LoadFromEditorPlacedGameObject(GameObject gameObject) { }

        /// <summary>
        /// Loads this model from a parsed JSON node.
        /// </summary>
        /// <param name="jsonNode">The parsed JSON node from which Model data should be loaded.</param>
        public virtual void LoadFromJson(JSONNode jsonNode) { }
    }
}
