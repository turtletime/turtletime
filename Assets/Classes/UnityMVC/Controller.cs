namespace UnityMVC
{
    /// <summary>
    /// A class that contains logic for updating the game state.
    /// This class should only contain public pointers to Model or ModelCollection
    /// objects (as class properties).
    /// </summary>
    abstract class Controller
    {
        /// <summary>
        /// Updates models applicable by this controller.
        /// </summary>
        /// <param name="deltaTime">The time that has elapsed since the last time Update was called.</param>
        public abstract void Update(float deltaTime);
    }
}
