using SimpleJSON;

namespace CheloniiUnity
{
    /// <summary>
    /// A class which represents part of the game's state.
    /// Any logic in this class MUST be limited to enforcing data constraints.
    /// </summary>
    class Model
    {
        /// <summary>
        /// Loads this model from a parsed JSON node. This method may be overridden by children of the Model class.
        /// </summary>
        /// <param name="jsonNode">The parsed JSON node from which Model data should be loaded.</param>
        public virtual void LoadFromJson(JSONNode jsonNode) { }
    }
}
