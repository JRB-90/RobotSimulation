using JSim.Core.Common;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Interface to define the behaviour of all ISceneEntity factory impementations.
    /// </summary>
    public interface ISceneEntityFactory : IDisposable
    {
        /// <summary>
        /// Instantiates a new instance of a ISceneEntity.
        /// </summary>
        /// <param name="nameRepository">Name repository for the scene.</param>
        /// <param name="parentAssembly">Parent assembly the object is attached to.</param>
        /// <returns>Instantiated scene implementation.</returns>
        ISceneEntity CreateSceneEntity(
            INameRepository nameRepository,
            ISceneAssembly parentAssembly);

        /// <summary>
        /// Instantiates a new instance of a ISceneEntity.
        /// </summary>
        /// <param name="nameRepository">Name repository for the scene.</param>
        /// <param name="id">Unique id of the object.</param>
        /// <param name="name">Unique name of the object.</param>
        /// <param name="parentAssembly">Parent assembly the object is attached to.</param>
        /// <returns>Instantiated scene implementation.</returns>
        ISceneEntity CreateSceneEntity(
            INameRepository nameRepository,
            Guid id,
            string name,
            ISceneAssembly parentAssembly
        );

        /// <summary>
        /// Releases a ISceneEntity object.
        /// </summary>
        /// <param name="sceneEntity">ISceneEntity to release.</param>
        void Destroy(ISceneEntity sceneEntity);
    }
}
