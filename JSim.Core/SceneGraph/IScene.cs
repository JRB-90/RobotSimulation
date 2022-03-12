namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Defines the behaviour of all Scene implementations.
    /// A scene represents an entire scene graph and provides various
    /// functionality for interacting with it, such as iterators etc.
    /// </summary>
    public interface IScene
    {
        /// <summary>
        /// Name of the scene.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Root assembly, representing the top level for the scene graph.
        /// </summary>
        ISceneAssembly Root { get; }
    }
}
