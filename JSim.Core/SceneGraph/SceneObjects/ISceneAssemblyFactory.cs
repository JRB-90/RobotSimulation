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
        /// <param name="parentAssembly">Parent assembly the object is attached to.</param>
        /// <returns>Instantiated scene implementation.</returns>
        ISceneAssembly CreateSceneAssembly(ISceneAssembly? parentAssembly);

        /// <summary>
        /// Instantiates a new instance of a ISceneAssembly.
        /// </summary>
        /// <param name="id">Unique id of the object.</param>
        /// <param name="name">Unique name of the object.</param>
        /// <param name="parentAssembly">Parent assembly the object is attached to.</param>
        /// <returns>Instantiated scene implementation.</returns>
        ISceneAssembly CreateSceneAssembly(
            Guid id,
            string name,
            ISceneAssembly? parentAssembly
        );

        /// <summary>
        /// Releases a ISceneAssembly object.
        /// </summary>
        /// <param name="sceneAssembly">ISceneAssembly to release.</param>
        void Destroy(ISceneAssembly sceneAssembly);
    }
}
