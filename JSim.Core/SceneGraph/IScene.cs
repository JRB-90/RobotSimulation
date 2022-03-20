using JSim.Core.Common;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Defines the behaviour of all Scene implementations.
    /// A scene represents an entire scene graph and provides various
    /// functionality for interacting with it, such as iterators etc.
    /// </summary>
    public interface IScene : 
        IEnumerable<ISceneObject>, 
        IMessageHandler<SceneObjectModified>, 
        IMessageHandler<SceneStructureChanged>,
        IDisposable
    {
        /// <summary>
        /// Name of the scene.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Root assembly, representing the top level for the scene graph.
        /// </summary>
        ISceneAssembly Root { get; }

        /// <summary>
        /// Event fired when a scene object in the scene has been modified.
        /// </summary>
        event SceneObjectModifiedEventHandler SceneObjectModified;

        /// <summary>
        /// Event fired when the structure of the scene graph has changed.
        /// </summary>
        event SceneStructureChangedEventHandler SceneStructureChanged;

        /// <summary>
        /// Attempts to find an object in the scene by its ID.
        /// </summary>
        /// <param name="id">ID of the scene object.</param>
        /// <param name="sceneObject">Found object. Null if not found.</param>
        /// <returns>True if object found.</returns>
        bool TryFindByID(Guid id, out ISceneObject? sceneObject);

        /// <summary>
        /// Attempts to find an object in the scene by its Name.
        /// </summary>
        /// <param name="name">NAme of the scene object.</param>
        /// <param name="sceneObject">Found object. Null if not found.</param>
        /// <returns>True if object found.</returns>
        bool TryFindByName(string name, out ISceneObject? sceneObject);
    }
}
