namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Interface to define the behaviour of all ISceneAssembly objects.
    /// Scene assemblies can contain a group of child objects as a method
    /// of supporting a scene tree.
    /// </summary>
    public interface ISceneAssembly : ISceneObject, IEnumerable<ISceneObject>
    {
        IReadOnlyCollection<ISceneObject> Children { get; }

        /// <summary>
        /// Creates a new scene assembly and attaches it to this assemblies children.
        /// </summary>
        /// <returns>Newly instantiated scene assembly object.</returns>
        ISceneAssembly CreateNewAssembly();

        /// <summary>
        /// Creates a new scene entity and attaches it to this assemblies children.
        /// </summary>
        /// <returns>Newly instantiated scene entity object.</returns>
        ISceneEntity CreateNewEntity();

        /// <summary>
        /// Removes a scene object from this assemblies children.
        /// </summary>
        /// <param name="sceneObject">Scene object to remove from this assembly.</param>
        /// <returns>Returns true if successfully removed.</returns>
        bool RemoveObject(ISceneObject sceneObject);
    }
}
