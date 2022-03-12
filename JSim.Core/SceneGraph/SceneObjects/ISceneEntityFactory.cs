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
        /// <returns>Instantiated scene implementation.</returns>
        ISceneEntity GetSceneEntity();

        /// <summary>
        /// Releases a ISceneEntity object.
        /// </summary>
        /// <param name="sceneEntity">ISceneEntity to release.</param>
        void Destroy(ISceneEntity sceneEntity);
    }
}
