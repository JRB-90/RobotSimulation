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
        /// <returns>New SceneAssembly object.</returns>
        ISceneAssembly CreateSceneAssembly();

        /// <summary>
        /// Creates a new SceneEntity.
        /// </summary>
        /// <returns>New SceneEntity object.</returns>
        ISceneEntity CreateSceneEntity();
    }
}
