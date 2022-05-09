using JSim.Core.Importer;

namespace JSim.Core.SceneGraph
{
    /// <summary>
    /// Interface to define the behavior of all scene managers.
    /// A scene manager is used to create scenes, interact with them and clean up
    /// resources after use.
    /// </summary>
    public interface ISceneManager : IDisposable
    {
        /// <summary>
        /// Gets the current scene.
        /// </summary>
        IScene CurrentScene { get; }

        /// <summary>
        /// Gets the currently configured model importer.
        /// </summary>
        IModelImporter ModelImporter { get; }

        /// <summary>
        /// Event fired when the current scene has changed.
        /// </summary>
        event CurrentSceneChangedEventHandler CurrentSceneChanged;

        /// <summary>
        /// Removes the current scene and sets it to a blank scene.
        /// </summary>
        void NewScene();

        /// <summary>
        /// Saves the current scene to disk.
        /// </summary>
        /// <param name="path">Path to save the file to.</param>
        void SaveScene(string path);

        /// <summary>
        /// Loads a new scene from disk.
        /// </summary>
        /// <param name="path">Path to the file to load from.</param>
        void LoadScene(string path);
    }
}
