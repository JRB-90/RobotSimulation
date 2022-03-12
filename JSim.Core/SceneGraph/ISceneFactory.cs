namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Interface to define the behaviour of all IScene factory impementations.
    /// </summary>
    public interface ISceneFactory : IDisposable
    {
        /// <summary>
        /// Instantiates a new instance of a IScene.
        /// </summary>
        /// <returns>Instantiated scene implementation.</returns>
        IScene GetScene();

        /// <summary>
        /// Releases a IScene object.
        /// </summary>
        /// <param name="scene">IScene to release.</param>
        void Destroy(IScene scene);
    }
}
