namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Interface to define the behaviour of all ISceneAssembly factory impementations.
    /// </summary>
    public interface ISceneAssemblyFactory : IDisposable
    {
        /// <summary>
        /// Instantiates a new instance of a ISceneAssembly.
        /// </summary>
        /// <returns>Instantiated scene implementation.</returns>
        ISceneAssembly GetSceneAssembly();

        /// <summary>
        /// Releases a ISceneAssembly object.
        /// </summary>
        /// <param name="sceneAssembly">ISceneAssembly to release.</param>
        void Destroy(ISceneAssembly sceneAssembly);
    }
}
