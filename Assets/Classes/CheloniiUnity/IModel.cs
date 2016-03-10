using SimpleJSON;

namespace CheloniiUnity
{
    /// <summary>
    /// A class which represents part of the game's state.
    /// Any logic in this class MUST be limited to enforcing data constraints.
    /// </summary>
    interface IModel
    {
        /// <summary>
        /// Loads this model from a parsed JSON node.
        /// </summary>
        /// <param name="jsonNode">The parsed JSON node from which Model data should be loaded.</param>
        void LoadFromJson(JSONNode jsonNode);
    }
}
