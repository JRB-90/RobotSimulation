using JSim.Core.Common;

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
        /// <param name="nameRepository">Name repository for the scene.</param>
        /// <param name="creator">Scene object creator to use for creating children in the assembly.</param>
        /// <param name="parentAssembly">Parent assembly the object is attached to.</param>
        /// <returns>Instantiated scene implementation.</returns>
        ISceneAssembly CreateSceneAssembly(
            INameRepository nameRepository,
            ISceneObjectCreator creator,
            ISceneAssembly? parentAssembly
        );

        /// <summary>
        /// Instantiates a new instance of a ISceneAssembly.
        /// </summary>
        /// <param name="nameRepository">Name repository for the scene.</param>
        /// <param name="creator">Scene object creator to use for creating children in the assembly.</param>
        /// <param name="id">Unique id of the object.</param>
        /// <param name="name">Unique name of the object.</param>
        /// <param name="parentAssembly">Parent assembly the object is attached to.</param>
        /// <returns>Instantiated scene implementation.</returns>
        ISceneAssembly CreateSceneAssembly(
            INameRepository nameRepository,
            ISceneObjectCreator creator,
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
