namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Interface to define the behaviour of all ISceneAssembly objects.
    /// Scene assemblies can contain a group of child objects as a method
    /// of supporting a scene tree.
    /// </summary>
    public interface ISceneAssembly : ISceneObject
    {
        IReadOnlyCollection<ISceneObject> Children { get; }

        bool AddChild(ISceneObject child);

        bool RemoveChild(ISceneObject child);
    }
}
