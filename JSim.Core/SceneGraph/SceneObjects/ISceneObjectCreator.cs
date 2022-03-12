namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Interface defining the behaviour of all scene object creators.
    /// </summary>
    public interface ISceneObjectCreator : IDisposable
    {
        /// <summary>
        /// Creates a new SceneAssembly.
        /// </summary>
        /// <param name="parentAssembly">Parent assembly the object is attached to.</param>
        /// <returns>New SceneAssembly object.</returns>
        ISceneAssembly CreateSceneAssembly(ISceneAssembly? parentAssembly);

        /// <summary>
        /// Creates a new SceneEntity.
        /// </summary>
        /// <param name="parentAssembly">Parent assembly the object is attached to.</param>
        /// <returns>New SceneEntity object.</returns>
        ISceneEntity CreateSceneEntity(ISceneAssembly parentAssembly);
    }
}
